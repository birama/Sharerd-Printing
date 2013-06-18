using System;
using System.Runtime.Remoting;
using general;
using System.Collections;

namespace printproject{

class Server{

public Server(){
List<IPrinter> printerList = new List<IPrinter>();
Console.WriteLine("static void Main(): \t-Server Started");
ATopology<IPrintTask> comm = new ServerClient<IPrintTask>(SoapProtocol<IPrinttask>);
comm.connect(PrintTask);
// the server will keep running until keypress.
Thread.Sleep(1000);
Printer ptest = new Printer("debug");
PrintTask pm = new PrintTask();
Thread.Sleep(1000);
PrintTask pmf = new PrintTask("me","doc",1);
Console.ReadLine();
}

}
}
