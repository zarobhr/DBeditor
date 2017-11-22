namespace TabControl_Spawns
{
    partial class Form_IconSearch
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
            this.lvItemIcons = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvItemIcons
            // 
            this.lvItemIcons.Location = new System.Drawing.Point(9, 10);
            this.lvItemIcons.Margin = new System.Windows.Forms.Padding(2);
            this.lvItemIcons.Name = "lvItemIcons";
            this.lvItemIcons.Size = new System.Drawing.Size(562, 499);
            this.lvItemIcons.TabIndex = 0;
            this.lvItemIcons.UseCompatibleStateImageBehavior = false;
            this.lvItemIcons.View = System.Windows.Forms.View.SmallIcon;
            this.lvItemIcons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvItemIcons_MouseDoubleClick);
            // 
            // Form_IconSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 517);
            this.Controls.Add(this.lvItemIcons);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_IconSearch";
            this.Text = "Icon Chooser";
            this.Load += new System.EventHandler(this.Form_ItemIconSearch_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvItemIcons;
    }
}