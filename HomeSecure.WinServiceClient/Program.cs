using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using HomeSecure.Data.Entities;
using HomeSecure.Client.Logic;
using HomeSecure.Data;
using HomeSecure.Client.Logic.Notifications;
using System.Configuration;
using HomeSecure.Client.Logic.Notifications.Filters;
using System.Threading;
using HomeSecure.Log;

namespace HomeSecure.WinServiceClient
{
    static class Program
    {
        private static SecurityEventSubscriber _emailSubscriber;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Logger.Init("Log.config", "HomeSecureServiceClient");
            Logger.Info("HomeSecure Service Client Started");

            Dictionary<string, string> emailParams = new Dictionary<string, string>();
            emailParams.Add("Host", ConfigurationManager.AppSettings["emailHost"]);
            emailParams.Add("Port", ConfigurationManager.AppSettings["emailPort"]);
            emailParams.Add("From", ConfigurationManager.AppSettings["emailFrom"]);
            emailParams.Add("To", ConfigurationManager.AppSettings["emailTo"]);
            emailParams.Add("Subject", ConfigurationManager.AppSettings["emailSubject"]);
            emailParams.Add("UserName", ConfigurationManager.AppSettings["emailUserName"]);
            emailParams.Add("Password", ConfigurationManager.AppSettings["emailPassword"]);

            _emailSubscriber = new EmailSecurityEventSubscriber();
            //_emailSubscriber.InitParams(emailParams);

            _emailSubscriber = new TimeoutFilter(_emailSubscriber, Int32.Parse(ConfigurationManager.AppSettings["emailTimeBetweenMailsSeconds"]));

            if ((args.Length > 0) && (args[0] == "/console"))
            {
                CameraDevice camera = CameraDeviceFactory.DetectCameraDeviceHardware();

                CameraMotionDetector motionDetector = new CameraMotionDetector(camera);
                motionDetector.MotionDetected += new EventHandler<MotionDetectedEventArgs>(motionDetector_MotionDetected);
                
                camera.StartVideo();
                Thread.Sleep(Int32.Parse(ConfigurationManager.AppSettings["timeToWaitBeforeStartDetectionSeconds"])*1000);
                Logger.Info("Start detecting");
                motionDetector.StartDetection();
                

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

        static void motionDetector_MotionDetected(object sender, Data.MotionDetectedEventArgs e)
        {
            Logger.Info("Motion detected");
            _emailSubscriber.Notify(e.MotionDetectionEvent);
        }
    }
}
