using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace IQtests
{
    public partial class EditGroups : Form
    {
        private List<Group> groups = new List<Group>();
        private List<User> group = new List<User>();

        public EditGroups()
        {
            InitializeComponent();

            ReadGroups();            
        }

        private void ReadGroups()
        {
            groups.Clear();
            groupsList.Items.Clear();
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
                    Group gr = new Group();
                    XmlNode attr = xnode.Attributes.GetNamedItem("id");
                    gr.id = Convert.ToInt16(attr.Value);
                    attr = xnode.Attributes.GetNamedItem("number");
                    gr.number = attr.Value;
                    groups.Add(gr);
                }                
            }
            foreach (Group gr in groups)
                groupsList.Items.Add(gr.number);
        }

        private void groupsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            usersList.Enabled = false;
            addUserB.Enabled = false;
            editUserB.Enabled = false;
            removeUserB.Enabled = false;
            groupName.Enabled = false;

            UpdateUsersList();
        }

        private void UpdateUsersList()
        {
            group.Clear();
            usersList.Items.Clear();

            groupName.Text = groupsList.SelectedItem.ToString();

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
                    XmlNode attr = xnode.Attributes.GetNamedItem("number");
                    if (attr.Value == groupsList.SelectedItem.ToString())
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
                usersList.Items.Add(s);
            }
        }

        private void editB_Click(object sender, EventArgs e)
        {
            if (groupsList.SelectedIndex != -1)
            {
                usersList.Enabled = true;
                addUserB.Enabled = true;
                editUserB.Enabled = true;
                removeUserB.Enabled = true;
                groupName.Enabled = true;
                confirmB.Enabled = true;

                groupsList.Enabled = false;
                addB.Enabled = false;
                editB.Enabled = false;
                removeB.Enabled = false;
            }
        }

        private void confirmB_Click(object sender, EventArgs e)
        {
            usersList.Enabled = false;
            addUserB.Enabled = false;
            editUserB.Enabled = false;
            removeUserB.Enabled = false;
            groupName.Enabled = false;
            confirmB.Enabled = false;

            groupsList.Enabled = true;
            addB.Enabled = true;
            editB.Enabled = true;
            removeB.Enabled = true;

            if(groupName.Text != groupsList.SelectedItem.ToString())
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;
                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {
                    // получаем атрибут id
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("id");
                        if (attr.Value == groups[groupsList.SelectedIndex].id.ToString())
                        {
                            XmlNode numberAttr = xnode.Attributes.GetNamedItem("number");
                            numberAttr.Value = groupName.Text;
                        }
                    }
                }
                xDoc.Save("data.xml");
            }
            ReadGroups();
        }

        private void addB_Click(object sender, EventArgs e)
        {
            AddGroup adg = new AddGroup();
            DialogResult dr = adg.ShowDialog();
            if (dr == DialogResult.OK)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                // создаем новый элемент group
                XmlElement groupElem = xDoc.CreateElement("group");
                // создаем атрибуты number и id
                XmlAttribute numberAttr = xDoc.CreateAttribute("number");
                XmlAttribute idAttr = xDoc.CreateAttribute("id");

                XmlText numberText = xDoc.CreateTextNode(adg.textBox1.Text);
                
                int id = 1;
                foreach (Group g in groups)
                {
                    if (g.id > id)
                        id = g.id;
                }
                XmlText idText = xDoc.CreateTextNode(Convert.ToString(id + 1));
                numberAttr.AppendChild(numberText);
                idAttr.AppendChild(idText);
                groupElem.Attributes.Append(numberAttr);
                groupElem.Attributes.Append(idAttr);
                xRoot.AppendChild(groupElem);
                xDoc.Save("data.xml");

                ReadGroups();
            }

            
        }

        private void removeB_Click(object sender, EventArgs e)
        {
            if (groupsList.SelectedIndex != -1)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;
                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {
                    // получаем атрибут id
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("id");
                        if (attr.Value == groups[groupsList.SelectedIndex].id.ToString())
                        {
                            xRoot.RemoveChild(xnode);
                            xDoc.Save("data.xml");
                            break;
                        }
                    }
                }
                ReadGroups();
            }
        }

        private void addUserB_Click(object sender, EventArgs e)
        {
            AddUser adu = new AddUser();
            DialogResult dr = adu.ShowDialog();
            if (dr == DialogResult.OK)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlNode xnodeGroups in xRoot)
                {
                    XmlNode attr = xnodeGroups.Attributes.GetNamedItem("id");
                    if (attr.Value == groups[groupsList.SelectedIndex].id.ToString())
                    {
                        // создаем новый элемент user
                        XmlElement userElem = xDoc.CreateElement("user");
                        // создаем атрибуты
                        XmlAttribute idAttr = xDoc.CreateAttribute("id");
                        XmlAttribute surnameAttr = xDoc.CreateAttribute("surname");
                        XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                        XmlAttribute patronAttr = xDoc.CreateAttribute("patron");
                        XmlAttribute birthdayAttr = xDoc.CreateAttribute("birthday");

                        XmlText surnameText = xDoc.CreateTextNode(adu.surnameBox.Text);
                        XmlText nameText = xDoc.CreateTextNode(adu.nameBox.Text);
                        XmlText patronText = xDoc.CreateTextNode(adu.patronBox.Text);
                        XmlText birthdayText = xDoc.CreateTextNode(adu.dateTimePicker1.Value.ToShortDateString());

                        int id = 0;
                        foreach (User us in group)
                        {
                            if (us.id > id)
                                id = us.id;
                        }
                        XmlText idText = xDoc.CreateTextNode(Convert.ToString(id + 1));

                        idAttr.AppendChild(idText);
                        surnameAttr.AppendChild(surnameText);
                        nameAttr.AppendChild(nameText);
                        patronAttr.AppendChild(patronText);
                        birthdayAttr.AppendChild(birthdayText);

                        userElem.Attributes.Append(idAttr);
                        userElem.Attributes.Append(nameAttr);
                        userElem.Attributes.Append(surnameAttr);
                        userElem.Attributes.Append(patronAttr);
                        userElem.Attributes.Append(birthdayAttr);
                        xnodeGroups.AppendChild(userElem);
                        xDoc.Save("data.xml");
                        break;
                    }
                }
                UpdateUsersList();
            }
        }

        private void editUserB_Click(object sender, EventArgs e)
        {
            if (usersList.SelectedIndex != -1)
            {
                int ind = usersList.SelectedIndex;
                AddUser adu = new AddUser(group[ind].name, group[ind].surname, group[ind].patron, group[ind].birthday);                
                DialogResult dr = adu.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("data.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode xnodeGroups in xRoot)
                    {
                        XmlNode attr = xnodeGroups.Attributes.GetNamedItem("id");
                        if (attr.Value == groups[groupsList.SelectedIndex].id.ToString())
                        {
                            foreach (XmlNode xnode in xnodeGroups)
                            {
                                XmlNode attr2 = xnode.Attributes.GetNamedItem("id");
                                if (attr2.Value == group[ind].id.ToString())
                                {
                                    attr2 = xnode.Attributes.GetNamedItem("surname");
                                    attr2.Value = adu.surnameBox.Text;
                                    attr2 = xnode.Attributes.GetNamedItem("name");
                                    attr2.Value = adu.nameBox.Text;
                                    attr2 = xnode.Attributes.GetNamedItem("patron");
                                    attr2.Value = adu.patronBox.Text;
                                    attr2 = xnode.Attributes.GetNamedItem("birthday");
                                    attr2.Value = adu.dateTimePicker1.Value.ToShortDateString();

                                    xDoc.Save("data.xml");
                                    UpdateUsersList();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void removeUserB_Click(object sender, EventArgs e)
        {
            if (usersList.SelectedIndex != -1)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;
                // обход всех узлов в корневом элементе
                foreach (XmlNode xnodeGroups in xRoot)
                {
                    // получаем атрибут id
                    if (xnodeGroups.Attributes.Count > 0)
                    {
                        XmlNode attr = xnodeGroups.Attributes.GetNamedItem("id");
                        if (attr.Value == groups[groupsList.SelectedIndex].id.ToString())
                        {
                            foreach (XmlNode xnode in xnodeGroups)
                            {
                                XmlNode attr2 = xnode.Attributes.GetNamedItem("id");
                                if (attr2.Value == group[usersList.SelectedIndex].id.ToString())
                                {
                                    xnodeGroups.RemoveChild(xnode);
                                    xDoc.Save("data.xml");
                                    break;
                                }
                            }
                        }
                    }
                }
                UpdateUsersList();
            }
        }
    }
}
