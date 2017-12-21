namespace IQtests
{
    partial class AddUser
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.surnameBox = new System.Windows.Forms.TextBox();
            this.patronBox = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.nameLabel = new System.Windows.Forms.Label();
            this.surnameLabel = new System.Windows.Forms.Label();
            this.patronLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.Label();
            this.AddB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(12, 40);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(171, 20);
            this.nameBox.TabIndex = 0;
            // 
            // surnameBox
            // 
            this.surnameBox.Location = new System.Drawing.Point(12, 84);
            this.surnameBox.Name = "surnameBox";
            this.surnameBox.Size = new System.Drawing.Size(171, 20);
            this.surnameBox.TabIndex = 1;
            // 
            // patronBox
            // 
            this.patronBox.Location = new System.Drawing.Point(12, 130);
            this.patronBox.Name = "patronBox";
            this.patronBox.Size = new System.Drawing.Size(171, 20);
            this.patronBox.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 178);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(171, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(9, 24);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(32, 13);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Имя:";
            // 
            // surnameLabel
            // 
            this.surnameLabel.AutoSize = true;
            this.surnameLabel.Location = new System.Drawing.Point(9, 68);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(59, 13);
            this.surnameLabel.TabIndex = 5;
            this.surnameLabel.Text = "Фамилия:";
            // 
            // patronLabel
            // 
            this.patronLabel.AutoSize = true;
            this.patronLabel.Location = new System.Drawing.Point(9, 114);
            this.patronLabel.Name = "patronLabel";
            this.patronLabel.Size = new System.Drawing.Size(57, 13);
            this.patronLabel.TabIndex = 6;
            this.patronLabel.Text = "Отчество:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.AutoSize = true;
            this.dateTimePicker.Location = new System.Drawing.Point(9, 162);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(89, 13);
            this.dateTimePicker.TabIndex = 7;
            this.dateTimePicker.Text = "Дата рождения:";
            // 
            // AddB
            // 
            this.AddB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddB.Location = new System.Drawing.Point(39, 216);
            this.AddB.Name = "AddB";
            this.AddB.Size = new System.Drawing.Size(121, 44);
            this.AddB.TabIndex = 8;
            this.AddB.Text = "Добавить";
            this.AddB.UseVisualStyleBackColor = true;
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 276);
            this.Controls.Add(this.AddB);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.patronLabel);
            this.Controls.Add(this.surnameLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.patronBox);
            this.Controls.Add(this.surnameBox);
            this.Controls.Add(this.nameBox);
            this.Name = "AddUser";
            this.Text = "AddUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox nameBox;
        public System.Windows.Forms.TextBox surnameBox;
        public System.Windows.Forms.TextBox patronBox;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.Label patronLabel;
        private System.Windows.Forms.Label dateTimePicker;
        private System.Windows.Forms.Button AddB;
    }
}