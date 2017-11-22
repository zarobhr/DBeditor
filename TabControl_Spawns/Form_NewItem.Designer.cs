namespace TabControl_Spawns {
    partial class Form_NewItem {
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
            this.button_ok = new System.Windows.Forms.Button();
            this.textBox_itemname = new System.Windows.Forms.TextBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_itemtype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_loaditem = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Name";
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(212, 156);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // textBox_itemname
            // 
            this.textBox_itemname.Location = new System.Drawing.Point(12, 65);
            this.textBox_itemname.MaxLength = 100;
            this.textBox_itemname.Name = "textBox_itemname";
            this.textBox_itemname.Size = new System.Drawing.Size(356, 20);
            this.textBox_itemname.TabIndex = 0;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(293, 156);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Creating an item will insert a blank record into the database.  You must go\r\nthro" +
                "ugh and insert the necessary fields.";
            // 
            // comboBox_itemtype
            // 
            this.comboBox_itemtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_itemtype.FormattingEnabled = true;
            this.comboBox_itemtype.Location = new System.Drawing.Point(12, 104);
            this.comboBox_itemtype.MaxDropDownItems = 20;
            this.comboBox_itemtype.Name = "comboBox_itemtype";
            this.comboBox_itemtype.Size = new System.Drawing.Size(117, 21);
            this.comboBox_itemtype.Sorted = true;
            this.comboBox_itemtype.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Item Type";
            // 
            // checkBox_loaditem
            // 
            this.checkBox_loaditem.AutoSize = true;
            this.checkBox_loaditem.Checked = true;
            this.checkBox_loaditem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_loaditem.Location = new System.Drawing.Point(230, 108);
            this.checkBox_loaditem.Name = "checkBox_loaditem";
            this.checkBox_loaditem.Size = new System.Drawing.Size(138, 17);
            this.checkBox_loaditem.TabIndex = 18;
            this.checkBox_loaditem.TabStop = false;
            this.checkBox_loaditem.Text = "Load item after insertion";
            this.checkBox_loaditem.UseVisualStyleBackColor = true;
            // 
            // Form_NewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 191);
            this.Controls.Add(this.checkBox_loaditem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_itemtype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.textBox_itemname);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_NewItem";
            this.Text = "Create a New Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.TextBox textBox_itemname;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_itemtype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_loaditem;
    }
}