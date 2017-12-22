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
    public partial class EditLS : Form
    {
        public EditLS(int mode, int id, int vzvod, string rank, string surname,
                        string name, string patron, string surnameR, string number)
        {
            InitializeComponent();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.TableLS". При необходимости она может быть перемещена или удалена.
            this.tableLSTableAdapter.Fill(this.korshunyDataSet.TableLS);

            tableLSDataGridView.Columns[0].Visible = false;
            if (mode == 1)
                AddKorshun(id, vzvod, rank, surname, name, patron, surnameR, number); //пользовательская функция добавления в бд
            if (mode == 2)
                ChangeKorshun(id, vzvod, rank, surname, name, patron, surnameR, number); //пользовательская функция изменения строки в бд
        }

        public EditLS()
        {
            InitializeComponent();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.TableLS". При необходимости она может быть перемещена или удалена.
            this.tableLSTableAdapter.Fill(this.korshunyDataSet.TableLS);

            tableLSDataGridView.Columns[0].Visible = false;
        }

        private void ChangeKorshun(int id, int vzvod, string rank, string surname, string name,
                                 string patron, string surnameR, string number)
        {            
            korshunyDataSet.TableLS[id].rank = rank; //Задаем новые значения для строки
            korshunyDataSet.TableLS[id].vzvod = vzvod;
            korshunyDataSet.TableLS[id].surname = Names(surname);
            korshunyDataSet.TableLS[id].name = Names(name);
            korshunyDataSet.TableLS[id].patron = Names(patron);
            korshunyDataSet.TableLS[id].surnameR = surnameR.ToUpper();
            korshunyDataSet.TableLS[id].tnumber = number;

            this.tableLSTableAdapter.Update(this.korshunyDataSet.TableLS); //обновляем адаптер для сохранения изменений
        }

        private void AddKorshun(int id, int vzvod, string rank, string surname, string name,
                                 string patron, string surnameR, string number)
        {
            DataRow row = korshunyDataSet.TableLS.NewRow(); //новая строка структуры нашей таблицы
                        
            //Задаем параметры строки
            row["Id"] = id; 
            row["rank"] = rank; 
            row["vzvod"] = vzvod;
            row["surname"] = Names(surname);
            row["name"] = Names(name);
            row["patron"] = Names(patron);
            row["surnameR"] = surnameR.ToUpper();
            row["tnumber"] = number;

            // Add the row to the Region table
            this.korshunyDataSet.TableLS.Rows.Add(row); //добавляем строку
            //// Save the new row to the database
            this.tableLSTableAdapter.Update(this.korshunyDataSet.TableLS); //обновляем адаптер для сохранения изменений
            
        }

        private string Names(string name) //функц. для приведения регистров букв в записях бд к единому формату
        {
            string name2 = "";
            string name3 = "";

            name2 = name.Substring(0, 1).ToUpper(); //Первая буква прописная
            name3 = name.Substring(1).ToLower(); //остальные строчные
            name2 += name3; //объединение в одну строку прописной буквы и строчных

            return name2;
        }

        private void EditLS_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = korshunyDataSet.TableLS[korshunyDataSet.TableLS.Rows.Count - 1].Id + 1;
            AddKorsh fadd = new AddKorsh(id); //вызов окна добавления коршуна
            Hide();
            fadd.ShowDialog();
            Close();
            
        }

        private void tableLSBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int indexRow = tableLSDataGridView.CurrentRow.Index; //получаем индекс выбранного для изменения элемента
            string textMsg = "Вы уверены, что хотите изменить запись \n" + DataFromRow(1, indexRow) +
                                " " + DataFromRow(3, indexRow);
            DialogResult dlr =
                MessageBox.Show(textMsg, "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dlr == DialogResult.Yes)
            {
                string rank     = DataFromRow(1, indexRow); //задаем переменные для функции вызова формы изменения
                string vzvod    = DataFromRow(2, indexRow);                
                string surname  = DataFromRow(3, indexRow);
                string name     = DataFromRow(4, indexRow);
                string patron   = DataFromRow(5, indexRow);
                string surnameR = DataFromRow(6, indexRow);
                string number   = DataFromRow(7, indexRow);
                AddKorsh fadd   = new AddKorsh(indexRow, rank, vzvod, surname, name, patron, surnameR, number); //вызов формы изменения коршунов

                Hide();
                fadd.ShowDialog();
                Close();
            }
        }
               
        private string DataFromRow(int indexColumn, int indexRow) //функция для вытаскивания данных из таблицы с формы
        {            
            return Convert.ToString(tableLSDataGridView[indexColumn, indexRow].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int indexRow = tableLSDataGridView.CurrentRow.Index; //получаем индекс выбранного для удаления элемента
            string textMsg = "Вы уверены, что хотите удалить запись \n" + DataFromRow(1, indexRow) +
                                " " + DataFromRow(3, indexRow);
            DialogResult dlr =
                MessageBox.Show(textMsg, "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                // Locate the row to delete.
                korshunyDataSet.TableLS.FindById(Convert.ToInt32(tableLSDataGridView[0, indexRow].Value)).Delete();
                
                // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.TableLS". При необходимости она может быть перемещена или удалена.
                this.tableLSTableAdapter.Update(this.korshunyDataSet.TableLS);
                
            }

        }
    }
}
