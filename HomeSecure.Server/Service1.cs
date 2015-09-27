using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using HomeSecure.Server.Logic;

namespace HomeSecure.Server
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        public void StartHomeSecureService()
        {
            ServerLogicEntry sle = new ServerLogicEntry();
            sle.Start();
        }
    }
}
