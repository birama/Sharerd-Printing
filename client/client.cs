using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using general;
using System;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace client
{
    class client
    {
        delegate void SetValueDelegate(int value);
        delegate String GetNameDelegate();

        static void Main(string[] args)
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            IPrintTask obj = (IPrintTask)Activator.GetObject(
            typeof(IPrintTask),
            "http://localhost:1234/MyRemoteObject.soap");
            Console.WriteLine("Client.Main(): Reference to rem.obj. acquired");
            //Console.WriteLine(obj.GetPrintName());
            //SetValueDelegate svDelegate = new SetValueDelegate(obj.SetPrintTask(v1, v2, v3));
            SetValueDelegate svDelegate = new SetValueDelegate(obj.SetCopies);
            IAsyncResult svAsyncres = svDelegate.BeginInvoke(1, null, null);
			
			Console.WriteLine("Client.Main(): Will call getPrintName()");
			GetPrintNameDelegate gnPrintDelegate = new GetPrintNameDelegate(obj.GetPrintName);
			IAsyncResult gnAsyncres = gnPrintDelegate.BeginInvoke(null,null);
		
			
			Console.WriteLine("Client.Main(): Will call getUserName()");
			GetUserNameDelegate gnUserDelegate = new GetUserNameDelegate(obj.GetUserName);
			IAsyncResult gnAsyncres2 = gnUserDelegate.BeginInvoke(null,null);
			

			Console.WriteLine("Client.Main(): EndInvoke for setValue()");
			svDelegate.EndInvoke(svAsyncres);
			
			Console.WriteLine("Client.Main(): EndInvoke for getPrintName()");
			String printname = gnPrintDelegate.EndInvoke(gnAsyncres);
			
			Console.WriteLine("Client.Main(): EndInvoke for getUserName()");
			String username = gnUserDelegate.EndInvoke(gnAsyncres2);

			Console.WriteLine("Client.Main(): received name {0}",printname);
			Console.WriteLine("Client.Main(): received name {0}",username);

			Console.WriteLine("Client.Main(): Will now read value");
			int copy = obj.GetCopies();
			Console.WriteLine("Client.Main(): New server side value {0}", copy);
			
            Console.ReadLine();

        }
    }
}
