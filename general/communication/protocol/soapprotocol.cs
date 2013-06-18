using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using general.IProtocol;

namespace general{

public ProtocolSoap<Ttransertype> : IProtocol<Ttransfertype>{
//Private
private HttpChannel chnl = new HttpChannel(1234);

//Public
public bool connect(TtransferType objtype, string url){
ChannelServices.RegisterChannel(this.chnl);
RemotingConfiguration.RegisterWellKnownServiceType(typeof(objtype),url,WellKnownObjectMode.Singleton);
chnl.StartListening();
return true;
}

public void disconnect(){
this.chnl.StopListening();
}

public bool send(){}
public bool recv(){}
}

}
