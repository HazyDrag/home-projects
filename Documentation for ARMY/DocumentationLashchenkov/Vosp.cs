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
    public partial class Vosp : Form
    {
        public Vosp()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now;

            while(true)
            {
                if (dt.DayOfWeek != DayOfWeek.Friday)
                    dt = dt.AddDays(1);
                else
                    break;
            }

            dateTimePicker1.Value = dt;
            dateTimePicker2.Value = dt.AddDays(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"\Данные\Воспитанник";

            if (!File.Exists(path))
                File.Create(path);

            File.AppendAllText(path, dateTimePicker1.Value + "\n");
            File.AppendAllText(path, dateTimePicker2.Value + "\n");

        }

        private void Update()
        {
            string path = @"\Данные\Воспитанник";
            string[] dates = File.ReadAllLines(path);
        }
    }
}
