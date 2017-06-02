using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LkDataContext;
using XMCAServer.Data;
using AppConfig = XMCAServer.Data.AppConfig;

namespace XMCAServer.Job
{
    public class MailJob : AbstractJob
    {
        public override string TaskName
        {
            get { return "Mail Server"; }
        }

        protected override void RunningTask(object sender)
        {
            using (var dataContext = new CVAcademicianDataContext(AppConfig.SqlConnection))
            {
                var mailList = dataContext.MailQueue.Where(it => !it.IsSended).ToList();
                foreach (var mail in mailList)
                {
                    if (EmailHelper.SendEmail(mail.Name, mail.Message, mail.Receiver, mail.Sender, mail.Sender))
                    {
                        mail.IsSended = true;
                        dataContext.SubmitChanges();
                    }
                }

                LogHelper.Log("Mail Job Running", LogType.Information);
            }
        }

        public override int SleepSecond
        {
            get { return 2 * 60; }
        }

    }
}
