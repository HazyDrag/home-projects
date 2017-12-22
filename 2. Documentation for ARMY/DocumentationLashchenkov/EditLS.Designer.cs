namespace DocumentationLashchenkov
{
    partial class EditLS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.korshunyDataSet = new DocumentationLashchenkov.KorshunyDataSet();
            this.tableLSBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLSTableAdapter = new DocumentationLashchenkov.KorshunyDataSetTableAdapters.TableLSTableAdapter();
            this.tableAdapterManager = new DocumentationLashchenkov.KorshunyDataSetTableAdapters.TableAdapterManager();
            this.tableLSDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tableLSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.korshunyDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableLSBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableLSDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 49);
            this.button1.TabIndex = 2;
            this.button1.Text = "Добавить коршуна";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLSBindingSource
            // 
            this.tableLSBindingSource.DataMember = "TableLS";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 339);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 48);
            this.button2.TabIndex = 4;
            this.button2.Text = "Изменить коршуна";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(232, 339);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 48);
            this.button3.TabIndex = 5;
            this.button3.Text = "ДМБ коршуна";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // korshunyDataSet
            // 
            this.korshunyDataSet.DataSetName = "KorshunyDataSet";
            this.korshunyDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLSBindingSource1
            // 
            this.tableLSBindingSource1.DataMember = "TableLS";
            this.tableLSBindingSource1.DataSource = this.korshunyDataSet;
            // 
            // tableLSTableAdapter
            // 
            this.tableLSTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TableLSTableAdapter = this.tableLSTableAdapter;
            this.tableAdapterManager.UpdateOrder = DocumentationLashchenkov.KorshunyDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.VacTableTableAdapter = null;
            // 
            // tableLSDataGridView
            // 
            this.tableLSDataGridView.AllowUserToAddRows = false;
            this.tableLSDataGridView.AllowUserToDeleteRows = false;
            this.tableLSDataGridView.AllowUserToResizeRows = false;
            this.tableLSDataGridView.AutoGenerateColumns = false;
            this.tableLSDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tableLSDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLSDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.tableLSDataGridView.DataSource = this.tableLSBindingSource1;
            this.tableLSDataGridView.Location = new System.Drawing.Point(3, 3);
            this.tableLSDataGridView.Name = "tableLSDataGridView";
            this.tableLSDataGridView.ReadOnly = true;
            this.tableLSDataGridView.RowHeadersVisible = false;
            this.tableLSDataGridView.Size = new System.Drawing.Size(682, 330);
            this.tableLSDataGridView.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn9.HeaderText = "Id";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 41;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "rank";
            this.dataGridViewTextBoxColumn10.HeaderText = "rank";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 53;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "vzvod";
            this.dataGridViewTextBoxColumn11.HeaderText = "vzvod";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 61;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "surname";
            this.dataGridViewTextBoxColumn12.HeaderText = "surname";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 72;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn13.HeaderText = "name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 58;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "patron";
            this.dataGridViewTextBoxColumn14.HeaderText = "patron";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 62;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "surnameR";
            this.dataGridViewTextBoxColumn15.HeaderText = "surnameR";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 80;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "tnumber";
            this.dataGridViewTextBoxColumn16.HeaderText = "tnumber";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 70;
            // 
            // EditLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 407);
            this.Controls.Add(this.tableLSDataGridView);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "EditLS";
            this.Text = "EditLS";
            this.Load += new System.EventHandler(this.EditLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableLSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.korshunyDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableLSBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableLSDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        //  private KorshunyDataSet1 korshunyDataSet1;
        private System.Windows.Forms.BindingSource tableLSBindingSource;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button button3;
        private KorshunyDataSet korshunyDataSet;
        private System.Windows.Forms.BindingSource tableLSBindingSource1;
        private KorshunyDataSetTableAdapters.TableLSTableAdapter tableLSTableAdapter;
        private KorshunyDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView tableLSDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
    }
}