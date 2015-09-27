using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data;
using System.Net.Mail;
using System.Net;
using HomeSecure.Data.Entities;
using HomeSecure.Log;

namespace HomeSecure.Client.Logic.Notifications
{
    public class EmailSecurityEventSubscriber : SecurityEventSubscriber
    {
        private string _host;
        private string _from;
        private string _to;
        private string _subject;

        private int _port;

        private NetworkCredential _networkCredentials;

        public EmailSecurityEventSubscriber()
        {
        }

        public override void InitParams(Dictionary<string, NotificationEntityParams> parameters)
        {
            _host = GetSafeValue<string>(parameters, "Host", string.Empty);
            _port = GetSafeValue<int>(parameters, "Port", -1);
            _from = GetSafeValue<string>(parameters, "From", string.Empty);
            _to = GetSafeValue<string>(parameters, "To", string.Empty);
            _subject = GetSafeValue<string>(parameters, "Subject", string.Empty);

            string userName = GetSafeValue<string>(parameters, "UserName", string.Empty);
            string password = GetSafeValue<string>(parameters, "Password", string.Empty);
            if ((!string.IsNullOrEmpty(userName)) && (!string.IsNullOrEmpty(password)))
            {
                _networkCredentials = new NetworkCredential(userName, password);
            }
        }


        public override void Notify(SecurityEvent securityEvent)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_host, _port))
                {
                    client.Credentials = _networkCredentials;
                    client.Send(_from, _to, _subject, securityEvent.GetEmailNotificationBody());
                    Logger.InfoFormat("Email notification sent to {0}", _to);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to send email notification", ex);
            }
            
        }
    }
}
