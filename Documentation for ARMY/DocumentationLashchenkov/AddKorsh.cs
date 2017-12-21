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
    public partial class AddKorsh : Form
    {
        public AddKorsh(int id, string rank, string vzvod, string surname, string name, string patron, string surnameR, string number)
        {
            InitializeComponent();
            
            label8.Text = Convert.ToString(id); //записываем в лейбл переданный id
            ChangeKorshun(rank, vzvod, surname, name, patron, surnameR, number); //пользовательская функция
            
        }

        public AddKorsh(int id)
        {
            InitializeComponent();

            label8.Text = Convert.ToString(id); //записываем в лейбл переданный id

            comboBox1.SelectedIndex = 1; //устанавливаем значения по умолчанию для удобства
            comboBox2.SelectedIndex = 0;
            
        }

            private void ChangeKorshun(string rank, string vzvod, string surname, string name, string patron, string surnameR, string number)
        {
            //Данная функция записывает во все элементы старые значения для их изменения
            if (rank == "ефрейтор  ")
                comboBox1.SelectedIndex = 0;
            if (rank == "рядовой   ")
                comboBox1.SelectedIndex = 1;

            comboBox2.SelectedIndex = Convert.ToInt32(vzvod) - 1;
            textBox1.Text = surname;
            textBox2.Text = name;
            textBox3.Text = patron;
            textBox4.Text = surnameR;
            maskedTextBox1.Text = number;
            button1.Text = "Изменить";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && 
                textBox2.Text != "" && 
                 textBox3.Text != "" && 
                  textBox4.Text != "" &&
                   maskedTextBox1.Text != "")
            {
                Hide();

                int mode = 0;

                if (button1.Text == "Добавить")
                    mode = 1;
                else
                    mode = 2;

                EditLS edit = new EditLS(mode, Convert.ToInt32(label8.Text), comboBox2.SelectedIndex + 1,
                                                comboBox1.Text, textBox1.Text, textBox2.Text,
                                                    textBox3.Text, textBox4.Text, maskedTextBox1.Text);
                edit.ShowDialog();
                Close();
            }
        }
    }
}
