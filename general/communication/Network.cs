using log4net;
using log4net.Config;

namespace general { 
class Network : INetwork {
// Public
	/*
	  + Load file into memory and tranmists.
	*/
	public void sendfile(string file){}

	public IPrintTask recvfile(){}

	public bool connect(){
	this.pro.connect(this.url,this.port);
	}

	public bool startserver(){
	this.pro.listen();
	}

	public void close(){
	this.pro.disconnect();
	this.net.disconnect();
	}
// Protected
	protected string url;
	protected int port;
// Private
	private Queue<IPrintTask> = new Queue<IPrintTask>;
	private Atopology<AComputer> net;
	private Iprotocol<AComputer> pro;

	/*
	  + 
	*/
	private void selectprotocol(string protocol = "NULL"){
	switch (protocol) {
	case "Socket":
		this.protocol = new Socket<AComputer> (ref this.net.recv);
		break;
	case "ProtocolSoap":
		this.protocol = new ProtocolSoap<AComputer> (ref this.net.recv);
		break;
	default:
		this.protocol = new ProtocolSoap<AComputer> (ref this.net.recv);
		break;
	}
	}

	/*
	  + On update, update protocol on new obj.
	*/
	private void selecttopology(){
	this.net = ServerClient<AComputer> ();
	}
}
}
