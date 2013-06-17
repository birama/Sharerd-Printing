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
            Console.ReadLine();

        }
    }
}
