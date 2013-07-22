namespace general{
public interface INetwork<Ttransfertype> {
	void send (Ttransfertype obj, string id);
	void recv(Ttransfertype obj,string id);
	void connect();
	void startserver();
	void close();
}
}