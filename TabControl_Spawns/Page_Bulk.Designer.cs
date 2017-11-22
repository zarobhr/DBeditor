namespace TabControl_Spawns
{
    partial class Page_Bulk
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
            this.label_select_zone = new System.Windows.Forms.Label();
            this.comboBox_select_zone = new System.Windows.Forms.ComboBox();
            this.tabControl_Bulk = new System.Windows.Forms.TabControl();
            this.tabPage_loot = new System.Windows.Forms.TabPage();
            this.ListView_bulk_loottables = new System.Windows.Forms.ListView();
            this.ListView_bulk_spawns = new System.Windows.Forms.ListView();
            this.lblLootTables = new System.Windows.Forms.Label();
            this.lblLootSpawns = new System.Windows.Forms.Label();
            this.txtSpawnsNotice = new System.Windows.Forms.TextBox();
            this.button_bulk_loot_update = new System.Windows.Forms.Button();
            this.columnHeader_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_mincoin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_maxcoin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_mindrop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_maxdrop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_coin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_loot_spawn_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_loot_spawn_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_loot_spawn_level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_filter_1 = new System.Windows.Forms.TextBox();
            this.textBox_filter_2 = new System.Windows.Forms.TextBox();
            this.comboBox_filter = new System.Windows.Forms.ComboBox();
            this.button_close = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.tabControl_Bulk.SuspendLayout();
            this.tabPage_loot.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_select_zone
            // 
            this.label_select_zone.AutoSize = true;
            this.label_select_zone.Location = new System.Drawing.Point(14, 14);
            this.label_select_zone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_select_zone.Name = "label_select_zone";
            this.label_select_zone.Size = new System.Drawing.Size(91, 16);
            this.label_select_zone.TabIndex = 5;
            this.label_select_zone.Text = "Select a Zone";
            // 
            // comboBox_select_zone
            // 
            this.comboBox_select_zone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select_zone.FormattingEnabled = true;
            this.comboBox_select_zone.Location = new System.Drawing.Point(18, 34);
            this.comboBox_select_zone.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_select_zone.MaxDropDownItems = 50;
            this.comboBox_select_zone.Name = "comboBox_select_zone";
            this.comboBox_select_zone.Size = new System.Drawing.Size(491, 24);
            this.comboBox_select_zone.Sorted = true;
            this.comboBox_select_zone.TabIndex = 4;
            this.comboBox_select_zone.TabStop = false;
            this.comboBox_select_zone.SelectedIndexChanged += new System.EventHandler(this.comboBox_select_zone_SelectedIndexChanged);
            // 
            // tabControl_Bulk
            // 
            this.tabControl_Bulk.Controls.Add(this.tabPage_loot);
            this.tabControl_Bulk.Location = new System.Drawing.Point(18, 65);
            this.tabControl_Bulk.Name = "tabControl_Bulk";
            this.tabControl_Bulk.SelectedIndex = 0;
            this.tabControl_Bulk.Size = new System.Drawing.Size(1356, 689);
            this.tabControl_Bulk.TabIndex = 6;
            // 
            // tabPage_loot
            // 
            this.tabPage_loot.Controls.Add(this.comboBox_filter);
            this.tabPage_loot.Controls.Add(this.textBox_filter_2);
            this.tabPage_loot.Controls.Add(this.textBox_filter_1);
            this.tabPage_loot.Controls.Add(this.label2);
            this.tabPage_loot.Controls.Add(this.label1);
            this.tabPage_loot.Controls.Add(this.button_bulk_loot_update);
            this.tabPage_loot.Controls.Add(this.txtSpawnsNotice);
            this.tabPage_loot.Controls.Add(this.lblLootSpawns);
            this.tabPage_loot.Controls.Add(this.lblLootTables);
            this.tabPage_loot.Controls.Add(this.ListView_bulk_spawns);
            this.tabPage_loot.Controls.Add(this.ListView_bulk_loottables);
            this.tabPage_loot.Location = new System.Drawing.Point(4, 25);
            this.tabPage_loot.Name = "tabPage_loot";
            this.tabPage_loot.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_loot.Size = new System.Drawing.Size(1348, 660);
            this.tabPage_loot.TabIndex = 0;
            this.tabPage_loot.Text = "Loot";
            this.tabPage_loot.UseVisualStyleBackColor = true;
            // 
            // ListView_bulk_loottables
            // 
            this.ListView_bulk_loottables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_ID,
            this.columnHeader_Name,
            this.columnHeader_mincoin,
            this.columnHeader_maxcoin,
            this.columnHeader_mindrop,
            this.columnHeader_maxdrop,
            this.columnHeader_coin});
            this.ListView_bulk_loottables.FullRowSelect = true;
            this.ListView_bulk_loottables.GridLines = true;
            this.ListView_bulk_loottables.HideSelection = false;
            this.ListView_bulk_loottables.Location = new System.Drawing.Point(21, 64);
            this.ListView_bulk_loottables.MultiSelect = false;
            this.ListView_bulk_loottables.Name = "ListView_bulk_loottables";
            this.ListView_bulk_loottables.Size = new System.Drawing.Size(760, 517);
            this.ListView_bulk_loottables.TabIndex = 0;
            this.ListView_bulk_loottables.UseCompatibleStateImageBehavior = false;
            this.ListView_bulk_loottables.View = System.Windows.Forms.View.Details;
            this.ListView_bulk_loottables.SelectedIndexChanged += new System.EventHandler(this.ListView_bulk_loottables_SelectedIndexChanged);
            // 
            // ListView_bulk_spawns
            // 
            this.ListView_bulk_spawns.CheckBoxes = true;
            this.ListView_bulk_spawns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_loot_spawn_id,
            this.columnHeader_loot_spawn_name,
            this.columnHeader_loot_spawn_level});
            this.ListView_bulk_spawns.FullRowSelect = true;
            this.ListView_bulk_spawns.GridLines = true;
            this.ListView_bulk_spawns.HideSelection = false;
            this.ListView_bulk_spawns.Location = new System.Drawing.Point(787, 64);
            this.ListView_bulk_spawns.Name = "ListView_bulk_spawns";
            this.ListView_bulk_spawns.Size = new System.Drawing.Size(555, 517);
            this.ListView_bulk_spawns.TabIndex = 1;
            this.ListView_bulk_spawns.UseCompatibleStateImageBehavior = false;
            this.ListView_bulk_spawns.View = System.Windows.Forms.View.Details;
            // 
            // lblLootTables
            // 
            this.lblLootTables.AutoSize = true;
            this.lblLootTables.Location = new System.Drawing.Point(18, 45);
            this.lblLootTables.Name = "lblLootTables";
            this.lblLootTables.Size = new System.Drawing.Size(80, 16);
            this.lblLootTables.TabIndex = 2;
            this.lblLootTables.Text = "Loot Tables";
            // 
            // lblLootSpawns
            // 
            this.lblLootSpawns.AutoSize = true;
            this.lblLootSpawns.Location = new System.Drawing.Point(784, 45);
            this.lblLootSpawns.Name = "lblLootSpawns";
            this.lblLootSpawns.Size = new System.Drawing.Size(56, 16);
            this.lblLootSpawns.TabIndex = 3;
            this.lblLootSpawns.Text = "Spawns";
            // 
            // txtSpawnsNotice
            // 
            this.txtSpawnsNotice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtSpawnsNotice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.064F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpawnsNotice.ForeColor = System.Drawing.Color.Crimson;
            this.txtSpawnsNotice.Location = new System.Drawing.Point(846, 6);
            this.txtSpawnsNotice.Multiline = true;
            this.txtSpawnsNotice.Name = "txtSpawnsNotice";
            this.txtSpawnsNotice.ReadOnly = true;
            this.txtSpawnsNotice.Size = new System.Drawing.Size(495, 52);
            this.txtSpawnsNotice.TabIndex = 4;
            this.txtSpawnsNotice.Text = "This utility wipes and inserts spawn loot data for the zone and loottable selecte" +
    "d, you have been warned!!";
            // 
            // button_bulk_loot_update
            // 
            this.button_bulk_loot_update.BackgroundImage = global::TabControl_Spawns.Properties.Resources.save;
            this.button_bulk_loot_update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_bulk_loot_update.Location = new System.Drawing.Point(1310, 597);
            this.button_bulk_loot_update.Margin = new System.Windows.Forms.Padding(4);
            this.button_bulk_loot_update.Name = "button_bulk_loot_update";
            this.button_bulk_loot_update.Size = new System.Drawing.Size(31, 28);
            this.button_bulk_loot_update.TabIndex = 25;
            this.button_bulk_loot_update.UseVisualStyleBackColor = true;
            this.button_bulk_loot_update.Click += new System.EventHandler(this.button_bulk_loot_update_Click);
            // 
            // columnHeader_ID
            // 
            this.columnHeader_ID.Text = "ID";
            // 
            // columnHeader_Name
            // 
            this.columnHeader_Name.Text = "Name";
            this.columnHeader_Name.Width = 230;
            // 
            // columnHeader_mincoin
            // 
            this.columnHeader_mincoin.Text = "MinCoin";
            this.columnHeader_mincoin.Width = 55;
            // 
            // columnHeader_maxcoin
            // 
            this.columnHeader_maxcoin.Text = "MaxCoin";
            this.columnHeader_maxcoin.Width = 55;
            // 
            // columnHeader_mindrop
            // 
            this.columnHeader_mindrop.Text = "MinDrop";
            this.columnHeader_mindrop.Width = 55;
            // 
            // columnHeader_maxdrop
            // 
            this.columnHeader_maxdrop.Text = "MaxDrop";
            this.columnHeader_maxdrop.Width = 55;
            // 
            // columnHeader_coin
            // 
            this.columnHeader_coin.Text = "Coin %";
            this.columnHeader_coin.Width = 54;
            // 
            // columnHeader_loot_spawn_id
            // 
            this.columnHeader_loot_spawn_id.Text = "ID";
            // 
            // columnHeader_loot_spawn_name
            // 
            this.columnHeader_loot_spawn_name.Text = "Name";
            this.columnHeader_loot_spawn_name.Width = 220;
            // 
            // columnHeader_loot_spawn_level
            // 
            this.columnHeader_loot_spawn_level.Text = "Level";
            this.columnHeader_loot_spawn_level.Width = 76;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(786, 584);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Filter #1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1031, 584);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Filter #2";
            // 
            // textBox_filter_1
            // 
            this.textBox_filter_1.Location = new System.Drawing.Point(787, 603);
            this.textBox_filter_1.Name = "textBox_filter_1";
            this.textBox_filter_1.Size = new System.Drawing.Size(163, 22);
            this.textBox_filter_1.TabIndex = 28;
            this.textBox_filter_1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_filter_1_KeyUp);
            // 
            // textBox_filter_2
            // 
            this.textBox_filter_2.Location = new System.Drawing.Point(1034, 603);
            this.textBox_filter_2.Name = "textBox_filter_2";
            this.textBox_filter_2.Size = new System.Drawing.Size(163, 22);
            this.textBox_filter_2.TabIndex = 29;
            this.textBox_filter_2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_filter_2_KeyUp);
            // 
            // comboBox_filter
            // 
            this.comboBox_filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_filter.FormattingEnabled = true;
            this.comboBox_filter.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.comboBox_filter.Location = new System.Drawing.Point(957, 600);
            this.comboBox_filter.Name = "comboBox_filter";
            this.comboBox_filter.Size = new System.Drawing.Size(71, 24);
            this.comboBox_filter.TabIndex = 30;
            this.comboBox_filter.SelectedIndexChanged += new System.EventHandler(this.comboBox_filter_SelectedIndexChanged);
            // 
            // button_close
            // 
            this.button_close.BackgroundImage = global::TabControl_Spawns.Properties.Resources.close;
            this.button_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_close.Location = new System.Drawing.Point(551, 32);
            this.button_close.Margin = new System.Windows.Forms.Padding(4);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(31, 28);
            this.button_close.TabIndex = 31;
            this.button_close.TabStop = false;
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.BackgroundImage = global::TabControl_Spawns.Properties.Resources.refresh;
            this.button_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_refresh.Location = new System.Drawing.Point(517, 32);
            this.button_refresh.Margin = new System.Windows.Forms.Padding(4);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(31, 28);
            this.button_refresh.TabIndex = 32;
            this.button_refresh.TabStop = false;
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // Page_Bulk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.tabControl_Bulk);
            this.Controls.Add(this.label_select_zone);
            this.Controls.Add(this.comboBox_select_zone);
            this.Name = "Page_Bulk";
            this.Size = new System.Drawing.Size(1389, 780);
            this.Load += new System.EventHandler(this.Page_Bulk_Load);
            this.tabControl_Bulk.ResumeLayout(false);
            this.tabPage_loot.ResumeLayout(false);
            this.tabPage_loot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_select_zone;
        private System.Windows.Forms.ComboBox comboBox_select_zone;
        private System.Windows.Forms.TabControl tabControl_Bulk;
        private System.Windows.Forms.TabPage tabPage_loot;
        private System.Windows.Forms.TextBox txtSpawnsNotice;
        private System.Windows.Forms.Label lblLootSpawns;
        private System.Windows.Forms.Label lblLootTables;
        private System.Windows.Forms.ListView ListView_bulk_spawns;
        private System.Windows.Forms.ListView ListView_bulk_loottables;
        private System.Windows.Forms.Button button_bulk_loot_update;
        private System.Windows.Forms.ColumnHeader columnHeader_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_mincoin;
        private System.Windows.Forms.ColumnHeader columnHeader_maxcoin;
        private System.Windows.Forms.ColumnHeader columnHeader_mindrop;
        private System.Windows.Forms.ColumnHeader columnHeader_maxdrop;
        private System.Windows.Forms.ColumnHeader columnHeader_coin;
        private System.Windows.Forms.ColumnHeader columnHeader_loot_spawn_id;
        private System.Windows.Forms.ColumnHeader columnHeader_loot_spawn_name;
        private System.Windows.Forms.ColumnHeader columnHeader_loot_spawn_level;
        private System.Windows.Forms.TextBox textBox_filter_2;
        private System.Windows.Forms.TextBox textBox_filter_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_filter;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_refresh;
    }
}
