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
    public partial class UpdateData : Form
    {
        public UpdateData()
        {
            InitializeComponent();
        }

        private void rentersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.rentersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void UpdateData_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.RentersArchive". При необходимости она может быть перемещена или удалена.
            this.rentersArchiveTableAdapter.Fill(this.marketDBDataSet.RentersArchive);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Renters". При необходимости она может быть перемещена или удалена.
            this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
            rentersDataGridView.CurrentCell = null;
            rentersDataGridView.Rows[0].Visible = false;

            foreach (DataGridViewColumn column in rentersArchiveDataGridView.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            foreach (DataGridViewColumn column in rentersDataGridView.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            for(int i = rentersDataGridView.Rows.Count - 1; i > 0; i--)//при изменении теста фильтрации восстанавливаем все строки в таблице и скрываем неподходящие. кроме первой строки с данными для разовых арендаторов
            {
                string searchRow = null;
                rentersDataGridView.Rows[i].Visible = true;//показываем строку, которая предыдущим поиском могла быть скрыта
                //создаем одну строку из всех ячеек строки, для поискаа по любой ячейке
                
                for (int j = 0; j < 6; j++)
                {
                    searchRow += rentersDataGridView[j, i].Value.ToString() + " ";
                }

                if (!searchRow.Contains(textBox5.Text)) //если эта строка не содержит искомого текста, убираем ее
                {
                    rentersDataGridView.CurrentCell = null;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    button1.Enabled = false;
                    rentersDataGridView.Rows[i].Visible = false;
                }
            }
        }
        

        private void rentersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexNow = rentersDataGridView.CurrentRow.Index; //при выборе ячейки в датагрид, заносим данные из этой строки
            textBox1.Text = rentersDataGridView[1, indexNow].Value.ToString();
            textBox2.Text = rentersDataGridView[2, indexNow].Value.ToString();
            textBox3.Text = rentersDataGridView[3, indexNow].Value.ToString();
            textBox4.Text = rentersDataGridView[4, indexNow].Value.ToString();

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            button1.Enabled = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            for (int i = rentersArchiveDataGridView.Rows.Count - 1; i >= 0; i--)//при изменении текста фильтрации восстанавливаем все строки в таблице и скрываем неподходящие
            {
                string searchRow = null;
                rentersArchiveDataGridView.Rows[i].Visible = true;//показываем строку, которая предыдущим поиском могла быть скрыта
                //создаем одну строку из всех ячеек строки, для поиска по любому столбцу
                for (int j = 0; j < 6; j++)
                {
                    searchRow += rentersArchiveDataGridView[j, i].Value.ToString() + " ";
                }
                if (!searchRow.Contains(textBox6.Text)) //если эта строка не содержит искомого текста, убираем ее
                {
                    rentersArchiveDataGridView.CurrentCell = null;
                    rentersArchiveDataGridView.Rows[i].Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //находим ID выбранного в списке арендатора
            string renterID = rentersDataGridView[0, rentersDataGridView.CurrentRow.Index].Value.ToString();

            DataRow[] rentersRow =
                    marketDBDataSet.Tables["Renters"].Select("RenterID = '" + renterID + "'");
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")//если все данные заполнены

                if (textBox1.Text != rentersRow[0]["Permit"].ToString() ||
                    textBox2.Text != rentersRow[0]["Name"].ToString() ||
                    textBox3.Text != rentersRow[0]["Address"].ToString() ||
                    textBox4.Text != rentersRow[0]["Profile"].ToString())//Проверяем, изменялись ли данные
                {
                    //находим следующий порядковый номер копии в архиве
                    int maxrentID = 0;
                    foreach (DataRow dr in marketDBDataSet.RentersArchive.Rows) //находим максимальное ID аренды, чтобы не сбивались столбцы при отображении
                    {
                        int rentID = dr.Field<int>("RenterID");
                        maxrentID = Math.Max(maxrentID, rentID);
                    }
                    int renterAID = maxrentID + 1;
                    int indexNow = rentersDataGridView.CurrentRow.Index;
                    //создаем копию этой строки в архив. Записываем сегодняшнюю дату
                    rentersArchiveTableAdapter.Insert(renterAID, rentersDataGridView[1, indexNow].Value.ToString(), rentersDataGridView[2, indexNow].Value.ToString(), rentersDataGridView[3, indexNow].Value.ToString(), rentersDataGridView[4, indexNow].Value.ToString(), DateTime.Now);

                    //Находим строку в таблице по выбранному ID в dataGrid и заменяем значения в строке на введеные                
                    rentersRow[0]["Permit"] = textBox1.Text;
                    rentersRow[0]["Name"] = textBox2.Text;
                    rentersRow[0]["Address"] = textBox3.Text;
                    rentersRow[0]["Profile"] = textBox4.Text;
                    rentersRow[0]["RefreshDate"] = DateTime.Now;

                    rentersTableAdapter.Update(this.marketDBDataSet.Renters);

                    this.rentersArchiveTableAdapter.Fill(this.marketDBDataSet.RentersArchive);
                    this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
                    rentersDataGridView.CurrentCell = null;
                    rentersDataGridView.Rows[0].Visible = false;

                    MessageBox.Show("Данные успешно обновлены!");
                    button1.Enabled = false;
                }
                else MessageBox.Show("Данные не изменились, обновление не требуется");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return; //все такие функции для ограничения ввода букв в текстбоксы. Защита от "альтернативно одаренных" пользователей
            else
                e.Handled = true;
        }
    }
}
