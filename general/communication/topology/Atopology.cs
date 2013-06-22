using general;

namespace general{

public abstract class ATopology<Ttransfertype>{
//private
//private IUser[] user = new IUser[2];
private string[] protocols = new string[] {"ProtocolSoap"};

//protected
protected IProtocol<Ttransfertype> protocol;

//public
public string name = "Default Abstract Topology!";

public ATopology(string protocol{
switch (protocol){
	case "ProtocolSoap":
		this.protocol = ProtocolSoap<Ttransfertype>();
	default:
		this.protocol = ProtocolSoap<Ttransfertype>();
}
}

public ATopology(){
this.protocol = ProtocolSoap<Ttransfertype>();
}

public string protocolTypes{
get { return protocols;}
protected set { this.protocols = value;}
}

abstract public bool connect(Ttransfertype obj);
abstract public void disconnect();

}
}
