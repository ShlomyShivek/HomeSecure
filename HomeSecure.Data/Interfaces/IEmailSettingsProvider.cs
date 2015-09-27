using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;

namespace HomeSecure.Data.Interfaces
{
    public interface IEmailSettingsProvider
    {
        EmailSettingsNotificatonDataWrapper GetEmailSettings();
    }
}
