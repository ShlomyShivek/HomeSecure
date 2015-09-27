using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;
using HomeSecure.Client.Logic.Notifications;

namespace HomeSecure.Client
{
    public class EventLogNotification : SecurityEventSubscriber
    {
        private EventLog _eventLogWindow;

        public EventLogNotification(EventLog eventLogWindow)
        {
            _eventLogWindow = eventLogWindow;
        }

        public override void InitParams(Dictionary<string, NotificationEntityParams> parameters)
        {
        }

        public override void Notify(SecurityEvent securityEvent)
        {
            _eventLogWindow.AddSecurityEvent(securityEvent);
        }
    }
}
