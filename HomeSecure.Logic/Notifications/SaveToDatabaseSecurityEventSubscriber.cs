using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client.Logic.Notifications
{
    public class SaveToDatabaseSecurityEventSubscriber : SecurityEventSubscriber
    {
        private IHomeSecureConfig _config;

        public SaveToDatabaseSecurityEventSubscriber(IHomeSecureConfig config)
        {
            _config = config;
        }

        public override void InitParams(Dictionary<string, NotificationEntityParams> parameters)
        {
        }

        public override void Notify(SecurityEvent securityEvent)
        {
            _config.AddSecurityEvent(securityEvent);
            _config.Save();
        }
    }
}
