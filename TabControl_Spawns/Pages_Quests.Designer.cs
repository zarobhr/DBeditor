namespace TabControl_Spawns
{
    partial class Pages_Quests
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_select_zone = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_select_quest = new System.Windows.Forms.ComboBox();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_questregister = new System.Windows.Forms.TabPage();
            this.tabPage_questdetails = new System.Windows.Forms.TabPage();
            this.button_close = new System.Windows.Forms.Button();
            this.tabControl_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pick A Zone";
            // 
            // comboBox_select_zone
            // 
            this.comboBox_select_zone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select_zone.FormattingEnabled = true;
            this.comboBox_select_zone.Location = new System.Drawing.Point(7, 41);
            this.comboBox_select_zone.MaxDropDownItems = 20;
            this.comboBox_select_zone.Name = "comboBox_select_zone";
            this.comboBox_select_zone.Size = new System.Drawing.Size(369, 21);
            this.comboBox_select_zone.Sorted = true;
            this.comboBox_select_zone.TabIndex = 17;
            this.comboBox_select_zone.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Pick A Quest";
            // 
            // comboBox_select_quest
            // 
            this.comboBox_select_quest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select_quest.FormattingEnabled = true;
            this.comboBox_select_quest.Location = new System.Drawing.Point(382, 41);
            this.comboBox_select_quest.MaxDropDownItems = 50;
            this.comboBox_select_quest.Name = "comboBox_select_quest";
            this.comboBox_select_quest.Size = new System.Drawing.Size(341, 21);
            this.comboBox_select_quest.Sorted = true;
            this.comboBox_select_quest.TabIndex = 19;
            this.comboBox_select_quest.TabStop = false;
            this.comboBox_select_quest.Visible = false;
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.tabPage_questregister);
            this.tabControl_main.Controls.Add(this.tabPage_questdetails);
            this.tabControl_main.Location = new System.Drawing.Point(3, 94);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(1123, 593);
            this.tabControl_main.TabIndex = 22;
            this.tabControl_main.Visible = false;
            // 
            // tabPage_questregister
            // 
            this.tabPage_questregister.Location = new System.Drawing.Point(4, 22);
            this.tabPage_questregister.Name = "tabPage_questregister";
            this.tabPage_questregister.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_questregister.Size = new System.Drawing.Size(1115, 567);
            this.tabPage_questregister.TabIndex = 0;
            this.tabPage_questregister.Text = "Register";
            this.tabPage_questregister.UseVisualStyleBackColor = true;
            // 
            // tabPage_questdetails
            // 
            this.tabPage_questdetails.Location = new System.Drawing.Point(4, 22);
            this.tabPage_questdetails.Name = "tabPage_questdetails";
            this.tabPage_questdetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_questdetails.Size = new System.Drawing.Size(1115, 567);
            this.tabPage_questdetails.TabIndex = 1;
            this.tabPage_questdetails.Text = "Details";
            this.tabPage_questdetails.UseVisualStyleBackColor = true;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(1047, 41);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 30;
            this.button_close.TabStop = false;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // Pages_Quests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.tabControl_main);
            this.Controls.Add(this.comboBox_select_quest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_select_zone);
            this.Controls.Add(this.label1);
            this.Name = "Pages_Quests";
            this.Size = new System.Drawing.Size(1132, 699);
            this.Load += new System.EventHandler(this.pages_Quest_Load);
            this.tabControl_main.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_select_zone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_select_quest;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_questregister;
        private System.Windows.Forms.TabPage tabPage_questdetails;
        private System.Windows.Forms.Button button_close;
    }
}
