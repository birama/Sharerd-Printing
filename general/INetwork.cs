namespace general{
public interface INetwork {
	// Send File
	void sendfile(string file);

	// Recive next file
	byte[] recvfile();

	// Connect to host
	bool connect();

	// start server
	bool startserver();

	// Close connection
	void close();
}
}
