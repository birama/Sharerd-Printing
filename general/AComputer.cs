using System;
namespace general {
[Serializable]
public abstract class AComputer <Ttransfertype> {
	protected string username;
	public bool topologymessage = false;
	public string id;
	public string to;
	public bool isserver = false;
	public bool newactivity = false;
	public Ttransfertype data;
	public string connectionid;
	public bool whoami = false; // Set to true when on self machine.
}
}
