using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace general { 
public class Network<Ttransfertype> : INetwork<Ttransfertype> {
	protected string url = "localhost";
	protected int port = 8080;
	protected int maxconnections = 10;
	private ATopology<Ttransfertype> net;
	private AProtocol<Ttransfertype> pro;
	public delegate void callback(Ttransfertype data,string id);
	callback caller;
	readonly private ILog log = LogManager.GetLogger (typeof(Network<Ttransfertype>));

	public Network(callback c){
	BasicConfigurator.Configure ();
	this.caller = c;
	}

	public void send(Ttransfertype obj, string id){
	log.Debug ("Sending.");
	this.pro.send (ref obj, id);
	}

	public void recv(Ttransfertype obj, string id){
	log.Debug ("New data recived from: " + id);
	this.caller (obj, id);
	}

	public void connect(){
		if (this.net == null){
		selecttopology ();
		}
		if (this.pro == null){
		selectprotocol ();
		}
	this.pro.connect(this.url,this.port);
	}

	public void startserver(){
		if (this.net == null){
		selecttopology ();
		}
		if (this.pro == null){
		selectprotocol ();
		}
	this.pro.listen(this.url, this.port);
	}

	public void close(){
	this.pro.disconnect();
	this.net.disconnect();
	}

	private void selectprotocol(string protocol = "NULL"){
	switch (protocol) {
	case "Socket":
		this.pro = new Socket<Ttransfertype> (new AProtocol<Ttransfertype>.Ddecapsulate(this.net.recv), 
				                              new AProtocol<Ttransfertype>.Dencapsulate(this.net.encapsulate), 
				                              new AProtocol<Ttransfertype>.Dtopomsg(this.net.topomsg));
		break;
	case "ProtocolSoap":
		//this.pro = new ProtocolSoap<Ttransfertype> (new AProtocol<Ttransfertype>.caller(this.net.recv), new AProtocol<Ttransfertype>.encapsulate(this.net.encapsulate));
		break;
	default:
		this.pro = new Socket<Ttransfertype> (new AProtocol<Ttransfertype>.Ddecapsulate(this.net.recv), 
		                                      new AProtocol<Ttransfertype>.Dencapsulate(this.net.encapsulate), 
		                                      new AProtocol<Ttransfertype>.Dtopomsg(this.net.topomsg));
		break;
	}
	}
		
	private void selecttopology(){
	this.net = new ServerClient<Ttransfertype> (new ATopology<Ttransfertype>.Ddecap(this.recv));
	}
}
}
