namespace MarketManagerLashchenkova
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.marketDBDataSet = new MarketManagerLashchenkova.MarketDBDataSet();
            this.rentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rentsTableAdapter = new MarketManagerLashchenkova.MarketDBDataSetTableAdapters.RentsTableAdapter();
            this.tableAdapterManager = new MarketManagerLashchenkova.MarketDBDataSetTableAdapters.TableAdapterManager();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.rentersArchiveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rentersArchiveTableAdapter = new MarketManagerLashchenkova.MarketDBDataSetTableAdapters.RentersArchiveTableAdapter();
            this.rentersArchiveDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button5 = new System.Windows.Forms.Button();
            this.rentsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.placesTableAdapter = new MarketManagerLashchenkova.MarketDBDataSetTableAdapters.PlacesTableAdapter();
            this.placesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.marketDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentersArchiveBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentersArchiveDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.placesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.placesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // marketDBDataSet
            // 
            this.marketDBDataSet.DataSetName = "MarketDBDataSet";
            this.marketDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rentsBindingSource
            // 
            this.rentsBindingSource.DataMember = "Rents";
            this.rentsBindingSource.DataSource = this.marketDBDataSet;
            // 
            // rentsTableAdapter
            // 
            this.rentsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.PaymentsTableAdapter = null;
            this.tableAdapterManager.PlacesTableAdapter = null;
            this.tableAdapterManager.RentersArchiveTableAdapter = null;
            this.tableAdapterManager.RentersTableAdapter = null;
            this.tableAdapterManager.RentsTableAdapter = this.rentsTableAdapter;
            this.tableAdapterManager.UpdateOrder = MarketManagerLashchenkova.MarketDBDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(292, 157);
            this.button1.TabIndex = 0;
            this.button1.Text = "Новая сделка";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(343, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(292, 157);
            this.button2.TabIndex = 1;
            this.button2.Text = "Просмотр информации из БД";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(343, 347);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(292, 157);
            this.button3.TabIndex = 2;
            this.button3.Text = "Обновление данных арендаторов";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(343, 184);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(292, 157);
            this.button4.TabIndex = 3;
            this.button4.Text = "Добавление/изменение объектов недвижимости";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // rentersArchiveBindingSource
            // 
            this.rentersArchiveBindingSource.DataMember = "RentersArchive";
            this.rentersArchiveBindingSource.DataSource = this.marketDBDataSet;
            // 
            // rentersArchiveTableAdapter
            // 
            this.rentersArchiveTableAdapter.ClearBeforeFill = true;
            // 
            // rentersArchiveDataGridView
            // 
            this.rentersArchiveDataGridView.AutoGenerateColumns = false;
            this.rentersArchiveDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rentersArchiveDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.rentersArchiveDataGridView.DataSource = this.rentersArchiveBindingSource;
            this.rentersArchiveDataGridView.Location = new System.Drawing.Point(632, 119);
            this.rentersArchiveDataGridView.Name = "rentersArchiveDataGridView";
            this.rentersArchiveDataGridView.Size = new System.Drawing.Size(498, 220);
            this.rentersArchiveDataGridView.TabIndex = 4;
            this.rentersArchiveDataGridView.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "RenterID";
            this.dataGridViewTextBoxColumn1.HeaderText = "RenterID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Permit";
            this.dataGridViewTextBoxColumn2.HeaderText = "Permit";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn4.HeaderText = "Address";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Profile";
            this.dataGridViewTextBoxColumn5.HeaderText = "Profile";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DateOfReplace";
            this.dataGridViewTextBoxColumn6.HeaderText = "DateOfReplace";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(45, 184);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(292, 157);
            this.button5.TabIndex = 5;
            this.button5.Text = "Оплата аренды";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // rentsDataGridView
            // 
            this.rentsDataGridView.AutoGenerateColumns = false;
            this.rentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rentsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.rentsDataGridView.DataSource = this.rentsBindingSource;
            this.rentsDataGridView.Location = new System.Drawing.Point(632, 296);
            this.rentsDataGridView.Name = "rentsDataGridView";
            this.rentsDataGridView.Size = new System.Drawing.Size(300, 220);
            this.rentsDataGridView.TabIndex = 6;
            this.rentsDataGridView.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "RentID";
            this.dataGridViewTextBoxColumn7.HeaderText = "RentID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "PlaceID";
            this.dataGridViewTextBoxColumn8.HeaderText = "PlaceID";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "StartDate";
            this.dataGridViewTextBoxColumn9.HeaderText = "StartDate";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "TimeOfRent";
            this.dataGridViewTextBoxColumn10.HeaderText = "TimeOfRent";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "RenterID";
            this.dataGridViewTextBoxColumn11.HeaderText = "RenterID";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Payment";
            this.dataGridViewTextBoxColumn12.HeaderText = "Payment";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // placesBindingSource
            // 
            this.placesBindingSource.DataMember = "Places";
            this.placesBindingSource.DataSource = this.marketDBDataSet;
            // 
            // placesTableAdapter
            // 
            this.placesTableAdapter.ClearBeforeFill = true;
            // 
            // placesDataGridView
            // 
            this.placesDataGridView.AutoGenerateColumns = false;
            this.placesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.placesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn20});
            this.placesDataGridView.DataSource = this.placesBindingSource;
            this.placesDataGridView.Location = new System.Drawing.Point(632, 12);
            this.placesDataGridView.Name = "placesDataGridView";
            this.placesDataGridView.Size = new System.Drawing.Size(300, 220);
            this.placesDataGridView.TabIndex = 7;
            this.placesDataGridView.Visible = false;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "PlaceID";
            this.dataGridViewTextBoxColumn13.HeaderText = "PlaceID";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Location";
            this.dataGridViewTextBoxColumn14.HeaderText = "Location";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Area";
            this.dataGridViewTextBoxColumn15.HeaderText = "Area";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Equipment";
            this.dataGridViewTextBoxColumn16.HeaderText = "Equipment";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "DayCost";
            this.dataGridViewTextBoxColumn17.HeaderText = "DayCost";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "MounthCost";
            this.dataGridViewTextBoxColumn18.HeaderText = "MounthCost";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "YearCost";
            this.dataGridViewTextBoxColumn19.HeaderText = "YearCost";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Status";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Status";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn20.HeaderText = "Type";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(45, 347);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(292, 157);
            this.button6.TabIndex = 8;
            this.button6.Text = "Просмотр арендных сборов и списки должников";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(202, 510);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(292, 157);
            this.button7.TabIndex = 9;
            this.button7.Text = "Динамика занятости рабочих мест";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 686);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.placesDataGridView);
            this.Controls.Add(this.rentsDataGridView);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.rentersArchiveDataGridView);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Система управления муниципальным рынком";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.marketDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentersArchiveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentersArchiveDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.placesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.placesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MarketDBDataSet marketDBDataSet;
        private System.Windows.Forms.BindingSource rentsBindingSource;
        private MarketDBDataSetTableAdapters.RentsTableAdapter rentsTableAdapter;
        private MarketDBDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.BindingSource rentersArchiveBindingSource;
        private MarketDBDataSetTableAdapters.RentersArchiveTableAdapter rentersArchiveTableAdapter;
        private System.Windows.Forms.DataGridView rentersArchiveDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView rentsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.BindingSource placesBindingSource;
        private MarketDBDataSetTableAdapters.PlacesTableAdapter placesTableAdapter;
        private System.Windows.Forms.DataGridView placesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

