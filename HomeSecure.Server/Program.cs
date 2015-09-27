using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using HomeSecure.Log;

namespace HomeSecure.Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Logger.Init("Log.config", "HomeSecureServerLogger");
            Logger.Info("HomeSecure Server Started");

            if (args[0] == "/console")
            {
                Service1 service = new Service1();
                service.StartHomeSecureService();

                Console.WriteLine("Application started, press enter to stop");
                Console.ReadLine();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			{ 
				new Service1() 
			};
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
