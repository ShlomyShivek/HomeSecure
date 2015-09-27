using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeSecure.Data.Entities
{
    public class NotificationEntityData
    {
        public NotificationEntityData()
        {
            NotificationParams = new List<NotificationEntityParams>();
        }

        public int ID { get; set; }

        public string FriendlyTypeName { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        //public List<NotificationEntityParams> NotificationParams { get; set; }

        // Navigation property 
        public virtual ICollection<NotificationEntityParams> NotificationParams { get; set; }

        /// <summary>
        /// Update existing notification param or add new one if key doesn't exist.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetNotificationParamValue(string key, string value)
        {
            NotificationEntityParams paramObj = NotificationParams.FirstOrDefault(a => a.Key == key);
            if (paramObj == null)
            {
                paramObj = new NotificationEntityParams()
                {
                    Key = key,
                    NotificationEntityDataID = ID,
                };
                NotificationParams.Add(paramObj);
            }
            paramObj.Value = value;
        }

        public string GetNotificationParamValue(string key, string defaultValue = "")
        {
            string result = defaultValue;

            NotificationEntityParams paramObj = NotificationParams.FirstOrDefault(a => a.Key == key);
            if (paramObj != null)
            {
                result = paramObj.Value;
            }

            return result;
        }
    }
}
