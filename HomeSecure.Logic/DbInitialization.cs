using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;
using HomeSecure.Data;

namespace HomeSecure.Client.Logic
{
    public static class DbInitialization
    {
        public static void InitDbOnFirstRun(IHomeSecureConfig config)
        {
            config.DeleteAllNotificationEntityParams();
            config.DelteAllNotificationEntityData();
            config.Save();

            NotificationEntityData notificationEntityData = new NotificationEntityData();
            notificationEntityData.FriendlyTypeName = "Email Notification";
            notificationEntityData.Name = "Notify My Gmail";
            notificationEntityData.Type = "HomeSecure.Client.Logic.Notifications.EmailSecurityEventSubscriber, HomeSecure.Client.Logic";
            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "Host",
                    Value = "smtp.gmail.com"
                });
            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "Port",
                    Value = "25"
                });
            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "UserName",
                    Value = "kookygateway"
                });
            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "Password",
                    Value = "k00kygatewa1"
                });
            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "From",
                    Value = "kookygateway@gmail.com"
                });

            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "To",
                    Value = "shlomyshivek@gmail.com"
                });

            notificationEntityData.NotificationParams.Add(
                new NotificationEntityParams()
                {
                    Key = "Subject",
                    Value = "Security Alert"
                });

            config.AddNotificationEntityData(notificationEntityData);

            config.Save();
        }
    }
}
