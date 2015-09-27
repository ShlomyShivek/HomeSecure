using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeSecure.Data.Entities
{
    public class NotificationEntityParams
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public int NotificationEntityDataID { get; set; }
    }
}
