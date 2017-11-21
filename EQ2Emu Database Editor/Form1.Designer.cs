namespace EQ2Emu_Database_Editor {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.linkLabel_spawns = new System.Windows.Forms.LinkLabel();
            this.linkLabel_characters = new System.Windows.Forms.LinkLabel();
            this.linkLabel_quests = new System.Windows.Forms.LinkLabel();
            this.linkLabel_items = new System.Windows.Forms.LinkLabel();
            this.linkLabel_zones = new System.Windows.Forms.LinkLabel();
            this.linkLabel_spells = new System.Windows.Forms.LinkLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_file_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_view_tabs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_view_tabs_normal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_view_tabs_buttons = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_view_tabs_flatbuttons = new System.Windows.Forms.ToolStripMenuItem();
            this.appsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_calculators_itemqueststep = new System.Windows.Forms.ToolStripMenuItem();
            this.dBLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waypointParserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_whatsnew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_status = new System.Windows.Forms.PictureBox();
            this.label_status = new System.Windows.Forms.Label();
            this.timer_status = new System.Windows.Forms.Timer(this.components);
            this.label_connected_user = new System.Windows.Forms.Label();
            this.linkLabel_serverdetails = new System.Windows.Forms.LinkLabel();
            this.linkLabel_Bulk = new System.Windows.Forms.LinkLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_status)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_main
            // 
            this.tabControl_main.Location = new System.Drawing.Point(16, 49);
            this.tabControl_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl_main.Multiline = true;
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(1555, 822);
            this.tabControl_main.TabIndex = 0;
            this.tabControl_main.TabStop = false;
            // 
            // linkLabel_spawns
            // 
            this.linkLabel_spawns.AutoSize = true;
            this.linkLabel_spawns.Location = new System.Drawing.Point(213, 30);
            this.linkLabel_spawns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_spawns.Name = "linkLabel_spawns";
            this.linkLabel_spawns.Size = new System.Drawing.Size(56, 16);
            this.linkLabel_spawns.TabIndex = 0;
            this.linkLabel_spawns.TabStop = true;
            this.linkLabel_spawns.Text = "Spawns";
            this.linkLabel_spawns.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_spawns.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_spawns_LinkClicked);
            // 
            // linkLabel_characters
            // 
            this.linkLabel_characters.AutoSize = true;
            this.linkLabel_characters.Location = new System.Drawing.Point(16, 30);
            this.linkLabel_characters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_characters.Name = "linkLabel_characters";
            this.linkLabel_characters.Size = new System.Drawing.Size(73, 16);
            this.linkLabel_characters.TabIndex = 0;
            this.linkLabel_characters.TabStop = true;
            this.linkLabel_characters.Text = "Characters";
            this.linkLabel_characters.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_characters.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_characters_LinkClicked);
            // 
            // linkLabel_quests
            // 
            this.linkLabel_quests.AutoSize = true;
            this.linkLabel_quests.Location = new System.Drawing.Point(152, 30);
            this.linkLabel_quests.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_quests.Name = "linkLabel_quests";
            this.linkLabel_quests.Size = new System.Drawing.Size(50, 16);
            this.linkLabel_quests.TabIndex = 0;
            this.linkLabel_quests.TabStop = true;
            this.linkLabel_quests.Text = "Quests";
            this.linkLabel_quests.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_quests.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_quests_LinkClicked);
            // 
            // linkLabel_items
            // 
            this.linkLabel_items.AutoSize = true;
            this.linkLabel_items.Location = new System.Drawing.Point(101, 30);
            this.linkLabel_items.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_items.Name = "linkLabel_items";
            this.linkLabel_items.Size = new System.Drawing.Size(40, 16);
            this.linkLabel_items.TabIndex = 0;
            this.linkLabel_items.TabStop = true;
            this.linkLabel_items.Text = "Items";
            this.linkLabel_items.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_items.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_items_LinkClicked);
            // 
            // linkLabel_zones
            // 
            this.linkLabel_zones.AutoSize = true;
            this.linkLabel_zones.Location = new System.Drawing.Point(336, 30);
            this.linkLabel_zones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_zones.Name = "linkLabel_zones";
            this.linkLabel_zones.Size = new System.Drawing.Size(46, 16);
            this.linkLabel_zones.TabIndex = 0;
            this.linkLabel_zones.TabStop = true;
            this.linkLabel_zones.Text = "Zones";
            this.linkLabel_zones.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_zones.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_zones_LinkClicked);
            // 
            // linkLabel_spells
            // 
            this.linkLabel_spells.AutoSize = true;
            this.linkLabel_spells.Location = new System.Drawing.Point(281, 30);
            this.linkLabel_spells.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_spells.Name = "linkLabel_spells";
            this.linkLabel_spells.Size = new System.Drawing.Size(46, 16);
            this.linkLabel_spells.TabIndex = 0;
            this.linkLabel_spells.TabStop = true;
            this.linkLabel_spells.Text = "Spells";
            this.linkLabel_spells.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_spells.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_spells_LinkClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.appsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1587, 29);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_file_exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuItem_file_exit
            // 
            this.menuItem_file_exit.Name = "menuItem_file_exit";
            this.menuItem_file_exit.Size = new System.Drawing.Size(104, 26);
            this.menuItem_file_exit.Text = "Exit";
            this.menuItem_file_exit.Click += new System.EventHandler(this.menuItem_file_exit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_view_tabs});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // menuItem_view_tabs
            // 
            this.menuItem_view_tabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_view_tabs_normal,
            this.menuItem_view_tabs_buttons,
            this.menuItem_view_tabs_flatbuttons});
            this.menuItem_view_tabs.Name = "menuItem_view_tabs";
            this.menuItem_view_tabs.Size = new System.Drawing.Size(112, 26);
            this.menuItem_view_tabs.Text = "Tabs";
            // 
            // menuItem_view_tabs_normal
            // 
            this.menuItem_view_tabs_normal.Checked = true;
            this.menuItem_view_tabs_normal.CheckOnClick = true;
            this.menuItem_view_tabs_normal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItem_view_tabs_normal.Name = "menuItem_view_tabs_normal";
            this.menuItem_view_tabs_normal.Size = new System.Drawing.Size(162, 26);
            this.menuItem_view_tabs_normal.Text = "Normal";
            this.menuItem_view_tabs_normal.Click += new System.EventHandler(this.menuItem_view_tabs_normal_Click);
            // 
            // menuItem_view_tabs_buttons
            // 
            this.menuItem_view_tabs_buttons.CheckOnClick = true;
            this.menuItem_view_tabs_buttons.Name = "menuItem_view_tabs_buttons";
            this.menuItem_view_tabs_buttons.Size = new System.Drawing.Size(162, 26);
            this.menuItem_view_tabs_buttons.Text = "Buttons";
            this.menuItem_view_tabs_buttons.Click += new System.EventHandler(this.menuItem_view_tabs_buttons_Click);
            // 
            // menuItem_view_tabs_flatbuttons
            // 
            this.menuItem_view_tabs_flatbuttons.CheckOnClick = true;
            this.menuItem_view_tabs_flatbuttons.Name = "menuItem_view_tabs_flatbuttons";
            this.menuItem_view_tabs_flatbuttons.Size = new System.Drawing.Size(162, 26);
            this.menuItem_view_tabs_flatbuttons.Text = "Flat Buttons";
            this.menuItem_view_tabs_flatbuttons.Click += new System.EventHandler(this.menuItem_view_tabs_flatbuttons_Click);
            // 
            // appsToolStripMenuItem
            // 
            this.appsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconCalculatorToolStripMenuItem,
            this.dBLoginToolStripMenuItem,
            this.waypointParserToolStripMenuItem});
            this.appsToolStripMenuItem.Name = "appsToolStripMenuItem";
            this.appsToolStripMenuItem.Size = new System.Drawing.Size(59, 25);
            this.appsToolStripMenuItem.Text = "Tools";
            // 
            // iconCalculatorToolStripMenuItem
            // 
            this.iconCalculatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_calculators_itemqueststep});
            this.iconCalculatorToolStripMenuItem.Name = "iconCalculatorToolStripMenuItem";
            this.iconCalculatorToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.iconCalculatorToolStripMenuItem.Text = "Calculators";
            // 
            // menuItem_calculators_itemqueststep
            // 
            this.menuItem_calculators_itemqueststep.Name = "menuItem_calculators_itemqueststep";
            this.menuItem_calculators_itemqueststep.Size = new System.Drawing.Size(193, 26);
            this.menuItem_calculators_itemqueststep.Text = "Item/Quest Step";
            this.menuItem_calculators_itemqueststep.Click += new System.EventHandler(this.menuItem_calculators_itemqueststep_Click);
            // 
            // dBLoginToolStripMenuItem
            // 
            this.dBLoginToolStripMenuItem.Name = "dBLoginToolStripMenuItem";
            this.dBLoginToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.dBLoginToolStripMenuItem.Text = "DB Login";
            this.dBLoginToolStripMenuItem.Click += new System.EventHandler(this.dBLoginToolStripMenuItem_Click_1);
            // 
            // waypointParserToolStripMenuItem
            // 
            this.waypointParserToolStripMenuItem.Name = "waypointParserToolStripMenuItem";
            this.waypointParserToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.waypointParserToolStripMenuItem.Text = "Waypoint Parser";
            this.waypointParserToolStripMenuItem.Click += new System.EventHandler(this.waypointParserToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_whatsnew,
            this.toolStripSeparator1,
            this.menuItem_about});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menuItem_whatsnew
            // 
            this.menuItem_whatsnew.Name = "menuItem_whatsnew";
            this.menuItem_whatsnew.Size = new System.Drawing.Size(164, 26);
            this.menuItem_whatsnew.Text = "What\'s New";
            this.menuItem_whatsnew.Click += new System.EventHandler(this.menuItem_whatsnew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // menuItem_about
            // 
            this.menuItem_about.Name = "menuItem_about";
            this.menuItem_about.ShortcutKeyDisplayString = "";
            this.menuItem_about.Size = new System.Drawing.Size(164, 26);
            this.menuItem_about.Text = "About";
            this.menuItem_about.Click += new System.EventHandler(this.menuItem_about_Click);
            // 
            // pictureBox_status
            // 
            this.pictureBox_status.BackColor = System.Drawing.Color.Red;
            this.pictureBox_status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_status.Location = new System.Drawing.Point(8, 879);
            this.pictureBox_status.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox_status.Name = "pictureBox_status";
            this.pictureBox_status.Size = new System.Drawing.Size(26, 24);
            this.pictureBox_status.TabIndex = 8;
            this.pictureBox_status.TabStop = false;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(43, 887);
            this.label_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(56, 16);
            this.label_status.TabIndex = 9;
            this.label_status.Text = "00:00:00";
            // 
            // timer_status
            // 
            this.timer_status.Interval = 1000;
            this.timer_status.Tick += new System.EventHandler(this.timer_status_Tick);
            // 
            // label_connected_user
            // 
            this.label_connected_user.AutoSize = true;
            this.label_connected_user.Location = new System.Drawing.Point(152, 887);
            this.label_connected_user.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_connected_user.Name = "label_connected_user";
            this.label_connected_user.Size = new System.Drawing.Size(134, 16);
            this.label_connected_user.TabIndex = 10;
            this.label_connected_user.Text = "Connected as <user>";
            // 
            // linkLabel_serverdetails
            // 
            this.linkLabel_serverdetails.AutoSize = true;
            this.linkLabel_serverdetails.Location = new System.Drawing.Point(393, 30);
            this.linkLabel_serverdetails.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_serverdetails.Name = "linkLabel_serverdetails";
            this.linkLabel_serverdetails.Size = new System.Drawing.Size(93, 16);
            this.linkLabel_serverdetails.TabIndex = 0;
            this.linkLabel_serverdetails.TabStop = true;
            this.linkLabel_serverdetails.Text = "Server Details";
            this.linkLabel_serverdetails.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_serverdetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_serverdetails_LinkClicked);
            // 
            // linkLabel_Bulk
            // 
            this.linkLabel_Bulk.AutoSize = true;
            this.linkLabel_Bulk.Location = new System.Drawing.Point(494, 30);
            this.linkLabel_Bulk.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_Bulk.Name = "linkLabel_Bulk";
            this.linkLabel_Bulk.Size = new System.Drawing.Size(34, 16);
            this.linkLabel_Bulk.TabIndex = 11;
            this.linkLabel_Bulk.TabStop = true;
            this.linkLabel_Bulk.Text = "Bulk";
            this.linkLabel_Bulk.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_Bulk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Bulk_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1587, 914);
            this.Controls.Add(this.linkLabel_Bulk);
            this.Controls.Add(this.linkLabel_serverdetails);
            this.Controls.Add(this.label_connected_user);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.pictureBox_status);
            this.Controls.Add(this.linkLabel_spells);
            this.Controls.Add(this.linkLabel_zones);
            this.Controls.Add(this.linkLabel_items);
            this.Controls.Add(this.linkLabel_quests);
            this.Controls.Add(this.linkLabel_characters);
            this.Controls.Add(this.linkLabel_spawns);
            this.Controls.Add(this.tabControl_main);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EQ2Emu Database Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.LinkLabel linkLabel_spawns;
        private System.Windows.Forms.LinkLabel linkLabel_characters;
        private System.Windows.Forms.LinkLabel linkLabel_quests;
        private System.Windows.Forms.LinkLabel linkLabel_items;
        private System.Windows.Forms.LinkLabel linkLabel_zones;
        private System.Windows.Forms.LinkLabel linkLabel_spells;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_file_exit;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_view_tabs;
        private System.Windows.Forms.ToolStripMenuItem menuItem_view_tabs_normal;
        private System.Windows.Forms.ToolStripMenuItem menuItem_view_tabs_buttons;
        private System.Windows.Forms.ToolStripMenuItem menuItem_view_tabs_flatbuttons;
        private System.Windows.Forms.PictureBox pictureBox_status;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Timer timer_status;
        private System.Windows.Forms.Label label_connected_user;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_about;
        private System.Windows.Forms.LinkLabel linkLabel_serverdetails;
        private System.Windows.Forms.ToolStripMenuItem menuItem_whatsnew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem appsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iconCalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_calculators_itemqueststep;
        private System.Windows.Forms.ToolStripMenuItem dBLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waypointParserToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel_Bulk;
    }
}

