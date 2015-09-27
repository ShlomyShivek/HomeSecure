using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Log;

namespace HomeSecure.Server.Logic
{
    public class ServerLogicEntry
    {
        private HomeSecureServerInputsListener _wcfListener;

        public ServerLogicEntry()
        {
            _wcfListener = new HomeSecureServerInputsListener();
        }

        public void Start()
        {
            _wcfListener.Start();
            Logger.Info("WCF Listener started");
        }
    }
}
