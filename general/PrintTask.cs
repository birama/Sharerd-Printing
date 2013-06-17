namespace general{

class PrintTask : MarshalByRefObject, IPrintTask{
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
}
