namespace IQtests
{
    partial class EditGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGroups));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupsList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.confirmB = new System.Windows.Forms.Button();
            this.groupName = new System.Windows.Forms.TextBox();
            this.usersList = new System.Windows.Forms.ListBox();
            this.groupNameL = new System.Windows.Forms.Label();
            this.removeUserB = new System.Windows.Forms.Button();
            this.editUserB = new System.Windows.Forms.Button();
            this.addUserB = new System.Windows.Forms.Button();
            this.removeB = new System.Windows.Forms.Button();
            this.editB = new System.Windows.Forms.Button();
            this.addB = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removeB);
            this.groupBox1.Controls.Add(this.editB);
            this.groupBox1.Controls.Add(this.addB);
            this.groupBox1.Controls.Add(this.groupsList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 319);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Группы:";
            // 
            // groupsList
            // 
            this.groupsList.FormattingEnabled = true;
            this.groupsList.Location = new System.Drawing.Point(15, 27);
            this.groupsList.Name = "groupsList";
            this.groupsList.Size = new System.Drawing.Size(118, 238);
            this.groupsList.TabIndex = 21;
            this.groupsList.SelectedIndexChanged += new System.EventHandler(this.groupsList_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.confirmB);
            this.groupBox2.Controls.Add(this.groupName);
            this.groupBox2.Controls.Add(this.removeUserB);
            this.groupBox2.Controls.Add(this.editUserB);
            this.groupBox2.Controls.Add(this.usersList);
            this.groupBox2.Controls.Add(this.addUserB);
            this.groupBox2.Controls.Add(this.groupNameL);
            this.groupBox2.Location = new System.Drawing.Point(170, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 319);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ученики";
            // 
            // confirmB
            // 
            this.confirmB.Enabled = false;
            this.confirmB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.confirmB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confirmB.Location = new System.Drawing.Point(170, 271);
            this.confirmB.Name = "confirmB";
            this.confirmB.Size = new System.Drawing.Size(34, 34);
            this.confirmB.TabIndex = 30;
            this.confirmB.Text = "ОК";
            this.confirmB.UseVisualStyleBackColor = true;
            this.confirmB.Click += new System.EventHandler(this.confirmB_Click);
            // 
            // groupName
            // 
            this.groupName.Enabled = false;
            this.groupName.Location = new System.Drawing.Point(55, 27);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(149, 20);
            this.groupName.TabIndex = 28;
            // 
            // usersList
            // 
            this.usersList.Enabled = false;
            this.usersList.FormattingEnabled = true;
            this.usersList.Location = new System.Drawing.Point(16, 53);
            this.usersList.Name = "usersList";
            this.usersList.Size = new System.Drawing.Size(188, 212);
            this.usersList.TabIndex = 25;
            // 
            // groupNameL
            // 
            this.groupNameL.AutoSize = true;
            this.groupNameL.Location = new System.Drawing.Point(13, 30);
            this.groupNameL.Name = "groupNameL";
            this.groupNameL.Size = new System.Drawing.Size(45, 13);
            this.groupNameL.TabIndex = 29;
            this.groupNameL.Text = "Группа:";
            // 
            // removeUserB
            // 
            this.removeUserB.Enabled = false;
            this.removeUserB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.removeUserB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.removeUserB.Image = ((System.Drawing.Image)(resources.GetObject("removeUserB.Image")));
            this.removeUserB.Location = new System.Drawing.Point(92, 271);
            this.removeUserB.Name = "removeUserB";
            this.removeUserB.Size = new System.Drawing.Size(34, 34);
            this.removeUserB.TabIndex = 27;
            this.removeUserB.UseVisualStyleBackColor = true;
            this.removeUserB.Click += new System.EventHandler(this.removeUserB_Click);
            // 
            // editUserB
            // 
            this.editUserB.Enabled = false;
            this.editUserB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.editUserB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editUserB.Image = ((System.Drawing.Image)(resources.GetObject("editUserB.Image")));
            this.editUserB.Location = new System.Drawing.Point(54, 271);
            this.editUserB.Name = "editUserB";
            this.editUserB.Size = new System.Drawing.Size(34, 34);
            this.editUserB.TabIndex = 26;
            this.editUserB.UseVisualStyleBackColor = true;
            this.editUserB.Click += new System.EventHandler(this.editUserB_Click);
            // 
            // addUserB
            // 
            this.addUserB.Enabled = false;
            this.addUserB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addUserB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUserB.Image = ((System.Drawing.Image)(resources.GetObject("addUserB.Image")));
            this.addUserB.Location = new System.Drawing.Point(16, 271);
            this.addUserB.Name = "addUserB";
            this.addUserB.Size = new System.Drawing.Size(34, 34);
            this.addUserB.TabIndex = 25;
            this.addUserB.UseVisualStyleBackColor = true;
            this.addUserB.Click += new System.EventHandler(this.addUserB_Click);
            // 
            // removeB
            // 
            this.removeB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.removeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.removeB.Image = ((System.Drawing.Image)(resources.GetObject("removeB.Image")));
            this.removeB.Location = new System.Drawing.Point(99, 271);
            this.removeB.Name = "removeB";
            this.removeB.Size = new System.Drawing.Size(34, 34);
            this.removeB.TabIndex = 24;
            this.removeB.UseVisualStyleBackColor = true;
            this.removeB.Click += new System.EventHandler(this.removeB_Click);
            // 
            // editB
            // 
            this.editB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.editB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editB.Image = ((System.Drawing.Image)(resources.GetObject("editB.Image")));
            this.editB.Location = new System.Drawing.Point(61, 271);
            this.editB.Name = "editB";
            this.editB.Size = new System.Drawing.Size(34, 34);
            this.editB.TabIndex = 23;
            this.editB.UseVisualStyleBackColor = true;
            this.editB.Click += new System.EventHandler(this.editB_Click);
            // 
            // addB
            // 
            this.addB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addB.Image = ((System.Drawing.Image)(resources.GetObject("addB.Image")));
            this.addB.Location = new System.Drawing.Point(23, 271);
            this.addB.Name = "addB";
            this.addB.Size = new System.Drawing.Size(34, 34);
            this.addB.TabIndex = 22;
            this.addB.UseVisualStyleBackColor = true;
            this.addB.Click += new System.EventHandler(this.addB_Click);
            // 
            // EditGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 344);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditGroups";
            this.Text = "Edit";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeB;
        private System.Windows.Forms.Button editB;
        private System.Windows.Forms.Button addB;
        private System.Windows.Forms.ListBox groupsList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox usersList;
        private System.Windows.Forms.Button removeUserB;
        private System.Windows.Forms.Button editUserB;
        private System.Windows.Forms.Button addUserB;
        private System.Windows.Forms.TextBox groupName;
        private System.Windows.Forms.Label groupNameL;
        private System.Windows.Forms.Button confirmB;
    }
}