namespace general{

public interface IProtocol<Ttransertype>{

/* Connect
   + Takes T and string as url.
*/
public bool connect(Ttranfertype, string);
public void dissconnect();
public bool send(Ttransfertype);
public Ttransfertype recv();
}
}
