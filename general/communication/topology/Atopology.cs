using log4net;
using log4net.Config;
using System;

namespace general {
	public abstract class ATopology<Ttransfertype> {
//private
	readonly private ILog log = LogManager.GetLogger (typeof(ATopology<Ttransfertype>));
	SortedDictionary<string, Ttransfertype> computer;
//public
	public string name = "Default Abstract Topology!";

	public ATopology(string name) {
		BasicConfigu1rator.Configure ();
		this.computer = new SortedDirectory<string, Ttransfertype>();
		this.name = name;
	}

//Abstract member functions
	/*
	  + Used to recv any new network actvity.
	*/
	abstract public void recv (Ttransfertype);
}
}
