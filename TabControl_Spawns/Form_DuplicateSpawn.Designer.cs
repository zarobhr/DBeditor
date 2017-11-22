namespace TabControl_Spawns {
    partial class Form_DuplicateSpawn {
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
            this.label_duplicateinfo = new System.Windows.Forms.Label();
            this.button_duplicate = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_duplicateinfo
            // 
            this.label_duplicateinfo.AutoSize = true;
            this.label_duplicateinfo.Location = new System.Drawing.Point(12, 9);
            this.label_duplicateinfo.Name = "label_duplicateinfo";
            this.label_duplicateinfo.Size = new System.Drawing.Size(195, 13);
            this.label_duplicateinfo.TabIndex = 0;
            this.label_duplicateinfo.Text = "Duplicating <spawn_name> <spawn id>";
            // 
            // button_duplicate
            // 
            this.button_duplicate.Location = new System.Drawing.Point(266, 123);
            this.button_duplicate.Name = "button_duplicate";
            this.button_duplicate.Size = new System.Drawing.Size(75, 23);
            this.button_duplicate.TabIndex = 1;
            this.button_duplicate.Text = "Duplicate";
            this.button_duplicate.UseVisualStyleBackColor = true;
            this.button_duplicate.Click += new System.EventHandler(this.button_duplicate_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(347, 123);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name for new spawn:\r\n(If left blank, name from duplicating spawn will be used)";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(15, 76);
            this.textBox_name.MaxLength = 64;
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(407, 20);
            this.textBox_name.TabIndex = 0;
            // 
            // Form_DuplicateSpawn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 158);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_duplicate);
            this.Controls.Add(this.label_duplicateinfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_DuplicateSpawn";
            this.Text = "Duplicate Spawn";
            this.Load += new System.EventHandler(this.Form_DuplicateSpawn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_duplicateinfo;
        private System.Windows.Forms.Button button_duplicate;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_name;
    }
}