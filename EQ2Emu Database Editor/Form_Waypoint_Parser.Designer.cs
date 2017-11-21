namespace EQ2Emu_Database_Editor
{
    partial class Form_Waypoint_Parser
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_min_base = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_random_high = new System.Windows.Forms.TextBox();
            this.textBox_random_low = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_random_enable = new System.Windows.Forms.CheckBox();
            this.textBox_default_time = new System.Windows.Forms.TextBox();
            this.button_parse = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_walk_speed = new System.Windows.Forms.TextBox();
            this.richTextBox_log_input = new System.Windows.Forms.RichTextBox();
            this.checkBox_loop = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_min_base);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_random_high);
            this.groupBox1.Controls.Add(this.textBox_random_low);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox_random_enable);
            this.groupBox1.Controls.Add(this.textBox_default_time);
            this.groupBox1.Location = new System.Drawing.Point(310, 356);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pause";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Default";
            // 
            // textBox_min_base
            // 
            this.textBox_min_base.Enabled = false;
            this.textBox_min_base.Location = new System.Drawing.Point(91, 51);
            this.textBox_min_base.Name = "textBox_min_base";
            this.textBox_min_base.Size = new System.Drawing.Size(36, 20);
            this.textBox_min_base.TabIndex = 7;
            this.textBox_min_base.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Min";
            // 
            // textBox_random_high
            // 
            this.textBox_random_high.Enabled = false;
            this.textBox_random_high.Location = new System.Drawing.Point(175, 51);
            this.textBox_random_high.Name = "textBox_random_high";
            this.textBox_random_high.Size = new System.Drawing.Size(36, 20);
            this.textBox_random_high.TabIndex = 5;
            this.textBox_random_high.Text = "60";
            this.textBox_random_high.TextChanged += new System.EventHandler(this.textBox_random_high_TextChanged);
            // 
            // textBox_random_low
            // 
            this.textBox_random_low.Enabled = false;
            this.textBox_random_low.Location = new System.Drawing.Point(133, 51);
            this.textBox_random_low.Name = "textBox_random_low";
            this.textBox_random_low.Size = new System.Drawing.Size(36, 20);
            this.textBox_random_low.TabIndex = 4;
            this.textBox_random_low.Text = "30";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "High";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Low";
            // 
            // checkBox_random_enable
            // 
            this.checkBox_random_enable.AutoSize = true;
            this.checkBox_random_enable.Location = new System.Drawing.Point(91, 14);
            this.checkBox_random_enable.Name = "checkBox_random_enable";
            this.checkBox_random_enable.Size = new System.Drawing.Size(102, 17);
            this.checkBox_random_enable.TabIndex = 1;
            this.checkBox_random_enable.Text = "Random Enable";
            this.checkBox_random_enable.UseVisualStyleBackColor = true;
            this.checkBox_random_enable.CheckedChanged += new System.EventHandler(this.checkBox_random_enable_CheckedChanged);
            // 
            // textBox_default_time
            // 
            this.textBox_default_time.Location = new System.Drawing.Point(8, 51);
            this.textBox_default_time.Name = "textBox_default_time";
            this.textBox_default_time.Size = new System.Drawing.Size(65, 20);
            this.textBox_default_time.TabIndex = 0;
            this.textBox_default_time.Text = "0";
            // 
            // button_parse
            // 
            this.button_parse.Location = new System.Drawing.Point(540, 410);
            this.button_parse.Name = "button_parse";
            this.button_parse.Size = new System.Drawing.Size(75, 23);
            this.button_parse.TabIndex = 2;
            this.button_parse.Text = "Parse";
            this.button_parse.UseVisualStyleBackColor = true;
            this.button_parse.Click += new System.EventHandler(this.button_parse_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(621, 410);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Walk Speed";
            // 
            // textBox_walk_speed
            // 
            this.textBox_walk_speed.Location = new System.Drawing.Point(208, 407);
            this.textBox_walk_speed.Name = "textBox_walk_speed";
            this.textBox_walk_speed.Size = new System.Drawing.Size(66, 20);
            this.textBox_walk_speed.TabIndex = 5;
            this.textBox_walk_speed.Text = "1.6";
            // 
            // richTextBox_log_input
            // 
            this.richTextBox_log_input.Location = new System.Drawing.Point(12, 12);
            this.richTextBox_log_input.Name = "richTextBox_log_input";
            this.richTextBox_log_input.Size = new System.Drawing.Size(684, 338);
            this.richTextBox_log_input.TabIndex = 6;
            this.richTextBox_log_input.Text = "";
            // 
            // checkBox_loop
            // 
            this.checkBox_loop.AutoSize = true;
            this.checkBox_loop.Checked = true;
            this.checkBox_loop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_loop.Location = new System.Drawing.Point(208, 365);
            this.checkBox_loop.Name = "checkBox_loop";
            this.checkBox_loop.Size = new System.Drawing.Size(50, 17);
            this.checkBox_loop.TabIndex = 7;
            this.checkBox_loop.Text = "Loop";
            this.checkBox_loop.UseVisualStyleBackColor = true;
            this.checkBox_loop.CheckedChanged += new System.EventHandler(this.checkBox_loop_CheckedChanged);
            // 
            // Form_Waypoint_Parser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 438);
            this.Controls.Add(this.checkBox_loop);
            this.Controls.Add(this.richTextBox_log_input);
            this.Controls.Add(this.textBox_walk_speed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_parse);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Waypoint_Parser";
            this.Text = "Waypoint Parser";
            this.Load += new System.EventHandler(this.Form_Waypoint_Parser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_min_base;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_random_high;
        private System.Windows.Forms.TextBox textBox_random_low;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_random_enable;
        private System.Windows.Forms.TextBox textBox_default_time;
        private System.Windows.Forms.Button button_parse;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_walk_speed;
        private System.Windows.Forms.RichTextBox richTextBox_log_input;
        private System.Windows.Forms.CheckBox checkBox_loop;
    }
}