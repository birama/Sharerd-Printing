namespace general{
public class Callback<Ttransfertype> {
	public delegate void caller (Ttransfertype return_value);
	public caller callee;

	Callback() {
	//this.callee = new caller(c);
	}
}
}