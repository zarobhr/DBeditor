namespace TabControl_Spawns
{
    partial class Form_ItemSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ItemSearch));
            this.listView_Items_Search_Results = new System.Windows.Forms.ListView();
            this.columnHeader_Item_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Item_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_display = new System.Windows.Forms.Label();
            this.textBoxItemSearch = new System.Windows.Forms.TextBox();
            this.button_Item_Search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_Items_Search_Results
            // 
            this.listView_Items_Search_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Item_ID,
            this.columnHeader_Item_Name});
            this.listView_Items_Search_Results.FullRowSelect = true;
            this.listView_Items_Search_Results.Location = new System.Drawing.Point(12, 40);
            this.listView_Items_Search_Results.MultiSelect = false;
            this.listView_Items_Search_Results.Name = "listView_Items_Search_Results";
            this.listView_Items_Search_Results.Size = new System.Drawing.Size(507, 394);
            this.listView_Items_Search_Results.TabIndex = 9;
            this.listView_Items_Search_Results.UseCompatibleStateImageBehavior = false;
            this.listView_Items_Search_Results.View = System.Windows.Forms.View.Details;
            this.listView_Items_Search_Results.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Items_Search_Results_MouseDoubleClick);
            // 
            // columnHeader_Item_ID
            // 
            this.columnHeader_Item_ID.Text = "ID";
            // 
            // columnHeader_Item_Name
            // 
            this.columnHeader_Item_Name.Text = "Name";
            this.columnHeader_Item_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader_Item_Name.Width = 416;
            // 
            // label_display
            // 
            this.label_display.AutoSize = true;
            this.label_display.Location = new System.Drawing.Point(12, 437);
            this.label_display.Name = "label_display";
            this.label_display.Size = new System.Drawing.Size(82, 13);
            this.label_display.TabIndex = 11;
            this.label_display.Text = "Results: 0 Items";
            // 
            // textBoxItemSearch
            // 
            this.textBoxItemSearch.Location = new System.Drawing.Point(12, 13);
            this.textBoxItemSearch.Name = "textBoxItemSearch";
            this.textBoxItemSearch.Size = new System.Drawing.Size(478, 20);
            this.textBoxItemSearch.TabIndex = 8;
            // 
            // button_Item_Search
            // 
            this.button_Item_Search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_Item_Search.BackgroundImage")));
            this.button_Item_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Item_Search.Location = new System.Drawing.Point(496, 11);
            this.button_Item_Search.Name = "button_Item_Search";
            this.button_Item_Search.Size = new System.Drawing.Size(23, 23);
            this.button_Item_Search.TabIndex = 10;
            this.button_Item_Search.UseVisualStyleBackColor = true;
            this.button_Item_Search.Click += new System.EventHandler(this.button_Item_Search_Click);
            // 
            // Form_ItemSearch
            // 
            this.AcceptButton = this.button_Item_Search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 456);
            this.Controls.Add(this.listView_Items_Search_Results);
            this.Controls.Add(this.label_display);
            this.Controls.Add(this.button_Item_Search);
            this.Controls.Add(this.textBoxItemSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_ItemSearch";
            this.Text = "Item Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_Items_Search_Results;
        private System.Windows.Forms.ColumnHeader columnHeader_Item_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_Item_Name;
        private System.Windows.Forms.Label label_display;
        private System.Windows.Forms.Button button_Item_Search;
        private System.Windows.Forms.TextBox textBoxItemSearch;
    }
}