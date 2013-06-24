namespace general {
public abstract class AComputer<Tprinter, Tuser> {
	public Tprinter[] printer = new Tprinter[2];
	public Tuser[] user = new Tuser[2];
	public string name;
}
}
