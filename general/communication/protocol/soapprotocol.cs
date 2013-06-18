using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
using general;

namespace general{

public class ProtocolSoap<Ttransertype> : IProtocol<Ttransertype>{
//Private
private HttpChannel chnl = new HttpChannel(1234);
private Ttransertype placeholder;

//Public
public bool connect(Ttransertype objtype, string url){
ChannelServices.RegisterChannel(this.chnl,false);
RemotingConfiguration.RegisterWellKnownServiceType(typeof(objtype),url,WellKnownObjectMode.Singleton);
chnl.StartListening();
return true;
}

public void disconnect(){
this.chnl.StopListening();
}

public bool send(Ttransertype obj){
return true;
}

//public Ttransertype recv(return this.placeholder;);

}

}
