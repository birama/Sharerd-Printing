﻿using System;

namespace general {
public interface IPrintTask {
	void SetPrintTask (string userName, string printName, int printCopies);

	String GetUserName ();

	void SetUserName (string name);

	String GetPrintName ();

	void SetPrintName (string name);

	int GetCopies ();

	void SetCopies (int copies);
}
}
