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
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Globalization;

namespace DocumentationLashchenkov
{
    public partial class Hired : Form
    {
        private string[,] allKorshuns = null;
        public string thistime = "";
        public List<uvalParam> uvalParams = new List<uvalParam>();
        public List<vacParam> vacParams = new List<vacParam>();
        public List<vacParam> vacParamsDay = new List<vacParam>();

        public class uvalParam
        {
            public string id;
            public string time;

            public uvalParam(string id1, string time1)
            {
                id = id1;
                time = time1;
            }
        }

        public class vacParam
        {
            public int id;
            public int korshId;
            public DateTime startDate;
            public DateTime endDate;
            public bool otmetka;
        }

        public Hired()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label1.Text = "Выбранная дата: " + monthCalendar1.SelectionStart.ToLongDateString(); //меняем текст для наглядности

            ReadFromFile(monthCalendar1.SelectionStart.ToLongDateString());
            UpdateVacatorsList();
        }

        private void ReadFromFile(string date)
        {
            uvalParams.Clear();

            if (File.Exists("Данные/" + date))
            {
                string[] uvalFile = File.ReadAllLines("Данные/" + date);

                for (int i = 0; i < uvalFile.Count(); i = i + 2)
                {
                    uvalParam up = new uvalParam(uvalFile[i], uvalFile[i + 1]);
                    uvalParams.Add(up);
                }

            }

            UpdateListBoxs();
        }

        private void tableLSBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableLSBindingSource.EndEdit();
            this.tableAdapterManager1.UpdateAll(this.korshunyDataSet);
        }

        private void Hired_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.VacTable". При необходимости она может быть перемещена или удалена.
            this.vacTableTableAdapter.Fill(this.korshunyDataSet.VacTable);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.TableLS". При необходимости она может быть перемещена или удалена.
            this.tableLSTableAdapter1.Fill(this.korshunyDataSet.TableLS);
            allKorshuns = new string[tableLSDataGridView1.ColumnCount, tableLSDataGridView1.RowCount];
            label1.Text = "Выбранная дата: " + monthCalendar1.TodayDate.ToLongDateString(); //меняем текст для наглядности
            monthCalendar1.SelectionStart = monthCalendar1.TodayDate;

            ReadKorshuns();
            UpdateVacatorsList();
        }

        private void ReadKorshuns()
        {
            for (int i = 0; i < tableLSDataGridView1.ColumnCount; i++) //составляем массив со всеми возможными коршунами
                for (int j = 0; j < tableLSDataGridView1.RowCount - 1; j++)
                    allKorshuns[i, j] = tableLSDataGridView1[i, j].Value.ToString();

            for (int i = 0; i < tableLSDataGridView1.RowCount - 1; i++) //удаляем лишние пробелы из ячеек бд
            {
                int index = allKorshuns[3, i].IndexOf(Convert.ToChar(" "));//индекс первого пробела
                allKorshuns[3, i] = allKorshuns[3, i].Remove(index + 1);

                index = allKorshuns[6, i].IndexOf(Convert.ToChar(" "));//индекс первого пробела
                allKorshuns[6, i] = allKorshuns[6, i].Remove(index + 1);

                index = allKorshuns[1, i].IndexOf(Convert.ToChar(" "));//индекс первого пробела
                allKorshuns[1, i] = allKorshuns[1, i].Remove(index);
            }

            ReadFromFile(monthCalendar1.SelectionStart.ToLongDateString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GoToUval("21", "00");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GoToUval("14", "00");
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //строка-фильтр
        {
            Filter();
        }

        private void Filter()
        {
            UpdateListBoxs();

            for (int i = listBox1.Items.Count - 1; i >= 0; i--) //двигаемся с нижней строки, чтобы при удалении строки, у оставшихся не менялся индекс
                if (!listBox1.Items[i].ToString().ToLower().Contains(textBox1.Text.ToLower())) //Если строка из listbox не содержит в себе строки из textbox'a поиска, удаляем ее из listbox. 
                {
                    listBox1.Items.RemoveAt(i);
                    listBox3.Items.RemoveAt(i);
                }
            listBox1.SelectedIndex = -1;
        }

        private void UpdateListBoxs()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox6.Items.Clear();

            string vzvod = "0";
            if (radioButton2.Checked)
                vzvod = "1";
            if (radioButton3.Checked)
                vzvod = "2";
            if (radioButton4.Checked)
                vzvod = "3";

            List<uvalParam> uvalParams1 = new List<uvalParam>();
            for (int i = 0; i < uvalParams.Count; i++)
                uvalParams1.Add(uvalParams[i]);

            uvalParams.Clear();

            for (int i = 0; i < tableLSDataGridView1.RowCount - 1; i++) //добавляем в листбоксы список коршунов, не идущих в увал
            {
                bool check = true;

                for (int j = 0; j < uvalParams1.Count; j++)
                    if (allKorshuns[0, i] == uvalParams1[j].id) //если такой элемент присутствует в списке увольняемых, добавлем его в листбоксы увольняемых
                    {
                        check = false;
                        break;
                    }

                if (check)
                {
                    if (vzvod == allKorshuns[2, i] || vzvod == "0")
                    {
                        listBox1.Items.Add(allKorshuns[3, i] + allKorshuns[4, i]);
                        listBox3.Items.Add(allKorshuns[0, i]);
                    }
                }
                else
                {
                    listBox2.Items.Add(allKorshuns[3, i] + allKorshuns[4, i]);


                    for (int z = 0; z < uvalParams1.Count; z++)
                        if (uvalParams1[z].id == allKorshuns[0, i])
                        {
                            listBox6.Items.Add(uvalParams1[z].time);
                            uvalParams.Add(uvalParams1[z]);
                        }
                }
            }

            int vacatorsCount = listBox2.Items.Count;
            label4.Text = "Всего: " + vacatorsCount + " человек";


            //обновляем файл с данными увольняемыми
            string filepath = "Данные/" + monthCalendar1.SelectionStart.ToLongDateString();
            File.Delete(filepath);

            for (int i = 0; i < uvalParams.Count(); i++)
                File.AppendAllText(filepath, uvalParams[i].id + "\n" + uvalParams[i].time + "\n");

            Sorter();
        }

        protected void Sorter() //ДА НАЧНЕТСЯ СОРТИРОВКА!
        {
            List<string> sortNames = new List<string>();

            for (int i = 0; i < listBox1.Items.Count; i++)
                sortNames.Add(listBox1.Items[i].ToString() + "*" + listBox3.Items[i].ToString());

            sortNames.Sort();

            listBox1.Items.Clear();
            listBox3.Items.Clear();
            for (int i = 0; i < sortNames.Count; i++)
            {
                string[] s = sortNames[i].Split(Convert.ToChar("*"));
                listBox1.Items.Add(s[0]);
                listBox3.Items.Add(s[1]);
            }

            sortNames.Clear();
            //Сортировка для списка увольняемых
            for (int i = 0; i < uvalParams.Count; i++)
                sortNames.Add(listBox6.Items[i].ToString() + "*"
                                + listBox2.Items[i].ToString() + "*"
                                        + uvalParams[i].id);

            sortNames.Sort();

            listBox6.Items.Clear();
            listBox2.Items.Clear();
            uvalParams.Clear();
            for (int i = 0; i < sortNames.Count; i++)
            {
                string[] s = sortNames[i].Split(Convert.ToChar("*"));
                listBox6.Items.Add(s[0]);
                listBox2.Items.Add(s[1]);

                uvalParam up = new uvalParam(s[2], s[0]);
                uvalParams.Add(up);
            }

            sortNames.Clear();
        }

        private void GoToUval(int hour, int minute, bool frm)
        {
            if (listBox1.SelectedIndex != -1 && frm)
            {
                TimeForm timefrm = new TimeForm(hour, minute);
                DialogResult dlr = timefrm.ShowDialog(this);
                if (dlr == DialogResult.OK)
                {
                    //добавляем коршуна в список идущих в увал                    
                    uvalParam up = new uvalParam(listBox3.Items[listBox1.SelectedIndex].ToString(), thistime);
                    uvalParams.Add(up);
                    UpdateListBoxs();
                }
            }
        }

        private void GoToUval(string hour, string minute)
        {
            if (listBox1.SelectedIndex != -1)
            {
                thistime = hour + ":" + minute;
                //добавляем коршуна в список идущих в увал                    
                uvalParam up = new uvalParam(listBox3.Items[listBox1.SelectedIndex].ToString(), thistime);
                uvalParams.Add(up);
                UpdateListBoxs();

                textBox1.Text = "";//сбрасываем "фильтр"
            }
        }

        private void GoToRota()
        {
            if (listBox2.SelectedIndex != -1)
            {
                uvalParams.RemoveAt(listBox2.SelectedIndex);

                UpdateListBoxs();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if (index != -1)
            {
                GoToRota();

                if (index != 0)
                    listBox2.SelectedIndex = index - 1;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox6.SelectedIndex = listBox2.SelectedIndex;
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox6.SelectedIndex;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GoToUval("17", "00");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GoToUval(18, 0, true);
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private Word._Application application;
        private Word._Document document;
        
        public static int GetWeekNumber(DateTime dtPassed)//определение номера недели
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        private void Dovolstvie()
        {

            {
                Object missingObj = System.Reflection.Missing.Value;
                Object falseObj = false;

                //создаем обьект приложения word
                application = new Word.Application();
                // создаем путь к файлу
                string path = Path.GetFullPath(@"Данные\Шаблоны\Снятие с довольствия (шаблон).docx");
                Object templatePathObj = path;

                // если вылетим не этом этапе, приложение останется открытым
                try
                {
                    document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
                }
                catch (Exception error)
                {
                    document.Close(ref falseObj, ref missingObj, ref missingObj);
                    application.Quit(ref missingObj, ref missingObj, ref missingObj);
                    document = null;
                    application = null;
                    throw error;
                }

                //выбираем закладки и вставляем туда текст
                document.Bookmarks["date"].Select();
                string date = monthCalendar1.SelectionStart.ToLongDateString();
                date = '\u00ab' + monthCalendar1.SelectionStart.Day.ToString() + '\u00bb' + date.Substring(date.IndexOf(' '));
                application.Selection.TypeText(date);

                document.Bookmarks["when"].Select();
                string when = "";
                if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
                    when = "завтрака, обеда и ужина";
                else if (checkBox2.Checked && checkBox3.Checked)
                    when = "обеда и ужина";
                else if (checkBox1.Checked && checkBox3.Checked)
                    when = "завтрака и ужина";
                else if (checkBox1.Checked && checkBox2.Checked)
                    when = "завтрака и обеда";
                else if (checkBox3.Checked)
                    when = "ужина";
                else if (checkBox2.Checked)
                    when = "обеда";
                else if (checkBox1.Checked)
                    when = "завтрака";

                application.Selection.TypeText(when);

                document.Bookmarks["peopleCount"].Select();
                string peopleCount = uvalParams.Count.ToString();
                application.Selection.TypeText(peopleCount);

                //создаем новый лист для снятия с довольствия, чтобы его отсортировать    
                List<string> sortList = new List<string>();

                for (int i = 0; i < uvalParams.Count; i++)
                    for (int j = 0; j < tableLSDataGridView1.RowCount - 1; j++)
                        if (uvalParams[i].id == allKorshuns[0, j])
                        {
                            string mergedStr = ""; //добавляем в строку звание
                            if (allKorshuns[1, j] == "ефрейтор")
                                mergedStr += "ефрейтора*";
                            else
                                if (allKorshuns[1, j] == "рядовой")
                                mergedStr += "рядового*";

                            mergedStr += allKorshuns[6, j]; //добавляем фамилию в родительном падеже
                            //добавляем первую букву имени и отчества
                            mergedStr += allKorshuns[4, j].Substring(0, 1) + "." + allKorshuns[5, j].Substring(0, 1) + ".";

                            sortList.Add(mergedStr);
                            break;
                        }
                sortList.Sort(); //сортируем список

                //заполняем таблицу в документе
                for (int i = 0; i < sortList.Count; i++)
                {
                    string[] split = new string[2];
                    split = sortList[i].Split(Convert.ToChar("*"));

                    if (i != 0)
                        document.Tables[1].Rows.Add(ref missingObj);

                    document.Tables[1].Cell(i + 1, 1).Select();
                    application.Selection.TypeText(split[0]);

                    document.Tables[1].Cell(i + 1, 2).Select();
                    application.Selection.TypeText(split[1]);
                }

                //составляем путь для сохранения файла
                string str = Environment.CurrentDirectory;
                str = str.Remove(str.LastIndexOf((@"\"))); //поднимаемся на один каталог выше расположения программы                        
                string nameOfDirectory = @"\4. Снятие с довольствия увольняемых (СРЕДА)\";
                if (!Directory.Exists(str + nameOfDirectory))
                    Directory.CreateDirectory(str + nameOfDirectory);

                int weekNumber = GetWeekNumber(monthCalendar1.SelectionStart) + 1;
                string nameOfFile = weekNumber + ".Снятие с довольствия(" + monthCalendar1.SelectionStart.ToShortDateString() + ").doc";
                str = str + nameOfDirectory + nameOfFile;

                Object pathToSaveObj = str;

                if (File.Exists(str))
                {
                    DialogResult dlr =
                        MessageBox.Show("Файл с этой датой уже существует. Перезаписать?", "Файл уже существует", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlr == DialogResult.Yes)
                        document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);
                }
                else
                    document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);

                application.Visible = true;
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            Dovolstvie();
        }

        private void VacationDoc(List<vacParam> VacatorsDay)
        {
            Object missingObj = System.Reflection.Missing.Value;
            Object falseObj = false;

            //создаем обьект приложения word
            application = new Word.Application();
            // создаем путь к файлу
            string path = Path.GetFullPath(@"Данные\Шаблоны\Отпускники (шаблон).docx");
            Object templatePathObj = path;

            // если вылетим не этом этапе, приложение останется открытым
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                document.Close(ref falseObj, ref missingObj, ref missingObj);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                document = null;
                application = null;
                throw error;
            }

            int i = 0;
            //заполняем таблицу в документе 
            foreach (vacParam vp in VacatorsDay)
            {                
                if (i != 0)
                    document.Tables[1].Rows.Add(ref missingObj);

                for (int j = 0; j < tableLSDataGridView1.RowCount - 1; j++)
                    if (tableLSDataGridView1[0, j].Value.ToString() == vp.korshId.ToString())
                    {
                        document.Tables[1].Cell(i + 2, 2).Select(); //+2 потому что заполнение начинается со 2 строки таблицы
                        application.Selection.TypeText(allKorshuns[1, j]);

                        document.Tables[1].Cell(i + 2, 3).Select();
                        application.Selection.TypeText(allKorshuns[3, j] + allKorshuns[4, j].Substring(0,1) + "." + allKorshuns[5, j].Substring(0, 1) + ".");
                        
                        break;
                    }
                
                document.Tables[1].Cell(i + 2, 4).Select();
                application.Selection.TypeText(vp.endDate.ToShortDateString().ToString() + " 20:00");

                if (!vp.otmetka)
                {
                    document.Tables[1].Cell(i + 2, 5).Select();
                    application.Selection.TypeText("Без отметки");
                }

                i++;
            }

            //выбираем закладки и вставляем туда текст
            document.Bookmarks["dateStart"].Select();
            string date = VacatorsDay[0].startDate.ToLongDateString();
            date = '\u00ab' + VacatorsDay[0].startDate.Day.ToString() + '\u00bb' + date.Substring(date.IndexOf(' '));
            application.Selection.TypeText(date);
                      


            document.Bookmarks["what"].Select();
            if (VacatorsDay[0].startDate.ToShortDateString() == monthCalendar1.SelectionStart.ToShortDateString())
            {
                application.Selection.TypeText("убывающего");
                document.Bookmarks["otmetka"].Select();
                application.Selection.TypeText(monthCalendar1.SelectionStart.AddDays(1).ToShortDateString());
            }
            else
            {
                application.Selection.TypeText("убывшего");
                document.Bookmarks["otmetka"].Select();
                application.Selection.TypeText(monthCalendar1.SelectionStart.ToShortDateString());
            }


            object missing = Missing.Value;
            object what = Word.WdGoToItem.wdGoToLine;
            object which = Word.WdGoToDirection.wdGoToLast;
            Word.Range endRange = document.GoTo(ref what, ref which, ref missing, ref missing);

            //составляем путь для сохранения временного файла
            string strVr = Environment.CurrentDirectory + @"\Данные\Шаблоны\temp.doc";
            Object pathToSaveObjVr = strVr;
            //Вставка в другой файл
            endRange.InsertFile(strVr);
            //сохраняем во временный файл
            document.SaveAs(ref pathToSaveObjVr, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);
                        
            //закрытие файла
            document.Close(ref falseObj, ref missingObj, ref missingObj);
            application.Quit(ref missingObj, ref missingObj, ref missingObj);
            document = null;
            application = null;
        }

        private void vacationButton_Click(object sender, EventArgs e)
        {
            Object missingObj = System.Reflection.Missing.Value;
            Object falseObj = false;

            //создаем обьект приложения word
            application = new Word.Application();
            // создаем путь к файлу
            string path = Path.GetFullPath(@"Данные\Шаблоны\Увольняемые (шаблон).docx");
            Object templatePathObj = path;

            //Новая форма прогресса
            Progress prgfrm = new Progress();
            prgfrm.Show();

            prgfrm.label1.Text = "Запуск процесса Word";

            // если вылетим не этом этапе, приложение останется открытым
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                document.Close(ref falseObj, ref missingObj, ref missingObj);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                document = null;
                application = null;
                throw error;
            }
            
            prgfrm.label1.Text = "Сортировка списка увольняемых";



            //создаем новый лист для списка увольнения, чтобы его отсортировать    
            List<string> sortList = new List<string>();

            for (int i = 0; i < uvalParams.Count; i++)
                for (int j = 0; j < tableLSDataGridView1.RowCount - 1; j++)
                    if (uvalParams[i].id == allKorshuns[0, j])
                    {
                        string mergedStr = "";
                        //время увольнения
                        mergedStr += uvalParams[i].time + "*";
                        //добавляем звание
                        mergedStr += allKorshuns[1, j] + "*";
                        //добавляем фамилию и первую букву имени и отчества
                        mergedStr += allKorshuns[3, j] + allKorshuns[4, j].Substring(0, 1) + "." + allKorshuns[5, j].Substring(0, 1) + ".*";
                        //телефон
                        mergedStr += allKorshuns[7, j];
                        

                        sortList.Add(mergedStr);
                        break;
                    }
            sortList.Sort();

            prgfrm.progressBar1.Value = 5;
            prgfrm.label1.Text = "Заполнение списка увольняемых";
            int stepValueForUval = 75/sortList.Count;

            //заполняем таблицу в документе 
            for (int i = 0; i < sortList.Count; i++)
            {
                string[] split = new string[4];
                split = sortList[i].Split(Convert.ToChar("*"));

                if (i != 0)
                    document.Tables[1].Rows.Add(ref missingObj);

                document.Tables[1].Cell(i + 6, 2).Select(); //+6 потому что заполнение начинается с 6 строки таблицы
                application.Selection.TypeText(split[1]);

                document.Tables[1].Cell(i + 6, 3).Select();
                application.Selection.TypeText(split[2]);

                document.Tables[1].Cell(i + 6, 4).Select();
                application.Selection.TypeText(split[3]);

                document.Tables[1].Cell(i + 6, 5).Select();
                application.Selection.TypeText(split[0]);

                prgfrm.progressBar1.Value += stepValueForUval;
            }


            //выбираем закладки и вставляем туда текст
            document.Bookmarks["date2"].Select();
            string date = monthCalendar1.SelectionStart.ToLongDateString();
            date = '\u00ab' + monthCalendar1.SelectionStart.Day.ToString() + '\u00bb' + date.Substring(date.IndexOf(' '));
            application.Selection.TypeText(date);

            document.Bookmarks["date1"].Select();
            application.Selection.TypeText(date.Remove(date.Count() - 3));

            //составляем путь для сохранения временного файла
            string strVr = Environment.CurrentDirectory + @"\Данные\Шаблоны\temp.doc";
            Object pathToSaveObjVr = strVr;

            //сохраняем во временный файл
            document.SaveAs(ref pathToSaveObjVr, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);


            document.Close(ref falseObj, ref missingObj, ref missingObj);
            application.Quit(ref missingObj, ref missingObj, ref missingObj);
            document = null;
            application = null;

            prgfrm.label1.Text = "Заполнение списков отпускников";

            //документы с отпускниками
            if (checkBox4.Checked && vacParamsDay.Count > 0)
            {
                List<vacParam> vacatorsDayAll = vacParamsDay;
                
                while (true)
                {
                    DateTime startDate = vacatorsDayAll[0].startDate;
                    foreach (vacParam vp in vacatorsDayAll)
                    {
                        if (vp.startDate > startDate)
                            startDate = vp.startDate;
                    }

                    List<vacParam> vacatorsNow = new List<vacParam>();
                    for (int i = 0; i < vacatorsDayAll.Count; i++)
                        if (vacatorsDayAll[i].startDate == startDate)
                        {
                            vacatorsNow.Add(vacatorsDayAll[i]);
                            vacatorsDayAll.RemoveAt(i);
                            i--;
                        }

                    VacationDoc(vacatorsNow);

                    if(prgfrm.progressBar1.Value <= 90) // коряво, можно сделать красиво, но это добавится еще кусок кода строк на 10 и ухудшится производительность
                        prgfrm.progressBar1.Value += 5;

                    if (vacatorsDayAll.Count == 0)
                        break;
                }
            }
            
            application = new Word.Application();
            document = application.Documents.Add(ref pathToSaveObjVr, ref missingObj, ref missingObj, ref missingObj);

            prgfrm.progressBar1.Value = 95;
            prgfrm.label1.Text = "Исправление ошибок форматирования";
            //Исправление ошибок форматирования, которые появляются хер знает почему при объединении файлов
            //увеличивается шрифт в таблице
            int tablesCount = document.Tables.Count;
            Word.Range rng = document.Tables[tablesCount].Range;
            rng.Font.Size = 11;
            //и подчеркивается заголовок
            rng = document.Range();
            rng.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            //Прям очень коряво. Но кому сейчас легко?

            //составляем путь для сохранения файла
            string str = Environment.CurrentDirectory;
            str = str.Remove(str.LastIndexOf((@"\"))); //поднимаемся на один каталог выше расположения программы                        
            string nameOfDirectory = @"\5. Увольняемые\";
            if (!Directory.Exists(str + nameOfDirectory))
                Directory.CreateDirectory(str + nameOfDirectory);

            int weekNumber = GetWeekNumber(monthCalendar1.SelectionStart) + 1;
            string nameOfFile = weekNumber + ".Увольняемые(" + monthCalendar1.SelectionStart.ToShortDateString() + ").doc";
            str = str + nameOfDirectory + nameOfFile;
            Object pathToSaveObj = str;

            prgfrm.progressBar1.Value = 100;
            prgfrm.label1.Text = "Готово!";
            if (File.Exists(str))
            {
                DialogResult dlr =
                    MessageBox.Show("Файл с этой датой уже существует. Перезаписать?", "Файл уже существует", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                    document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);
                else
                    //сохраняем во временный файл
                    document.SaveAs(ref pathToSaveObjVr, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);
            }
            else
                document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);


            application.Visible = true;

            UpdateVacatorsList();

            prgfrm.Close();


        }

        private void vacButton_Click(object sender, EventArgs e)
        {
            AddVacator newfrm = new AddVacator(monthCalendar1.SelectionStart);
            DialogResult dr = newfrm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                // TODO: данная строка кода позволяет загрузить данные в таблицу "korshunyDataSet.VacTable". При необходимости она может быть перемещена или удалена.
                this.vacTableTableAdapter.Fill(this.korshunyDataSet.VacTable);
                UpdateVacatorsList();
            }
        }

        private void UpdateVacatorsList()
        {
            vacatorsList.Items.Clear();
            vacParams.Clear();
            vacParamsDay.Clear();
            for (int i = 0; i < vacTableDataGridView.RowCount - 1; i++)
            {
                vacParam vp = new vacParam();
                vp.id = Convert.ToInt32(vacTableDataGridView[0, i].Value);
                vp.korshId = Convert.ToInt32(vacTableDataGridView[1, i].Value);
                vp.startDate = Convert.ToDateTime(vacTableDataGridView[2, i].Value);
                vp.endDate = Convert.ToDateTime(vacTableDataGridView[3, i].Value);
                vp.otmetka = Convert.ToBoolean(vacTableDataGridView[4, i].Value);

                vacParams.Add(vp);

                if (monthCalendar1.SelectionStart <= vp.endDate &&
                    monthCalendar1.SelectionStart >= vp.startDate)
                {
                    string nameVac = "";
                    for (int j = 0; j < tableLSDataGridView1.RowCount - 1; j++)
                        if (tableLSDataGridView1[0, j].Value.ToString() == vp.korshId.ToString())
                        {
                            nameVac = allKorshuns[3, j] + allKorshuns[4, j];
                            break;
                        }
                    vacParamsDay.Add(vp);
                    vacatorsList.Items.Add(vp.startDate.ToShortDateString() + "-" + vp.endDate.ToShortDateString() + " | " + nameVac);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (vacatorsList.SelectedIndex != -1)
            {
                vacParam vp = vacParamsDay[vacatorsList.SelectedIndex];
                
                this.vacTableTableAdapter.Delete(vp.id, vp.korshId, vp.startDate, vp.endDate, vp.otmetka);
                this.vacTableTableAdapter.Fill(this.korshunyDataSet.VacTable);

                UpdateVacatorsList();
            }
        }

        private void Hired_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.D1:
                    button1.PerformClick();// имитируем нажатие кнопки "Увал"
                    break;

                case Keys.D2:
                    button3.PerformClick();// имитируем нажатие кнопки "в храм"
                    break;

                case Keys.D3:
                    button5.PerformClick();// имитируем нажатие кнопки "Почта"
                    break;

                case Keys.D4:
                    button6.PerformClick();//нажатие кнопки "Другое"
                    break;

                case Keys.D5:
                    button2.PerformClick();//кнопка "<<<"
                    break;
            }
        }
    }
}
