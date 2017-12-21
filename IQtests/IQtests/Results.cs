using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace IQtests
{
    public partial class Results : Form
    {
        string[] transferToSten = null;
        string[] transferToIq = null;
        public List<string> numbersOfErrors = new List<string>();

        public Results(string result, string variant)
        {
            InitializeComponent();

            string path = Directory.GetCurrentDirectory() + @"\resourses\test";
            string[] info = File.ReadAllText(path).Split(' ');

            Group gr = new Group();
            gr.id = Convert.ToInt32(info[0]);
            User us = new User();
            us.id = Convert.ToInt32(info[1]);
            Test ts = new Test();
            ts.result = result;
            ts.date = DateTime.Now.ToShortDateString();
            label12.Text += ts.date;
            ts.variant = variant;
            //Задаем таблицу для перевода в стены и iq
            transferToSten = File.ReadAllLines("Sten.txt");
            transferToIq = File.ReadAllLines("iq.txt");

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("data.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode groupnode in xRoot)
            {
                XmlNode attr = groupnode.Attributes.GetNamedItem("id");
                if (attr.Value == info[0])
                {
                    attr = groupnode.Attributes.GetNamedItem("number");
                    label3.Text += attr.Value;
                    gr.number = attr.Value;
                    foreach (XmlNode usernode in groupnode.ChildNodes)
                        if (usernode.Attributes.Count > 0)
                        {
                            XmlNode attr2 = usernode.Attributes.GetNamedItem("id");
                            if (attr2.Value == info[1])
                            {                                
                                attr2 = usernode.Attributes.GetNamedItem("surname");
                                label2.Text += attr2.Value + " ";
                                attr2 = usernode.Attributes.GetNamedItem("name");
                                label2.Text += attr2.Value;
                                attr2 = usernode.Attributes.GetNamedItem("birthday");
                                us.birthday = attr2.Value.ToString();
                                
                                // создаем новый элемент user
                                XmlElement testElem = xDoc.CreateElement("test");
                                // создаем атрибуты
                                XmlAttribute idAttr = xDoc.CreateAttribute("id");
                                XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                                XmlAttribute variantAttr = xDoc.CreateAttribute("variant");
                                XmlAttribute dateAttr = xDoc.CreateAttribute("date");

                                XmlText nameText = xDoc.CreateTextNode("Методика ТИП");
                                XmlText variantText = xDoc.CreateTextNode(variant);
                                XmlText dateText = xDoc.CreateTextNode(DateTime.Now.ToShortDateString());

                                int id = 1;
                                foreach (XmlNode testnode in usernode.ChildNodes)
                                {
                                    XmlNode attr3 = testnode.Attributes.GetNamedItem("id");
                                    int aintid = Convert.ToInt32(attr3.Value);
                                    if (aintid >= id)                                    
                                        id = aintid + 1;
                                }
                                XmlText idText = xDoc.CreateTextNode(Convert.ToString(id));

                                idAttr.AppendChild(idText);
                                nameAttr.AppendChild(nameText);
                                variantAttr.AppendChild(variantText);
                                dateAttr.AppendChild(dateText);

                                testElem.Attributes.Append(idAttr);
                                testElem.Attributes.Append(nameAttr);
                                testElem.Attributes.Append(variantAttr);
                                testElem.Attributes.Append(dateAttr);
                                testElem.InnerText = result;
                                usernode.AppendChild(testElem);
                                xDoc.Save("data.xml");
                                break;
                            }
                        }
                }
            }

            //определение возраста
            DateTime dateBirthDay = Convert.ToDateTime(us.birthday);
            int age = DateTime.Now.Year - dateBirthDay.Year;
            if (DateTime.Now.Month < dateBirthDay.Month ||
                (DateTime.Now.Month == dateBirthDay.Month && DateTime.Now.Day < dateBirthDay.Day)) age--;
            label10.Text += age;

            ts = CalculationOfResults(ts, us);

            label7.Text = ts.raw + " из 29";
            label8.Text = ts.iq;
            label9.Text = ts.sten;

            AddListOfErrors(gr, us, ts);
        }


        public Results(Group gr, User us, Test ts)
        {
            InitializeComponent();

            button1.Text = "Закрыть";
            //Задаем таблицу для перевода в стены и iq
            transferToSten = File.ReadAllLines("Sten.txt");
            transferToIq = File.ReadAllLines("iq.txt");

            ts = CalculationOfResults(ts, us);

            label7.Text = ts.raw + " из 29";
            label8.Text = ts.iq;
            label9.Text = ts.sten;
            label12.Text += ts.date;

            //определение возраста
            DateTime dateBirthDay = Convert.ToDateTime(us.birthday);
            int age = DateTime.Now.Year - dateBirthDay.Year;
            if (DateTime.Now.Month < dateBirthDay.Month ||
                (DateTime.Now.Month == dateBirthDay.Month && DateTime.Now.Day < dateBirthDay.Day)) age--;
            label10.Text += age;

            label3.Text += gr.number;
            label2.Text += us.surname + " " + us.name;

            AddListOfErrors(gr, us, ts);
        }

        public void AddListOfErrors(Group gr, User us, Test ts)
        {
            if (numbersOfErrors.Count != 0)
            {
                string path = @"resourses\";
                if (ts.variant == "A")
                    path += @"A\";
                if (ts.variant == "B")
                    path += @"B\";
                //заполняем список ошибок
                for (int i = 0; i < numbersOfErrors.Count; i++)
                {
                    int numberOfError = Convert.ToInt32(numbersOfErrors[i]);

                    //создаем группу необходимых контролов для отображения ошибки (надписи, картинки с ответами)

                    //groupBox для хранения ошибки
                    GroupBox gb = new GroupBox();
                    gb.Location = new Point(5, 30 + (i * 180));
                    gb.Size = new Size(360, 170);
                    gb.Name = i + "";
                    gb.Text = "Задание №" + numbersOfErrors[i];
                    panel1.Controls.Add(gb);

                    Label lb = new Label();
                    lb.Location = new Point(5, 85);
                    lb.Text = "Варианты ответа:";
                    gb.Controls.Add(lb);

                    //картинки с вопросами
                    for (int j = 0; j < 10; j++)
                    {
                        PictureBox pb = new PictureBox();

                        int x = 0;
                        int y = 0;
                        if (j < 4)
                        {
                            x = 5 + (j * 55);
                            y = 20;
                        }
                        if (j > 3)
                        {
                            x = 5 + ((j - 4) * 55);
                            y = 110;
                        }

                        //Параметры создаваемой картинки. Расположение, картинка и т.д.
                        pb.Location = new Point(x, y);
                        pb.Size = new Size(50, 50);
                        pb.Name = (j + 1) + "";
                        pb.BorderStyle = BorderStyle.FixedSingle;


                        //определение, на каком этапе была ошибка
                        int step = numberOfError / 5;
                        int numberPrimerInStep = numberOfError % 5;
                        if (numberPrimerInStep > 0)
                            step++;
                        if (numberPrimerInStep == 0)
                            numberPrimerInStep = 5;


                        int numberImage = ((numberPrimerInStep - 1) * 10) + (j + 1);
                        if (j == 3)
                        {
                            pb.ImageLocation = @"resourses\q.png";
                            gb.Controls.Add(pb);

                            //PictureBox pbr = new PictureBox();
                            //pbr.Location = new Point(x - 2, y - 2);
                            //pbr.Size = new Size(54, 54);
                            //pbr.ImageLocation = @"resourses\red.png";
                            //gb.Controls.Add(pbr);
                        }
                        else
                        {
                            pb.ImageLocation = path + step + @"\" + numberImage + ".png";
                            gb.Controls.Add(pb);
                        }

                        if (ts.result[numberOfError - 1].ToString() == (j - 3).ToString())
                        {
                            PictureBox pbr = new PictureBox();
                            pbr.Location = new Point(x - 2, y - 2);
                            pbr.Size = new Size(54, 54);
                            pbr.ImageLocation = @"resourses\red.png";
                            gb.Controls.Add(pbr);
                        }

                        string answer = "";
                        if (ts.variant == "A")
                            answer = "44652435426242354233312631452";
                        if (ts.variant == "B")
                            answer = "23263562335265413562561423165";

                        if (answer[numberOfError - 1].ToString() == (j - 3).ToString())
                        {
                            PictureBox pbg = new PictureBox();
                            pbg.Location = new Point(x - 2, y - 2);
                            pbg.Size = new Size(54, 54);
                            pbg.ImageLocation = @"resourses\green.png";
                            gb.Controls.Add(pbg);
                        }
                    }
                }
            }
            else
            {
                panel1.Visible = false;
                this.Width = 375;
            }
        }

        public Test CalculationOfResults(Test ts, User us)
        {
            numbersOfErrors.Clear();
            //последовательности правильных ответов
            string answer = "";
            if (ts.variant == "A")
                answer = "44652435426242354233312631452";
            if (ts.variant == "B")
                answer = "23263562335265413562561423165";

            //расчет сырых баллов
            int raw = 0;
            for (int i = 0; i < ts.result.Count(); i++)
            {
                if (ts.result[i] == answer[i])
                    raw++;
                else
                    numbersOfErrors.Add((i+1).ToString());
            }
            ts.raw = raw.ToString();

            //Сколько лет было на момент теста
            DateTime dateOfTest = Convert.ToDateTime(ts.date);
            DateTime dateBirthDay = Convert.ToDateTime(us.birthday);
            int age = dateOfTest.Year - dateBirthDay.Year;
            if (dateOfTest.Month < dateBirthDay.Month ||
                (dateOfTest.Month == dateBirthDay.Month && dateOfTest.Day < dateBirthDay.Day)) age--;

            if (age < 17 && age > 6)
            {
                //Расчет по шкале стена
                char sten = transferToSten[raw][age - 7];
                if (sten == '0')
                    ts.sten = "10";
                else
                    ts.sten = sten.ToString();

                //Расчет по шкале iq
                string[] iqs = transferToIq[raw].Split('*');
                ts.iq = iqs[age - 7];
            }
            else
            {
                ts.sten = "_";
                ts.iq = "_";
            }

            return ts;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
