using general;
using log4net;

using log4net.Config;
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Client
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ComboBox comboBox1 = new ComboBox();
        //private ComboBox comboBox3 = new ComboBox();
         private PrintDocument pdoc = new PrintDocument();
        // private Label label2 = new Label();



        private void ClientGUI_Load(object sender, System.EventArgs e)
        {
            //    PrintDocument pdoc = new PrintDocument();
            string strDefaultPrinter = pdoc.PrinterSettings.PrinterName;
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(strPrinter);
                if (strPrinter == strDefaultPrinter)
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(strPrinter);
                }
            }
        }

        public void btnBrowse_Click(object sender, EventArgs e)
        {
            //   DialogResult result = openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                if (filename.EndsWith(".txt") || filename.EndsWith(".pdf") || filename.EndsWith(".docx") || filename.EndsWith(".doc") || filename.EndsWith(".png") || filename.EndsWith(".jpg"))
                {

                    textBox1.Text = DateTime.Now.ToString();
                    Console.WriteLine("File Name:" + filename);
                    textBox1.Text = openFileDialog1.FileName;

                }
                else
                {
                    // error message
                    textBox1.Text = "This file is not supported";
                }
            }
            txtFileName.Text = openFileDialog1.FileName;
        }


        public class Client : IApplication
        {

            private ATopology<Computer> comm;
            readonly private ILog log = LogManager.GetLogger(typeof(Client));

            public Client()
            {
                BasicConfigurator.Configure();
                this.comm = new ServerClient<Computer>();
            }

            public bool init()
            {
                log.Info("Initlzing Client.");
                return true;
            }

            private void btnPrint_Click(object sender, EventArgs e)
            {
                start();
            }

            public bool start()
            {

                log.Info("Client Started.");
                this.comm.connect(ATopology<Computer>.role.client);
                this.comm.things[0].setFile("FileName");
                return true;
            }

            public bool stop()
            {
                log.Info("Stopping Client");
                this.comm.disconnect();
                return true;
            }
        }

    }
}

