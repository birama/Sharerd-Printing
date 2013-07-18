using log4net;
using log4net.Config;

namespace general {
	public class Socket<Ttransfertype> : Callback<Ttransfertype>, IProtocol<Ttransfertype> {
//Private
	private string url;
	private bool ttl;
	private int timeout;
	private Socket socket;
	private int tickcountstart = Enviroment.TickCount;
// csharp-examples.net/socket-send-receive
	readonly private ILog log = LogManager.GetLogger (typeof(ProtocolSoap<Ttransfertype>));
//Public
	public Socket(Callback.call c) : base(c){
		BasicConfigurator.Configure ();
		this.ttl = true;
	}

	public bool connect (string url, int port) {
		this.url = "http://" + url + ":" + port.ToString() + "/obj.soap";
		log.Info ("Connecting to: " + this.url);
		this.callback();
		return true;
	}

	public bool listen (string url, int port) {
		log.Info ("Listing on port: " + port.ToString());
		this.url = url;
		while(ttl){
		this.callback();
		}
		return true;
	}

	public void disconnect () {
		log.Info ("Stopping to listen on channel.");
		this.ttl = false;
		this.chnl.StopListening (null);
	}

	public bool send (ref Ttransfertype obj) {
		log.Debug ("Sending on socket.");
		this.tickcountstart = Enviroment.TickCount;
		byte[] buffer = Encoding.UTF8.GetBytes(obj);
		int offset = 0;
		int size = buffer.size();
		int sent = 0;
		do {
			if (Enviroment.Tickcount > this.tickcountstart + this.timeout){
			log.Error("Socket Timed out on sedning.");
			return false;
			}
			try {
			sent += this.socket.Send(buffer, offset + size, size - sent, SocketFlags.none);
			} catch (SocketException ex){
				if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.IOPending || ex.SocketErrorCode == NoBufferSpaceAvailable){
				Thread.Sleep(30);
				} else {
				log.Error("Could not send on socket.");
				return false;
				}
			}
		} while (sent < size);
		return true;
	}

	private void recv(){
	do {
	rec + this.socket.Receive(buffer, offset + rec, size - rec, SocketFlags.none);
	} while (rec < size);
	}
}
}
