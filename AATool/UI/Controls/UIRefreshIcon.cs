﻿using System;
using AATool.Configuration;
using AATool.Graphics;
using AATool.Net;
using AATool.Saves;
using AATool.UI.Screens;
using AATool.Utilities;
using AATool.Utilities.Easings;
using Microsoft.Xna.Framework;

namespace AATool.UI.Controls
{
    class UIRefreshIcon : UIControl
    {
        private const string SyncTexture = "sync";
        private const string SyncingTexture = "syncing";
        private const string LockedTexture = "locked";
        private const string ClearTexture = "clear_manual_checklist";

        private UIButton button;
        private UIPicture icon;
        private UIPicture lockIcon;

        private bool repeat;
        private Rectangle glowBounds;

        private readonly Easing fade;

        public UIRefreshIcon()
        {
            this.BuildFromTemplate();
            this.fade = new Easing(Ease.Circular, 1.0, true, false);
        }

        public override void InitializeThis(UIScreen screen)
        {
            this.button = this.First<UIButton>("manual_sync");
            this.button.OnClick += this.OnClick;

            this.icon = this.First<UIPicture>("icon");
            this.lockIcon = this.First<UIPicture>("lock");
        }

        public override void ResizeThis(Rectangle parent)
        {
            base.ResizeThis(parent);
            this.glowBounds = new Rectangle(
                this.X - 8,
                this.Y - 8,
                this.Width + 16,
                this.Height + 16);
        }

        private void OnClick(UIControl sender)
        {
            if (sender == this.button)
            {
                if (Config.Tracking.ManualChecklistMode)
                {
                    this.PromptClearManualChecklist();
                }
                else if (Config.Tracking.UseSftp)
                {
                    MinecraftServer.Sync();
                }
                else if (Config.Tracking.Source != TrackerSource.SpecificWorld)
                {
                    Tracker.ToggleWorldLock();
                    if (!Tracker.WorldLocked)
                        Tracker.Invalidate();
                }
            }
        }

        private void PromptClearManualChecklist()
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(
                "You are about to clear all manually checked Advancements and sub-criteria. Are you sure?",
                "Clear manually checked items",
                System.Windows.Forms.MessageBoxButtons.OKCancel,
                System.Windows.Forms.MessageBoxIcon.Warning
            );

            if (result is System.Windows.Forms.DialogResult.OK)
            {
                (this.Root() as UIMainScreen).checklist.Clear();
            }
        }

        protected override void UpdateThis(Time time)
        {
            this.fade.Update(time);

            if (Config.Tracking.ManualChecklistMode)
            {
                this.icon.SetTexture(ClearTexture);
                this.icon.SetTint(Color.White);
                this.lockIcon.SetTexture(string.Empty);
            }
            else if (!Config.Tracking.UseSftp || Peer.IsClient)
            {
                //update style
                this.icon.SetTexture(((string)Config.Main.RefreshIcon).Replace(" ", "_").ToLower());
                float alpha = this.repeat
                    ? 20 * this.fade.In()
                    : 1 - this.fade.Out();
                this.icon.SetTint(ColorHelper.Fade(Color.White, alpha));

                //update state
                if (Tracker.Invalidated || Config.Main.RefreshIcon.Changed)
                {
                    this.fade.Reset();
                    this.repeat = true;
                }
                if (this.fade.IsExpired && this.repeat)
                {
                    this.fade.Reset();
                    this.repeat = false;
                }

                //configure appearance as refresh indicator
                this.button.Enabled = Tracker.IsWorking && !Peer.IsClient && Tracker.Source is not TrackerSource.SpecificWorld;
                this.lockIcon.SetTexture(Tracker.WorldLocked || Tracker.Source is TrackerSource.SpecificWorld ? LockedTexture : string.Empty);
                alpha = Math.Max(0.25f, 1 - (alpha * 4));
                if (!this.button.Enabled)
                    alpha /= 3;
                this.lockIcon.SetTint(ColorHelper.Fade(Config.Main.TextColor, alpha));
            }  
            else
            {
                //configure appearance as sftp button
                bool ready = MinecraftServer.State is SyncState.Ready;
                this.button.Enabled = ready;
                this.icon.SetTexture(ready ? SyncTexture : SyncingTexture);
                this.icon.SetTint(Config.Main.TextColor);
                this.lockIcon.SetTexture(string.Empty);
            }    
        }

        public override void DrawThis(Canvas canvas)
        {
            //glow effect
            if (this.icon.Texture is "xp_orb")
                canvas.Draw("xp_orb_glow", this.glowBounds, this.icon.Tint, Layer.Glow);

            base.DrawThis(canvas);
        }
    }
}