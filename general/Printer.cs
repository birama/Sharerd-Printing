using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace general{

public class Printer : MarshalByRefObject, IPrinter{
//Private
//Protected

//Public
public string printerName;
private Queue<string> printerTasks = new Queue<string>();
bool idle;

public Printer(){
Console.WriteLine("Printer(): \t\t-New Empty Printer created");
idle = CheckIdle();
}

public Printer(string printerName){
SetPrinterName(printerName);
Console.WriteLine("Printer(): \t\t-New Full Printer created : " + GetPrinterName() );
idle = CheckIdle();
}

//GETTERS AND SETTERS

public string GetPrinterName(){
return printerName;
}

public void SetPrinterName(string name){
printerName = name;
}


public bool CheckIdle(){
	if (this.printerTasks.Count == 0){
	Console.WriteLine("CheckIdle(): \t\t-Set to Idle");
	return true;
	} else {
	Console.WriteLine("CheckIdle(): \t\t-Set to Busy");
	return false;
	}
}

public void LoadPrintTask(string printName){
printerTasks.Enqueue(printName);
	if (idle == false){
	Console.WriteLine("LoadPrintTask(): \t-Busy");
	} else {
	Console.WriteLine("LoadPrintTask(): \t-Idle");
	Print(); 
	}
}

public void Print(){
Thread.Sleep(1300);
Console.WriteLine(printerTasks.Dequeue() + "PRINTED");
	if (!CheckIdle()){
	Print();
	} else {
	Console.WriteLine ("Print() \t\t-All Tasks Completed");
	}
}

}
}
