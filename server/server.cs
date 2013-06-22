using System;
using System.Runtime.Remoting;
using general;
using System.Collections;
using System.Collections.Generic;

namespace Server{

class Server : IApplication{

private List<IPrinter<IPrintTask,int>> printerList;
private ATopology<IPrintTask> comm; 

public Server(){
this.printerList = new List<IPrinter>();
this.comm = new ServerClient<IPrintTask>();
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

public bool stop(){
return this.comm.disconnect();
}

}
}
