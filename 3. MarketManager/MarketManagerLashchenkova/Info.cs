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
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void rentsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.rentsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.marketDBDataSet);

        }

        private void Info_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Payments". При необходимости она может быть перемещена или удалена.
            this.paymentsTableAdapter.Fill(this.marketDBDataSet.Payments);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.RentersArchive". При необходимости она может быть перемещена или удалена.
            this.rentersArchiveTableAdapter.Fill(this.marketDBDataSet.RentersArchive);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Renters". При необходимости она может быть перемещена или удалена.
            this.rentersTableAdapter.Fill(this.marketDBDataSet.Renters);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Places". При необходимости она может быть перемещена или удалена.
            this.placesTableAdapter.Fill(this.marketDBDataSet.Places);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "marketDBDataSet.Rents". При необходимости она может быть перемещена или удалена.
            this.rentsTableAdapter.Fill(this.marketDBDataSet.Rents);

        }
    }
}
