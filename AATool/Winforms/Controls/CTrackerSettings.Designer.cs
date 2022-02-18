﻿
namespace AATool.Winforms.Controls
{
    partial class CTrackerSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.autoVersion = new System.Windows.Forms.CheckBox();
            this.gameVersion = new System.Windows.Forms.ComboBox();
            this.localGroup = new System.Windows.Forms.GroupBox();
            this.browseWorld = new System.Windows.Forms.Button();
            this.TrackSpecificWorld = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.customWorldPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.trackActiveInstance = new System.Windows.Forms.RadioButton();
            this.trackCustomSavesFolder = new System.Windows.Forms.RadioButton();
            this.trackDefaultSaves = new System.Windows.Forms.RadioButton();
            this.browseSaves = new System.Windows.Forms.Button();
            this.customSavesPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.worldLocal = new System.Windows.Forms.RadioButton();
            this.worldRemote = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.remoteGroup = new System.Windows.Forms.GroupBox();
            this.sftpCompatibility = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sftpRoot = new System.Windows.Forms.TextBox();
            this.sftpPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.toggleCredentials = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.sftpValidate = new System.Windows.Forms.Button();
            this.sftpPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sftpHost = new System.Windows.Forms.TextBox();
            this.sftpUser = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.category = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.configureOpenTracker = new System.Windows.Forms.Button();
            this.enableOpenTracker = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.localGroup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.remoteGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoVersion
            // 
            this.autoVersion.AutoSize = true;
            this.autoVersion.Location = new System.Drawing.Point(9, 50);
            this.autoVersion.Name = "autoVersion";
            this.autoVersion.Size = new System.Drawing.Size(83, 17);
            this.autoVersion.TabIndex = 26;
            this.autoVersion.Text = "Auto-Detect";
            this.autoVersion.UseVisualStyleBackColor = true;
            this.autoVersion.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // gameVersion
            // 
            this.gameVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameVersion.FormattingEnabled = true;
            this.gameVersion.Location = new System.Drawing.Point(9, 22);
            this.gameVersion.Name = "gameVersion";
            this.gameVersion.Size = new System.Drawing.Size(96, 21);
            this.gameVersion.TabIndex = 18;
            this.gameVersion.SelectedIndexChanged += new System.EventHandler(this.OnIndexChanged);
            // 
            // localGroup
            // 
            this.localGroup.Controls.Add(this.browseWorld);
            this.localGroup.Controls.Add(this.TrackSpecificWorld);
            this.localGroup.Controls.Add(this.button2);
            this.localGroup.Controls.Add(this.customWorldPath);
            this.localGroup.Controls.Add(this.label9);
            this.localGroup.Controls.Add(this.label8);
            this.localGroup.Controls.Add(this.trackActiveInstance);
            this.localGroup.Controls.Add(this.trackCustomSavesFolder);
            this.localGroup.Controls.Add(this.trackDefaultSaves);
            this.localGroup.Controls.Add(this.browseSaves);
            this.localGroup.Controls.Add(this.customSavesPath);
            this.localGroup.Location = new System.Drawing.Point(116, 90);
            this.localGroup.Name = "localGroup";
            this.localGroup.Size = new System.Drawing.Size(417, 214);
            this.localGroup.TabIndex = 23;
            this.localGroup.TabStop = false;
            this.localGroup.Text = "Local Save Tracking";
            // 
            // browseWorld
            // 
            this.browseWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseWorld.Location = new System.Drawing.Point(358, 182);
            this.browseWorld.Name = "browseWorld";
            this.browseWorld.Size = new System.Drawing.Size(53, 22);
            this.browseWorld.TabIndex = 42;
            this.browseWorld.Text = "Browse";
            this.browseWorld.UseVisualStyleBackColor = true;
            this.browseWorld.Click += new System.EventHandler(this.OnClicked);
            // 
            // TrackSpecificWorld
            // 
            this.TrackSpecificWorld.AutoSize = true;
            this.TrackSpecificWorld.Location = new System.Drawing.Point(6, 160);
            this.TrackSpecificWorld.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TrackSpecificWorld.Name = "TrackSpecificWorld";
            this.TrackSpecificWorld.Size = new System.Drawing.Size(128, 17);
            this.TrackSpecificWorld.TabIndex = 41;
            this.TrackSpecificWorld.TabStop = true;
            this.TrackSpecificWorld.Text = "Track Specific World:";
            this.TrackSpecificWorld.UseVisualStyleBackColor = true;
            this.TrackSpecificWorld.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(425, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 22);
            this.button2.TabIndex = 40;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // customWorldPath
            // 
            this.customWorldPath.Location = new System.Drawing.Point(6, 183);
            this.customWorldPath.Name = "customWorldPath";
            this.customWorldPath.Size = new System.Drawing.Size(346, 20);
            this.customWorldPath.TabIndex = 39;
            this.customWorldPath.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label9.Location = new System.Drawing.Point(3, 83);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 0, 3, 10);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.label9.Size = new System.Drawing.Size(210, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "🛈  (...AppData/Roaming/.minecraft/saves)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(3, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 0, 3, 10);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.label8.Size = new System.Drawing.Size(401, 15);
            this.label8.TabIndex = 37;
            this.label8.Text = "🛈 AATool will instantly detect and track the correct path when you switch instan" +
    "ces";
            // 
            // trackActiveInstance
            // 
            this.trackActiveInstance.AutoSize = true;
            this.trackActiveInstance.Location = new System.Drawing.Point(6, 19);
            this.trackActiveInstance.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.trackActiveInstance.Name = "trackActiveInstance";
            this.trackActiveInstance.Size = new System.Drawing.Size(185, 17);
            this.trackActiveInstance.TabIndex = 9;
            this.trackActiveInstance.TabStop = true;
            this.trackActiveInstance.Text = "Seamlessly Track Active Instance";
            this.trackActiveInstance.UseVisualStyleBackColor = true;
            this.trackActiveInstance.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // trackCustomSavesFolder
            // 
            this.trackCustomSavesFolder.AutoSize = true;
            this.trackCustomSavesFolder.Location = new System.Drawing.Point(6, 108);
            this.trackCustomSavesFolder.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.trackCustomSavesFolder.Name = "trackCustomSavesFolder";
            this.trackCustomSavesFolder.Size = new System.Drawing.Size(159, 17);
            this.trackCustomSavesFolder.TabIndex = 8;
            this.trackCustomSavesFolder.TabStop = true;
            this.trackCustomSavesFolder.Text = "Track Custom Saves Folder:";
            this.trackCustomSavesFolder.UseVisualStyleBackColor = true;
            this.trackCustomSavesFolder.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // trackDefaultSaves
            // 
            this.trackDefaultSaves.AutoSize = true;
            this.trackDefaultSaves.Location = new System.Drawing.Point(6, 63);
            this.trackDefaultSaves.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.trackDefaultSaves.Name = "trackDefaultSaves";
            this.trackDefaultSaves.Size = new System.Drawing.Size(155, 17);
            this.trackDefaultSaves.TabIndex = 7;
            this.trackDefaultSaves.TabStop = true;
            this.trackDefaultSaves.Text = "Track Default Saves Folder";
            this.trackDefaultSaves.UseVisualStyleBackColor = true;
            this.trackDefaultSaves.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // browseSaves
            // 
            this.browseSaves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSaves.Location = new System.Drawing.Point(358, 130);
            this.browseSaves.Name = "browseSaves";
            this.browseSaves.Size = new System.Drawing.Size(53, 22);
            this.browseSaves.TabIndex = 1;
            this.browseSaves.Text = "Browse";
            this.browseSaves.UseVisualStyleBackColor = true;
            this.browseSaves.Click += new System.EventHandler(this.OnClicked);
            // 
            // customSavesPath
            // 
            this.customSavesPath.Location = new System.Drawing.Point(6, 131);
            this.customSavesPath.Name = "customSavesPath";
            this.customSavesPath.Size = new System.Drawing.Size(346, 20);
            this.customSavesPath.TabIndex = 0;
            this.customSavesPath.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.worldLocal);
            this.groupBox3.Controls.Add(this.worldRemote);
            this.groupBox3.Location = new System.Drawing.Point(3, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(107, 214);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "World Type";
            // 
            // worldLocal
            // 
            this.worldLocal.AutoSize = true;
            this.worldLocal.Location = new System.Drawing.Point(6, 19);
            this.worldLocal.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.worldLocal.Name = "worldLocal";
            this.worldLocal.Size = new System.Drawing.Size(70, 17);
            this.worldLocal.TabIndex = 35;
            this.worldLocal.TabStop = true;
            this.worldLocal.Text = "Set Local";
            this.worldLocal.UseVisualStyleBackColor = true;
            this.worldLocal.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // worldRemote
            // 
            this.worldRemote.AutoSize = true;
            this.worldRemote.Location = new System.Drawing.Point(6, 45);
            this.worldRemote.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.worldRemote.Name = "worldRemote";
            this.worldRemote.Size = new System.Drawing.Size(98, 17);
            this.worldRemote.TabIndex = 36;
            this.worldRemote.TabStop = true;
            this.worldRemote.Text = "Remote (SFTP)";
            this.worldRemote.UseVisualStyleBackColor = true;
            this.worldRemote.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoVersion);
            this.groupBox1.Controls.Add(this.gameVersion);
            this.groupBox1.Location = new System.Drawing.Point(164, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 81);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Version";
            // 
            // remoteGroup
            // 
            this.remoteGroup.Controls.Add(this.sftpCompatibility);
            this.remoteGroup.Controls.Add(this.label7);
            this.remoteGroup.Controls.Add(this.label5);
            this.remoteGroup.Controls.Add(this.sftpRoot);
            this.remoteGroup.Controls.Add(this.sftpPort);
            this.remoteGroup.Controls.Add(this.label11);
            this.remoteGroup.Controls.Add(this.toggleCredentials);
            this.remoteGroup.Controls.Add(this.label4);
            this.remoteGroup.Controls.Add(this.sftpValidate);
            this.remoteGroup.Controls.Add(this.sftpPass);
            this.remoteGroup.Controls.Add(this.label3);
            this.remoteGroup.Controls.Add(this.label6);
            this.remoteGroup.Controls.Add(this.sftpHost);
            this.remoteGroup.Controls.Add(this.sftpUser);
            this.remoteGroup.Location = new System.Drawing.Point(116, 90);
            this.remoteGroup.Name = "remoteGroup";
            this.remoteGroup.Size = new System.Drawing.Size(417, 214);
            this.remoteGroup.TabIndex = 31;
            this.remoteGroup.TabStop = false;
            this.remoteGroup.Text = "Remote Minecraft Server Login";
            // 
            // sftpCompatibility
            // 
            this.sftpCompatibility.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sftpCompatibility.Location = new System.Drawing.Point(3, 193);
            this.sftpCompatibility.Name = "sftpCompatibility";
            this.sftpCompatibility.Size = new System.Drawing.Size(411, 18);
            this.sftpCompatibility.TabIndex = 62;
            this.sftpCompatibility.TabStop = true;
            this.sftpCompatibility.Text = "SFTP compatibility";
            this.sftpCompatibility.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(6, 147);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 0, 3, 10);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.label7.Size = new System.Drawing.Size(114, 15);
            this.label7.TabIndex = 61;
            this.label7.Text = "🛈 Leave blank for root";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Custom Server Path:";
            // 
            // sftpRoot
            // 
            this.sftpRoot.Location = new System.Drawing.Point(9, 124);
            this.sftpRoot.Name = "sftpRoot";
            this.sftpRoot.Size = new System.Drawing.Size(160, 20);
            this.sftpRoot.TabIndex = 56;
            this.sftpRoot.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // sftpPort
            // 
            this.sftpPort.Location = new System.Drawing.Point(331, 34);
            this.sftpPort.Name = "sftpPort";
            this.sftpPort.Size = new System.Drawing.Size(31, 20);
            this.sftpPort.TabIndex = 38;
            this.sftpPort.Text = "7767";
            this.sftpPort.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(328, 18);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Port:";
            // 
            // toggleCredentials
            // 
            this.toggleCredentials.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.toggleCredentials.Location = new System.Drawing.Point(331, 78);
            this.toggleCredentials.Name = "toggleCredentials";
            this.toggleCredentials.Size = new System.Drawing.Size(80, 22);
            this.toggleCredentials.TabIndex = 55;
            this.toggleCredentials.Text = "Show Login";
            this.toggleCredentials.UseVisualStyleBackColor = true;
            this.toggleCredentials.Click += new System.EventHandler(this.OnClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Password:";
            // 
            // sftpValidate
            // 
            this.sftpValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sftpValidate.Location = new System.Drawing.Point(288, 147);
            this.sftpValidate.Name = "sftpValidate";
            this.sftpValidate.Size = new System.Drawing.Size(105, 42);
            this.sftpValidate.TabIndex = 1;
            this.sftpValidate.Text = "Sync";
            this.sftpValidate.UseVisualStyleBackColor = true;
            this.sftpValidate.Click += new System.EventHandler(this.OnClicked);
            // 
            // sftpPass
            // 
            this.sftpPass.Location = new System.Drawing.Point(175, 79);
            this.sftpPass.Name = "sftpPass";
            this.sftpPass.Size = new System.Drawing.Size(150, 20);
            this.sftpPass.TabIndex = 29;
            this.sftpPass.UseSystemPasswordChar = true;
            this.sftpPass.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Username:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Host:";
            // 
            // sftpHost
            // 
            this.sftpHost.Location = new System.Drawing.Point(9, 34);
            this.sftpHost.Name = "sftpHost";
            this.sftpHost.Size = new System.Drawing.Size(316, 20);
            this.sftpHost.TabIndex = 50;
            this.sftpHost.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // sftpUser
            // 
            this.sftpUser.Location = new System.Drawing.Point(9, 79);
            this.sftpUser.Name = "sftpUser";
            this.sftpUser.Size = new System.Drawing.Size(160, 20);
            this.sftpUser.TabIndex = 0;
            this.sftpUser.UseSystemPasswordChar = true;
            this.sftpUser.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.category);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 81);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speedrun Category";
            // 
            // category
            // 
            this.category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category.Enabled = false;
            this.category.FormattingEnabled = true;
            this.category.Items.AddRange(new object[] {
            "All Advancements",
            "Half Percent",
            "All Achievements",
            "Monsters Hunted",
            "Adventuring Time",
            "Balanced Diet"});
            this.category.Location = new System.Drawing.Point(8, 22);
            this.category.Name = "category";
            this.category.Size = new System.Drawing.Size(137, 21);
            this.category.TabIndex = 18;
            this.category.SelectedIndexChanged += new System.EventHandler(this.OnIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.configureOpenTracker);
            this.groupBox4.Controls.Add(this.enableOpenTracker);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(283, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(252, 81);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "OpenTracker";
            this.groupBox4.Visible = false;
            // 
            // configureOpenTracker
            // 
            this.configureOpenTracker.Location = new System.Drawing.Point(73, 20);
            this.configureOpenTracker.Name = "configureOpenTracker";
            this.configureOpenTracker.Size = new System.Drawing.Size(75, 23);
            this.configureOpenTracker.TabIndex = 59;
            this.configureOpenTracker.Text = "Configure";
            this.configureOpenTracker.UseVisualStyleBackColor = true;
            this.configureOpenTracker.Click += new System.EventHandler(this.OnClicked);
            // 
            // enableOpenTracker
            // 
            this.enableOpenTracker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enableOpenTracker.AutoSize = true;
            this.enableOpenTracker.Location = new System.Drawing.Point(8, 24);
            this.enableOpenTracker.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.enableOpenTracker.Name = "enableOpenTracker";
            this.enableOpenTracker.Size = new System.Drawing.Size(59, 17);
            this.enableOpenTracker.TabIndex = 58;
            this.enableOpenTracker.Text = "Enable";
            this.enableOpenTracker.UseVisualStyleBackColor = true;
            this.enableOpenTracker.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(3, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 10);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.label1.Size = new System.Drawing.Size(216, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "🛈 Share your progress to the web in realtime";
            // 
            // CTrackerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.localGroup);
            this.Controls.Add(this.remoteGroup);
            this.Name = "CTrackerSettings";
            this.Size = new System.Drawing.Size(974, 307);
            this.localGroup.ResumeLayout(false);
            this.localGroup.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.remoteGroup.ResumeLayout(false);
            this.remoteGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox autoVersion;
        private System.Windows.Forms.ComboBox gameVersion;
        private System.Windows.Forms.GroupBox localGroup;
        private System.Windows.Forms.Button browseSaves;
        private System.Windows.Forms.TextBox customSavesPath;
        private System.Windows.Forms.RadioButton trackCustomSavesFolder;
        private System.Windows.Forms.RadioButton trackDefaultSaves;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox remoteGroup;
        private System.Windows.Forms.Button sftpValidate;
        private System.Windows.Forms.TextBox sftpUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sftpPass;
        private System.Windows.Forms.Button toggleCredentials;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox sftpHost;
        private System.Windows.Forms.TextBox sftpPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox category;
        private System.Windows.Forms.RadioButton worldLocal;
        private System.Windows.Forms.RadioButton worldRemote;
        private System.Windows.Forms.TextBox sftpRoot;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox enableOpenTracker;
        private System.Windows.Forms.Button configureOpenTracker;
        private System.Windows.Forms.RadioButton trackActiveInstance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel sftpCompatibility;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton TrackSpecificWorld;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox customWorldPath;
        private System.Windows.Forms.Button browseWorld;
    }
}
