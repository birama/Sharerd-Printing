using general;
using log4net;
using log4net.Config;
using System;

namespace Client {
public class Client : IApplication {

	private ATopology<Computer> comm;
	readonly private ILog log = LogManager.GetLogger (typeof(Client));

	public Client() {
		BasicConfigurator.Configure ();
		this.comm = new ServerClient<Computer> ();
	}

	public bool init () {
		log.Info ("Initlzing Client.");
		//this.comm.call.callback = this.callme;
		return true;
	}

	public bool start () {
		log.Info ("Client Started.");
		this.comm.connect (ATopology<Computer>.role.client, "host", 69);
		Console.WriteLine ("Enter file name to print: ");
		string file = Console.ReadLine ();
		this.comm.clients[0].printme = file;
		return true;
	}

	public bool stop () {
		log.Info ("Stopping Client");
		this.comm.disconnect ();
		return true;
	}
	
/*	public void callme(Computer c){
		Console.WriteLine ("Enter file name to print: ");
		string file = Console.ReadLine ();
		c.printme = file;
	}*/
}
}
