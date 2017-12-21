using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IQtests
{
    public partial class LearnIP : Form
    {
        public List<PictureBox> boxes = new List<PictureBox>();
        public List<GroupBox> groupboxes = new List<GroupBox>();
        int[] answers = { 0, 0, 0, 0, 0 };
        string variant1 = "";
        string step1 = "";
        string results1 = "";

        public LearnIP(string variant, string step, string results)
        {
            InitializeComponent();

            results1 = results;
            variant1 = variant;
            step1 = step;

            label1.Text = "Этап " + step + " из 6";

            for (int i = 0; i < 5; i++) //Создаем контролы для хранения картинок с задачами
            {
                GroupBox gb = new GroupBox();
                gb.Location = new Point(10, 30 + (i * 80));
                gb.Size = new Size(700,75);
                gb.Name = i + "";
                gb.Text = "Пример " + (i + 1);
                Controls.Add(gb);
                groupboxes.Add(gb);
            }

            string path = "";
            if (variant == "training")
            {
                path = @"resourses\training\";
                groupboxes[4].Visible = false;
                button1.Text = "Закончить";
                pictureBox1.Visible = true;
                label1.Visible = false;
            }
            if (variant == "A")
                path = @"resourses\A\" + step + @"\";
            if (variant == "B")
                path = @"resourses\B\" + step + @"\";

            if (Convert.ToInt32(step) == 6)
            {
                button1.Text = "Закончить";
                groupboxes[4].Visible = false;
            }

            
            for (int j = 0; j < groupboxes.Count; j++) //создаем картинки с тестом
            {
                for (int i = 0; i < 10; i++)
                {
                    PictureBox pb = new PictureBox();

                    int x = 10 + (i * 55);
                    if (i % 10 > 3)
                        x += 130;                    

                    //Параметры создаваемой картинки. Расположение, картинка и т.д.
                    pb.Location = new Point(x, 15);
                    pb.Size = new Size(50, 50);
                    int numberImage = (j * 10) + (i + 1);
                    pb.ImageLocation = path + numberImage + ".png";
                    pb.Name = (i + 1) + "";
                    pb.BorderStyle = BorderStyle.FixedSingle;

                    groupboxes[j].Controls.Add(pb);
                    boxes.Add(pb);

                    if (i % 10 == 3) //добавляем под каждую картинку вопроса красный квадрат для рамки
                    {
                        PictureBox pbr = new PictureBox();
                        pbr.Location = new Point(x - 2, 13);
                        pbr.Size = new Size(54, 54);
                        pbr.ImageLocation = @"resourses\red.png";
                        pb.ImageLocation = @"resourses\q.png";
                        pb.Click += new System.EventHandler(picQ_Click);
                        groupboxes[j].Controls.Add(pbr);
                    }
                    
                    //обработчик для нажатия на одну из картинок выбора
                    if (i % 10 > 3)
                            pb.Click += new System.EventHandler(pic_Click);
                }
            }            
        }

        private void picQ_Click(object sender, EventArgs e)
        {
            (sender as PictureBox).ImageLocation = @"resourses\q.png";
            int rowNumber = Convert.ToInt32((sender as PictureBox).Parent.Name); //по имени контрола-родителя получаем в какой из задач мы нажали на картинку
            answers[rowNumber] = 0;
        }

        private void pic_Click(object sender, EventArgs e)
        {
            int rowNumber = Convert.ToInt32((sender as PictureBox).Parent.Name); //по имени контрола-родителя получаем в какой из задач мы нажали на картинку
            answers[rowNumber] = Convert.ToInt32((sender as PictureBox).Name) - 4; //по имени картинки записываем в массив выбранный ответ
            int numberOfQuestBox = (rowNumber * 10) + 3;
            boxes[numberOfQuestBox].ImageLocation = (sender as PictureBox).ImageLocation; //заменяем картинку в отете на выбранную нами
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (variant1 == "training")
            {
                string trueAnswer = "6443";

                int counter = 0;
                string message = "Ошибки в примерах номер: ";
                for (int i = 0; i < 4; i++)
                {
                    if (trueAnswer[i].ToString() != answers[i].ToString())
                    {
                        if (counter != 0)
                            message += ", ";
                        counter++;
                        message += (i + 1);
                    }
                }
                if (counter == 0)
                    message = "Без ошибок";

                DialogResult dr = MessageBox.Show("Тренировка завершена!\n" + message + "\n\n Повторить тренировку?", "Тренировка завершена", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                    for (int i = 0; i < 5; i++)
                    {
                        answers[i] = 0;
                        boxes[(i * 10) + 3].ImageLocation = @"resourses\q.png";
                    }
                else
                {
                    ChooseVar cv = new ChooseVar();
                    this.Hide();
                    cv.ShowDialog();
                    this.Close();
                }

            }
            else
            {
                if (Convert.ToInt32(step1) == 6)
                {
                    for (int i = 0; i < 4; i++)
                        results1 += answers[i].ToString();

                    Results res = new Results(results1, variant1);
                    this.Hide();
                    res.ShowDialog();
                    this.Close();
                }
                else
                { 
                    step1 = (Convert.ToInt32(step1) + 1).ToString();

                    foreach (int i in answers)
                        results1 += i.ToString();

                    this.Hide();
                    LearnIP lip = new LearnIP(variant1, step1, results1);
                    lip.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
