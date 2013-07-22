using log4net;
using log4net.Config;
using System.Net;
using System.Net.Sockets;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace general {
public class Socket<Ttransfertype> : AProtocol<Ttransfertype> {
	private bool ttl;
	private int timeout;
	private Socket server;
	private Dictionary<string,Socket> sockets;
	private int tickcountstart;
	private IPHostEntry hostentry;
	readonly private ILog log = LogManager.GetLogger (typeof(Socket<Ttransfertype>));

	public Socket(Ddecapsulate d, Dencapsulate e, Dtopomsg c) : base(d,e,c) {
	this.ttl = true;
	this.sockets = new Dictionary<string,Socket> ();
	this.timeout = 300;
	log.Debug ("Created Socket Protocol.");
	}

	override public void connect (string url, int port) {
	log.Info ("Connecting to: " + url + " on port: " + port);
	this.hostentry = Dns.GetHostEntry(Dns.GetHostName());
	try {
		foreach(IPAddress address in this.hostentry.AddressList){
		IPEndPoint ipe = new IPEndPoint(address, port);
		this.sockets.Add(url,new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp));
		this.sockets[url].Connect (ipe);
			if(this.sockets[url].Connected){
			log.Debug("Protocol connected, asking topology to handshake.");
			this.topomsg(null,url,new ATopology<Ttransfertype>.Dsend(this.sendmsg),new ATopology<Ttransfertype>.Drecv(this.recv));
			break;
			} else {
			continue;
			}
		}
	} catch (Exception e){
	log.Error ("Cannot Connect: " + e.ToString());
	}
	}

	override public void listen (string url, int port) {
	log.Info ("Listing on port: " + port.ToString() + " with url: " + url);
	IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
	IPAddress ipAddress = ipHostInfo.AddressList[0];
	IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
	this.server = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
	int id = 0;
	try{
	server.Bind(localEndPoint);
	server.Listen(10);
		while(ttl || id < 10){
		++id;
		log.Debug ("Waiting for network activity.");
		this.sockets.Add(url+id.ToString(),server.Accept());
		log.Debug("Waiting for: " + url+id.ToString() + " ,connected party to make request.");
		this.recv(url+id.ToString());
		}
	} catch (Exception e){
	log.Error ("Listen failed: " + e.ToString());
	}
	}

	override public void disconnect () {
	log.Info ("Stopping to listen.");
	this.ttl = false;
	}

	override public void sendmsg (AComputer<Ttransfertype> obj) {
	log.Debug ("Sending on socket id: " + obj.to);
	this.tickcountstart = Environment.TickCount;
	
	byte[] buffsol = System.Text.Encoding.Unicode.GetBytes ("<SOL>");
	byte[] buff = System.Text.Encoding.Unicode.GetBytes(obj.ToString());
	byte[] buffeol = System.Text.Encoding.Unicode.GetBytes ("<EOL>");
	byte[] buffer  = new byte[buff.Length + buffeol.Length];
	buff.CopyTo (buffer, 0);
	buffeol.CopyTo (buffer, buff.Length);

	int offset = 0;
	int size = buffer.Length-1;
	int sent = 0;
		do {
			if (Environment.TickCount > this.tickcountstart + this.timeout){
			log.Error("Socket Timed out on sedning.");
			}
			try {
			log.Debug("Sending bytes: " + size);
			sent += this.sockets[obj.to].Send(buffer, offset + sent, size - sent, SocketFlags.None);
			} catch (SocketException ex){
				if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.IOPending || ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable){
				Thread.Sleep(30);
				} else {
				log.Error("Could not send on socket Exception: " + ex.ToString());
				}
			}
		} while (sent < size);
	}

	override public void send (ref Ttransfertype obj, string id){
	log.Debug ("encapsulating data to send to: " + id);
	this.encap(ref obj,new ATopology<Ttransfertype>.Dsend(this.sendmsg),id);
	}

	public override void recv(string id){
	log.Debug ("Recving on socket id: " + id);
	this.tickcountstart = Environment.TickCount;
	List<byte> buffer = new List<byte> ();
	byte[] buff = new byte[255];
	int rec = 0;
		if (this.sockets [id].Poll (-1, SelectMode.SelectWrite)) {
		do {
			if (Environment.TickCount > this.tickcountstart + this.timeout) {
			log.Error ("Socket Timed out on recving.");
			}
			try {
			log.Debug ("Recving more bytes.");
			rec += this.sockets [id].Receive (buff,255, SocketFlags.None);
			buffer.AddRange (buff);
			} catch (SocketException ex) {
				if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.IOPending || ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable) {
				Thread.Sleep (30);
				} else {
				log.Error ("Could not recive on socket Exception: " + ex.ToString());
				}
			}
			string t = new string(System.Text.Encoding.Unicode.GetChars(buffer.ToArray()));
			log.Debug("T is: " + t);
			if (System.Text.RegularExpressions.Regex.IsMatch(t,"." + System.Text.RegularExpressions.Regex.Escape("<EOL>") + ".")){
			log.Debug("EOL FOUND.");
			break;
			}
		} while (true);
		} else {
		log.Debug ("Poll returned nothing to read.");
		}
		AComputer<Ttransfertype> obj = new Computer<Ttransfertype> ();
		obj.connectionid = new string(System.Text.Encoding.Unicode.GetChars (buffer.ToArray()));
		obj.topologymessage = false;
		if (obj.data != null || obj != null || obj.topologymessage != false) {
		log.Debug ("Data found returning to client.");
		this.decap (obj, id);
		} else {
		log.Debug ("Message found sending to topology.");
		this.topomsg (obj, id, new ATopology<Ttransfertype>.Dsend (this.sendmsg), new ATopology<Ttransfertype>.Drecv (this.recv));
		}
	}
}
}