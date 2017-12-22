using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentationLashchenkov
{
    public partial class TimeForm : Form
    {
        public TimeForm(int hour, int minutes)
        {
            InitializeComponent();

            numericUpDown1.Value = hour;
            numericUpDown2.Value = minutes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hour = numericUpDown1.Value.ToString();
            string minutes = numericUpDown2.Value.ToString();
            if (minutes.Count() == 1)
                minutes = "0" + minutes;

            ((Hired)this.Owner).thistime = hour + ":" + minutes;

            this.Close();
        }
    }
}
