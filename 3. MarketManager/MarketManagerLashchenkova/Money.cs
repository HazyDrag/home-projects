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
    public partial class Money : Form
    {
        public Money()
        {
            InitializeComponent();
        }

        private void paymentsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.paymentsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void Money_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Renters". При необходимости она может быть перемещена или удалена.
            this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Rents". При необходимости она может быть перемещена или удалена.
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Payments". При необходимости она может быть перемещена или удалена.
            this.paymentsTableAdapter.Fill(this.marketDBDataSet.Payments);
            UpdateTable();
            debtorsFunc();


        }

        private void debtorsFunc()
        {
            for (int i = paymentsDataGridView.Rows.Count - 1; i >= 0; i--)//находим неоплаченные арендные договоры
                if (paymentsDataGridView[0, i].Value.ToString() == "")
                {
                    string debtor = "";
                    //по номеру аренды находим дату начала аренды, из которой получаем количество дней задолженности, а также получаем сумму задолжености и id арендатора
                    for (int j = 0; j < rentsDataGridView.Rows.Count - 1; j++)
                        if (rentsDataGridView[0, j].Value.ToString() == paymentsDataGridView[1, i].Value.ToString())
                        {
                            //Получаем сумму задолженности
                            debtor += rentsDataGridView[5, j].Value.ToString() + " руб.";
                            for (int z = debtor.Length; z < 10; z++) //устанавливаем "ширину столбца"
                                debtor += "  ";
                            // Вычисляем срок задолженности в днях
                            TimeSpan ts = DateTime.Now - Convert.ToDateTime(rentsDataGridView[2,j].Value);
                            int days = ts.Days;

                            string debtorDays = "";
                            debtorDays += "|  " + days + " дней ";                            
                            for (int x = debtorDays.Length; x < 13; x++) //устанавливаем "ширину столбца"
                                debtorDays += "  ";
                            debtor += debtorDays;

                            //Находим ФИО арендатора по его ID из договора
                            for (int z = 0; z < rentersDataGridView.Rows.Count - 1; z++)
                                if (rentersDataGridView[0, z].Value.ToString() == rentsDataGridView[4, j].Value.ToString()) 
                                {
                                    debtor += "|  " + rentersDataGridView[2, z].Value.ToString();
                                    listBox1.Items.Add(debtor);
                                }
                        }
                }
        }

        private void UpdateTable()
        {
            int payment = 0;
            for (int i = paymentsDataGridView.Rows.Count - 1; i >= 0; i--)//при изменении границ фильтрации восстанавливаем все строки в таблице и скрываем неподходящие.
            {
                paymentsDataGridView.CurrentCell = null;
                paymentsDataGridView.Rows[i].Visible = false;//убираем строку, которая предыдущим поиском могла быть показана

                if (paymentsDataGridView[0, i].Value.ToString() != "")
                {
                    DateTime dateOfPayments = Convert.ToDateTime(paymentsDataGridView[0, i].Value);
                    if (dateOfPayments.CompareTo(dateTimePicker1.Value) == 1 || dateOfPayments.CompareTo(dateTimePicker1.Value) == 0) //если эта строка не попадает в границы, убираем ее
                        if (dateOfPayments.CompareTo(dateTimePicker2.Value) == -1 || dateOfPayments.CompareTo(dateTimePicker2.Value) == 0)
                        {
                            paymentsDataGridView.CurrentCell = null;
                            paymentsDataGridView.Rows[i].Visible = true;
                            int rentID = Convert.ToInt32(paymentsDataGridView[1, i].Value);
                            
                            for(int j =0; j < rentsDataGridView.Rows.Count -1; j++) //находим в таблице Rents нужную сделку и из нее получаем оплаченную стоимость
                                if(Convert.ToInt32(rentsDataGridView[0,j].Value) == rentID)
                                {
                                    payment += Convert.ToInt32(rentsDataGridView[5, j].Value);
                                }
                                
                        }
                }
            }
            label3.Text = payment + "руб.";
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
            UpdateTable();
        }
    }
}
