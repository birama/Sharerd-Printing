using System;
using System.Runtime.Remoting;
using general;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace printproject
{
    class PrintTask : MarshalByRefObject, IPrintTask
    {
        string userName;
        string printName;
        int printCopies;

        public PrintTask()
        {
            Console.WriteLine("PrintTask(): \t\t-New Empty Task created");
        }

        public PrintTask(string userName, string printName, int printCopies)
        {
            SetUserName(userName);
            SetPrintName(printName);
            SetCopies(printCopies);
            Console.WriteLine("PrintTask(3): \t\t-New Full Task created: "+GetPrintName());
            Console.WriteLine("\t\t\t\t." + userName);
            Console.WriteLine("\t\t\t\t." + printName);
            Console.WriteLine("\t\t\t\t." + printCopies);
        }

        //GETTERS AND SETTERS

        public void SetPrintTask(string userName, string printName, int printCopies)
        {
            SetUserName(userName);
            SetPrintName(printName);
            SetCopies(printCopies);
        }

        public string GetUserName()
        {
            return userName;
        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public string GetPrintName()
        {
            return printName;
        }

        public void SetPrintName(string name)
        {
            printName = name;
        }

        public int GetCopies()
        {
            return printCopies;
        }

        public void SetCopies(int copies)
        {
            printCopies = copies;
        }
    }

    
    class Printer : MarshalByRefObject, IPrinter
    {
        string printerName;
        Queue<string> printerTasks = new Queue<string>();
        //bool active;
        bool idle;

        public Printer()
        {
            Console.WriteLine("Printer(): \t\t-New Empty Printer created");
            idle = CheckIdle();
            
        }

        public Printer(string printerName)
        {
            SetPrinterName(printerName);
            Console.WriteLine("Printer(): \t\t-New Full Printer created : " + GetPrinterName() );
            idle = CheckIdle();
        }

        //GETTERS AND SETTERS

        public string GetPrinterName()
        {
            return printerName;
        }

        public void SetPrinterName(string name)
        {
            printerName = name;
        }


        public bool CheckIdle()
        {
            if (this.printerTasks.Count == 0)
            {
                Console.WriteLine("CheckIdle(): \t\t-Set to Idle");
                return true;
            }

            else
            {
                Console.WriteLine("CheckIdle(): \t\t-Set to Busy");
                return false;
            }
        }

        public void LoadPrintTask(string printName)
        {

            printerTasks.Enqueue(printName);

            if (idle == false)
            {
                Console.WriteLine("LoadPrintTask(): \t-Busy");
            }

            else
            {
                Console.WriteLine("LoadPrintTask(): \t-Idle");
                Print(); 
            }
        }

        public void Print()
        {
            Thread.Sleep(1300);
            Console.WriteLine(printerTasks.Dequeue() + "PRINTED");

            if (!CheckIdle())
            {
                Print();
            }

            else
            {
                Console.WriteLine ("Print() \t\t-All Tasks Completed");
            }
        }

    }


    class ServerStartup
    {
        List<Printer> printerList = new List<Printer>();

        static void Main(string[] args)
        {
            Console.WriteLine("static void Main(): \t-Server Started");
            HttpChannel chnl = new HttpChannel(1234);
            ChannelServices.RegisterChannel(chnl);
            RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(PrintTask),
            "MyRemoteObject.soap",
            WellKnownObjectMode.Singleton);
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
