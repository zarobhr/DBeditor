namespace TabControl_Spawns
{
    partial class Form_SpawnSearch
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SpawnSearch));
            this.textBoxSpawnSearch = new System.Windows.Forms.TextBox();
            this.listView_Spawn_Search_Results = new System.Windows.Forms.ListView();
            this.columnHeader__Search_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Search_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Search_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Search_Zone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Search_Zone_Short = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Spawn_Name_Long = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_display = new System.Windows.Forms.Label();
            this.button_Spawn_Search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSpawnSearch
            // 
            this.textBoxSpawnSearch.Location = new System.Drawing.Point(13, 7);
            this.textBoxSpawnSearch.Name = "textBoxSpawnSearch";
            this.textBoxSpawnSearch.Size = new System.Drawing.Size(478, 20);
            this.textBoxSpawnSearch.TabIndex = 1;
            // 
            // listView_Spawn_Search_Results
            // 
            this.listView_Spawn_Search_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader__Search_ID,
            this.columnHeader_Search_Name,
            this.columnHeader_Search_Type,
            this.columnHeader_Search_Zone,
            this.columnHeader_Search_Zone_Short,
            this.columnHeader_Spawn_Name_Long});
            this.listView_Spawn_Search_Results.FullRowSelect = true;
            this.listView_Spawn_Search_Results.Location = new System.Drawing.Point(13, 34);
            this.listView_Spawn_Search_Results.MultiSelect = false;
            this.listView_Spawn_Search_Results.Name = "listView_Spawn_Search_Results";
            this.listView_Spawn_Search_Results.Size = new System.Drawing.Size(507, 394);
            this.listView_Spawn_Search_Results.TabIndex = 2;
            this.listView_Spawn_Search_Results.UseCompatibleStateImageBehavior = false;
            this.listView_Spawn_Search_Results.View = System.Windows.Forms.View.Details;
            this.listView_Spawn_Search_Results.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Spawn_Search_Results_MouseDoubleClick);
            // 
            // columnHeader__Search_ID
            // 
            this.columnHeader__Search_ID.Text = "ID";
            // 
            // columnHeader_Search_Name
            // 
            this.columnHeader_Search_Name.Text = "Name";
            this.columnHeader_Search_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader_Search_Name.Width = 320;
            // 
            // columnHeader_Search_Type
            // 
            this.columnHeader_Search_Type.Text = "Type";
            this.columnHeader_Search_Type.Width = 50;
            // 
            // columnHeader_Search_Zone
            // 
            this.columnHeader_Search_Zone.Text = "Zone";
            this.columnHeader_Search_Zone.Width = 0;
            // 
            // columnHeader_Search_Zone_Short
            // 
            this.columnHeader_Search_Zone_Short.Text = "Zone";
            this.columnHeader_Search_Zone_Short.Width = 80;
            // 
            // columnHeader_Spawn_Name_Long
            // 
            this.columnHeader_Spawn_Name_Long.Width = 0;
            // 
            // label_display
            // 
            this.label_display.AutoSize = true;
            this.label_display.Location = new System.Drawing.Point(10, 431);
            this.label_display.Name = "label_display";
            this.label_display.Size = new System.Drawing.Size(82, 13);
            this.label_display.TabIndex = 4;
            this.label_display.Text = "Results: 0 Items";
            // 
            // button_Spawn_Search
            // 
            this.button_Spawn_Search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_Spawn_Search.BackgroundImage")));
            this.button_Spawn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Spawn_Search.Location = new System.Drawing.Point(497, 5);
            this.button_Spawn_Search.Name = "button_Spawn_Search";
            this.button_Spawn_Search.Size = new System.Drawing.Size(23, 23);
            this.button_Spawn_Search.TabIndex = 3;
            this.button_Spawn_Search.UseVisualStyleBackColor = true;
            this.button_Spawn_Search.Click += new System.EventHandler(this.button_Spawn_Search_Click);
            // 
            // Form_SpawnSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 448);
            this.Controls.Add(this.label_display);
            this.Controls.Add(this.button_Spawn_Search);
            this.Controls.Add(this.listView_Spawn_Search_Results);
            this.Controls.Add(this.textBoxSpawnSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_SpawnSearch";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Spawn Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSpawnSearch;
        private System.Windows.Forms.ListView listView_Spawn_Search_Results;
        private System.Windows.Forms.ColumnHeader columnHeader__Search_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_Search_Name;
        private System.Windows.Forms.Button button_Spawn_Search;
        private System.Windows.Forms.Label label_display;
        private System.Windows.Forms.ColumnHeader columnHeader_Search_Type;
        private System.Windows.Forms.ColumnHeader columnHeader_Search_Zone;
        private System.Windows.Forms.ColumnHeader columnHeader_Search_Zone_Short;
        private System.Windows.Forms.ColumnHeader columnHeader_Spawn_Name_Long;
    }
}