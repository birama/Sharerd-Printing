namespace general{

public interface IProtocol<Ttransfertype>{

/* Connect
   + Takes T and string as url.
*/
bool connect(Ttransfertype obj, string url);
void disconnect();
bool send(Ttransfertype obj);
//Ttransertype recv();

}
}
