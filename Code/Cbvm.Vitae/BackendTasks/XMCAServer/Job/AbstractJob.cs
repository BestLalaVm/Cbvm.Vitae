using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMCAServer.Data;

namespace XMCAServer.Job
{
    public abstract class AbstractJob:IJob
    {
        public abstract string TaskName
        {
            get;
        }

        public abstract int SleepSecond
        {
            get;
        }

        public void Running(object sender)
        {
            string msg = string.Format("running in task:{0} at:{1}", TaskName, DateTime.Now);
            LogHelper.Log(msg, LogType.Information);
            RunningTask(sender);
            System.Threading.Thread.Sleep(SleepSecond * 1000);
        }

        protected abstract void RunningTask(object sender);

        public virtual void Stop()
        {
            LogHelper.Log(string.Format("stopping in task:{0}   at:{1}", TaskName, DateTime.Now), LogType.Information);
        }
    }
}
