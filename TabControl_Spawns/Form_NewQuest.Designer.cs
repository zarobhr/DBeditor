namespace TabControl_Spawns
{
    partial class Form_NewQuest
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
            this.button_NewQuestParse = new System.Windows.Forms.Button();
            this.btn_NewQuestCancel = new System.Windows.Forms.Button();
            this.textBox_NewQuestCompletedtext = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_NewQuestLevel = new System.Windows.Forms.TextBox();
            this.textBox_NewQuestZone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_NewQuestEncLevel = new System.Windows.Forms.TextBox();
            this.textBox_NewQuestType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_NewQuestName = new System.Windows.Forms.TextBox();
            this.btn_NewQuestAdd = new System.Windows.Forms.Button();
            this.textBox_newquest_spawnId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_NewQuestLuaScript = new System.Windows.Forms.TextBox();
            this.textBox_NewQuestDescription = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox_newquest_questid = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_NewQuestParse
            // 
            this.button_NewQuestParse.Location = new System.Drawing.Point(12, 503);
            this.button_NewQuestParse.Name = "button_NewQuestParse";
            this.button_NewQuestParse.Size = new System.Drawing.Size(75, 23);
            this.button_NewQuestParse.TabIndex = 122;
            this.button_NewQuestParse.Text = "Parse";
            this.button_NewQuestParse.UseVisualStyleBackColor = true;
            this.button_NewQuestParse.Click += new System.EventHandler(this.button_NewQuestParse_Click);
            // 
            // btn_NewQuestCancel
            // 
            this.btn_NewQuestCancel.Location = new System.Drawing.Point(314, 503);
            this.btn_NewQuestCancel.Name = "btn_NewQuestCancel";
            this.btn_NewQuestCancel.Size = new System.Drawing.Size(75, 23);
            this.btn_NewQuestCancel.TabIndex = 111;
            this.btn_NewQuestCancel.Text = "Cancel";
            this.btn_NewQuestCancel.UseVisualStyleBackColor = true;
            this.btn_NewQuestCancel.Click += new System.EventHandler(this.btn_NewQuestCancel_Click_1);
            // 
            // textBox_NewQuestCompletedtext
            // 
            this.textBox_NewQuestCompletedtext.Location = new System.Drawing.Point(6, 342);
            this.textBox_NewQuestCompletedtext.Multiline = true;
            this.textBox_NewQuestCompletedtext.Name = "textBox_NewQuestCompletedtext";
            this.textBox_NewQuestCompletedtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_NewQuestCompletedtext.Size = new System.Drawing.Size(363, 135);
            this.textBox_NewQuestCompletedtext.TabIndex = 109;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 116;
            this.label4.Text = "Zone:";
            // 
            // textBox_NewQuestLevel
            // 
            this.textBox_NewQuestLevel.Location = new System.Drawing.Point(6, 110);
            this.textBox_NewQuestLevel.Name = "textBox_NewQuestLevel";
            this.textBox_NewQuestLevel.Size = new System.Drawing.Size(117, 20);
            this.textBox_NewQuestLevel.TabIndex = 105;
            this.textBox_NewQuestLevel.Text = "0";
            // 
            // textBox_NewQuestZone
            // 
            this.textBox_NewQuestZone.Location = new System.Drawing.Point(252, 110);
            this.textBox_NewQuestZone.Name = "textBox_NewQuestZone";
            this.textBox_NewQuestZone.Size = new System.Drawing.Size(117, 20);
            this.textBox_NewQuestZone.TabIndex = 104;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 117;
            this.label5.Text = "Level:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 115;
            this.label3.Text = "Type:";
            // 
            // textBox_NewQuestEncLevel
            // 
            this.textBox_NewQuestEncLevel.Location = new System.Drawing.Point(129, 110);
            this.textBox_NewQuestEncLevel.Name = "textBox_NewQuestEncLevel";
            this.textBox_NewQuestEncLevel.Size = new System.Drawing.Size(117, 20);
            this.textBox_NewQuestEncLevel.TabIndex = 106;
            this.textBox_NewQuestEncLevel.Text = "0";
            // 
            // textBox_NewQuestType
            // 
            this.textBox_NewQuestType.Location = new System.Drawing.Point(252, 32);
            this.textBox_NewQuestType.Name = "textBox_NewQuestType";
            this.textBox_NewQuestType.Size = new System.Drawing.Size(117, 20);
            this.textBox_NewQuestType.TabIndex = 103;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(129, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 118;
            this.label6.Text = "Enc Level:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 114;
            this.label7.Text = "Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 119;
            this.label8.Text = "Description:";
            // 
            // textBox_NewQuestName
            // 
            this.textBox_NewQuestName.Location = new System.Drawing.Point(6, 71);
            this.textBox_NewQuestName.Name = "textBox_NewQuestName";
            this.textBox_NewQuestName.Size = new System.Drawing.Size(363, 20);
            this.textBox_NewQuestName.TabIndex = 102;
            // 
            // btn_NewQuestAdd
            // 
            this.btn_NewQuestAdd.Location = new System.Drawing.Point(93, 503);
            this.btn_NewQuestAdd.Name = "btn_NewQuestAdd";
            this.btn_NewQuestAdd.Size = new System.Drawing.Size(75, 23);
            this.btn_NewQuestAdd.TabIndex = 110;
            this.btn_NewQuestAdd.Text = "Add";
            this.btn_NewQuestAdd.UseVisualStyleBackColor = true;
            this.btn_NewQuestAdd.Click += new System.EventHandler(this.btn_NewQuestAdd_Click_1);
            // 
            // textBox_newquest_spawnId
            // 
            this.textBox_newquest_spawnId.Location = new System.Drawing.Point(129, 32);
            this.textBox_newquest_spawnId.Name = "textBox_newquest_spawnId";
            this.textBox_newquest_spawnId.Size = new System.Drawing.Size(117, 20);
            this.textBox_newquest_spawnId.TabIndex = 101;
            this.textBox_newquest_spawnId.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 120;
            this.label9.Text = "Completed Text:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 113;
            this.label10.Text = "Quest ID:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(129, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 112;
            this.label18.Text = "Spawn ID:";
            // 
            // textBox_NewQuestLuaScript
            // 
            this.textBox_NewQuestLuaScript.Location = new System.Drawing.Point(6, 149);
            this.textBox_NewQuestLuaScript.Name = "textBox_NewQuestLuaScript";
            this.textBox_NewQuestLuaScript.Size = new System.Drawing.Size(363, 20);
            this.textBox_NewQuestLuaScript.TabIndex = 107;
            // 
            // textBox_NewQuestDescription
            // 
            this.textBox_NewQuestDescription.Location = new System.Drawing.Point(6, 188);
            this.textBox_NewQuestDescription.Multiline = true;
            this.textBox_NewQuestDescription.Name = "textBox_NewQuestDescription";
            this.textBox_NewQuestDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_NewQuestDescription.Size = new System.Drawing.Size(363, 135);
            this.textBox_NewQuestDescription.TabIndex = 108;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 133);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 13);
            this.label19.TabIndex = 121;
            this.label19.Text = "Lua Script:";
            // 
            // textBox_newquest_questid
            // 
            this.textBox_newquest_questid.Location = new System.Drawing.Point(6, 32);
            this.textBox_newquest_questid.Name = "textBox_newquest_questid";
            this.textBox_newquest_questid.Size = new System.Drawing.Size(117, 20);
            this.textBox_newquest_questid.TabIndex = 100;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox_newquest_questid);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.textBox_NewQuestCompletedtext);
            this.groupBox1.Controls.Add(this.textBox_newquest_spawnId);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox_NewQuestDescription);
            this.groupBox1.Controls.Add(this.textBox_NewQuestName);
            this.groupBox1.Controls.Add(this.textBox_NewQuestZone);
            this.groupBox1.Controls.Add(this.textBox_NewQuestLuaScript);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_NewQuestType);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.textBox_NewQuestLevel);
            this.groupBox1.Controls.Add(this.textBox_NewQuestEncLevel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 485);
            this.groupBox1.TabIndex = 123;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Quest";
            // 
            // Form_NewQuest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 537);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_NewQuestParse);
            this.Controls.Add(this.btn_NewQuestCancel);
            this.Controls.Add(this.btn_NewQuestAdd);
            this.Name = "Form_NewQuest";
            this.Text = "Form_NewQuest";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_NewQuestParse;
        private System.Windows.Forms.Button btn_NewQuestCancel;
        private System.Windows.Forms.TextBox textBox_NewQuestCompletedtext;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_NewQuestLevel;
        private System.Windows.Forms.TextBox textBox_NewQuestZone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_NewQuestEncLevel;
        private System.Windows.Forms.TextBox textBox_NewQuestType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_NewQuestName;
        private System.Windows.Forms.Button btn_NewQuestAdd;
        private System.Windows.Forms.TextBox textBox_newquest_spawnId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_NewQuestLuaScript;
        private System.Windows.Forms.TextBox textBox_NewQuestDescription;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_newquest_questid;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}