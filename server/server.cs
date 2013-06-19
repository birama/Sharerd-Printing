using System;
using System.Runtime.Remoting;
using general;
using System.Collections;

namespace Server{

class Server : IApplication{

private List<IPrinter> printerList;
private ATopology<IPrintTask> comm; 

public Server(){
this.printerlist = new List<IPrinter>();
this.comm = new ServerClient<IPrintTask>(SoapProtocol<IPrinttask>);
}

public bool init(){
PrintTask pm = new PrintTask();
Printer ptest = new Printer("debug");
PrintTask pmf = new PrintTask("me","doc",1);
return true;
}

public bool start(){
Console.WriteLine("static void Main(): \t-Server Started");
this.comm.connect(PrintTask);
return true;
}

public bool stop(){}
return this.comm.disconnect();
}
}
