namespace TabControl_Spawns {
    partial class Form_StateLookup {
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
            this.listView_visualstates = new System.Windows.Forms.ListView();
            this.column_visualstates_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_visualstates_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.label_display = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView_visualstates
            // 
            this.listView_visualstates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_visualstates_name,
            this.column_visualstates_id});
            this.listView_visualstates.FullRowSelect = true;
            this.listView_visualstates.GridLines = true;
            this.listView_visualstates.Location = new System.Drawing.Point(12, 51);
            this.listView_visualstates.Name = "listView_visualstates";
            this.listView_visualstates.Size = new System.Drawing.Size(388, 555);
            this.listView_visualstates.TabIndex = 3;
            this.listView_visualstates.UseCompatibleStateImageBehavior = false;
            this.listView_visualstates.View = System.Windows.Forms.View.Details;
            this.listView_visualstates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_visualstates_MouseDoubleClick);
            // 
            // column_visualstates_name
            // 
            this.column_visualstates_name.Text = "Name";
            this.column_visualstates_name.Width = 282;
            // 
            // column_visualstates_id
            // 
            this.column_visualstates_id.Text = "State ID";
            this.column_visualstates_id.Width = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search";
            // 
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(12, 25);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(388, 20);
            this.textBox_search.TabIndex = 5;
            this.textBox_search.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
            // 
            // label_display
            // 
            this.label_display.AutoSize = true;
            this.label_display.Location = new System.Drawing.Point(12, 609);
            this.label_display.Name = "label_display";
            this.label_display.Size = new System.Drawing.Size(128, 13);
            this.label_display.TabIndex = 7;
            this.label_display.Text = "Displaying 0 visual states.";
            // 
            // Form_StateLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 625);
            this.Controls.Add(this.label_display);
            this.Controls.Add(this.textBox_search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView_visualstates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_StateLookup";
            this.Text = "State Lookup";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_StateLookup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_visualstates;
        private System.Windows.Forms.ColumnHeader column_visualstates_id;
        private System.Windows.Forms.ColumnHeader column_visualstates_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Label label_display;
    }
}