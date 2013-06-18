using general;

namespace general{

public abstract class ATopology<Ttransfertype>{
//private
//private IUser[] user = new IUser[2];

//protected
protected IProtocol<Ttransfertype> protocol;

//public
public string name = "Default Abstract Topology!";

public ATopology(IProtocol<Ttransfertype> protocol){
this.protocol = protocol;
}

abstract public bool connect(Ttransfertype obj);
abstract public void disconnect();

}
}
