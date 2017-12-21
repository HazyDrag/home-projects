using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketManagerLashchenkova
{
    public partial class AddPlace : Form
    {
        public AddPlace()
        {
            InitializeComponent();
        }

        private void placesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.placesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void AddPlace_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);

            comboBox1.SelectedIndex = 0;
            //Добавляем в комбобоксы существующие типы оборудованности и существующие объекты
            for (int i = 0; i < placesDataGridView.Rows.Count - 1; i++)
            {
                comboBox3.Items.Add(placesDataGridView[1, i].Value.ToString());//добавляем названия существующих объектов

                string currenteq = placesDataGridView[3, i].Value.ToString();
                bool click = false;
                for (int j = 0; j < comboBox2.Items.Count; j++)//проверяем, присутствует ли уже такое значение в combobox, если нет, то добавляем
                    if (currenteq == (string)comboBox2.Items[j])
                        click = true;
                if (!click)
                {
                    comboBox2.Items.Add(currenteq);
                    comboBox4.Items.Add(currenteq);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                // получаем следующий PlaceID                
                int placeID = (int)placesDataGridView[0, placesDataGridView.Rows.Count - 2].Value + 1;

                // Определяем расположение нового объекта. по типу места выбираем зону и назначаем следующее порядковое число этой зоны
                int counter = 1; //1, потому что у нас уже есть один объект этой зоны- тот, что мы добавляем
                for (int i = 0; i < placesDataGridView.Rows.Count - 1; i++)
                    if ((int)comboBox1.SelectedIndex + 1 == (int)placesDataGridView[8, i].Value)
                        counter++;

                //Определяем букву зоны и добавляем порядковый номер
                string[] locarray = { "А", "Б", "В", "Г" };
                string loc = locarray[comboBox1.SelectedIndex] + counter.ToString();

                int area = Convert.ToInt32(textBox1.Text);//Площадь
                string equip = comboBox2.Text;//Оборудование
                double dayCost = Convert.ToDouble(textBox2.Text); //стоимость дня и года
                double yearCost = Convert.ToDouble(textBox4.Text);
                double? monthCost = null;

                if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 3)
                    monthCost = Convert.ToDouble(textBox3.Text);

                DialogResult dlgrslt = MessageBox.Show("PlaceID: " + placeID + "\nРасположение: " + loc + "\nПлощадь: " + area + "\nОборудование: " + equip + "\nСтоимость день/месяц(если есть)/год: " + dayCost + "/" + monthCost + "/" + yearCost, "Данные верны?", MessageBoxButtons.OKCancel);
                if (dlgrslt == DialogResult.OK)
                {
                    placesTableAdapter.Insert(placeID, loc, area, equip, dayCost, monthCost, yearCost, false, comboBox1.SelectedIndex + 1);//Добавляем в БД
                    comboBox3.Items.Add(loc);
                }
            }
            else
                MessageBox.Show("Введите все данные");

            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 2)
            {
                textBox3.Text = "Недоступно для этого вида объекта";
                textBox3.Enabled = false;
            }
            else
            {
                textBox3.Text = "";
                textBox3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "" && textBox5.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox6.Text != "")
            {
                //изменяем параметры объекта
                string loc = comboBox3.Text;
                DataRow[] placesRow =
                    marketDBDataSet.Tables["Places"].Select("Location = '" + loc + "'");
                placesRow[0]["Area"] = textBox8.Text;
                placesRow[0]["Equipment"] = comboBox4.Text;
                placesRow[0]["DayCost"] = textBox7.Text;
                placesRow[0]["YearCost"] = textBox5.Text;
                placesRow[0]["MounthCost"] = DBNull.Value;

                if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 3)
                    placesRow[0]["MounthCost"] = Convert.ToDouble(textBox6.Text);

                placesTableAdapter.Update(this.marketDBDataSet.Places);
            }
            else
                MessageBox.Show("Введите все данные");

            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            comboBox3.SelectedIndex = 0;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //заносим в формы значения выбранного объекта
                if (placesDataGridView[1, i].Value.ToString() == comboBox3.Items[comboBox3.SelectedIndex].ToString())
                {
                    comboBox4.Text = placesDataGridView[3, i].Value.ToString();
                    textBox8.Text = placesDataGridView[2, i].Value.ToString();
                    textBox7.Text = placesDataGridView[4, i].Value.ToString();
                    if (comboBox3.Items[comboBox3.SelectedIndex].ToString().Contains("Б") || comboBox3.Items[comboBox3.SelectedIndex].ToString().Contains("В"))
                    {
                        textBox6.Text = "Недоступно для этого вида объекта";
                        textBox6.Enabled = false;
                    }
                    else
                    {
                        textBox6.Text = placesDataGridView[5, i].Value.ToString();
                        textBox6.Enabled = true;
                    }
                    textBox5.Text = placesDataGridView[6, i].Value.ToString();
                    break;
                }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }
    }
}
