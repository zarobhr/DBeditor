using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EQ2Emu_Database_Editor {
    public partial class Form_Calculator : Form {
        public Form_Calculator(string initial_type) {
            InitializeComponent();
            comboBox_type.SelectedItem = initial_type;
        }

        private void Calculate(string type) {
            if (type == "Item/Quest Step")
                CalculateItemQuestStep(type);
        }

        private void CalculateItemQuestStep(string type) {
            try {
                int icon_file_number = Convert.ToInt32(textBox_iconfilenumber.Text);
                int icon_number_in_file = Convert.ToInt32(textBox_iconnumberinfile.Text);
                int icon_number = ((icon_file_number - 1) * 36)+ (icon_number_in_file - 1);
                textBox_iconnumber.Text = icon_number.ToString();
            }
            catch (Exception ex) {
                textBox_iconnumber.Text = "-";
            }
        }

        private void ChangeType(string type) {
            this.Text = "Calculator [" + type + "]";
            Calculate(type);
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e) {
            ChangeType((string)comboBox_type.SelectedItem);
        }


        private void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                button_calculate_Click(null, null);
        }

        private void button_calculate_Click(object sender, EventArgs e) {
            Calculate((string)comboBox_type.SelectedItem);
        }
    }
}