using general.IProtocol;
using general.IUser;
using general.I

namespace general{

abstract ATopology<Transfertype>{
//private
private IUser[] user = new IUser[];
private IProtocol<Transfertype> protocol;

//public
public const string name;

public ATopology(IProtocol<Ttransfertype> protocol){
this.protocol = protocol;
}

abstract public bool connect(Ttransfertype);
abstract public void dissconnect();

}
}
