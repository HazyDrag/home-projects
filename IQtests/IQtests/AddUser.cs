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
    public partial class AddUser : Form
    {
        public AddUser(string name, string surname, string patron, string date)
        {
            InitializeComponent();
            nameBox.Text = name;
            surnameBox.Text = surname;
            patronBox.Text = patron;
            dateTimePicker1.Value = Convert.ToDateTime(date);
        }

        public AddUser()
        {
            InitializeComponent();
        }        
    }
}
