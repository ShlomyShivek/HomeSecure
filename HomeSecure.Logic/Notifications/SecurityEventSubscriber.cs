using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client.Logic.Notifications
{
    public abstract class SecurityEventSubscriber
    {
        public abstract void InitParams(Dictionary<string, NotificationEntityParams> parameters);

        public abstract void Notify(SecurityEvent securityEvent);

        protected T GetSafeValue<T>(Dictionary<string, NotificationEntityParams> parameters, string key, T defaultValue)
        {
            T result = defaultValue;
            if (parameters.ContainsKey(key))
            {
                try
                {
                    result = (T)Convert.ChangeType(parameters[key].Value, typeof(T));
                }
                catch
                {
                    result = defaultValue;
                }
            }

            return result;
        }
    }
}
