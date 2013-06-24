using log4net;
using log4net.Config;

namespace general {
public abstract class ATopology<Ttransfertype> {
//private
	private string[] protocols = new string[] { "ProtocolSoap" };
	readonly private ILog log = LogManager.GetLogger (typeof(ATopology<Ttransfertype>));
//protected
	protected IProtocol<Ttransfertype> protocol;
//public
	public enum role {
	server, client
	};

	public string name = "Default Abstract Topology!";

	public ATopology(string protocol) {
		BasicConfigurator.Configure ();
		log.Info ("using protocol: " + protocol);
		switch (protocol) {
		case "ProtocolSoap":
			this.protocol = new ProtocolSoap<Ttransfertype> ();
			break;
		default:
			this.protocol = new ProtocolSoap<Ttransfertype> ();
			break;
		}
	}

	public ATopology() {
		BasicConfigurator.Configure ();
		log.Info ("Using default protocol");
		this.protocol = new ProtocolSoap<Ttransfertype> ();
	}

	public string[] protocolTypes {
		get { return protocols;}
		protected set { this.protocols = value;}
	}
//Abstract member functions
	abstract public bool connect (role currentrole);

	abstract public void disconnect ();
}
}
