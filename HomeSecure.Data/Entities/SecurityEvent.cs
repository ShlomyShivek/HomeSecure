using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace HomeSecure.Data.Entities
{
    [DataContract]
    public abstract class SecurityEvent
    {
        public SecurityEvent()
        {
            SecurityEventTime = DateTime.Now;
        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime SecurityEventTime { get; set; }

        [IgnoreDataMember]
        public IInputDevice InputDevice { get; set; }

        public abstract string GetEmailNotificationBody();
    }
}
