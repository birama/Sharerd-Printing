using log4net;
using log4net.Config;

namespace general {
public class ServerClient<Ttransfertype> : ATopology<Ttransfertype> where Ttransfertype : AComputer, new(){
	readonly private ILog log = LogManager.GetLogger (typeof(ServerClient<Ttransfertype>));
	private int id =0;

	public ServerClient() : base("ServerClient") {
		BasicConfigurator.Configure ();
	}

	override public void recv (Ttransfertype t) {
		log.Info ("asking protcol to disconnect.");
		++id;
		this.computer.add(id.toString(),t);
	}
}
}
