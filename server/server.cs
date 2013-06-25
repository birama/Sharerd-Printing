using System.Runtime.Remoting;
using System.Collections;
using System.Collections.Generic;
using general;
using printing;
using log4net;
using log4net.Config;
using System;

namespace Server {
class Server : IApplication {

	private ATopology<Computer> comm;
	readonly private ILog log = LogManager.GetLogger (typeof(Server));
	private Printer printer = new Printer();

	public Server() {
		BasicConfigurator.Configure ();
		this.comm = new ServerClient<Computer> ();
	}

/*	public void callme(Computer c){
		Console.ReadLine ();
		this.printer.LoadTask(c.printme,c.copies);
	}*/

	public bool init () {
		log.Info ("Initilzing Server.");
		//this.comm.call.callback = this.callme;
		return true;
	}

	public bool start () {
		log.Info ("Server Started");
		this.comm.connect (ATopology<Computer>.role.server, "host", 69);
		Console.ReadLine ();
		this.printer.LoadTask (this.comm.clients[0].printme, this.comm.clients [0].copies);
		return true;
	}

	public bool stop () {
		log.Info ("Server Stopped");
		this.comm.disconnect ();
		return true;
	}
}
}