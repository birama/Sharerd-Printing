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

namespace serverGUI 
{
    public partial class Form2 : Form
    {
        public Form2()
        {
        //    InitializeComponent();
        }
        
class Server : IApplication {

	private ATopology<Computer> comm;
	readonly private ILog log = LogManager.GetLogger (typeof(Server));
    private Printer printer = new Printer();

	public Server() {
		BasicConfigurator.Configure ();
		this.comm = new ServerClient<Computer> ();
	}

	public bool init () {
		log.Info ("Initilzing Server.");
		return true;
	}

	public bool start () {
		log.Info ("Server Started");
		this.comm.connect (ATopology<Computer>.role.server);
        Console.ReadLine();
        printer.LoadTask(this.comm.things[0].getFile());
		return true;
	}

	public bool stop () {
		log.Info ("Server Stopped");
		this.comm.disconnect ();
		return true;
	}
}
}
    }

