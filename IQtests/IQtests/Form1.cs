using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace IQtests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditGroups eg = new EditGroups();
            this.Hide();
            eg.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LkEntry lke = new LkEntry();
            this.Hide();
            lke.ShowDialog();
            this.Show();
        }
    }
}
