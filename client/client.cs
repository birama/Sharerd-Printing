using general;
using log4net;
using log4net.Config;

namespace Client {
public class Client : IApplication {

	private ATopology<IPrintTask> comm;
	readonly private ILog log = LogManager.GetLogger (typeof(Client));

	public Client() {
		BasicConfigurator.Configure ();
		this.comm = new ServerClient<IPrintTask> ();
	}

	public bool init () {
		log.Info ("Initlzing Client.");
		return true;
	}

	public bool start () {
		log.Info ("Client Started.");
		this.comm.connect (ATopology<IPrintTask>.role.client);
		return true;
	}

	public bool stop () {
		log.Info ("Stopping Client");
		this.comm.disconnect ();
		return true;
	}
}
}
