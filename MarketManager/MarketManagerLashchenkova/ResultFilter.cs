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
    public partial class ResultFilter : Form
    {
        public static bool state1;  //переменные для приема данных фильтрации
        public static bool state2;
        public static bool state3;
        public static bool state4;

        public static int areamin;
        public static int areamax;

        public static string equip;


        public ResultFilter()
        {
            InitializeComponent();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
                   
        }

        private void placesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();  //добавлено стандартным обработчиком
            this.placesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void ResultFilter_Load(object sender, EventArgs e)
        {
            placesDataGridView.CurrentCell = null;
            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //фильтрация результатов
            {
                if ((bool)placesDataGridView[7, i].Value == true) //если у места статус true(что означает занято), убираем его из таблицы
                    placesDataGridView.Rows[i].Visible = false;

                if (!state1 && placesDataGridView[8, i].Value.ToString() == "1") //фильтрация по выбранному типу места
                    placesDataGridView.Rows[i].Visible = false;
                if (!state2 && placesDataGridView[8, i].Value.ToString() == "2")
                    placesDataGridView.Rows[i].Visible = false;
                if (!state3 && placesDataGridView[8, i].Value.ToString() == "3")
                    placesDataGridView.Rows[i].Visible = false;
                if (!state4 && placesDataGridView[8, i].Value.ToString() == "4")
                    placesDataGridView.Rows[i].Visible = false;

                if (areamin > (int)placesDataGridView[2, i].Value) //фильтрация по площади
                    placesDataGridView.Rows[i].Visible = false;
                if (areamax < (int)placesDataGridView[2, i].Value)
                    placesDataGridView.Rows[i].Visible = false;

                if (equip != "Любая") //по оборудованию
                {
                    placesDataGridView.Rows[i].Visible = false;
                    if (placesDataGridView[3, i].Value.ToString() == equip)
                        placesDataGridView.Rows[i].Visible = true;
                }

                //if ((double)placesDataGridView[5, i].Value == -1)
                  //  placesDataGridView[5, i].Vis = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registration rgs = new Registration();
            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //передаем значения расположения отобранных объектов
                if (placesDataGridView.Rows[i].Visible)
                {
                    rgs.comboBox1.Items.Add(placesDataGridView[1, i].Value.ToString());
                }
                        
            rgs.Show();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateRent crtRnt = new CreateRent();
            crtRnt.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Registration rgs2 = new Registration();
            rgs2.Show();

            for (int i = 0; i < placesDataGridView.Rows.Count; i++) //передаем значения расположения отобранных объектов
                if (placesDataGridView.Rows[i].Visible)
                {
                    rgs2.comboBox1.Items.Add(placesDataGridView[1, i].Value.ToString());
                }

            rgs2.tabControl1.Visible = false;  //убираем лишние элементы на открывающейся форме
            rgs2.label4.Visible = false;
            rgs2.comboBox2.Visible = false;
            rgs2.comboBox1.SelectedIndex = 0;
            rgs2.comboBox3.Visible = true; //подготавливаем нужный нам элемент для работы
            rgs2.comboBox3.Items.Clear();
            rgs2.comboBox3.Items.Add("День");
            rgs2.comboBox3.SelectedIndex = 0;
            rgs2.button3.Visible = true;

            this.Close();     
        }
    }
}
