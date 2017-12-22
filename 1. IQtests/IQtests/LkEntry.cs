using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace IQtests
{
    public partial class LkEntry : Form
    {
        private List<Group> groups = new List<Group>();
        private List<User> group = new List<User>();

        public LkEntry()
        {
            InitializeComponent();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("data.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                Group gr = new Group();
                XmlNode attr = xnode.Attributes.GetNamedItem("number");
                comboBox1.Items.Add(attr.Value);
                gr.number = attr.Value;
                attr = xnode.Attributes.GetNamedItem("id");
                gr.id = Convert.ToInt32(attr.Value);

                groups.Add(gr);
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            group.Clear();
            comboBox2.Items.Clear();
            comboBox2.Enabled = true;
            
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("data.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут number
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("id");
                    if (attr.Value == groups[comboBox1.SelectedIndex].id.ToString())
                    {
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            if (childnode.Attributes.Count > 0)
                            {
                                User newUser = new User();

                                XmlNode attr2 = childnode.Attributes.GetNamedItem("surname");
                                newUser.surname = attr2.Value;
                                attr2 = childnode.Attributes.GetNamedItem("id");
                                newUser.id = Convert.ToInt32(attr2.Value);
                                attr2 = childnode.Attributes.GetNamedItem("name");
                                newUser.name = attr2.Value;
                                attr2 = childnode.Attributes.GetNamedItem("patron");
                                newUser.patron = attr2.Value;
                                attr2 = childnode.Attributes.GetNamedItem("birthday");
                                newUser.birthday = attr2.Value;

                                group.Add(newUser);
                            }
                        }
                    }
                }
            }
            foreach (User us in group)
            {
                string s = us.surname + " " + us.name + " " + us.patron;
                comboBox2.Items.Add(s);
            }
            if (comboBox2.Items.Count != 0)
                comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                User us = new User();
                us = group[comboBox2.SelectedIndex];
                Group gr = new Group();
                gr = groups[comboBox1.SelectedIndex];

                Lk lk = new Lk(gr, us);
                this.Hide();
                lk.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Выберите ученика!");
        }
    }
}
