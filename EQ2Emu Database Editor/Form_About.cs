using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EQ2Emu_Database_Editor {
    public partial class Form_About : Form {
        public Form_About() {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("http://www.eq2emulator.net");
        }

        private void button_close_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Form_About_Load(object sender, EventArgs e)
        {

        }
    }
}