namespace general {
public interface IProtocol<Ttransfertype> {

/* Connect
   + Takes T and string as url.
*/
	bool connect (string url, int port);

	bool listen (string url, int port);

	void disconnect ();

	bool send (ref Ttransfertype obj);
}
}
