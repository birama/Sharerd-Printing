using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
using general;

namespace general{

public class ProtocolSoap<Ttransfertype> : IProtocol<Ttransfertype>{
//Private
private HttpChannel chnl;
private Ttransfertype placeholder;

//Public
public ProtocolSoap(int port){
this.chnl = new HttpChannel(port);
}

public bool connect(Ttransfertype objtype, string url){
ChannelServices.RegisterChannel(this.chnl,false);
RemotingConfiguration.RegisterWellKnownServiceType(typeof(Ttransfertype),url,WellKnownObjectMode.Singleton);
chnl.StartListening();
return true;
}

public void disconnect(){
this.chnl.StopListening();
}

public bool send(Ttransfertype obj){
return true;
}

//public Ttransfertype recv(return this.placeholder;);

}

}
