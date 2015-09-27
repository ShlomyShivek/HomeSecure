using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client.Logic
{
    public interface IClientView
    {
        void OnMotionDetectionEvent(MotionDetectionEvent motionDetectionEvent);

        void AddCamera(CameraDevice camera);
    }
}
