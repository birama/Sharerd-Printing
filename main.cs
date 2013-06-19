using general;
using general.Server;
using general.Client;

namespace Shared-Printing{

/* Sharerd Printing main
	+ used to start either a client or server using client or server classes.
	+ seperates the configuration from the startup process.
*/
class Shared-Printing{
private IApplication app;
static void Main(string[] args){
	if (args[0] == "server"){
	this.app = new Server();
	} else if (args[0]) == "client"{
	this.app = new Client();
	}
}

}
}
