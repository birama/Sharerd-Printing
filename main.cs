using general;
using Server;
using Client;
using log4net;
using log4net.Config;
using System;

//using Client;
namespace SharedPrinting {
/* Sharerd Printing main
	+ used to start either a client or server using client or server classes.
	+ seperates the configuration from the startup process.
*/
class SharedPrinting {
	static readonly private ILog log = LogManager.GetLogger (typeof(SharedPrinting));

	static void Main (string[] args) {
		BasicConfigurator.Configure ();
		IApplication app = null;

		if (args == null || args.Length < 1) {
		app = new Server.Server ();
		} else {
			if (args [0] == "server") {
			app = new Server.Server ();
			} else if (args [0] == "client") {
			app = new Client.Client ();
			} else {

			}
		}
		log.Info ("Statung app");
		if (app != null) {
			if (app.init ()) {
			app.start ();
			}
		}
		Console.ReadLine ();
		log.Info ("Stoping app");
	}
}
}