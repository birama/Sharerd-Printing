using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using general.IProtocol;

namespace general{

public ProtocolSoap<Ttransertype> : IProtocol<Ttransfertype>{

public bool connect(TtransferType objtype, string url){
HttpChannel chnl = new HttpChannel(1234);
ChannelServices.RegisterChannel(chnl);
RemotingConfiguration.RegisterWellKnownServiceType(typeof(objtype),url,WellKnownObjectMode.Singleton);
}

public void disconnect(){}
public bool send(){}
public bool recv(){}
}

}
