namespace general{

public interface IComputer<Tprinter, Tuser>{
	public Tprinter[] printer = new Tprinter[];
	public Tuser[] user = new Tuser[];
	public string name;
}

}
