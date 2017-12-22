using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MarketManagerLashchenkova
{
    public partial class Registration : Form
    {
        DateTime nowDate = DateTime.Now;

        public Registration()
        {
            InitializeComponent();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
            this.paymentsTableAdapter.Fill(this.marketDBDataSet.Payments);            
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);
        }

        private void Registration_Load(object sender, EventArgs e)
        {            
            tabPage1.Text = "Новый арендатор";
            tabPage2.Text = "Поиск по БД";
            
            FillListboxRenters(); //созданная нами функция см.ниже

        }

        public void FillListboxRenters()
        {
            for (int i = 1; i < rentersDataGridView.Rows.Count - 1; i++)  //добавляем в листбокс данные о существующих записях арендаторов в БД. Разрешение и ФИО. КРОМЕ РАЗОВОГО Арендатора
            {
                string item = rentersDataGridView[0, i].Value.ToString() + "   " + rentersDataGridView[1, i].Value.ToString() + "   " + rentersDataGridView[2, i].Value.ToString();
                listBox1.Items.Add(item);
            }
            //listBox1.SelectedIndex = 0;

            return;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox2.Items.Clear();
            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //получаем данные о сроках оформления объектов рынка
            {
                string a = (string)comboBox1.Items[comboBox1.SelectedIndex]; //Выбранное значение в комбобоксе
                string b = placesDataGridView[1, i].Value.ToString(); //значение из бд (проходит построчно и ищет первое совпадение)
                if (b == a)
                {
                    if (placesDataGridView[5, i].Value.ToString() != "")
                        comboBox2.Items.Add("Месяц");
                    comboBox2.Items.Add("Год");
                    comboBox2.SelectedIndex = 0;
                    break;
                }

            }

            if (comboBox3.SelectedIndex != -1)//костыль для того, чтобы при смене магазина автоматически менялась стоимость на верную
            {
                comboBox3.SelectedIndex = -1;
                comboBox3.SelectedIndex = 0;
            }

            comboBox2.Enabled = true;
        }

        private void placesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.placesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //получаем данные о стоимости объектов рынка на конкретный срок
            {
                string a = (string)comboBox1.Items[comboBox1.SelectedIndex]; //опять ищем нужную нам строку с объектом
                string b = placesDataGridView[1, i].Value.ToString();
                if (b == a)
                {
                    if (comboBox2.SelectedItem.ToString() == "Месяц")
                        textBox1.Text = placesDataGridView[5, i].Value.ToString();
                    if (comboBox2.SelectedItem.ToString() == "Год")
                        textBox1.Text = placesDataGridView[6, i].Value.ToString();
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" )
            {
                int b = rentersDataGridView.Rows.Count;
                int renterID = (int)rentersDataGridView[0, b - 2].Value + 1; //находим следующий id клиента                
                
                

                DialogResult result = MessageBox.Show("\n Номер разрешения: " + textBox2.Text + "\n ФИО: " + textBox3.Text + "\n Адрес: " + textBox4.Text + "\n Профиль: " + textBox5.Text + "\n Дата: " + nowDate + "\n\n Значения верны?", "Проверьте правильность введенных данных!", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    rentersTableAdapter.Insert(renterID, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, nowDate); //Вставляем значения в новую строку таблицы
                    this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
                    MessageBox.Show("Арендатор " + textBox3.Text + " успешно занесен в БД");

                    RegistrationFunc(renterID);
                }
            }
            else
                MessageBox.Show("Одно из полей не заполнено");
        }

        public void RegistrationFunc(int _renterID)
        {
            int placeID = 0;
            for (int i = 0; i < placesDataGridView.Rows.Count - 1; i++)//находим ID выбранного объекта рынка
                if (placesDataGridView[1, i].Value.ToString() == (string)comboBox1.Items[comboBox1.SelectedIndex])
                    placeID = (int)placesDataGridView[0, i].Value;

            int rentID = 0;
            foreach (DataRow dr in marketDBDataSet.Rents.Rows) //находим максимальное ID аренды
            {
                int crentID = dr.Field<int>("RentID");
                rentID = Math.Max(rentID, crentID);
            }
            rentID += 1;

            //найдем количество дней в этом месяце и количество дней в этом году
            int yearNow = nowDate.Year;//год
            int monthNow = nowDate.Month;//месяц
            int daysOfRent = 0;
            if (comboBox2.Visible && comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Месяц") //если выбрано на месяц, то оформляем на количество дней, какое в этом месяце
                daysOfRent = DateTime.DaysInMonth(yearNow, monthNow);
            else
            {
                if (DateTime.IsLeapYear(yearNow)) //если сделка на год, проверяем, високосный ли он и оформляем на соответствующее количество дней
                    daysOfRent = 366;
                else
                    daysOfRent = 365;
            }

            if (comboBox3.Visible)//Если включен этот комбобокс, значит окно было вызвано через кнопку оформления разовой сделки, значит оформляем на 1 день
             daysOfRent = 1;

            //MessageBox.Show("Номер сделки: " + rentID + "\nID объекта: " + placeID + "\nВремя аренды(д): " + daysOfRent + "\nДень оформления: " + nowDate + "\nАрендатор ID: " + _renterID + "\nСтоимость: " + Convert.ToInt32(textBox1.Text));

            //После создания записи информации о Арендаторе мы заносим данные в таблицу с текущими договорами об аренде
            rentsTableAdapter.Insert(rentID, placeID, nowDate, daysOfRent, _renterID, Convert.ToDouble(textBox1.Text));
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);

            //заносим запись в таблицу с платежами
            paymentsTableAdapter.Insert(null, rentID);
            this.paymentsTableAdapter.Fill(this.marketDBDataSet.Payments);

            //изменяем статус арендованного места на true  
            DataRow[] placesRow =
                marketDBDataSet.Tables["Places"].Select("PlaceID = '" + placeID + "'");
            placesRow[0]["Status"] = true;
            placesTableAdapter.Update(this.marketDBDataSet.Places);

            MessageBox.Show("Ваш объект: " + (string)comboBox1.Items[comboBox1.SelectedIndex] + "\nНа срок: " + daysOfRent + "\nОплата составит: " + Convert.ToInt32(textBox1.Text), "Сделка успешно оформлена!");

            this.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();//удаляем арендаторов из listbox, так как после предыдущей фильтрации их осталась только часть
            FillListboxRenters();//снова добавляем всех арендаторов в список, чтобы потом отсеять не попадающих под условия поиска

            for (int i = listBox1.Items.Count-1; i >= 0; i--) //двигаемся с нижней строки, чтобы при удалении строки, у оставшихся не менялся индекс
                if (!listBox1.Items[i].ToString().Contains(textBox6.Text)) //Если строка из listbox не содержит в себе строки из textbox'a поиска, удаляем ее из listbox. 
                    listBox1.Items.RemoveAt(i);   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && textBox1.Text!="")
            {
                string itemListbox = listBox1.Items[listBox1.SelectedIndex].ToString(); //получаем данные о выбранном Арендаторе
                int renterID = Convert.ToInt32(itemListbox.Substring(0, itemListbox.IndexOf(" "))); //получаем Id из строки с данными арендатора

                RegistrationFunc(renterID);
            }
            else
                MessageBox.Show("Выберите арендатора из списка и выберите необходимый объект");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //получаем данные о стоимости объектов рынка на день
            {
                string a = (string)comboBox1.Items[comboBox1.SelectedIndex]; 
                string b = placesDataGridView[1, i].Value.ToString();
                if (b == a)//ищем нужную нам строку с объектом
                {
                    textBox1.Text = placesDataGridView[4, i].Value.ToString();
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int renterID = 1;
            RegistrationFunc(renterID);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            map.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return; //все такие функции для ограничения ввода букв в текстбоксы. Защита от "альтернативно одаренных" пользователей
            else
                e.Handled = true;
        }
    }
}

