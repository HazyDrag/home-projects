using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace MarketManagerLashchenkova
{
    public partial class Dynamic : Form
    {
        public Dynamic()
        {
            InitializeComponent();
        }

        private void rentsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.rentsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void DrawFunc(DateTime startDate, DateTime endDate, string profile)
        {
            chart1.Series[0].Points.Clear();//очищаем график для постройки нового
            DateTime j = startDate;
            while (j <= endDate) //от дата заданного начала до даты конца с шагом в 1 день
            {
                int counter = 0;
                for (int i = 0; i < rentsDataGridView.Rows.Count - 1; i++) //проходим по всем оформленным арендам, и если в эту дату была активная аренда, добавляем к счетчики в этот день
                {
                    DateTime rentStartDate = Convert.ToDateTime(rentsDataGridView[2, i].Value);

                    if (j.CompareTo(rentStartDate) == 1 || j.CompareTo(rentStartDate) == 0)
                    {
                        int daysPlus = Convert.ToInt32(rentsDataGridView[3, i].Value);
                        DateTime rentEndDate = rentStartDate.AddDays(daysPlus);
                        if (j.CompareTo(rentEndDate) == -1 || j.CompareTo(rentEndDate) == 0)
                            if (profile == "Любое") //отфильтровываем по профилю. Если любой, то фильтрации не требуется, если же профиль есть, запускаем функцию фильтрации
                            {
                                counter++;
                            }
                            else
                            {
                                for (int z = 0; z < placesDataGridView.Rows.Count - 1; z++) //если арендный договор попадает в этот промежуток времени, проверяем, на какой объект она оформлена и проверяем его оборудование
                                    if (placesDataGridView[0, z].Value.ToString() == rentsDataGridView[1, i].Value.ToString())
                                        if(placesDataGridView[3,z].Value.ToString() == profile)
                                        {
                                            counter++;
                                        }
                            }
                    }
                }
                chart1.Series[0].Points.AddXY(j, counter); //добавляем точку на график
                j = j.AddDays(1);
            }
        }

        private void Dynamic_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Rents". При необходимости она может быть перемещена или удалена.
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);

            //начало добавления в комбобокс о видах существующих степеней оборудованности. Используем метод прямого запроса
            string connStr2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MarketDB.mdf;Integrated Security=True";//подключаемся
            SqlConnection dbConn2 = new SqlConnection(connStr2);
            string sqlStr2 = @"SELECT Equipment FROM Places";//выбираем столбец и таблицу
            SqlCommand cmd2 = new SqlCommand(sqlStr2, dbConn2);
            dbConn2.Open();
            SqlDataReader rdr2 = cmd2.ExecuteReader();

            string currenteq = null;

            while (rdr2.Read())
            {
                currenteq = rdr2.GetString(0);
                bool click = false;
                for (int i = 0; i < comboBox1.Items.Count; i++)//проверяем, присутствует ли уже такое значение в combobox, если нет, то добавляем
                    if (currenteq == (string)comboBox1.Items[i])
                        click = true;
                if (!click)
                    comboBox1.Items.Add(currenteq);
            }
            comboBox1.SelectedIndex = 0;
            dbConn2.Close();
            //завершили добавление в комбобокс
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawFunc(dateTimePicker1.Value, dateTimePicker2.Value, comboBox1.Text);
        }
    }
}
