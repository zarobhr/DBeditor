namespace TabControl_Spawns {
    partial class Form_AppearanceTypeLookup {
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
            this.label_display = new System.Windows.Forms.Label();
            this.textBox_search_1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView_equiptypes = new System.Windows.Forms.ListView();
            this.column_visualstates_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_visualstates_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_search = new System.Windows.Forms.Button();
            this.checkBox_searchwhiletyping = new System.Windows.Forms.CheckBox();
            this.pbViewer = new System.Windows.Forms.PictureBox();
            this.comboBox_creature_types = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_npc_wearable_types = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_wearable_items = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_slots = new System.Windows.Forms.ComboBox();
            this.textBox_search_2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // label_display
            // 
            this.label_display.AutoSize = true;
            this.label_display.Location = new System.Drawing.Point(12, 519);
            this.label_display.Name = "label_display";
            this.label_display.Size = new System.Drawing.Size(124, 13);
            this.label_display.TabIndex = 12;
            this.label_display.Text = "Displaying 0 equip types.";
            // 
            // textBox_search_1
            // 
            this.textBox_search_1.Location = new System.Drawing.Point(12, 25);
            this.textBox_search_1.Name = "textBox_search_1";
            this.textBox_search_1.Size = new System.Drawing.Size(275, 20);
            this.textBox_search_1.TabIndex = 10;
            this.textBox_search_1.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
            this.textBox_search_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Search";
            // 
            // listView_equiptypes
            // 
            this.listView_equiptypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_visualstates_name,
            this.column_visualstates_id});
            this.listView_equiptypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_equiptypes.FullRowSelect = true;
            this.listView_equiptypes.GridLines = true;
            this.listView_equiptypes.Location = new System.Drawing.Point(12, 74);
            this.listView_equiptypes.MultiSelect = false;
            this.listView_equiptypes.Name = "listView_equiptypes";
            this.listView_equiptypes.Size = new System.Drawing.Size(462, 442);
            this.listView_equiptypes.TabIndex = 8;
            this.listView_equiptypes.UseCompatibleStateImageBehavior = false;
            this.listView_equiptypes.View = System.Windows.Forms.View.Details;
            this.listView_equiptypes.SelectedIndexChanged += new System.EventHandler(this.listView_equiptypes_SelectedIndexChanged);
            this.listView_equiptypes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_equiptypes_MouseDoubleClick);
            // 
            // column_visualstates_name
            // 
            this.column_visualstates_name.Text = "Name";
            this.column_visualstates_name.Width = 376;
            // 
            // column_visualstates_id
            // 
            this.column_visualstates_id.Text = "ID";
            this.column_visualstates_id.Width = 74;
            // 
            // button_search
            // 
            this.button_search.BackgroundImage = global::TabControl_Spawns.Properties.Resources.find;
            this.button_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_search.Location = new System.Drawing.Point(609, 24);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(23, 23);
            this.button_search.TabIndex = 13;
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // checkBox_searchwhiletyping
            // 
            this.checkBox_searchwhiletyping.AutoSize = true;
            this.checkBox_searchwhiletyping.Location = new System.Drawing.Point(12, 51);
            this.checkBox_searchwhiletyping.Name = "checkBox_searchwhiletyping";
            this.checkBox_searchwhiletyping.Size = new System.Drawing.Size(103, 17);
            this.checkBox_searchwhiletyping.TabIndex = 14;
            this.checkBox_searchwhiletyping.Text = "Search as I type";
            this.checkBox_searchwhiletyping.UseVisualStyleBackColor = true;
            // 
            // pbViewer
            // 
            this.pbViewer.ImageLocation = "";
            this.pbViewer.InitialImage = global::TabControl_Spawns.Properties.Resources._100;
            this.pbViewer.Location = new System.Drawing.Point(479, 241);
            this.pbViewer.Name = "pbViewer";
            this.pbViewer.Size = new System.Drawing.Size(178, 274);
            this.pbViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbViewer.TabIndex = 15;
            this.pbViewer.TabStop = false;
            // 
            // comboBox_creature_types
            // 
            this.comboBox_creature_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_creature_types.FormattingEnabled = true;
            this.comboBox_creature_types.Location = new System.Drawing.Point(482, 89);
            this.comboBox_creature_types.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_creature_types.Name = "comboBox_creature_types";
            this.comboBox_creature_types.Size = new System.Drawing.Size(152, 21);
            this.comboBox_creature_types.TabIndex = 16;
            this.comboBox_creature_types.SelectedIndexChanged += new System.EventHandler(this.comboBox_creature_types_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(482, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Creature Types";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "NPC Wearable Types";
            // 
            // comboBox_npc_wearable_types
            // 
            this.comboBox_npc_wearable_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_npc_wearable_types.FormattingEnabled = true;
            this.comboBox_npc_wearable_types.Location = new System.Drawing.Point(482, 131);
            this.comboBox_npc_wearable_types.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_npc_wearable_types.Name = "comboBox_npc_wearable_types";
            this.comboBox_npc_wearable_types.Size = new System.Drawing.Size(152, 21);
            this.comboBox_npc_wearable_types.TabIndex = 19;
            this.comboBox_npc_wearable_types.SelectedIndexChanged += new System.EventHandler(this.comboBox_npc_wearable_types_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(482, 159);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Wearable Items";
            // 
            // comboBox_wearable_items
            // 
            this.comboBox_wearable_items.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_wearable_items.FormattingEnabled = true;
            this.comboBox_wearable_items.Location = new System.Drawing.Point(482, 175);
            this.comboBox_wearable_items.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_wearable_items.Name = "comboBox_wearable_items";
            this.comboBox_wearable_items.Size = new System.Drawing.Size(152, 21);
            this.comboBox_wearable_items.TabIndex = 21;
            this.comboBox_wearable_items.SelectedIndexChanged += new System.EventHandler(this.comboBox_wearable_items_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(482, 201);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Slot";
            // 
            // comboBox_slots
            // 
            this.comboBox_slots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_slots.FormattingEnabled = true;
            this.comboBox_slots.Location = new System.Drawing.Point(479, 216);
            this.comboBox_slots.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_slots.Name = "comboBox_slots";
            this.comboBox_slots.Size = new System.Drawing.Size(152, 21);
            this.comboBox_slots.TabIndex = 23;
            this.comboBox_slots.SelectedIndexChanged += new System.EventHandler(this.comboBox_slots_SelectedIndexChanged);
            // 
            // textBox_search_2
            // 
            this.textBox_search_2.Location = new System.Drawing.Point(328, 25);
            this.textBox_search_2.Name = "textBox_search_2";
            this.textBox_search_2.Size = new System.Drawing.Size(275, 20);
            this.textBox_search_2.TabIndex = 24;
            this.textBox_search_2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_2_KeyDown_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(296, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "And";
            // 
            // Form_AppearanceTypeLookup
            // 
            this.AcceptButton = this.button_search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 535);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_search_2);
            this.Controls.Add(this.comboBox_slots);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_wearable_items);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_npc_wearable_types);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_creature_types);
            this.Controls.Add(this.pbViewer);
            this.Controls.Add(this.checkBox_searchwhiletyping);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.label_display);
            this.Controls.Add(this.textBox_search_1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView_equiptypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 418);
            this.Name = "Form_AppearanceTypeLookup";
            this.Text = "Appearance Type Lookup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_AppearanceTypeLookup_FormClosing);
            this.Load += new System.EventHandler(this.Form_EquipTypeLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        #endregion

        private System.Windows.Forms.Label label_display;
        private System.Windows.Forms.TextBox textBox_search_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView_equiptypes;
        private System.Windows.Forms.ColumnHeader column_visualstates_name;
        private System.Windows.Forms.ColumnHeader column_visualstates_id;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.CheckBox checkBox_searchwhiletyping;
        private System.Windows.Forms.PictureBox pbViewer;
        private System.Windows.Forms.ComboBox comboBox_creature_types;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_npc_wearable_types;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_wearable_items;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_slots;
        private System.Windows.Forms.TextBox textBox_search_2;
        private System.Windows.Forms.Label label6;
    }
}