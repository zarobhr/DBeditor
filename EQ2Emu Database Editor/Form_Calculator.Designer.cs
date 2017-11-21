namespace EQ2Emu_Database_Editor {
    partial class Form_Calculator {
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.textBox_iconfilenumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_iconnumberinfile = new System.Windows.Forms.TextBox();
            this.button_calculate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_iconnumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Type";
            // 
            // comboBox_type
            // 
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "Item/Quest Step"});
            this.comboBox_type.Location = new System.Drawing.Point(12, 25);
            this.comboBox_type.MaxDropDownItems = 20;
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(181, 21);
            this.comboBox_type.Sorted = true;
            this.comboBox_type.TabIndex = 1;
            this.comboBox_type.TabStop = false;
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_SelectedIndexChanged);
            // 
            // textBox_iconfilenumber
            // 
            this.textBox_iconfilenumber.Location = new System.Drawing.Point(12, 76);
            this.textBox_iconfilenumber.Name = "textBox_iconfilenumber";
            this.textBox_iconfilenumber.Size = new System.Drawing.Size(100, 20);
            this.textBox_iconfilenumber.TabIndex = 0;
            this.textBox_iconfilenumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Icon file #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Icon # in file";
            // 
            // textBox_iconnumberinfile
            // 
            this.textBox_iconnumberinfile.Location = new System.Drawing.Point(118, 76);
            this.textBox_iconnumberinfile.Name = "textBox_iconnumberinfile";
            this.textBox_iconnumberinfile.Size = new System.Drawing.Size(100, 20);
            this.textBox_iconnumberinfile.TabIndex = 1;
            this.textBox_iconnumberinfile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // button_calculate
            // 
            this.button_calculate.Location = new System.Drawing.Point(224, 73);
            this.button_calculate.Name = "button_calculate";
            this.button_calculate.Size = new System.Drawing.Size(75, 23);
            this.button_calculate.TabIndex = 3;
            this.button_calculate.Text = "Calculate";
            this.button_calculate.UseVisualStyleBackColor = true;
            this.button_calculate.Click += new System.EventHandler(this.button_calculate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Icon #";
            // 
            // textBox_iconnumber
            // 
            this.textBox_iconnumber.Location = new System.Drawing.Point(12, 115);
            this.textBox_iconnumber.Name = "textBox_iconnumber";
            this.textBox_iconnumber.ReadOnly = true;
            this.textBox_iconnumber.Size = new System.Drawing.Size(100, 20);
            this.textBox_iconnumber.TabIndex = 6;
            this.textBox_iconnumber.TabStop = false;
            // 
            // Form_Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 145);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_iconnumber);
            this.Controls.Add(this.button_calculate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_iconnumberinfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_iconfilenumber);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Calculator";
            this.Text = "Calculator [type]";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.TextBox textBox_iconfilenumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_iconnumberinfile;
        private System.Windows.Forms.Button button_calculate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_iconnumber;
    }
}