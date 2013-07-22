using System;
using System.Runtime.Remoting;
using System.Collections;
using System.Collections.Generic;
using general;
using log4net;
using log4net.Config;

namespace Server {
class Server {

	private INetwork<IPrintTask> net;
	readonly private ILog log = LogManager.GetLogger (typeof(Server));

	public Server() {
		BasicConfigurator.Configure ();
		this.net = new Network ();
		this.net.startserver ();
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