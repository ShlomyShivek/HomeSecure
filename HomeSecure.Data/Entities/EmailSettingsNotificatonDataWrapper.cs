using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeSecure.Data.Entities
{
    public class EmailSettingsNotificatonDataWrapper
    {
        private NotificationEntityData _emailSettingsFromDb;

        public EmailSettingsNotificatonDataWrapper(NotificationEntityData notificationData)
        {
            _emailSettingsFromDb = notificationData;
        }

        public string SmtpServer
        {
            get
            {
                return _emailSettingsFromDb.GetNotificationParamValue("Host");
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("Host", value);
            }
        }

        public int Port
        {
            get
            {
                return Int32.Parse(_emailSettingsFromDb.GetNotificationParamValue("Port"));
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("Port", value.ToString());
            }
        }

        public string UserName
        {
            get
            {
                return _emailSettingsFromDb.GetNotificationParamValue("UserName");
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("UserName", value);
            }
        }

        public string Password
        {
            get
            {
                return _emailSettingsFromDb.GetNotificationParamValue("Password");
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("Password", value);
            }
        }

        public string From
        {
            get
            {
                return _emailSettingsFromDb.GetNotificationParamValue("From");
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("From", value);
            }
        }

        public string To
        {
            get
            {
                return _emailSettingsFromDb.GetNotificationParamValue("To");
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("To", value);
            }
        }

        public string Subject
        {
            get
            {
                return _emailSettingsFromDb.GetNotificationParamValue("Subject");
            }
            set
            {
                _emailSettingsFromDb.SetNotificationParamValue("Subject", value);
            }
        }


    }
}
