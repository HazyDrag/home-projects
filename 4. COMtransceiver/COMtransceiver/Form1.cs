using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;

namespace COMtransceiver
{
    public partial class Form1 : Form
    {
        private SerialPort port;
        private List<Wifipoint> wifipoints = new List<Wifipoint>();

        public Form1()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textToSend = textBox1.Text;
            port.WriteLine(textToSend);
            label1.Text = "Отправлено: " + textToSend;
            
            richTextBox1.Text = port.ReadTo("THEEND");
            textBox1.Clear();
        }        

        private void button2_Click(object sender, EventArgs e)
        {
            port.WriteLine("thatall");
            label1.Text = "|Disconnected|";
            port.Close();
            
            button2.Enabled = false;
            tabControl1.Enabled = false;

            button3.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                string comName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                try
                {
                    port = new SerialPort(comName);
                    port.BaudRate = 9600;
                    port.Parity = Parity.None;
                    port.DataBits = 8;
                    port.StopBits = StopBits.One;

                    port.Open();
                    button3.Enabled = false;
                    button2.Enabled = true;
                    comboBox1.Enabled = false;
                    label1.Text = "|Connected|";
                    tabControl1.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка открытия порта " + comName + "\nВозможно другая программа использует его");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            port.WriteLine("sudo python Desktop/Wifi/pythser/airscan.py -i wlan0 -t " + numericUpDown1.Value);

            richTextBox2.Text = "Send: sudo python Desktop/Wifi/pythser/airscan.py -i wlan0 -t " + numericUpDown1.Value;
            comboBox2.Items.Clear();
            wifipoints.Clear();

            Thread myThread = new Thread(NewForm);
            myThread.Start();
            
            string wifiout = port.ReadTo("THEEND");
            richTextBox2.Text = wifiout;
            string[] wifioutC = wifiout.Split('\n');
            foreach (string s in wifioutC)
                if (s != "")
                {
                    Wifipoint wp = new Wifipoint();
                    string[] wifioutCC = s.Split('|');
                    
                    wp.channel = Convert.ToInt32(wifioutCC[0]);
                    wp.ssid = wifioutCC[1];
                    wp.mac = wifioutCC[2];

                    wifipoints.Add(wp);
                    comboBox2.Items.Add(wifioutCC[1]);
                }

        }

        private void NewForm()
        {
            WaitForm wf = new WaitForm(Convert.ToInt32(numericUpDown1.Value));
            wf.ShowDialog();
        }

        private void NewForm2()
        {
            WaitForm wf = new WaitForm(Convert.ToInt32(numericUpDown2.Value));
            wf.ShowDialog();
        }

        private void NewForm2(WaitForm wf)
        {
            wf.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                port.WriteLine("sudo python3 Desktop/Wifi/pythser/airplay.py " + numericUpDown2.Value + " " + wifipoints[comboBox2.SelectedIndex].mac + " " + wifipoints[comboBox2.SelectedIndex].channel + " wlan0");
                
                Thread myThread = new Thread(NewForm2);
                myThread.Start();

                string wifiout = port.ReadTo("THEEND");
                MessageBox.Show("Атака завершена!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                port.WriteLine("sudo rfkill unblock wifi; sudo mdk3 wlan0 b -n " + wifipoints[comboBox2.SelectedIndex].ssid + " -t -g -m");

                numericUpDown2.Value = numericUpDown2.Maximum;
                Thread myThread = new Thread(NewForm2);
                myThread.Start();

                string wifiout = port.ReadTo("THEEND");
                MessageBox.Show("Атака завершена!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            port.WriteLine("sudo rfkill unblock wifi; sudo mdk3 wlan0 b -t -g -m");

            numericUpDown2.Value = numericUpDown2.Maximum;
            Thread myThread = new Thread(NewForm2);
            myThread.Start();

            string wifiout = port.ReadTo("THEEND");
            MessageBox.Show("Атака завершена!");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "sudo ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "ls ";
        }
    }
}
