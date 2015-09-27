using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Server.Configuration;
using HomeSecure.Data.Entities;
using HomeSecure.Log;

namespace HomeSecure.Server.DataAccess.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Init("Log.config", "DataAccessTestLogger");

            MotionDetectionEvent mde = new MotionDetectionEvent()
            {
                CameraDevice = new CameraDevice()
                {
                    ID = "123",
                    Name = "Name"
                },
                ID = 1,
                NumberOfPixelsDetected = 100,
                SecurityEventTime = DateTime.Now
            };

            ServerDataAccess conf = new ServerDataAccess("HomeSecureData");
            conf.AddMotionDetectionEvent(mde);

            int total = 0;
            List<MotionDetectionEvent> motionDetectionEvents = conf.GetMotionDetectionEvents(2, 1, out total);
        }
    }
}
