using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HomeSecure.Data.Entities
{
    [DataContract]
    public class MotionDetectionEvent : SecurityEvent
    {
        [DataMember]
        public int NumberOfPixelsDetected { get; set; }

        [DataMember]
        public CameraDevice CameraDevice { get; set; }

        public override string GetEmailNotificationBody()
        {
            return string.Empty;
        }
    }
}
