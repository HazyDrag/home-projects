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
    public partial class AddVacator : Form
    {
        public AddVacator(DateTime dt)
        {
            InitializeComponent();

            dateTimePicker1.Value = dt;
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        public List<Vacators> vacators = new List<Vacators>();

        public class Vacators
        {
            public int id;
            public string name;

            public Vacators(int id1, string name1)
            {
                id = id1;
                name = name1;
            }
        }

        private void tableLSBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableLSBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.korshunyDataSet);

        }

        private void AddVacator_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.VacTable". При необходимости она может быть перемещена или удалена.
            this.vacTableTableAdapter.Fill(this.korshunyDataSet.VacTable);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.TableLS". При необходимости она может быть перемещена или удалена.
            this.tableLSTableAdapter.Fill(this.korshunyDataSet.TableLS);

            for (int i = 0; i < tableLSDataGridView.RowCount - 1; i++)  
            {
                int id = Convert.ToInt32(tableLSDataGridView[0, i].Value);
                string surname = tableLSDataGridView[3, i].Value.ToString();
                surname = surname.Remove(surname.IndexOf(' ') + 1);
                string name = tableLSDataGridView[4, i].Value.ToString();

                Vacators newVacator = new Vacators(id, surname + name);
                vacators.Add(newVacator);
            }

            List<string> vacatorsForSort = new List<string>();

            foreach (Vacators vac in vacators)
                vacatorsForSort.Add(vac.name + "*" + vac.id);

            vacatorsForSort.Sort();
            vacators.Clear();

            foreach (string vac in vacatorsForSort)
            {
                string[] vacs = new string[2];
                vacs = vac.Split('*');

                Vacators newVacator = new Vacators(Convert.ToInt32(vacs[1]), vacs[0]);
                vacators.Add(newVacator);
                comboBox1.Items.Add(newVacator.name);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.vacTableTableAdapter.InsertQuery(vacators[comboBox1.SelectedIndex].id, dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), checkBox1.Checked);

            MessageBox.Show("" + comboBox1.SelectedItem.ToString() + "\nуспешно добавлен в список отпускников!", "Успешно!");

            this.Close();
        }

        private void vacTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vacTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.korshunyDataSet);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
                dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-1);
        }
    }
}

