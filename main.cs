using general;
using Server;

//using Client;
namespace SharedPrinting {
/* Sharerd Printing main
	+ used to start either a client or server using client or server classes.
	+ seperates the configuration from the startup process.
*/
class SharedPrinting {
	static void Main (string[] args) {
		IApplication app = null;
		if (args == null || args.Length < 1) {
			app = new Server.Server ();
		} else {
			if (args [0] == "server") {
				app = new Server.Server ();
			} else if (args [0] == "client") {
//	app = new Client.Client();
				} else {

				}
		}
		if (app != null) {
			if (app.init ()) {
				app.start ();
			}
		}
	}
}
}