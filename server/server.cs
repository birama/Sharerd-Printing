using System;
using System.Runtime.Remoting;
using general;

using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace printproject{

class ServerStartup{
List<IPrinter> printerList = new List<IPrinter>();

static void Main(string[] args){
            Console.WriteLine("static void Main(): \t-Server Started");
		IProtcol<IPrintTask> comm = new SoapProtocol<IPrintTask>();
		comm.connect(PrintTask,"MyRemoteObject.soap");
            // the server will keep running until keypress.
            Thread.Sleep(1000);
            Printer ptest = new Printer("debug");
            PrintTask pm = new PrintTask();
            Thread.Sleep(1000);
            PrintTask pmf = new PrintTask("me","doc",1);
            Console.ReadLine();
        }

        /*
        public void AddPrinter(Printer printer)
        {
            printerList.Add(printer);
            Console.WriteLine("Printer: " + printer.GetPrinterName() + " has been added");
        }
          */
    }
}
