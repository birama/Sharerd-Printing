using System;
using System.Runtime.Remoting;
using System.Collections;
using System.Collections.Generic;
using general;
using log4net;
using log4net.Config;

namespace Server {
class Server : IApplication {

	private ATopology<IPrintTask> comm;
	readonly private ILog log = LogManager.GetLogger (typeof(Server));

	public Server() {
		BasicConfigurator.Configure ();
		this.comm = new ServerClient<IPrintTask> ();
	}

	public bool init () {
		log.Info ("Initilzing Server.");
		return true;
	}

	public bool start () {
		log.Info ("Server Started");
		this.comm.connect (ATopology<IPrintTask>.role.server);
		return true;
	}

	public bool stop () {
		log.Info ("Server Stopped");
		this.comm.disconnect ();
		return true;
	}
}
}