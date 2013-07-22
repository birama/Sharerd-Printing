using log4net;
using log4net.Config;
using System.Collections.Generic;

namespace general {
	public class ServerClient<Ttransfertype> : ATopology<Ttransfertype>{
	readonly private ILog log = LogManager.GetLogger (typeof(ServerClient<Ttransfertype>));
	private int id =0;
	private AComputer<Ttransfertype> me = new Computer<Ttransfertype>();

	public ServerClient(ATopology<Ttransfertype>.Ddecap d) : base("ServerClient",d) {
	log.Debug ("Created ServerCleint");
	}

	public override void recv (AComputer<Ttransfertype> t, string id) {
	log.Debug ("Decapsulating Message");
	this.machines.Add (id, t);
	log.Debug ("Calling retv to return data to app.");
	this.retv (t.data, id);
	}

	public override void disconnect(){
	log.Debug ("Disconnecting topology.");
	}

	public override void encapsulate (ref Ttransfertype obj, Dsend callback, string id) {
	log.Debug ("Encapsulating raw message.");
	AComputer<Ttransfertype> tempcomputer = new Computer<Ttransfertype> ();
	tempcomputer.data = obj;
	tempcomputer.id = id;
	log.Debug ("Calling Dsend to send msg");
	callback (tempcomputer);
	}

	public override void topomsg (AComputer<Ttransfertype> obj, string id, Dsend callbacksend, Drecv callbackrecv) {
	log.Debug ("Topology message being processed.");
		if (obj == null) { // New Server was connected to
		log.Debug ("New server has been connected to.");
		this.me = new Computer<Ttransfertype> ();
		this.me.topologymessage = true;
		this.me.id = id;
		this.me.to = id; // out the same way it came in.
		// Add any info about self and send to connected server
		callbacksend (this.me);
		callbackrecv (id);
		} else {
		log.Debug ("New client has connected.");
		// If self server obj does not exist create one
			if (this.me == null){
			log.Debug ("Creating a new server obj.");
			this.me = new Computer<Ttransfertype> ();
			this.me.isserver = true;
			}
		this.me.to = id;
		log.Debug ("send reply back to connecting client.");
		callbacksend (this.me);
		}
	}
}
}