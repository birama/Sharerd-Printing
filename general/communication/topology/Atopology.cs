using log4net;
using log4net.Config;
using System;

namespace general {
	public class Callback<Ttransfertype> {
		public delegate void call (Ttransfertype return_value);
		public call callback;
		Callback(call callback) {
			this.callback = callback;
		}

		Callback (){
		}
	}

	public abstract class ATopology<Ttransfertype> {
//private
	private string[] protocols = new string[] { "ProtocolSoap" };
	readonly private ILog log = LogManager.GetLogger (typeof(ATopology<Ttransfertype>));
	public Callback<Ttransfertype> call;
	public Ttransfertype[] clients = new Ttransfertype[2];
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

	public void setcallback(Func<Ttransfertype> call){
//		this.call = new Callback<Ttransfertype> (call);
	}

	public void callback(Ttransfertype obj){
		this.call.callback (obj);
	}
//Abstract member functions
	abstract public bool connect (role currentrole, string url, int port);

	abstract public void disconnect ();
}
}
