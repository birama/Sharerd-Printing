namespace general{
public class Callback<Ttransfertype> {
	public delegate void caller (Ttransfertype return_value);
	protected caller callback;

	Callback(caller callback) {
	this.callback = new callback;
	}
}
