using log4net;
using log4net.Config;

namespace general {
public abstract class AProtocol<Ttransfertype> {
	protected Ddecapsulate decap;
	protected Dencapsulate encap;
	protected Dtopomsg topomsg;
	readonly private ILog log = LogManager.GetLogger (typeof(AProtocol<Ttransfertype>));

	public AProtocol(Ddecapsulate d, Dencapsulate e, Dtopomsg c){
	log.Debug ("Created AProtocol.");
	this.decap = d;
	this.encap = e;
	this.topomsg = c;
	}
	
	// Used to call topology with new network activity to decapsulate.
	public delegate void Ddecapsulate (AComputer<Ttransfertype> return_value, string id);
	// Used to notify network topology of new raw message for encapsulation.
	public delegate void Dencapsulate (ref Ttransfertype return_value, ATopology<Ttransfertype>.Dsend callback, string id);
	// Used to create a cleint connected message. To send back to clients
	public delegate void Dtopomsg (AComputer<Ttransfertype> obj,string id,ATopology<Ttransfertype>.Dsend send, ATopology<Ttransfertype>.Drecv recv);
	abstract public void connect (string url, int port); // Should call clientconnect once complete
	abstract public void listen (string url, int port);
	abstract public void disconnect ();
	abstract public void send (ref Ttransfertype obj, string id);
	abstract public void sendmsg (AComputer<Ttransfertype> obj);
	abstract public void recv (string id);
}
}
