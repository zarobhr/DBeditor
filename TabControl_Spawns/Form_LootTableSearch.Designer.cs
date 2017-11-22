namespace TabControl_Spawns
{
    partial class Form_LootTableSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_LootTableSearch));
            this.listView_LootTable_Search_Results = new System.Windows.Forms.ListView();
            this.columnHeader_LootTable_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_LootTable_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_LootTable_Used = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxLootTableSearch = new System.Windows.Forms.TextBox();
            this.label_display = new System.Windows.Forms.Label();
            this.button_LootTable_Search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_LootTable_Search_Results
            // 
            this.listView_LootTable_Search_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_LootTable_ID,
            this.columnHeader_LootTable_Name,
            this.columnHeader_LootTable_Used});
            this.listView_LootTable_Search_Results.FullRowSelect = true;
            this.listView_LootTable_Search_Results.Location = new System.Drawing.Point(12, 35);
            this.listView_LootTable_Search_Results.MultiSelect = false;
            this.listView_LootTable_Search_Results.Name = "listView_LootTable_Search_Results";
            this.listView_LootTable_Search_Results.Size = new System.Drawing.Size(507, 394);
            this.listView_LootTable_Search_Results.TabIndex = 5;
            this.listView_LootTable_Search_Results.UseCompatibleStateImageBehavior = false;
            this.listView_LootTable_Search_Results.View = System.Windows.Forms.View.Details;
            this.listView_LootTable_Search_Results.SelectedIndexChanged += new System.EventHandler(this.listView_LootTable_Search_Results_SelectedIndexChanged);
            // 
            // columnHeader_LootTable_ID
            // 
            this.columnHeader_LootTable_ID.Text = "ID";
            // 
            // columnHeader_LootTable_Name
            // 
            this.columnHeader_LootTable_Name.Text = "Name";
            this.columnHeader_LootTable_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader_LootTable_Name.Width = 320;
            // 
            // columnHeader_LootTable_Used
            // 
            this.columnHeader_LootTable_Used.Text = "NPC\'s Using";
            this.columnHeader_LootTable_Used.Width = 83;
            // 
            // textBoxLootTableSearch
            // 
            this.textBoxLootTableSearch.Location = new System.Drawing.Point(12, 8);
            this.textBoxLootTableSearch.Name = "textBoxLootTableSearch";
            this.textBoxLootTableSearch.Size = new System.Drawing.Size(478, 20);
            this.textBoxLootTableSearch.TabIndex = 4;
            // 
            // label_display
            // 
            this.label_display.AutoSize = true;
            this.label_display.Location = new System.Drawing.Point(12, 432);
            this.label_display.Name = "label_display";
            this.label_display.Size = new System.Drawing.Size(82, 13);
            this.label_display.TabIndex = 7;
            this.label_display.Text = "Results: 0 Items";
            // 
            // button_LootTable_Search
            // 
            this.button_LootTable_Search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_LootTable_Search.BackgroundImage")));
            this.button_LootTable_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_LootTable_Search.Location = new System.Drawing.Point(496, 6);
            this.button_LootTable_Search.Name = "button_LootTable_Search";
            this.button_LootTable_Search.Size = new System.Drawing.Size(23, 23);
            this.button_LootTable_Search.TabIndex = 6;
            this.button_LootTable_Search.UseVisualStyleBackColor = true;
            this.button_LootTable_Search.Click += new System.EventHandler(this.button_LootTable_Search_Click);
            // 
            // Form_LootTableSearch
            // 
            this.AcceptButton = this.button_LootTable_Search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 452);
            this.Controls.Add(this.label_display);
            this.Controls.Add(this.button_LootTable_Search);
            this.Controls.Add(this.listView_LootTable_Search_Results);
            this.Controls.Add(this.textBoxLootTableSearch);
            this.Name = "Form_LootTableSearch";
            this.Text = "LootTable Lookup";
            this.Load += new System.EventHandler(this.Form_LootTableSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_LootTable_Search;
        private System.Windows.Forms.ListView listView_LootTable_Search_Results;
        private System.Windows.Forms.ColumnHeader columnHeader_LootTable_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_LootTable_Name;
        private System.Windows.Forms.TextBox textBoxLootTableSearch;
        private System.Windows.Forms.ColumnHeader columnHeader_LootTable_Used;
        private System.Windows.Forms.Label label_display;
    }
}