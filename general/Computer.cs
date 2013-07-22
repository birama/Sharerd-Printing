using general;
using System;

namespace general {
[Serializable]
public class Computer<Ttransfertype> : AComputer<Ttransfertype> {
	public string printme;
	public int copies = 1;
}
}
