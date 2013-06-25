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
            // InitializeComponent();
        }
       
        public void btnBrowse_Click(object sender, EventArgs e)
        {
         //   DialogResult result = openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
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
                this.comm.things[0].setFile("openFileDialog1.FileName");
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
