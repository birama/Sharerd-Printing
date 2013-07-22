using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Printing;
using System.Threading;

namespace printing
{
    public class Printer
    {
        string printerName;
        private Queue<string> printerTasks = new Queue<string>();
        bool idle = true;

        public Printer()
        {
            printerName = GetDefaultPrinter();
            Console.WriteLine("Printer(): \t\t-New Printer:" + printerName + " CREATED");
        }

        public string GetPrinterName()
        {
            return printerName;
        }

        public void SetPrinterName(string newName)
        {
            printerName = newName;
        }

        public void PrintFilter(string filename)
        {
            switch (Path.GetExtension(filename))
            {
                case ".txt":
                    //PrintDebug(filename);
                    Console.WriteLine("Test2");
                    Print(filename);
                    break;

                case ".png":
                    //PrintDebug(filename);
                    Print(filename);
                    break;

                case ".jpg":
                    //PrintDebug(filename);
                    Print(filename);
                    break;

                case ".docx":
                    //PrintDebug(filename);
                    Print(filename);
                    break;

                case ".pdf":
                    //PrintDebug(filename);
                    Print(filename);
                    break;

                default:
                    Console.WriteLine("UNSUPPORTED FILE TYPE: " + filename);
                    break;
            }
        }

        public void Print(string filename)
        {
            try
            {

                Console.WriteLine();
                ProcessStartInfo processP = new ProcessStartInfo(filename);
                processP.Verb = "PrintTo";
                processP.Arguments = printerName;
                processP.CreateNoWindow = true;
                processP.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(processP);
                Console.WriteLine("Printing File: -" + filename);
                Thread.Sleep(750);
            }

            catch (Exception e)
            {
                Console.WriteLine("Printing ERROR: " + e);
            }
        }

        public void PrintDebug(string filename) //Only Reads TXT
        {
            try
            {
                Console.WriteLine("");
                using (StreamReader rf = new StreamReader(filename))
                {
                    String line = rf.ReadToEnd();
                    Console.WriteLine(line);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("File not found: " + e);
            }
        }

        public string GetDefaultPrinter()
        {
            string value = "Error: No Printers connected";
            PrinterSettings settings = new PrinterSettings();
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printerName;
                if (settings.IsDefaultPrinter)
                {
                    value = ("\"" + printerName + "\"");
                    Console.WriteLine(printerName);
                }
            }

            return value;
        }

        public int GetNumberOfPrintTasks()
        {
            LocalPrintServer localServer = new LocalPrintServer();
            PrintQueueCollection queueCollection = localServer.GetPrintQueues();
            PrintQueue printQueue = null;

            foreach (PrintQueue pq in queueCollection)
            {
                printQueue = LocalPrintServer.GetDefaultPrintQueue();
            }

            int numTasks = 0;
            if (printQueue != null)
                numTasks = printQueue.NumberOfJobs;

            return numTasks;
        }


        public void LoadTask(string filename, int copies)
        {
            for (int x = 0; x < copies; x++)
            {
                printerTasks.Enqueue(filename);
            }

            if (idle == true)
            {
                Console.WriteLine("-Added to the Queue \t Printing Started");
                PrintManager();
            }

            else
            {
                Console.WriteLine("-Added to the Queue");
            }
        }

        public void PrintManager()
        {
			int x = 0;
            //int x = GetNumberOfPrintTasks();
            //int x = printerTasks.Count();
            if (x > 0)
            {
                idle = false;
                PrintFilter(printerTasks.Dequeue());
                PrintManager();
            }

            else
            {
                idle = true;
                Console.WriteLine("All Tasks COMPLETED");

            }
        }


    }
}