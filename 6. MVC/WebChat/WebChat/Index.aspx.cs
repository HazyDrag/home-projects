using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;

namespace WebChat
{
    public partial class Index : System.Web.UI.Page
    {
        string temp_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "systemdir", "temp.txt");
        string user_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "systemdir", "Clients.txt");
        string chat_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "systemdir", "History.txt");
        string uploads_dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
        public string[] UserMass = null;
                
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {              

                string user = TextBox1.Text;
                Label1.Text = "Ваш ник: ";
                Label2.Text = user;

                TextBox1.Visible = false;
                Button1.Visible = false;

                File.AppendAllText(user_file, user + Environment.NewLine); //добавляем в файл строчку со своим логином

                ListBox1.Visible = true;
                ListBox2.Visible = true;
                TextBox2.Visible = true;
                Button2.Visible = true;
                Label3.Visible = true;
                Button4.Visible = true;
                Button3.Visible = true;
                Repeater1.Visible = true;
                FileUpload1.Visible = true;


                Timer1.Enabled = true;

            }      
        }

         protected void Button2_Click(object sender, EventArgs e)
         {
             if (TextBox2.Text != " ")
             {
                 File.AppendAllText(chat_file, "[" + DateTime.Now + "] " + Label2.Text + ": " + TextBox2.Text + Environment.NewLine);

                 TextBox2.Text = "";
                 
                 Timer1.Enabled = true;
             }

         }

         protected void Timer1_Tick1(object sender, EventArgs e)
         {
             ListBox1.Items.Clear();
             ListBox2.Items.Clear();

             UserMass = File.ReadAllLines(user_file);
             foreach (string s in UserMass) 
                 ListBox1.Items.Add(s);
             

             string[] History = File.ReadAllLines(chat_file);
             foreach (string s in History)
                 ListBox2.Items.Add(s);


             if (Button1.Visible) TextBox1.Focus();
             else TextBox2.Focus();             
         }

         protected void Button3_Click(object sender, EventArgs e)
         {
             Timer1.Enabled = false;
             string user = Label2.Text;
             
             List<string> currentUsers = new List<string>();

             foreach (string line in File.ReadLines(user_file))
                 currentUsers.Add(line);

             currentUsers.Remove(user);

             File.Delete(user_file);

             foreach (string line2 in currentUsers)
                 File.AppendAllText(temp_file, line2 + Environment.NewLine);

             File.Move(temp_file, user_file);    

             if(currentUsers.Count == 1)
                 File.Create(chat_file);

             Label1.Text = "Введите свое имя!";
             TextBox1.Visible = true;
             Button1.Visible = true;

             ListBox1.Visible = false;
             ListBox2.Visible = false;
             TextBox2.Visible = false;
             Button2.Visible = false;
             Label3.Visible = false;
             Button4.Visible = false;
             Repeater1.Visible = false;
             FileUpload1.Visible = false;
             Button3.Visible = false;
         }


         protected void TextBox2_TextChanged(object sender, EventArgs e)
         {
             Timer1.Enabled = false;
         }

         protected void Repeater1_PreRender(object sender, EventArgs e)
         {
             DirectoryInfo uploads = new DirectoryInfo(uploads_dir);
             if (!uploads.Exists)
                 uploads.Create();
             Repeater1.DataSource = uploads.GetFiles();
             Repeater1.DataBind();
         }
        

         protected void Button4_Click(object sender, EventArgs e)
         {
             if (FileUpload1.HasFile)
             {
                 HttpPostedFile user_file = FileUpload1.PostedFile;

                 DirectoryInfo uploads = new DirectoryInfo(uploads_dir);
                 if (!uploads.Exists)
                     uploads.Create();

                 string user_file_server_name = Path.Combine(uploads.FullName, Path.GetFileName(user_file.FileName));
                 using (FileStream stream = new FileStream(user_file_server_name, FileMode.Create, FileAccess.Write, FileShare.None))
                 {
                     byte[] buffer = new byte[4096];
                     int bytes_read = 0;
                     do
                     {
                         stream.Write(buffer, 0, bytes_read);
                         bytes_read = user_file.InputStream.Read(buffer, 0, buffer.Length);
                     } while (bytes_read > 0);
                 }
             }
         }
    }
}