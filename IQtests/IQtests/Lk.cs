using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.IO;

namespace IQtests
{
    public partial class Lk : Form
    {
        public List<Test> tests = new List<Test>();
        string[] transferToSten = null;
        string[] transferToIq = null;
        Group gr1 = null;
        User us1 = null;
        public string[] answers = { "44652435426242354233312631452", "23263562335265413562561423165" };

        public Lk(Group gr, User us)
        {
            InitializeComponent();

            gr1 = gr;
            us1 = us;

            label1.Text = "Группа: " + gr.number;
            label2.Text = "Ученик: " + us.surname + " " + us.name + " " + us.patron;
            chooseTestBox.SelectedIndex = 0;

            //определение возраста
            DateTime dateBirthDay = Convert.ToDateTime(us.birthday);
            int age = DateTime.Now.Year - dateBirthDay.Year;
            if (DateTime.Now.Month < dateBirthDay.Month ||
                (DateTime.Now.Month == dateBirthDay.Month && DateTime.Now.Day < dateBirthDay.Day)) age--;
            label3.Text = "Возраст: " + age;

            //Задаем таблицу для перевода в стены и iq
            transferToSten = File.ReadAllLines("Sten.txt");
            transferToIq = File.ReadAllLines("iq.txt");

            ReadTests(gr, us);

            FullPeriodDraw();

            DrawGroupsDynamic(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        public void DrawGroupsDynamic(DateTime startDate, DateTime endDate)
        {
            
            List<Group> groups = new List<Group>();

            groupsListBox.Items.Clear();
            groups.Clear();
            chart4.Series[0].Points.Clear();//очищаем график для постройки нового

            //Заполнение checkedlistbox'a именами групп
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("data.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode groupsNodes in xRoot)
            {
                Group gr = new Group();
                gr.id = Convert.ToInt32(groupsNodes.Attributes.GetNamedItem("id").Value);
                gr.number = groupsNodes.Attributes.GetNamedItem("number").Value;
                groups.Add(gr);

                groupsListBox.Items.Add(gr.number);
                groupsListBox.SetItemChecked(groupsListBox.Items.Count - 1, true);
            }

            int iGroup = 0;
            foreach (Object chgr in groupsListBox.CheckedItems)
            {
                foreach (XmlNode xGroups in xRoot)
                    if (xGroups.Attributes.GetNamedItem("number").Value == chgr.ToString())
                    {
                        List<Test> testsInGroup = new List<Test>();

                        foreach (XmlNode xUsers in xGroups)
                            foreach (XmlNode xTests in xUsers)
                            {
                                Test ts = new Test();
                                ts.id = Convert.ToInt32(xTests.Attributes.GetNamedItem("id").Value);
                                ts.name = xTests.Attributes.GetNamedItem("name").Value;
                                ts.variant = xTests.Attributes.GetNamedItem("variant").Value;
                                ts.date = xTests.Attributes.GetNamedItem("date").Value;
                                ts.result = xTests.InnerText;

                                testsInGroup.Add(ts);                                
                            }

                        DateTime j = startDate;
                        while (j <= endDate)
                        {
                            int summ = 0;
                            int count = 0;
                            int medium = 0;
                            foreach (Test ts in testsInGroup)
                            {
                                if (ts.date == j.ToShortDateString())
                                {
                                    string answer = "";
                                    //последовательности правильных ответов
                                    if (ts.variant == "A")
                                        answer = answers[0];
                                    if (ts.variant == "B")
                                        answer = answers[1];

                                    //расчет сырых баллов
                                    int raw = 0;
                                    for (int i = 0; i < ts.result.Count(); i++)
                                    {
                                        if (ts.result[i] == answer[i])
                                            raw++;
                                    }
                                    summ += raw;
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                medium = summ / count;
                                chart4.Series[iGroup].Points.AddXY(j, medium);
                            }
                            j = j.AddDays(1);
                        }
                        break;
                    }
                iGroup++;
            }

            /*
            DateTime j = startDate;            
            while (j <= endDate) //от дата заданного начала до даты конца с шагом в 1 день
            {
                int iGroup = 0;
                foreach (Group gr in groups)
                {
                    int summ = 0;
                    int count = 0;
                    int medium = 0;
                    foreach (XmlNode groupsNodes in xRoot)
                    {                        
                        if (groupsNodes.Attributes.GetNamedItem("id").Value == gr.id.ToString())
                            foreach (XmlNode usersNodes in groupsNodes)
                            {
                                int summUs = 0;
                                int countUs = 0;
                                int mediumUs = 0;

                                foreach (XmlNode testsNodes in usersNodes)
                                {
                                    if (j.CompareTo(Convert.ToDateTime(testsNodes.Attributes.GetNamedItem("date").Value)) == 0)
                                    {
                                        string answer = "";
                                        //последовательности правильных ответов
                                        if (testsNodes.Attributes.GetNamedItem("variant").Value == "A")
                                            answer = answers[0];
                                        if (testsNodes.Attributes.GetNamedItem("variant").Value == "B")
                                            answer = answers[1];

                                        //расчет сырых баллов
                                        int raw = 0;
                                        string resultTest = testsNodes.InnerText;
                                        for (int i = 0; i < resultTest.Count(); i++)
                                        {
                                            if (resultTest[i] == answer[i])
                                                raw++;
                                        }
                                        summUs += raw;
                                        countUs++;
                                    }
                                }
                                if (countUs != 0)
                                {
                                    mediumUs = summUs / countUs;
                                    count++;
                                    summ += mediumUs;
                                }
                            }                        
                    }
                    if (count != 0)
                    {
                        medium = summ / count;
                        chart4.Series[iGroup].Points.AddXY(j, medium);
                    }
                    iGroup++;
                }
                j = j.AddDays(1);
            }*/
        }

        public void ReadTests(Group gr, User us)
        {
            testBox.Items.Clear();
            tests.Clear();
            //Заполнение listbox'a результатами тестирований
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("data.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode groupsNodes in xRoot)
            {
                XmlNode attr = groupsNodes.Attributes.GetNamedItem("id");
                if (attr.Value == gr.id.ToString())
                    foreach (XmlNode usersNodes in groupsNodes)
                    {
                        attr = usersNodes.Attributes.GetNamedItem("id");
                        if (attr.Value == us.id.ToString())
                        {
                            foreach (XmlNode testsNodes in usersNodes)
                            {
                                Test ts = new Test();
                                attr = testsNodes.Attributes.GetNamedItem("id");
                                ts.id = Convert.ToInt32(attr.Value);
                                attr = testsNodes.Attributes.GetNamedItem("name");
                                ts.name = attr.Value;
                                attr = testsNodes.Attributes.GetNamedItem("variant");
                                ts.variant = attr.Value;
                                attr = testsNodes.Attributes.GetNamedItem("date");
                                ts.date = attr.Value;
                                
                                ts.result = testsNodes.InnerText;

                                tests.Add(ts);
                            }
                            break;
                        }
                    }
            }
            for (int i = 0; i < tests.Count; i++)
            {
                tests[i] = CalculationOfResults(tests[i], us);
                string s = tests[i].date + " | " + tests[i].name + " | Вариант: " + tests[i].variant + " | Верных ответов: " + tests[i].raw + " | Iq: " + tests[i].iq + " | Стэны: " + tests[i].sten;
                testBox.Items.Add(s);
            }

        }

        private void DrawTestsUserFunc(DateTime startDate, DateTime endDate)
        {
            chart1.Series[0].Points.Clear();//очищаем график для постройки нового
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            DateTime j = startDate;
            while (j <= endDate) //от дата заданного начала до даты конца с шагом в 1 день
            {
                int rawCounter = 0;
                int iqCounter = 0;
                int stenCounter = 0;
                int rawResult = 0;
                int iqResult = 0;
                int stenResult = 0;
                for (int i = 0; i < tests.Count; i++) //проходим по всем пройденным тастам, и если в эту дату был тест, добавляем к счетчику в этот день
                {
                    DateTime dateOfTest = Convert.ToDateTime(tests[i].date);

                    if (j.CompareTo(dateOfTest) == 0)
                    {
                        rawCounter++;
                        rawResult += Convert.ToInt32(tests[i].raw);
                        if (tests[i].iq != "_")
                        {
                            iqCounter++;
                            iqResult += Convert.ToInt32(tests[i].iq);
                        }
                        if (tests[i].sten != "_")
                        {
                            stenCounter++;
                            stenResult += Convert.ToInt32(tests[i].sten);
                        }
                    }
                }     

                if (rawCounter != 0)
                {
                    int rawMed = rawResult / rawCounter;
                    chart1.Series[0].Points.AddXY(j, rawMed); //добавляем точку на график
                }
                
                if (iqCounter != 0)
                {
                    int iqMed = iqResult / iqCounter;
                    chart2.Series[0].Points.AddXY(j, iqMed); //добавляем точку на график
                }

                if (stenCounter != 0)
                {
                    int stenMed = stenResult / stenCounter;
                    chart3.Series[0].Points.AddXY(j, stenMed); //добавляем точку на график
                }
                
                j = j.AddDays(1);
            }
        }

        public void FullPeriodDraw()
        {
            if (tests.Count > 0)
            {
                //находим диапазон времени, в котором ученик проходил тестирования
                DateTime minDT = Convert.ToDateTime(tests[0].date);
                DateTime maxDT = Convert.ToDateTime(tests[0].date);
                foreach (Test ts in tests)
                {
                    if (Convert.ToDateTime(ts.date) > maxDT)
                        maxDT = Convert.ToDateTime(ts.date);
                    if (Convert.ToDateTime(ts.date) < minDT)
                        minDT = Convert.ToDateTime(ts.date);
                }

                dateTimePicker1.Value = minDT;
                dateTimePicker2.Value = maxDT;
                DrawTestsUserFunc(minDT, maxDT);
            }
        }

        public Test CalculationOfResults(Test ts, User us)
        {
            string answer = "";
            //последовательности правильных ответов
            if(ts.variant == "A")
                answer = answers[0];
            if (ts.variant == "B")
                answer = answers[1];

            //расчет сырых баллов
            int raw = 0;
            for (int i = 0; i < ts.result.Count(); i++)
            {
                if (ts.result[i] == answer[i])
                    raw++;
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

        private void startButton_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + @"\resourses\test";
            DialogResult dr = MessageBox.Show("Пройти обучение?", "Обучение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                LearnIP lip = new LearnIP("training", "0","");
                this.Hide();
                File.WriteAllText(path, gr1.id + " " + us1.id);
                lip.ShowDialog();
                ReadTests(gr1, us1);
                FullPeriodDraw();
                DrawGroupsDynamic(dateTimePicker4.Value, dateTimePicker3.Value);
                this.Show();
            }
            if (dr == DialogResult.No)
            {
                testBox.Items.Clear();
                ChooseVar cv = new ChooseVar();
                this.Hide();
                File.WriteAllText(path, gr1.id + " " + us1.id);
                cv.ShowDialog();
                ReadTests(gr1, us1);
                FullPeriodDraw();
                DrawGroupsDynamic(dateTimePicker4.Value, dateTimePicker3.Value);
                this.Show();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DrawTestsUserFunc(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DrawTestsUserFunc(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FullPeriodDraw();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            DrawGroupsDynamic(dateTimePicker4.Value, dateTimePicker3.Value);
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DrawGroupsDynamic(dateTimePicker4.Value, dateTimePicker3.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ind = testBox.SelectedIndex;
            if (ind != -1)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;
                // обход всех узлов в корневом элементе
                foreach (XmlNode xGroup in xRoot)
                {
                    XmlNode attr = xGroup.Attributes.GetNamedItem("id");
                    if (attr.Value == gr1.id.ToString())                    
                        foreach (XmlNode xUser in xGroup)
                        {
                            attr = xUser.Attributes.GetNamedItem("id");
                            if (attr.Value == us1.id.ToString())                            
                                foreach (XmlNode xTest in xUser)
                                {
                                    attr = xTest.Attributes.GetNamedItem("id");
                                    if (attr.Value == tests[ind].id.ToString())
                                    {
                                        xUser.RemoveChild(xTest);
                                        xDoc.Save("data.xml");
                                        break;
                                    }
                                }                            
                        }               
                }
            }

            ReadTests(gr1,us1);
            FullPeriodDraw();
            DrawGroupsDynamic(dateTimePicker4.Value, dateTimePicker3.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (testBox.SelectedIndex != -1)
            {
                Results rs = new Results(gr1, us1, tests[testBox.SelectedIndex]);
                rs.Show();
            }
            else
                MessageBox.Show("Выберите тест для подробного отчета");       
        }
    }
}
