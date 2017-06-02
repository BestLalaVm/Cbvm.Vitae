using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using XMCAServer.Data;
using XMCAServer.Job;
using System.Timers;
using Timer = System.Timers.Timer;

namespace XMCAServer
{
    public class MainThread
    {
        public void Start(object sender)
        {
            LogHelper.Log("Start to run the xmca server-MainThead", LogType.Information);

            Timer timer = new Timer(2*1000);

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
            timer.Interval = 2*1000;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ((Timer) sender).Stop();
                var jobs = new IJob[] {new MailJob(), new ReferralsQueueJob()};
                foreach (var job in jobs)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(job.Running));
                }
                Thread.Sleep(2*60*1000);
                ((Timer) sender).Start();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.ToString(), LogType.Error);
            }
        }

        public void Stop()
        {
            LogHelper.Log("Stop to run the xmca server-MainThead", LogType.Information);
        }
    }
}
