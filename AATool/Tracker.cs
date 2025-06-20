﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AATool.Configuration;
using AATool.Data.Categories;
using AATool.Data.Objectives;
using AATool.Data.Progress;
using AATool.Exceptions;
using AATool.Net;
using AATool.Saves;
using AATool.UI.Controllers;
using AATool.Utilities;

namespace AATool
{
    public static class Tracker
    {
        public static readonly AdvancementManifest Advancements = new ();
        public static readonly AchievementManifest Achievements = new ();
        public static readonly ComplexObjectiveManifest ComplexObjectives = new ();
        public static readonly BlockManifest Blocks = new ();
        public static readonly DeathManifest Deaths = new ();

        public static Category Category { get; private set; }
        public static WorldState State { get; private set; } = new();
        public static Exception LastError { get; private set; }
        public static bool DesignationsChanged { get; private set; }
        public static bool MainPlayerChanged { get; private set; }
        public static bool CoOpStateChanged { get; private set; }
        public static bool WorldLocked { get; private set; }
        public static string Status { get; private set; }

        public static bool Invalidated =>
            SavesFolderChanged || ObjectivesChanged || ProgressChanged;

        public static bool ManualChecklistInvalidated { get; set; }
        public static bool ManualChecklistChanged { get; set; }

        public static bool SavesFolderChanged => 
            !Config.Tracking.ManualChecklistMode && (PreviousSavesPath != Paths.Saves.CurrentFolder());

        public static bool WorldChanged => World.PathChanged;

        public static bool ObjectivesChanged => 
            Config.Tracking.GameCategory.Changed || Config.Tracking.GameVersion.Changed;

        public static bool ProgressChanged => 
            World.ProgressChanged || World.PathChanged || World.Invalidated || CoOpStateChanged;

        public static bool IsWorking => LastError is null;
        public static string CurrentCategory => Category?.Name;
        public static string CurrentVersion => Category?.CurrentVersion;

        public static AdvancementManifest CurrentAdvancementSet => Category is not AllAchievements
            ? Advancements
            : Achievements;

        public static Dictionary<(string adv, string crit), Criterion> AllCriteria =>
            CurrentAdvancementSet.AllCriteria;

        public static Dictionary<(string adv, string crit), Criterion> RemainingCriteria =>
            CurrentAdvancementSet.RemainingCriteria;

        public static string WorldName => World.Name;
        public static TimeSpan InGameTime => State.InGameTime;
        public static bool InGameTimeChanged => State.InGameTime != LastInGameTime;
        public static TrackerSource Source => Config.Tracking.Source;

        public static void ToggleWorldLock() => WorldLocked ^= true;

        public static string GetFullIgt() => $"{(int)InGameTime.TotalHours}:{InGameTime:mm':'ss}";

        public static string GetShortIgt() => $"{InGameTime:hh':'mm':'ss}";

        public static string GetDays() => InGameTime.Days is 0 ? string.Empty : $"{Math.Round(InGameTime.TotalDays, 2)} IRL Days";

        public static string GetDaysAndHours()
        {
            if (InGameTime.Days is 0)
                return string.Empty;

            return InGameTime.Days is 1
                ? $"1 Day, {InGameTime.Hours}.{(int)(InGameTime.Minutes / 60.0f * 100)} Hrs Played"
                : $"{InGameTime.Days} Days, {InGameTime.Hours}.{(int)(InGameTime.Minutes / 60.0f * 100)} Hrs Played";
        }

        public static string GetEstimateString(int seconds)
        {
            return seconds >= 60
                ? $"{seconds / 60} min & {seconds % 60} sec"
                : $"{seconds} seconds";
        }

        public static string GetLastRefresh(Time time)
        {
            int seconds = (int)Math.Max(time.TotalSeconds - LastRefresh, 0);
            if (seconds < 1)
                return "Refreshing Now";
            else if (seconds == 1)
                return $"Refreshed 1 second ago";
            else
                return $"Refreshed {GetEstimateString(seconds).Replace("& ", "")} ago";
        }

        private static readonly FileSystemWatcher WorldWatcher = new ();
        private static readonly FileSystemWatcher PracticeWorldWatcher = new ();
        private static readonly FileSystemWatcher AdvancementsWatcher = new ();
        private static readonly FileSystemWatcher StatisticsWatcher = new ();

        private static WorldFolder World;
        private static Timer RefreshTimer;
        private static Timer FilesystemEventDebounce;
        private static TimeSpan LastInGameTime;
        private static Uuid PreviousMainPlayer;
        private static string PreviousSavesPath;
        private static string PreviousWorldPath;
        private static string LastServerMessage;
        private static int PreviousActiveId = -1;
        private static bool FileSystemEventRaised;
        private static double LastRefresh;

        public static void InvalidateDesignations()
        {
            DesignationsChanged = true;
            CurrentAdvancementSet.RefreshRemainingCriteria();
        }

        public static bool TryGetAdvancement(string id, out Advancement advancement) =>
            CurrentAdvancementSet.TryGet(id, out advancement);

        public static bool TryGetCriterion(string adv, string crit, out Criterion criterion) =>
            AllCriteria.TryGetValue((adv, crit), out criterion);

        public static bool TryGetAdvancementGroup(string id, out HashSet<Advancement> group) =>
            Advancements.TryGet(id, out group);

        public static bool TryGetComplexObjective(string typeName, out ComplexObjective item) =>
            ComplexObjectives.TryGet(typeName, out item);

        public static bool TryGetBlock(string id, out Block block) =>
            Blocks.TryGet(id, out block);

        public static bool TryGetDeath(string id, out Death death) =>
            Deaths.TryGet(id, out death);

        public static Uuid GetMainPlayer()
        {
            Uuid mainPlayer;
            if (Config.Tracking.Filter == ProgressFilter.Solo)
                Player.TryGetUuid(Config.Tracking.SoloFilterName, out mainPlayer);
            else
                mainPlayer = State.Players.Keys.FirstOrDefault();

            if (mainPlayer == Uuid.Empty)
            {
                if (Config.Tracking.LastUuid != Uuid.Empty)
                    mainPlayer = Config.Tracking.LastUuid;
                else
                    Player.TryGetUuid(Config.Tracking.LastPlayer, out mainPlayer);
            }

            MainPlayerChanged |= mainPlayer != PreviousMainPlayer;
            Config.Tracking.LastUuid.Set(mainPlayer);
            return PreviousMainPlayer = mainPlayer;
        }

        public static HashSet<Uuid> GetAllPlayers()
        {
            var ids = new HashSet<Uuid>();
            foreach (Uuid id in State.Players.Keys)
                ids.Add(id);

            if (Peer.IsConnected && Peer.TryGetLobby(out Lobby lobby))
            {
                foreach (Uuid key in lobby.Users.Keys)
                    ids.Add(key);
            }
            else if (Config.Tracking.Filter == ProgressFilter.Solo)
            {
                if (Player.TryGetUuid(Config.Tracking.SoloFilterName, out Uuid soloPlayer))
                    ids.Add(soloPlayer);
            }
            ids.Remove(Uuid.Empty);
            return ids;
        }

        public static void Initialize()
        {
            World = new WorldFolder();
            State = new WorldState();
            RefreshTimer = new Timer();

            FilesystemEventDebounce = new(0.5f);
            FilesystemEventDebounce.Expire();

            WorldWatcher.Created += FileSystemChanged;
            WorldWatcher.Deleted += FileSystemChanged;
            WorldWatcher.Changed += FileSystemChanged;
            WorldWatcher.Deleted += FileSystemChanged;

            PracticeWorldWatcher.Created += FileSystemChanged;
            PracticeWorldWatcher.Deleted += FileSystemChanged;
            PracticeWorldWatcher.Changed += FileSystemChanged;
            PracticeWorldWatcher.Created += FileSystemChanged;

            AdvancementsWatcher.Created += FileSystemChanged;
            AdvancementsWatcher.Deleted += FileSystemChanged;
            AdvancementsWatcher.Changed += FileSystemChanged;
            AdvancementsWatcher.Deleted += FileSystemChanged;

            StatisticsWatcher.Created += FileSystemChanged;
            StatisticsWatcher.Deleted += FileSystemChanged;
            StatisticsWatcher.Changed += FileSystemChanged;
            StatisticsWatcher.Deleted += FileSystemChanged;

            MainPlayerChanged = true;

            string lastVersion = Config.Tracking.GameVersion;
            TrySetCategory(Config.Tracking.GameCategory);
            TrySetVersion(lastVersion);
        }

        public static void FileSystemChanged(object sender, FileSystemEventArgs e)
        {
            FileSystemEventRaised = true;
        }

        public static string GetStatusText()
        {
            if (Peer.IsServer && Peer.IsConnected)
            {
                return $"Hosting: \"{WorldName}\"";
            }

            if (Config.Tracking.ManualChecklistMode)
            {
                return "Manual Mode: Click items to mark as complete";
            }

            if (IsWorking)
            {
                return Source is TrackerSource.ActiveInstance && ActiveInstance.HasNumber
                    ? $"Instance {ActiveInstance.Number}: \"{WorldName}\""
                    : $"Tracking: \"{WorldName}\"";
            }
            
            return LastError.Message;
        }
        
        public static bool TrySetCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return false;

            //check if category is the same
            if (Category is not null && category == Category.Name)
                return false;

            try
            {
                Category = category.ToLower().Replace(" ", "").Replace("_", "") switch {       
                    //main categories
                    "alladvancements" => new AllAdvancements(),
                    "allachievements" => new AllAchievements(),
                    "halfpercent"     => new HalfPercent(),

                    //single advancement categories
                    "balanceddiet"    => new BalancedDiet(),
                    "adventuringtime" => new AdventuringTime(),
                    "monstershunted"  => new MonstersHunted(),

                    //random extensions
                    "allblocks" => new AllBlocks(),
                    "alldeaths" => new AllDeaths(),
                    "halfdeaths" => new HalfDeaths(),
                    "allportals" => new AllPortals(),
                    "allsmithingtemplates" => new AllSmithingTemplates(),

                    _ => throw new ArgumentException($"Category not supported: \"{category}\"."),
                };
                //save change to config
                Config.Tracking.GameCategory.Set(Category.Name);
                Config.Tracking.GameVersion.Set(Category.CurrentVersion);
                Config.Tracking.TrySave();
                Category.LoadObjectives();
                return true;
            }
            catch
            {
                if (Category is null)
                {
                    //fallback to all advancements
                    Category = new AllAdvancements();
                    Config.Tracking.GameCategory.Set(Category.Name);
                    Config.Tracking.TrySave();
                    Category.LoadObjectives();
                }
                return false;
            }
        }

        public static bool TrySetVersion(string versionNumber) => 
            Category.TrySetVersion(versionNumber);

        public static void Invalidate(bool invalidateWorld = false)
        {
            if (invalidateWorld)
                World.Invalidate();
            RefreshTimer.Expire();
        }

        public static void ClearFlags()
        {
            LastInGameTime = InGameTime;
            DesignationsChanged = false;
            MainPlayerChanged = false;
            CoOpStateChanged = false;
            ManualChecklistChanged = false;
            World.ClearFlags();
        }

        public static void Update(Time time)
        {
            RefreshTimer.Update(time);
            if (Client.TryGet(out Client client))
            {
                ParseCoOpProgress(time, client);
            }
            else if (Config.Tracking.ManualChecklistMode && Category is AllAdvancements)
            {
                bool needsRefresh = FileSystemEventRaised
                    || ManualChecklistInvalidated
                    || ObjectivesChanged
                    || Config.Tracking.SourceChanged;

                if (needsRefresh)
                {
                    ReadManualChecklist(time);
                    ManualChecklistChanged = true;
                    ManualChecklistInvalidated = false;
                }
            }
            else
            {
                bool needsRefresh = (FileSystemEventRaised && FilesystemEventDebounce.IsExpired)
                    || ObjectivesChanged 
                    || Config.Tracking.SourceChanged
                    || (ActiveInstance.Watching && PreviousActiveId != ActiveInstance.LastActiveId);

                if (needsRefresh)
                {
                    UpdateCurrentWorld();
                    ReadLocalFiles(time);
                    FilesystemEventDebounce.Reset();
                }
                PreviousActiveId = ActiveInstance.LastActiveId;
                UpdateFileSystemWatchers();
            }
            Category.Update();
            ComplexObjectives.UpdateDynamicIcons(time);
            FilesystemEventDebounce.Update(time);
        }

        private static void UpdateFileSystemWatchers()
        {
            if (!string.IsNullOrEmpty(World.FullName))
            {
                string advancements = Path.Combine(World.FullName, "advancements\\");
                if (!AdvancementsWatcher.EnableRaisingEvents && Directory.Exists(advancements))
                {
                    AdvancementsWatcher.Path = advancements;
                    AdvancementsWatcher.EnableRaisingEvents = true;
                }

                string statistics = Path.Combine(World.FullName, "stats\\");
                if (!StatisticsWatcher.EnableRaisingEvents && Directory.Exists(statistics))
                {
                    StatisticsWatcher.Path = statistics;
                    StatisticsWatcher.EnableRaisingEvents = true;
                }
            }
            FileSystemEventRaised = false;
        }

        private static void UpdateCurrentWorld()
        {
            if (Config.Tracking.Source.Changed)
                WorldLocked = false;

            string savesPath = string.Empty;
            string practiceSavesPath = string.Empty;
            string worldPath = string.Empty;
            DirectoryInfo latestWorld = null;
            bool isPracticeWorld = false;
            bool practiceFolderExists = false;
            try
            {
                if (Source is TrackerSource.SpecificWorld && !Config.Tracking.UseSftp)
                {
                    //set world to user-defined path
                    WorldLocked = true;
                    worldPath = Config.Tracking.CustomWorldPath;

                    //check if path is empty
                    if (string.IsNullOrEmpty(worldPath))
                    {
                        if (LastError is not ArgumentException || Config.Tracking.SourceChanged)
                            throw new ArgumentException("User-specified world path empty");
                        return;
                    }

                    //exit early if path invalid and unchanged
                    if (LastError is not InvalidPathException || Config.Tracking.SourceChanged)
                    {
                        //validate path (throws if invalid characters present)]
                        try
                        {
                            latestWorld = new DirectoryInfo(worldPath);
                        }
                        catch
                        {
                            throw new InvalidPathException();
                        }
                    }
                }
                else
                {
                    //get current saves folder
                    savesPath = Paths.Saves.CurrentFolder();
                    practiceSavesPath = Paths.Saves.CurrentPracticeSavesFolder();

                    //exit early if path invalid
                    if (LastError is InvalidPathException && savesPath == PreviousSavesPath)
                        return;

                    //unlock world if saves folder changed
                    if (PreviousSavesPath != savesPath)
                        WorldLocked = false;

                    //make sure path isn't empty
                    if (string.IsNullOrEmpty(savesPath))
                    {
                        if (LastError is not ArgumentException || Config.Tracking.SourceChanged)
                        {
                            throw Source is TrackerSource.ActiveInstance
                                ? new ArgumentException("Tab into Minecraft to start tracking")
                                : new ArgumentException("Custom saves path is empty");
                        }
                        return;
                    }

                    //validate path (throws if invalid characters present)
                    var savesFolder = new DirectoryInfo(savesPath);

                    //make sure folder actually exists
                    if (!savesFolder.Exists)
                    {
                        //avoid re-throwing duplicate exception
                        if (LastError is not NoSavesFolderException)
                            throw new NoSavesFolderException(savesFolder.FullName);
                        return;
                    }

                    if (WorldLocked)
                    {
                        //keep same world
                        worldPath = PreviousWorldPath;
                        latestWorld = new DirectoryInfo(worldPath);
                    }
                    else
                    {
                        //find most recently modified world in folder
                        DirectoryInfo[] potentialWorlds = savesFolder.GetDirectories();
                        foreach (DirectoryInfo worldFolder in potentialWorlds)
                        {
                            //skip any folders that definitely aren't worlds
                            if (!Paths.Saves.MightBeWorldFolder(worldFolder))
                                continue;

                            //sort by write time
                            if (worldFolder == Paths.Saves.MostRecentlyWritten(worldFolder, latestWorld))
                                latestWorld = worldFolder;
                        }

                        //include practice saves if present
                        DirectoryInfo[] potentialPracticeWorlds = Array.Empty<DirectoryInfo>();
                        if (Directory.Exists(practiceSavesPath))
                        {
                            practiceFolderExists = true;
                            try
                            {
                                potentialPracticeWorlds = new DirectoryInfo(practiceSavesPath).GetDirectories();
                            }
                            catch
                            {
                            }
                        }

                        foreach (DirectoryInfo worldFolder in potentialPracticeWorlds)
                        {
                            //skip any folders that definitely aren't worlds
                            if (!Paths.Saves.MightBeWorldFolder(worldFolder))
                                continue;

                            //sort by write time
                            if (worldFolder == Paths.Saves.MostRecentlyWritten(worldFolder, latestWorld))
                            {
                                latestWorld = worldFolder;
                                isPracticeWorld = true;
                            }
                        }
                        worldPath = latestWorld?.FullName;
                    }
                }

                //make sure folder actually exists
                if (latestWorld is null || !latestWorld.Exists)
                {
                    if (LastError is not NoWorldException || latestWorld?.FullName != World?.FullName)
                        throw new NoWorldException();
                    return;
                }

                if (latestWorld.FullName != World.FullName)
                {
                    World.SetPath(latestWorld);
                    if (practiceFolderExists && isPracticeWorld)
                    {
                        PracticeWorldWatcher.Path = practiceSavesPath;
                        PracticeWorldWatcher.EnableRaisingEvents = true;
                        WorldWatcher.Path = Path.Combine(latestWorld.Parent.Parent.FullName, "saves");
                    }
                    else
                    {
                        WorldWatcher.Path = latestWorld.Parent?.FullName;
                    }

                    WorldWatcher.EnableRaisingEvents = true;
                    AdvancementsWatcher.EnableRaisingEvents = false;
                    StatisticsWatcher.EnableRaisingEvents = false;
                }

                LastError = null;
            }
            catch (Exception e)
            {
                if (!World.IsEmpty)
                {
                    World.Unset();
                    WorldLocked = false;
                }
                LastError = e;
            }
            finally
            {
                PreviousSavesPath = savesPath;
                PreviousWorldPath = worldPath;
            }
        }

        private static void ParseCoOpProgress(Time time, Client client)
        {
            //update world from co-op server
            if (client is null || !client.TryGetData(Protocol.Headers.Progress, out string progress))
                return;

            if (LastServerMessage != progress)
            {
                CoOpStateChanged = true;
                LastServerMessage = progress;
                var networkState = NetworkState.FromJsonString(progress);
                State = new WorldState(networkState);

                //sync category and version with host
                TrySetCategory(networkState.GameCategory);
                TrySetVersion(networkState.GameVersion);

                //reload objectives if game version has changed
                if (ObjectivesChanged)
                    Category.LoadObjectives();

                SetState(State);

                LastRefresh = time.TotalSeconds;
            }
        }

        private static void ReadLocalFiles(Time time)
        {
            //reload objective manifests if game version has changed
            if (ObjectivesChanged)
                Category.LoadObjectives();

            //wait to refresh until sftp transer is complete
            if (Config.Tracking.UseSftp && MinecraftServer.IsDownloading)
                return;

            ReadLatestLog();

            //update progress if source has been invalidated
            if (World.TryRefresh() || Peer.StateChanged || Config.Tracking.FilterChanged || Config.Tracking.ManualChecklistMode.Changed)
            {
                LastServerMessage = null;
                State = World.GetState();
                SetState(State);

                //broadcast changes to connected clients if server is running
                if (Server.TryGet(out Server server) && server.Connected())
                    server.SendProgress();

                LastRefresh = time.TotalSeconds;
            }
        }
        
        private static void ReadManualChecklist(Time time)
        {
            //reload objective manifests if game version has changed
            if (ObjectivesChanged)
                Category.LoadObjectives();

            //update progress if source has been invalidated
            State = ManualChecklistController.GetCurrentState();
            SetState(State);
        }

        private static void ReadLatestLog()
        {  
            if (Category is AllDeaths)
            {
                //attempt to sync death messages
                int before = State.DeathMessages.Count;
                State.SyncDeathMessages();
                if (State.DeathMessages.Count != before)
                    Deaths.UpdateState(State);
            }
        }

        private static void SetState(WorldState world)
        {
            ProgressState activeState;
            if (Config.Tracking.Filter == ProgressFilter.Combined || Peer.IsRunning)
            {
                activeState = world;
            }
            else
            {
                Player.TryGetUuid(Config.Tracking.SoloFilterName, out Uuid player);
                world.Players.TryGetValue(player, out Contribution individual);
                activeState = individual;
            }

            Advancements.UpdateState(activeState);
            Achievements.UpdateState(activeState);
            Blocks.UpdateState(activeState);
            ComplexObjectives.UpdateState(activeState);
        }
    }
}