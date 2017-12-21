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
    public partial class PayForm : Form
    {
        public PayForm()
        {
            InitializeComponent();
        }

        private void paymentsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.paymentsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void RefreshListBoxFunc()
        {
            int maxrentID = 0;
            foreach (DataRow dr in marketDBDataSet.Rents.Rows) //находим максимальное ID аренды, чтобы не сбивались столбцы при отображении
            {
                int rentID = dr.Field<int>("RentID");
                maxrentID = Math.Max(maxrentID, rentID);
            }

            int counter = 0;
            for (int i = 0; i < paymentsDataGridView.Rows.Count - 1; i++)
                if (paymentsDataGridView[0, i].Value == DBNull.Value)
                {
                    string rent = null;
                    counter++; //считаем количество должников
                    for (int j = 0; j < rentsDataGridView.Rows.Count - 1; j++) //находим, к какой аренде относится этот долг
                        if (paymentsDataGridView[1, i].Value.ToString() == rentsDataGridView[0, j].Value.ToString())
                        {
                            string rentID = rentsDataGridView[0, j].Value.ToString();    //записываем в переменную для добавления в листбокс, ID аренды, дату оформления и срок аренды 
                            string dateOfRent = rentsDataGridView[2, j].Value.ToString();
                            string timeOfRent = rentsDataGridView[3, j].Value.ToString();

                            rent += rentID; 
                            for (int rid = rentID.Length; rid < maxrentID.ToString().Length; rid++) //цикл для записи недостающего количества пробелов, чтобы не нарушать порядок столбцов
                                rent += "  ";

                            rent += "                " + dateOfRent + "                " + timeOfRent;
                            for (int tor = timeOfRent.Length; tor < 3; tor++)
                                rent += "  ";
                            

                            for (int p = 0; p < placesDataGridView.Rows.Count - 1; p++)//по номеру места в таблице об арендах ищем расположение объекта
                                if (placesDataGridView[0, p].Value.ToString() == rentsDataGridView[1, j].Value.ToString())
                                {
                                    string loc = placesDataGridView[1, p].Value.ToString(); //и записываем данные в нашу строку
                                    rent += "                " + loc + "                ";
                                    break;
                                }

                            for (int r = 0; r < rentersDataGridView.Rows.Count - 1; r++) //определяем ФИО арендатора
                            {
                                if (rentersDataGridView[0, r].Value.ToString() == rentsDataGridView[4, j].Value.ToString())
                                {
                                    string renterFIO = rentersDataGridView[2, r].Value.ToString(); //и записываем данные в нашу строку
                                    rent += renterFIO + "                ";
                                    break;
                                }
                            }

                            listBox1.Items.Add(rent);
                            break;
                        }
                }

            label5.Text = "Не оплачено " + counter + " аренд(ы)!"; //выводим сообщение о количестве неоплаченых аренд

        }

        private void PayForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Renters". При необходимости она может быть перемещена или удалена.
            this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Rents". При необходимости она может быть перемещена или удалена.
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Payments". При необходимости она может быть перемещена или удалена.
            this.paymentsTableAdapter.Fill(this.marketDBDataSet.Payments);

            RefreshListBoxFunc();
        }

        int irentID = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Внести оплату в размере " + textBox2.Text + " руб.\n В счет оплаты аренды N: " + irentID + " ?", "Внести оплату?", MessageBoxButtons.YesNo);
            if (rslt == DialogResult.Yes)
            {
                DataRow[] paymentsRow =
                marketDBDataSet.Tables["Payments"].Select("RentID = '" + irentID + "'"); //находим нужную строку в таблице
                paymentsRow[0]["DateOfPayment"] = DateTime.Now; 
                paymentsTableAdapter.Update(this.marketDBDataSet.Payments);

                MessageBox.Show("Оплата успешно произведена!");
            }

            this.paymentsTableAdapter.Fill(this.marketDBDataSet.Payments);
            listBox1.Items.Clear();
            RefreshListBoxFunc();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = null;
            button1.Enabled = false;

            if (listBox1.SelectedIndex != -1)
            {
                string itemListbox = listBox1.Items[listBox1.SelectedIndex].ToString();
                irentID = Convert.ToInt32(itemListbox.Substring(0, itemListbox.IndexOf(" "))); //получаем ID выбранной аренды
                for (int i = 0; i < rentsDataGridView.Rows.Count - 1; i++)
                    if ((int)rentsDataGridView[0, i].Value == irentID) //нашли нужную строку и оттуда выясняем сумму, которую необходимо оплатить
                    {
                        textBox2.Text = rentsDataGridView[5, i].Value.ToString();
                        button1.Enabled = true;
                        break;
                    }
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); 
            RefreshListBoxFunc();//восстанавливаем все возможные строки в листбоксе

            for (int i = listBox1.Items.Count - 1; i >= 0; i--) //двигаемся с нижней строки, чтобы при удалении строки, у оставшихся не менялся индекс
                if (!listBox1.Items[i].ToString().Contains(textBox1.Text)) //Если строка из listbox не содержит в себе строки из textbox'a поиска, удаляем ее из listbox. 
                    listBox1.Items.RemoveAt(i);

            listBox1.SelectedIndex = -1;
            button1.Enabled = false;
            textBox2.Text = null;
        }
    }
}
