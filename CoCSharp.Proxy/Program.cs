﻿using CoCSharp.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace CoCSharp.Proxy
{
    public class Program
    {
        public static CoCProxy Proxy { get; set; }
        public static ProxyConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Console.Title = "CoC# Proxy";
            Console.WriteLine("Starting proxy...");

            Configuration = ProxyConfiguration.LoadConfiguration("proxyConfig.xml");
            if (Configuration.DeleteLogOnStartup)
                File.Delete("packets.log");

            Proxy = new CoCProxy();
            Proxy.ServerAddress = Configuration.ServerAddress;
            Proxy.ServerPort = Configuration.ServerPort;

            Proxy.Start(new IPEndPoint(IPAddress.Any, Configuration.ProxyPort));

            Console.WriteLine("CoC# is running on *:{0}", Configuration.ProxyPort);
            Thread.Sleep(-1);
        }
    }
}
