namespace TabControl_Spawns
{
    partial class Form_SpellSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SpellSearch));
            this.listView_spell_search_results = new System.Windows.Forms.ListView();
            this.columnHeader_Item_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Item_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_display = new System.Windows.Forms.Label();
            this.textBox_spell_search = new System.Windows.Forms.TextBox();
            this.button_spell_search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_spell_search_results
            // 
            this.listView_spell_search_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Item_ID,
            this.columnHeader_Item_Name});
            this.listView_spell_search_results.FullRowSelect = true;
            this.listView_spell_search_results.Location = new System.Drawing.Point(12, 37);
            this.listView_spell_search_results.MultiSelect = false;
            this.listView_spell_search_results.Name = "listView_spell_search_results";
            this.listView_spell_search_results.Size = new System.Drawing.Size(507, 394);
            this.listView_spell_search_results.TabIndex = 13;
            this.listView_spell_search_results.UseCompatibleStateImageBehavior = false;
            this.listView_spell_search_results.View = System.Windows.Forms.View.Details;
            this.listView_spell_search_results.DoubleClick += new System.EventHandler(this.listView_spell_search_results_DoubleClick);
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
            this.label_display.Location = new System.Drawing.Point(12, 434);
            this.label_display.Name = "label_display";
            this.label_display.Size = new System.Drawing.Size(82, 13);
            this.label_display.TabIndex = 15;
            this.label_display.Text = "Results: 0 Items";
            // 
            // textBox_spell_search
            // 
            this.textBox_spell_search.Location = new System.Drawing.Point(12, 10);
            this.textBox_spell_search.Name = "textBox_spell_search";
            this.textBox_spell_search.Size = new System.Drawing.Size(478, 20);
            this.textBox_spell_search.TabIndex = 12;
            // 
            // button_spell_search
            // 
            this.button_spell_search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_spell_search.BackgroundImage")));
            this.button_spell_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_spell_search.Location = new System.Drawing.Point(496, 8);
            this.button_spell_search.Name = "button_spell_search";
            this.button_spell_search.Size = new System.Drawing.Size(23, 23);
            this.button_spell_search.TabIndex = 14;
            this.button_spell_search.UseVisualStyleBackColor = true;
            this.button_spell_search.Click += new System.EventHandler(this.button_Spell_Search_Click);
            // 
            // Form_SpellSearch
            // 
            this.AcceptButton = this.button_spell_search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 458);
            this.Controls.Add(this.listView_spell_search_results);
            this.Controls.Add(this.label_display);
            this.Controls.Add(this.button_spell_search);
            this.Controls.Add(this.textBox_spell_search);
            this.Name = "Form_SpellSearch";
            this.Text = "Spell Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_spell_search_results;
        private System.Windows.Forms.ColumnHeader columnHeader_Item_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_Item_Name;
        private System.Windows.Forms.Label label_display;
        private System.Windows.Forms.Button button_spell_search;
        private System.Windows.Forms.TextBox textBox_spell_search;
    }
}