using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client.Logic.Notifications.Filters
{
    public class TimeoutFilter : SecurityEventSubscriber
    {
        private SecurityEventSubscriber _realNotification;
        private Dictionary<string, DateTime> _mapInputDeviceIDToLastNotificationTime;
        private int _totalSecondsBetweenAlerts;

        public TimeoutFilter(SecurityEventSubscriber realNotification, int totalSecondsBetweenAlerts = 5)
        {
            _totalSecondsBetweenAlerts = totalSecondsBetweenAlerts;
            _realNotification = realNotification;
            _mapInputDeviceIDToLastNotificationTime = new Dictionary<string, DateTime>();
        }


        public override void InitParams(Dictionary<string, NotificationEntityParams> parameters)
        {
        }


        public override void Notify(SecurityEvent securityEvent)
        {
            if (!_mapInputDeviceIDToLastNotificationTime.ContainsKey(securityEvent.InputDevice.ID))
            {
                _mapInputDeviceIDToLastNotificationTime.Add(securityEvent.InputDevice.ID, DateTime.MinValue);
            }
            DateTime lastNotificationForInputDevice = _mapInputDeviceIDToLastNotificationTime[securityEvent.InputDevice.ID];
            if ((DateTime.Now - lastNotificationForInputDevice).TotalSeconds > _totalSecondsBetweenAlerts)
            {
                _realNotification.Notify(securityEvent);
                _mapInputDeviceIDToLastNotificationTime[securityEvent.InputDevice.ID] = DateTime.Now;
            }
        }
    }
}
