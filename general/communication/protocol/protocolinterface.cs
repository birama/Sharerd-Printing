namespace general{

public interface IProtocol<Ttransertype>{

/* Connect
   + Takes T and string as url.
*/
bool connect(Ttransertype obj, string url);
void disconnect();
bool send(Ttransertype obj);
//Ttransertype recv();

}
}
