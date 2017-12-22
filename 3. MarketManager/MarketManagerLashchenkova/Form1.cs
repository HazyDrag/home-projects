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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public void CheckRentsDate()  //Освобождаем все объекты и занимаем только те, на которые контракты еще действуют
        {
            for (int j = 1; j < placesDataGridView.Rows.Count; j++)
                {
                    DataRow[] placesRow =
                        marketDBDataSet.Tables["Places"].Select("PlaceID = '" + j.ToString() + "'"); //меняем статус объекта на занят
                    placesRow[0]["Status"] = false;
                    placesTableAdapter.Update(this.marketDBDataSet.Places);
                }

            
            for (int i = 0; i < rentsDataGridView.Rows.Count - 1; i++)
            {
                DateTime expDate = Convert.ToDateTime(rentsDataGridView[2, i].Value).AddDays(Convert.ToDouble(rentsDataGridView[3, i].Value)); //проверка, закончился ли срок аренды
                if(expDate.CompareTo(DateTime.Now) == 1)
                {
                    for (int j = 0; j < placesDataGridView.Rows.Count -1; j++)
                        if(rentsDataGridView[1, i].Value.ToString() == placesDataGridView[0,j].Value.ToString())
                        {
                            DataRow[] placesRow =
                                marketDBDataSet.Tables["Places"].Select("PlaceID = '" + rentsDataGridView[1, i].Value.ToString() + "'"); //меняем статус объекта на занят
                            placesRow[0]["Status"] = true;
                            placesTableAdapter.Update(this.marketDBDataSet.Places);
                            break;
                        }
                }
            }
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.RentersArchive". При необходимости она может быть перемещена или удалена.
            this.rentersArchiveTableAdapter.Fill(this.marketDBDataSet.RentersArchive);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.RentersArchive". При необходимости она может быть перемещена или удалена.
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);

            for (int i = 0; i < rentersArchiveDataGridView.Rows.Count - 1; i++)//определяем количество дней, которые хранятся данные в архиве
            {
                DateTime dateNow = DateTime.Now;
                DateTime dateRenter = Convert.ToDateTime(rentersArchiveDataGridView[5, i].Value);
                TimeSpan ts = dateNow.Subtract(dateRenter);

                string tss = ts.ToString().Substring(0, ts.ToString().IndexOf("."));
                int tsi = Convert.ToInt32(tss);
                int year = 366;
                
                if(tsi > year)
                {
                    rentersArchiveTableAdapter.Delete((int)rentersArchiveDataGridView[0, i].Value, Convert.ToDateTime(rentersArchiveDataGridView[5, i].Value));//удаляем запись которая хранится больше бога
                    //MessageBox.Show("Удален " + rentersArchiveDataGridView[2, i].Value.ToString());
                }
            }
            CheckRentsDate();

            this.rentersArchiveTableAdapter.Fill(this.marketDBDataSet.RentersArchive);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateRent createRent = new CreateRent();
            createRent.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Info inf = new Info();
            inf.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddPlace addplc = new AddPlace();
            addplc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateData upddt = new UpdateData();
            upddt.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PayForm pfrm = new PayForm();
            pfrm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Money moneyfrm = new Money();
            moneyfrm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dynamic dnm = new Dynamic();
            dnm.Show();
        }
    }
}
