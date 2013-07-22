using log4net;
using log4net.Config;
using System.Collections.Generic;

namespace general {
public abstract class ATopology<Ttransfertype> {
	readonly private ILog log = LogManager.GetLogger (typeof(ATopology<Ttransfertype>));
	protected SortedDictionary<string, AComputer<Ttransfertype>> machines; // Store all client and server data (n) transfer data.
	protected Queue<Ttransfertype> transfers; // Store transfers before fwd to application.
	protected string name = "Default Abstract Topology!";
	protected Ddecap retv;
	protected Drecv recvon;

	public ATopology(string name, Ddecap d) {
	log.Debug ("Created ATop.");
	this.machines = new SortedDictionary<string, AComputer<Ttransfertype>>();
	this.name = name;
	this.retv = d;
	this.transfers = new Queue<Ttransfertype> ();
	}
	
	// Used to send an encapsulated message through same protocol that called it.
	public delegate void Dsend (AComputer<Ttransfertype> value);
	// Recive on id
	public delegate void Drecv (string id);
	// Fwd a decapsulated message back to application
	public delegate void Ddecap (Ttransfertype obj, string id);
	// Used to recv any new network actvity.
	abstract public void recv (AComputer<Ttransfertype> obj, string id);
	abstract public void disconnect();
	abstract public void encapsulate (ref Ttransfertype obj,Dsend c,string id);
	abstract public void topomsg (AComputer<Ttransfertype> obj,string id, Dsend c, Drecv r); // Called by client on connection to server
}
}
