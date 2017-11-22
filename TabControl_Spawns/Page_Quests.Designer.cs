namespace TabControl_Spawns
{
    partial class Page_Quests
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_quest_select_zone = new System.Windows.Forms.ComboBox();
            this.label_select_quest = new System.Windows.Forms.Label();
            this.comboBox_quest_select_quest = new System.Windows.Forms.ComboBox();
            this.button_quest_descripton_insert = new System.Windows.Forms.Button();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_quest_description = new System.Windows.Forms.TabPage();
            this.textBox_quest_description_completedtext = new System.Windows.Forms.TextBox();
            this.button_quest_description_update = new System.Windows.Forms.Button();
            this.button_quest_description_remove = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_quest_description_description = new System.Windows.Forms.TextBox();
            this.textBox_quest_description_luascript = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_quest_description_zone = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_quest_description_type = new System.Windows.Forms.TextBox();
            this.textBox_quest_description_spawnid = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_quest_description_enclevel = new System.Windows.Forms.TextBox();
            this.textBox_quest_description_level = new System.Windows.Forms.TextBox();
            this.textBox_quest_description_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_quest_description_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage_quest_details = new System.Windows.Forms.TabPage();
            this.button_quest_details_remove = new System.Windows.Forms.Button();
            this.button_quest_details_update = new System.Windows.Forms.Button();
            this.button_quest_details_insert = new System.Windows.Forms.Button();
            this.groupBox_quest_details = new System.Windows.Forms.GroupBox();
            this.textBox_quest_details_quantity = new System.Windows.Forms.TextBox();
            this.textBox_quest_details_value = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBox_quest_details_subtype = new System.Windows.Forms.ComboBox();
            this.comboBox_quest_details_type = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_quest_details_factionid = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_quest_details_questid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_quest_details_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listView_quest_details = new System.Windows.Forms.ListView();
            this.column_quest_details_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_quest_details_questid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.olumn_quest_details_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.olumn_quest_details_subtype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.olumn_quest_details_value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.olumn_quest_details_factionid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.olumn_quest_details_quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_quest_script = new System.Windows.Forms.TabPage();
            this.button_close = new System.Windows.Forms.Button();
            this.tabControl_main.SuspendLayout();
            this.tabPage_quest_description.SuspendLayout();
            this.tabPage_quest_details.SuspendLayout();
            this.groupBox_quest_details.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Zone";
            // 
            // comboBox_quest_select_zone
            // 
            this.comboBox_quest_select_zone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_quest_select_zone.FormattingEnabled = true;
            this.comboBox_quest_select_zone.Location = new System.Drawing.Point(9, 50);
            this.comboBox_quest_select_zone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_quest_select_zone.MaxDropDownItems = 50;
            this.comboBox_quest_select_zone.Name = "comboBox_quest_select_zone";
            this.comboBox_quest_select_zone.Size = new System.Drawing.Size(247, 24);
            this.comboBox_quest_select_zone.Sorted = true;
            this.comboBox_quest_select_zone.TabIndex = 19;
            this.comboBox_quest_select_zone.TabStop = false;
            this.comboBox_quest_select_zone.SelectedIndexChanged += new System.EventHandler(this.comboBox_quest_select_zone_SelectedIndexChanged);
            // 
            // label_select_quest
            // 
            this.label_select_quest.AutoSize = true;
            this.label_select_quest.Location = new System.Drawing.Point(261, 31);
            this.label_select_quest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_select_quest.Name = "label_select_quest";
            this.label_select_quest.Size = new System.Drawing.Size(95, 16);
            this.label_select_quest.TabIndex = 20;
            this.label_select_quest.Text = "Select a Quest";
            this.label_select_quest.Visible = false;
            // 
            // comboBox_quest_select_quest
            // 
            this.comboBox_quest_select_quest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_quest_select_quest.FormattingEnabled = true;
            this.comboBox_quest_select_quest.Location = new System.Drawing.Point(265, 50);
            this.comboBox_quest_select_quest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_quest_select_quest.MaxDropDownItems = 50;
            this.comboBox_quest_select_quest.Name = "comboBox_quest_select_quest";
            this.comboBox_quest_select_quest.Size = new System.Drawing.Size(247, 24);
            this.comboBox_quest_select_quest.Sorted = true;
            this.comboBox_quest_select_quest.TabIndex = 21;
            this.comboBox_quest_select_quest.TabStop = false;
            this.comboBox_quest_select_quest.Visible = false;
            this.comboBox_quest_select_quest.SelectedIndexChanged += new System.EventHandler(this.comboBox_quest_select_quest_SelectedIndexChanged);
            // 
            // button_quest_descripton_insert
            // 
            this.button_quest_descripton_insert.Image = global::TabControl_Spawns.Properties.Resources.add;
            this.button_quest_descripton_insert.Location = new System.Drawing.Point(380, 570);
            this.button_quest_descripton_insert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_quest_descripton_insert.Name = "button_quest_descripton_insert";
            this.button_quest_descripton_insert.Size = new System.Drawing.Size(31, 28);
            this.button_quest_descripton_insert.TabIndex = 22;
            this.button_quest_descripton_insert.TabStop = false;
            this.button_quest_descripton_insert.UseVisualStyleBackColor = true;
            this.button_quest_descripton_insert.Click += new System.EventHandler(this.button_quest_descripton_insert_Click);
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.tabPage_quest_description);
            this.tabControl_main.Controls.Add(this.tabPage_quest_details);
            this.tabControl_main.Controls.Add(this.tabPage_quest_script);
            this.tabControl_main.Location = new System.Drawing.Point(4, 116);
            this.tabControl_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(1497, 730);
            this.tabControl_main.TabIndex = 22;
            this.tabControl_main.Visible = false;
            // 
            // tabPage_quest_description
            // 
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_completedtext);
            this.tabPage_quest_description.Controls.Add(this.button_quest_description_update);
            this.tabPage_quest_description.Controls.Add(this.button_quest_descripton_insert);
            this.tabPage_quest_description.Controls.Add(this.button_quest_description_remove);
            this.tabPage_quest_description.Controls.Add(this.label13);
            this.tabPage_quest_description.Controls.Add(this.label12);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_description);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_luascript);
            this.tabPage_quest_description.Controls.Add(this.label11);
            this.tabPage_quest_description.Controls.Add(this.label10);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_zone);
            this.tabPage_quest_description.Controls.Add(this.label9);
            this.tabPage_quest_description.Controls.Add(this.label8);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_type);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_spawnid);
            this.tabPage_quest_description.Controls.Add(this.label7);
            this.tabPage_quest_description.Controls.Add(this.label6);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_enclevel);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_level);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_name);
            this.tabPage_quest_description.Controls.Add(this.label5);
            this.tabPage_quest_description.Controls.Add(this.textBox_quest_description_id);
            this.tabPage_quest_description.Controls.Add(this.label3);
            this.tabPage_quest_description.Location = new System.Drawing.Point(4, 25);
            this.tabPage_quest_description.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage_quest_description.Name = "tabPage_quest_description";
            this.tabPage_quest_description.Size = new System.Drawing.Size(1489, 701);
            this.tabPage_quest_description.TabIndex = 2;
            this.tabPage_quest_description.Text = "Description";
            this.tabPage_quest_description.UseVisualStyleBackColor = true;
            this.tabPage_quest_description.Click += new System.EventHandler(this.tabPage_quest_description_Click);
            // 
            // textBox_quest_description_completedtext
            // 
            this.textBox_quest_description_completedtext.Location = new System.Drawing.Point(4, 415);
            this.textBox_quest_description_completedtext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_completedtext.Multiline = true;
            this.textBox_quest_description_completedtext.Name = "textBox_quest_description_completedtext";
            this.textBox_quest_description_completedtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_quest_description_completedtext.Size = new System.Drawing.Size(483, 147);
            this.textBox_quest_description_completedtext.TabIndex = 29;
            // 
            // button_quest_description_update
            // 
            this.button_quest_description_update.Image = global::TabControl_Spawns.Properties.Resources.save;
            this.button_quest_description_update.Location = new System.Drawing.Point(419, 570);
            this.button_quest_description_update.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_quest_description_update.Name = "button_quest_description_update";
            this.button_quest_description_update.Size = new System.Drawing.Size(31, 28);
            this.button_quest_description_update.TabIndex = 27;
            this.button_quest_description_update.UseVisualStyleBackColor = true;
            this.button_quest_description_update.Click += new System.EventHandler(this.button_quest_description_update_Click);
            // 
            // button_quest_description_remove
            // 
            this.button_quest_description_remove.Image = global::TabControl_Spawns.Properties.Resources.delete;
            this.button_quest_description_remove.Location = new System.Drawing.Point(457, 570);
            this.button_quest_description_remove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_quest_description_remove.Name = "button_quest_description_remove";
            this.button_quest_description_remove.Size = new System.Drawing.Size(31, 28);
            this.button_quest_description_remove.TabIndex = 26;
            this.button_quest_description_remove.UseVisualStyleBackColor = true;
            this.button_quest_description_remove.Click += new System.EventHandler(this.button_quest_description_remove_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 395);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Completed Text";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 215);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 16);
            this.label12.TabIndex = 24;
            this.label12.Text = "Description";
            // 
            // textBox_quest_description_description
            // 
            this.textBox_quest_description_description.Location = new System.Drawing.Point(4, 235);
            this.textBox_quest_description_description.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_description.Multiline = true;
            this.textBox_quest_description_description.Name = "textBox_quest_description_description";
            this.textBox_quest_description_description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_quest_description_description.Size = new System.Drawing.Size(483, 147);
            this.textBox_quest_description_description.TabIndex = 23;
            // 
            // textBox_quest_description_luascript
            // 
            this.textBox_quest_description_luascript.Location = new System.Drawing.Point(4, 135);
            this.textBox_quest_description_luascript.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_luascript.MaxLength = 100;
            this.textBox_quest_description_luascript.Name = "textBox_quest_description_luascript";
            this.textBox_quest_description_luascript.Size = new System.Drawing.Size(483, 22);
            this.textBox_quest_description_luascript.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 116);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "LUA Script";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(332, 66);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 16);
            this.label10.TabIndex = 20;
            this.label10.Text = "Zone";
            // 
            // textBox_quest_description_zone
            // 
            this.textBox_quest_description_zone.Location = new System.Drawing.Point(332, 86);
            this.textBox_quest_description_zone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_zone.Name = "textBox_quest_description_zone";
            this.textBox_quest_description_zone.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_description_zone.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(332, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(168, 16);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Spawn ID";
            // 
            // textBox_quest_description_type
            // 
            this.textBox_quest_description_type.Location = new System.Drawing.Point(332, 36);
            this.textBox_quest_description_type.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_type.Name = "textBox_quest_description_type";
            this.textBox_quest_description_type.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_description_type.TabIndex = 16;
            // 
            // textBox_quest_description_spawnid
            // 
            this.textBox_quest_description_spawnid.Location = new System.Drawing.Point(168, 36);
            this.textBox_quest_description_spawnid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_spawnid.Name = "textBox_quest_description_spawnid";
            this.textBox_quest_description_spawnid.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_description_spawnid.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Enc Level";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 66);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Level";
            // 
            // textBox_quest_description_enclevel
            // 
            this.textBox_quest_description_enclevel.Location = new System.Drawing.Point(168, 86);
            this.textBox_quest_description_enclevel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_enclevel.Name = "textBox_quest_description_enclevel";
            this.textBox_quest_description_enclevel.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_description_enclevel.TabIndex = 12;
            // 
            // textBox_quest_description_level
            // 
            this.textBox_quest_description_level.Location = new System.Drawing.Point(4, 86);
            this.textBox_quest_description_level.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_level.Name = "textBox_quest_description_level";
            this.textBox_quest_description_level.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_description_level.TabIndex = 11;
            // 
            // textBox_quest_description_name
            // 
            this.textBox_quest_description_name.Location = new System.Drawing.Point(4, 186);
            this.textBox_quest_description_name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_name.MaxLength = 100;
            this.textBox_quest_description_name.Name = "textBox_quest_description_name";
            this.textBox_quest_description_name.Size = new System.Drawing.Size(483, 22);
            this.textBox_quest_description_name.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Name";
            // 
            // textBox_quest_description_id
            // 
            this.textBox_quest_description_id.Location = new System.Drawing.Point(4, 36);
            this.textBox_quest_description_id.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_description_id.Name = "textBox_quest_description_id";
            this.textBox_quest_description_id.ReadOnly = true;
            this.textBox_quest_description_id.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_description_id.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "ID";
            // 
            // tabPage_quest_details
            // 
            this.tabPage_quest_details.Controls.Add(this.button_quest_details_remove);
            this.tabPage_quest_details.Controls.Add(this.button_quest_details_update);
            this.tabPage_quest_details.Controls.Add(this.button_quest_details_insert);
            this.tabPage_quest_details.Controls.Add(this.groupBox_quest_details);
            this.tabPage_quest_details.Controls.Add(this.listView_quest_details);
            this.tabPage_quest_details.Location = new System.Drawing.Point(4, 25);
            this.tabPage_quest_details.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage_quest_details.Name = "tabPage_quest_details";
            this.tabPage_quest_details.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage_quest_details.Size = new System.Drawing.Size(1489, 701);
            this.tabPage_quest_details.TabIndex = 0;
            this.tabPage_quest_details.Text = "Details";
            this.tabPage_quest_details.UseVisualStyleBackColor = true;
            // 
            // button_quest_details_remove
            // 
            this.button_quest_details_remove.Location = new System.Drawing.Point(661, 662);
            this.button_quest_details_remove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_quest_details_remove.Name = "button_quest_details_remove";
            this.button_quest_details_remove.Size = new System.Drawing.Size(100, 28);
            this.button_quest_details_remove.TabIndex = 5;
            this.button_quest_details_remove.Text = "Remove";
            this.button_quest_details_remove.UseVisualStyleBackColor = true;
            this.button_quest_details_remove.Click += new System.EventHandler(this.button_quest_details_remove_Click);
            // 
            // button_quest_details_update
            // 
            this.button_quest_details_update.Location = new System.Drawing.Point(116, 662);
            this.button_quest_details_update.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_quest_details_update.Name = "button_quest_details_update";
            this.button_quest_details_update.Size = new System.Drawing.Size(100, 28);
            this.button_quest_details_update.TabIndex = 4;
            this.button_quest_details_update.Text = "Update";
            this.button_quest_details_update.UseVisualStyleBackColor = true;
            this.button_quest_details_update.Click += new System.EventHandler(this.button_quest_details_update_Click);
            // 
            // button_quest_details_insert
            // 
            this.button_quest_details_insert.Location = new System.Drawing.Point(8, 662);
            this.button_quest_details_insert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_quest_details_insert.Name = "button_quest_details_insert";
            this.button_quest_details_insert.Size = new System.Drawing.Size(100, 28);
            this.button_quest_details_insert.TabIndex = 3;
            this.button_quest_details_insert.Text = "Insert";
            this.button_quest_details_insert.UseVisualStyleBackColor = true;
            this.button_quest_details_insert.Click += new System.EventHandler(this.button_quest_details_insert_Click);
            // 
            // groupBox_quest_details
            // 
            this.groupBox_quest_details.Controls.Add(this.textBox_quest_details_quantity);
            this.groupBox_quest_details.Controls.Add(this.textBox_quest_details_value);
            this.groupBox_quest_details.Controls.Add(this.label18);
            this.groupBox_quest_details.Controls.Add(this.label17);
            this.groupBox_quest_details.Controls.Add(this.comboBox_quest_details_subtype);
            this.groupBox_quest_details.Controls.Add(this.comboBox_quest_details_type);
            this.groupBox_quest_details.Controls.Add(this.label16);
            this.groupBox_quest_details.Controls.Add(this.label15);
            this.groupBox_quest_details.Controls.Add(this.textBox_quest_details_factionid);
            this.groupBox_quest_details.Controls.Add(this.label14);
            this.groupBox_quest_details.Controls.Add(this.textBox_quest_details_questid);
            this.groupBox_quest_details.Controls.Add(this.label4);
            this.groupBox_quest_details.Controls.Add(this.textBox_quest_details_id);
            this.groupBox_quest_details.Controls.Add(this.label2);
            this.groupBox_quest_details.Location = new System.Drawing.Point(8, 474);
            this.groupBox_quest_details.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_quest_details.Name = "groupBox_quest_details";
            this.groupBox_quest_details.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_quest_details.Size = new System.Drawing.Size(753, 181);
            this.groupBox_quest_details.TabIndex = 2;
            this.groupBox_quest_details.TabStop = false;
            this.groupBox_quest_details.Text = "Quest Details";
            // 
            // textBox_quest_details_quantity
            // 
            this.textBox_quest_details_quantity.Location = new System.Drawing.Point(172, 137);
            this.textBox_quest_details_quantity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_details_quantity.Name = "textBox_quest_details_quantity";
            this.textBox_quest_details_quantity.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_details_quantity.TabIndex = 14;
            // 
            // textBox_quest_details_value
            // 
            this.textBox_quest_details_value.Location = new System.Drawing.Point(8, 137);
            this.textBox_quest_details_value.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_details_value.Name = "textBox_quest_details_value";
            this.textBox_quest_details_value.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_details_value.TabIndex = 13;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(172, 117);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 16);
            this.label18.TabIndex = 12;
            this.label18.Text = "Quantity";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 117);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 16);
            this.label17.TabIndex = 11;
            this.label17.Text = "Value";
            // 
            // comboBox_quest_details_subtype
            // 
            this.comboBox_quest_details_subtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_quest_details_subtype.FormattingEnabled = true;
            this.comboBox_quest_details_subtype.Location = new System.Drawing.Point(172, 87);
            this.comboBox_quest_details_subtype.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_quest_details_subtype.Name = "comboBox_quest_details_subtype";
            this.comboBox_quest_details_subtype.Size = new System.Drawing.Size(155, 24);
            this.comboBox_quest_details_subtype.TabIndex = 10;
            // 
            // comboBox_quest_details_type
            // 
            this.comboBox_quest_details_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_quest_details_type.FormattingEnabled = true;
            this.comboBox_quest_details_type.Location = new System.Drawing.Point(8, 87);
            this.comboBox_quest_details_type.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_quest_details_type.Name = "comboBox_quest_details_type";
            this.comboBox_quest_details_type.Size = new System.Drawing.Size(155, 24);
            this.comboBox_quest_details_type.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(172, 68);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 16);
            this.label16.TabIndex = 8;
            this.label16.Text = "Subtype";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 68);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 7;
            this.label15.Text = "Type";
            // 
            // textBox_quest_details_factionid
            // 
            this.textBox_quest_details_factionid.Location = new System.Drawing.Point(336, 39);
            this.textBox_quest_details_factionid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_details_factionid.Name = "textBox_quest_details_factionid";
            this.textBox_quest_details_factionid.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_details_factionid.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(336, 20);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 16);
            this.label14.TabIndex = 5;
            this.label14.Text = "Faction ID";
            // 
            // textBox_quest_details_questid
            // 
            this.textBox_quest_details_questid.Location = new System.Drawing.Point(172, 39);
            this.textBox_quest_details_questid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_details_questid.Name = "textBox_quest_details_questid";
            this.textBox_quest_details_questid.ReadOnly = true;
            this.textBox_quest_details_questid.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_details_questid.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Quest ID";
            // 
            // textBox_quest_details_id
            // 
            this.textBox_quest_details_id.Location = new System.Drawing.Point(8, 39);
            this.textBox_quest_details_id.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_quest_details_id.Name = "textBox_quest_details_id";
            this.textBox_quest_details_id.ReadOnly = true;
            this.textBox_quest_details_id.Size = new System.Drawing.Size(155, 22);
            this.textBox_quest_details_id.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID";
            // 
            // listView_quest_details
            // 
            this.listView_quest_details.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_quest_details_id,
            this.column_quest_details_questid,
            this.olumn_quest_details_type,
            this.olumn_quest_details_subtype,
            this.olumn_quest_details_value,
            this.olumn_quest_details_factionid,
            this.olumn_quest_details_quantity});
            this.listView_quest_details.FullRowSelect = true;
            this.listView_quest_details.GridLines = true;
            this.listView_quest_details.Location = new System.Drawing.Point(8, 7);
            this.listView_quest_details.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView_quest_details.Name = "listView_quest_details";
            this.listView_quest_details.Size = new System.Drawing.Size(752, 458);
            this.listView_quest_details.TabIndex = 0;
            this.listView_quest_details.UseCompatibleStateImageBehavior = false;
            this.listView_quest_details.View = System.Windows.Forms.View.Details;
            this.listView_quest_details.SelectedIndexChanged += new System.EventHandler(this.listView_quest_details_SelectedIndexChanged);
            // 
            // column_quest_details_id
            // 
            this.column_quest_details_id.Text = "ID";
            this.column_quest_details_id.Width = 80;
            // 
            // column_quest_details_questid
            // 
            this.column_quest_details_questid.Text = "Quest ID";
            this.column_quest_details_questid.Width = 80;
            // 
            // olumn_quest_details_type
            // 
            this.olumn_quest_details_type.Text = "Type";
            this.olumn_quest_details_type.Width = 80;
            // 
            // olumn_quest_details_subtype
            // 
            this.olumn_quest_details_subtype.Text = "Subtype";
            this.olumn_quest_details_subtype.Width = 80;
            // 
            // olumn_quest_details_value
            // 
            this.olumn_quest_details_value.Text = "Value";
            this.olumn_quest_details_value.Width = 80;
            // 
            // olumn_quest_details_factionid
            // 
            this.olumn_quest_details_factionid.Text = "Faction ID";
            this.olumn_quest_details_factionid.Width = 80;
            // 
            // olumn_quest_details_quantity
            // 
            this.olumn_quest_details_quantity.Text = "Quantity";
            this.olumn_quest_details_quantity.Width = 80;
            // 
            // tabPage_quest_script
            // 
            this.tabPage_quest_script.Location = new System.Drawing.Point(4, 25);
            this.tabPage_quest_script.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage_quest_script.Name = "tabPage_quest_script";
            this.tabPage_quest_script.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage_quest_script.Size = new System.Drawing.Size(1489, 701);
            this.tabPage_quest_script.TabIndex = 1;
            this.tabPage_quest_script.Text = "Script";
            this.tabPage_quest_script.UseVisualStyleBackColor = true;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(1396, 48);
            this.button_close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(100, 28);
            this.button_close.TabIndex = 23;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // Page_Quests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.tabControl_main);
            this.Controls.Add(this.comboBox_quest_select_quest);
            this.Controls.Add(this.label_select_quest);
            this.Controls.Add(this.comboBox_quest_select_zone);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Page_Quests";
            this.Size = new System.Drawing.Size(1509, 860);
            this.Load += new System.EventHandler(this.Page_Quests_Load);
            this.tabControl_main.ResumeLayout(false);
            this.tabPage_quest_description.ResumeLayout(false);
            this.tabPage_quest_description.PerformLayout();
            this.tabPage_quest_details.ResumeLayout(false);
            this.groupBox_quest_details.ResumeLayout(false);
            this.groupBox_quest_details.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_quest_select_zone;
        private System.Windows.Forms.Label label_select_quest;
        private System.Windows.Forms.ComboBox comboBox_quest_select_quest;
        private System.Windows.Forms.Button button_quest_descripton_insert;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_quest_details;
        private System.Windows.Forms.TabPage tabPage_quest_script;
        private System.Windows.Forms.TabPage tabPage_quest_description;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_quest_description_id;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_quest_description_luascript;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_quest_description_zone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_quest_description_type;
        private System.Windows.Forms.TextBox textBox_quest_description_spawnid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_quest_description_enclevel;
        private System.Windows.Forms.TextBox textBox_quest_description_level;
        private System.Windows.Forms.TextBox textBox_quest_description_name;
        private System.Windows.Forms.TextBox textBox_quest_description_completedtext;
        private System.Windows.Forms.Button button_quest_description_update;
        private System.Windows.Forms.Button button_quest_description_remove;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_quest_description_description;
        private System.Windows.Forms.ListView listView_quest_details;
        private System.Windows.Forms.ColumnHeader column_quest_details_id;
        private System.Windows.Forms.ColumnHeader column_quest_details_questid;
        private System.Windows.Forms.ColumnHeader olumn_quest_details_type;
        private System.Windows.Forms.ColumnHeader olumn_quest_details_subtype;
        private System.Windows.Forms.ColumnHeader olumn_quest_details_value;
        private System.Windows.Forms.ColumnHeader olumn_quest_details_factionid;
        private System.Windows.Forms.ColumnHeader olumn_quest_details_quantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_quest_details;
        private System.Windows.Forms.TextBox textBox_quest_details_id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_quest_details_questid;
        private System.Windows.Forms.TextBox textBox_quest_details_factionid;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBox_quest_details_subtype;
        private System.Windows.Forms.ComboBox comboBox_quest_details_type;
        private System.Windows.Forms.Button button_quest_details_insert;
        private System.Windows.Forms.TextBox textBox_quest_details_quantity;
        private System.Windows.Forms.TextBox textBox_quest_details_value;
        private System.Windows.Forms.Button button_quest_details_remove;
        private System.Windows.Forms.Button button_quest_details_update;
        private System.Windows.Forms.Button button_close;
    }
}
