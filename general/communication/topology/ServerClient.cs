using log4net;
using log4net.Config;

namespace general {
	public class ServerClient<Ttransfertype> : ATopology<Ttransfertype> where Ttransfertype : AComputer, new(){
	//public Ttransfertype[] clients = new Ttransfertype[2];
	private role currentrole;
	int i = 0;
	readonly private ILog log = LogManager.GetLogger (typeof(ServerClient<Ttransfertype>));

	public ServerClient() {
		BasicConfigurator.Configure ();
	}

	override public bool connect (role currentrole, string url, int port) {
		this.currentrole = currentrole;
		if (this.currentrole == role.server) {
		log.Info ("asking protocol to listen.");
		this.protocol.listen (url, port);
		//	while (this.i != -1) {
			this.clients [i] = new Ttransfertype ();
			this.protocol.send (ref this.clients[i]);
			++i;
		//	}
		} else {
		log.Info ("asking protocol to connect.");
		this.protocol.connect (url, port);
		this.clients [0] = this.protocol.recv ();
//		this.callback (this.protocol.recv());
		}
		return true;
	}

	override public void disconnect () {
		log.Info ("asking protcol to disconnect.");
		this.protocol.disconnect ();
		this.i = -1;
	}
}
}
