using log4net;
using log4net.Config;

namespace general {
public class ServerClient<Ttransfertype> : ATopology<Ttransfertype> {
	private role currentrole;
	private Ttransfertype a;
	readonly private ILog log = LogManager.GetLogger (typeof(ServerClient<Ttransfertype>));

	public ServerClient() {
		BasicConfigurator.Configure ();
	}

	override public bool connect (role currentrole) {
		this.currentrole = currentrole;
		if (this.currentrole == role.server) {
		log.Info ("asking protocol to listen.");
		this.protocol.listen ("host", 69);
		} else {
		log.Info ("asking protocol to connect.");
		this.protocol.connect ("host", 69);
		a = this.protocol.recv ();
				a.
		}
		return true;
	}

	override public void disconnect () {
		log.Info ("asking protcol to disconnect.");
		this.protocol.disconnect ();
	}
}
}
