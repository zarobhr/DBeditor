namespace EQ2Emu_Database_Editor {
    partial class Form_WhatsNew {
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
            this.textBox_whatsnew = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_whatsnew
            // 
            this.textBox_whatsnew.Location = new System.Drawing.Point(12, 12);
            this.textBox_whatsnew.Multiline = true;
            this.textBox_whatsnew.Name = "textBox_whatsnew";
            this.textBox_whatsnew.ReadOnly = true;
            this.textBox_whatsnew.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_whatsnew.Size = new System.Drawing.Size(594, 447);
            this.textBox_whatsnew.TabIndex = 0;
            this.textBox_whatsnew.WordWrap = false;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(531, 479);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 1;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // Form_WhatsNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 514);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.textBox_whatsnew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_WhatsNew";
            this.Text = "What\'s New";
            this.Load += new System.EventHandler(this.Form_WhatsNew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_whatsnew;
        private System.Windows.Forms.Button button_close;
    }
}