using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DocumentationLashchenkov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Hide();
            EditLS newedit = new EditLS();
            newedit.ShowDialog();
            Show();
            //Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();

            Hired newhired = new Hired();
            newhired.ShowDialog();
            
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();

            Vosp vsp = new Vosp();
            vsp.ShowDialog();

            Close();
        }
    }
}
