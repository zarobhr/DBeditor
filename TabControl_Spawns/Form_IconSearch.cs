using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TabControl_Spawns
{
    public partial class Form_IconSearch : Form
    {
        public string ReturnValue { get; set; }

        string icon_type = "";

        public Form_IconSearch(string type)
        {
            icon_type = type;
            InitializeComponent();
        }

        private void Form_ItemIconSearch_Load(object sender, EventArgs e)
        {
            ImageList il = new ImageList();
            DirectoryInfo dir = new DirectoryInfo(@"icons\" + icon_type);
            int j = 0;
            foreach (FileInfo file in dir.GetFiles())
            {
                il.Images.Add(file.Name, Image.FromFile(file.FullName‌));
               
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.Extension);
                item.ImageKey = file.Name;
                item.ImageIndex = j;
                lvItemIcons.Items.Add(item);
                j++;
            }

            lvItemIcons.View = View.LargeIcon;
            il.ImageSize = new Size(32, 32);
            lvItemIcons.LargeImageList = il;
        }

        private void lvItemIcons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int intselectedindex = lvItemIcons.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                this.ReturnValue = lvItemIcons.Items[intselectedindex].Text.Replace(@".jpg", "");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
