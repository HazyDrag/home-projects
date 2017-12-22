using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMtransceiver
{
    public partial class WaitForm : Form
    {
        int count = 0;
        public WaitForm(int s)
        {
            InitializeComponent();
            progressBar1.Maximum = s;
            progressBar1.Minimum = 0;

            label1.Text += s + " секунд";
            timer1.Start();
        }

        public WaitForm(int s, string ssid)
        {
            InitializeComponent();
            progressBar1.Maximum = s;
            progressBar1.Minimum = 0;

            label1.Text += s + " секунд\nТочка доступа " + ssid + " заблокирована";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = count;
            if (count == progressBar1.Maximum)
                this.Close();
            count++;
        }
    }
}
