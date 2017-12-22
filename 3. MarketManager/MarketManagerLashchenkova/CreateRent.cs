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
    public partial class CreateRent : Form
    {

        public CreateRent()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool state1 = checkBox1.Checked;     //подготавливаем данные для передачи
            bool state2 = checkBox2.Checked;
            bool state3 = checkBox3.Checked;
            bool state4 = checkBox4.Checked;

            int areamin = Convert.ToInt32(numericUpDown1.Value);
            int areamax = Convert.ToInt32(numericUpDown2.Value);

            string equip = (string)comboBox1.Items[comboBox1.SelectedIndex];

            ResultFilter.state1 = state1; //Передаем данные на новую форму
            ResultFilter.state2 = state2;
            ResultFilter.state3 = state3;
            ResultFilter.state4 = state4;

            ResultFilter.areamin = areamin;
            ResultFilter.areamax = areamax;

            ResultFilter.equip = equip;

            ResultFilter resultFilter = new ResultFilter(); //открываем другую форму
            resultFilter.Show();

            this.Close();
        }
        

        private void CreateRent_Load(object sender, EventArgs e)
        {            
            //подключаемся к БД
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MarketDB.mdf;Integrated Security=True";
            SqlConnection dbConn = new SqlConnection(connStr);
            string sqlStr = @"SELECT Area FROM Places"; //выбираем столбец и таблицу
            SqlCommand cmd = new SqlCommand(sqlStr, dbConn);
            dbConn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(); 

            int current = 0;
            int maxArea = 0;
            while (rdr.Read()) //получаем данные из запущенного ридера и находим максимальное значение из этого столбца
            {
                current = rdr.GetInt32(0);
                if (current > maxArea)
                    maxArea = current;
            }
            dbConn.Close();
            numericUpDown2.Maximum = maxArea; //назначаем это максимальное значение верхней границей возможных изменений
            numericUpDown1.Maximum = maxArea;
            numericUpDown2.Value = maxArea;

            //начало добавления в комбобокс о видах существующих степеней оборудованности. Используем метод прямого запроса
            string connStr2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MarketDB.mdf;Integrated Security=True";//подключаемся
            SqlConnection dbConn2 = new SqlConnection(connStr2);
            string sqlStr2 = @"SELECT Equipment FROM Places";//выбираем столбец и таблицу
            SqlCommand cmd2 = new SqlCommand(sqlStr2, dbConn2);
            dbConn2.Open();
            SqlDataReader rdr2 = cmd2.ExecuteReader();
            
            string currenteq = null;
            comboBox1.Items.Add("Любая");

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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) //для того, чтобы нижняя граница размеров площади всегда была меньше или равна верхней заданной границе поиска
        {
            if(numericUpDown1.Value > numericUpDown2.Value)
            {
                numericUpDown1.Value = numericUpDown2.Value;
            }
        }
    }
}
