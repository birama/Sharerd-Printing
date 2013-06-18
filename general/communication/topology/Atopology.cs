using general;

namespace general{

public abstract class ATopology<Ttransertype>{
//private
//private IUser[] user = new IUser[2];

//protected
protected IProtocol<Ttransertype> protocol;

//public
public string name = "Default Abstract Topology!";

public ATopology(IProtocol<Ttransertype> protocol){
this.protocol = protocol;
}

abstract public bool connect(Ttransertype obj);
abstract public void disconnect();

}
}
