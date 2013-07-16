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
	private Ttransfertype remoteobj;
	private string url;
	readonly private ILog log = LogManager.GetLogger (typeof(ProtocolSoap<Ttransfertype>));
//Public
	public ProtocolSoap() {
		BasicConfigurator.Configure ();
	}

	public bool connect (string url, int port) {
		this.url = "http://" + url + ":" + port.ToString ();
		log.Info ("Connecting to: " + this.url);
		this.chnl = new HttpChannel ();
		ChannelServices.RegisterChannel (this.chnl, false);
		log.Debug ("Getting remote object.");
		this.wraps = (Wrapper<Ttransfertype>)Activator.GetObject (typeof(Wrapper<Ttransfertype>), "http://localhost:69/obj.soap");
		this.remoteobj = this.wraps.get ();
		return true;
	}

	public bool listen (string url, int port) {
		log.Info ("Listing on port: " + port.ToString());
		this.url = url;
		this.chnl = new HttpChannel (port);
		ChannelServices.RegisterChannel (this.chnl, false);
		chnl.StartListening (null);
		return true;
	}

	public void disconnect () {
		log.Info ("Stopping to listen on channel.");
		this.chnl.StopListening (null);
	}

	public bool send (ref Ttransfertype obj) {
		log.Debug ("Protocol is wrapping object in custom wrapper.");
		this.wraps = new Wrapper<Ttransfertype> (ref obj);
		RemotingServices.Marshal (this.wraps, "obj.soap");
		return false;
	}

	public Ttransfertype recv () {
		log.Debug ("Returning remote object to caller.");
		return this.wraps.get ();
	}
}
}
