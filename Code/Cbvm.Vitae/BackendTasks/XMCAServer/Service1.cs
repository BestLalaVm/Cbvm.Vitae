using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using XMCAServer.Data;

namespace XMCAServer
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        public void Start()
        {
            LogHelper.Log("Begin to execute the service", LogType.Information);
            var mainTHread = new MainThread();
            Thread thread =new Thread(new ParameterizedThreadStart(mainTHread.Start));
            thread.Start();
        }

        protected override void OnStop()
        {
            LogHelper.Log("Stop the service", LogType.Information);
        }
    }
}
