using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HomeSecure.Data.Interfaces;
using HomeSecure.Log;
using HomeSecure.Data.Entities;
using HomeSecure.Server.Configuration;

namespace HomeSecure.Server.Logic
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class HomeSecureServerInputsListener : IHomeSecureServerInputs
    {
        private ServiceHost _service;

        public HomeSecureServerInputsListener()
        {
            using (ServerDataAccess conf = new ServerDataAccess("HomeSecureData"))
            {
                bool dbCreated = conf.Database.CreateIfNotExists();
            }
        }

        public void Start()
        {
            _service = new ServiceHost(this);
            _service.Open();
        }

        public void AddMotionSecurityEvent(MotionDetectionEvent motionEvent)
        {
            Logger.Debug("Security event detected");
            using (ServerDataAccess conf = new ServerDataAccess("HomeSecureData"))
            {
                conf.AddMotionDetectionEvent(motionEvent);
            }
        }


        public List<MotionDetectionEvent> GetMotionSecurityEvents(int pageSize, int pageNumber, out int total)
        {
            List<MotionDetectionEvent> result;

            using (ServerDataAccess conf = new ServerDataAccess("HomeSecureData"))
            {
                result = conf.GetMotionDetectionEvents(pageSize,pageNumber,out total);
            }

            return result;
        }
    }
}
