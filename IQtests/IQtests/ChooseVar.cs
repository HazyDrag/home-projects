using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IQtests
{
    public partial class ChooseVar : Form
    {
        public ChooseVar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string variant = "";
            if (radioButton1.Checked)
                variant = "A";
            else
                variant = "B";
            LearnIP lip = new LearnIP(variant,"1","");
            this.Hide();
            lip.ShowDialog();
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
