using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
using System;
using general;
using log4net;
using log4net.Config;

namespace general {
	public class ProtocolSoap<Ttransfertype> : AProtocol<Ttransfertype> {
	// Warp object being sent so it can be marshaled.
	protected class Wrapper<Ttransfertype2> : MarshalByRefObject {
		public Ttransfertype2 obj;

		public Wrapper(ref Ttransfertype2 obj) {
			this.obj = obj;
		}

		public Ttransfertype2 get () {
			return this.obj;
		}
	}
//Private
	private Wrapper<Ttransfertype> wraps;
	private HttpChannel chnl;
	private string url;
	private bool ttl;
	readonly private ILog log = LogManager.GetLogger (typeof(ProtocolSoap<Ttransfertype>));
//Public
	public ProtocolSoap(AProtocol<Ttransfertype>.caller c, AProtocol<Ttransfertype>.encapsulate d) : base (c,d){
	BasicConfigurator.Configure ();
	this.ttl = true;
	}

	override public bool connect (string url, int port) {
		this.url = "http://" + url + ":" + port.ToString() + "/obj.soap";
		log.Info ("Connecting to: " + this.url);
		this.chnl = new HttpChannel ();
		ChannelServices.RegisterChannel (this.chnl, false);
		log.Debug ("Getting remote object.");
		this.wraps = (Wrapper<Ttransfertype>)Activator.GetObject (typeof(Wrapper<Ttransfertype>), "http://localhost:69/obj.soap");
		//this.callee(this.wraps.get());
		return true;
	}

	override public bool listen (string url, int port) {
		log.Info ("Listing on port: " + port.ToString());
		this.url = url;
		this.chnl = new HttpChannel (port);
		ChannelServices.RegisterChannel (this.chnl, false);
		chnl.StartListening (null);
		while(ttl){
		//this.callee(this.wraps.get());
		}
		return true;
	}

	override public void disconnect () {
		log.Info ("Stopping to listen on channel.");
		this.ttl = false;
		this.chnl.StopListening (null);
	}

	override public void send (ref Ttransfertype obj) {
		log.Debug ("Protocol is wrapping object in custom wrapper.");
		this.wraps = new Wrapper<Ttransfertype> (ref obj);
		RemotingServices.Marshal (this.wraps, "obj.soap");
	}
	
	override public void sendmsg (ref AComputer<Ttransfertype> obj){
	}
}
}
