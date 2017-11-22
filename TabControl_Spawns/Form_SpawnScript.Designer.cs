namespace TabControl_Spawns {
    partial class Form_SpawnScript {
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
            this.button_save = new System.Windows.Forms.Button();
            this.label_scriptname = new System.Windows.Forms.Label();
            this.textBox_script = new System.Windows.Forms.RichTextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(12, 499);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label_scriptname
            // 
            this.label_scriptname.AutoSize = true;
            this.label_scriptname.Location = new System.Drawing.Point(12, 9);
            this.label_scriptname.Name = "label_scriptname";
            this.label_scriptname.Size = new System.Drawing.Size(43, 13);
            this.label_scriptname.TabIndex = 4;
            this.label_scriptname.Text = "<none>";
            // 
            // textBox_script
            // 
            this.textBox_script.AcceptsTab = true;
            this.textBox_script.BackColor = System.Drawing.Color.Black;
            this.textBox_script.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_script.DetectUrls = false;
            this.textBox_script.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_script.ForeColor = System.Drawing.Color.White;
            this.textBox_script.Location = new System.Drawing.Point(12, 25);
            this.textBox_script.Name = "textBox_script";
            this.textBox_script.Size = new System.Drawing.Size(1095, 468);
            this.textBox_script.TabIndex = 0;
            this.textBox_script.Text = "";
            this.textBox_script.WordWrap = false;
            this.textBox_script.TextChanged += new System.EventHandler(this.textBox_script_TextChanged);
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(1032, 499);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // Form_SpawnScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 535);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label_scriptname);
            this.Controls.Add(this.textBox_script);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_SpawnScript";
            this.Text = "[spawn script name]";
            this.Load += new System.EventHandler(this.Form_SpawnScript_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_SpawnScript_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label_scriptname;
        private System.Windows.Forms.RichTextBox textBox_script;
        private System.Windows.Forms.Button button_close;
    }
}