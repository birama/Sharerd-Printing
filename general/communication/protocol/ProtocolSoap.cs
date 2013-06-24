using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
using System;
using general;
using log4net;
using log4net.Config;

namespace general {
public class ProtocolSoap<Ttransfertype> : IProtocol<Ttransfertype> {
//Private
	private HttpChannel chnl;
	private Ttransfertype remoteobj;
		readonly private ILog log = LogManager.GetLogger (typeof(IProtocol<Ttransfertype>));
//Public
	public ProtocolSoap() {
		BasicConfigurator.Configure ();
	}

	public bool connect (string url, int port) {
		string url2 = "http://" + url + ":" + port.ToString ();
		log.Info ("Connecting to: " + url2);
		this.chnl = new HttpChannel ();
		ChannelServices.RegisterChannel (this.chnl, false);
		log.Debug ("Getting remote object.");
		this.remoteobj = (Ttransfertype)Activator.GetObject (typeof(Ttransfertype), url2);
		return true;
	}

	public bool listen (string url, int port) {
		log.Info ("Listing on: " + url + " on port: " + port.ToString());
		this.chnl = new HttpChannel (port);
		ChannelServices.RegisterChannel (this.chnl, false);
		RemotingConfiguration.RegisterWellKnownServiceType (typeof(Ttransfertype), url, WellKnownObjectMode.Singleton);
		chnl.StartListening (null);
		return true;
	}

	public void disconnect () {
		log.Info ("Stopping to listen on channel.");
		this.chnl.StopListening (null);
	}

	public bool send (Ttransfertype obj) {
		log.Error ("This protocol does not facilate sending objects. Only changing referaced objects.");
		return false;
	}

	public Ttransfertype recv () {
		log.Debug ("Returning remote object to caller.");
		return this.remoteobj;
	}
}
}