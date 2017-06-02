using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using LkDataContext;
using Presentation.Enum;
using AppConfig = XMCAServer.Data.AppConfig;

namespace XMCAServer.Job
{
    public class ReferralsQueueJob : AbstractJob
    {
        private IEnterpriseJobRequestQueueService jobRequestQueueServer;
        private static int? _JobReferralQueueDuration;
        private static int JobReferralQueueDuration
        {
            get
            {
                if (!_JobReferralQueueDuration.HasValue)
                {
                    using (var dataContext = new CVAcademicianDataContext(AppConfig.SqlConnection))
                    {
                        var config =
                            dataContext.CdConfig.FirstOrDefault(
                                it => it.Code == AppConfig.JOB_REFERRALS_QUEUE_DURATION);
                        if (config != null)
                        {
                            int duration = 0;
                            if (int.TryParse(config.Value, out duration))
                            {
                                if (duration >= 1*60*1000)
                                {
                                    _JobReferralQueueDuration = duration;
                                }
                            }

                        }
                    }
                }
                return _JobReferralQueueDuration ?? 5*60*1000;
            }
        }

        public ReferralsQueueJob()
        {
            jobRequestQueueServer = new EnterpriseJobRequestQueueService();
        }

        public override string TaskName
        {
            get { return "Referral Queue Job"; }
        }

        public override int SleepSecond
        {
            get { return 10; }
        }

        protected override void RunningTask(object sender)
        {
            using (var dataContext = new CVAcademicianDataContext(AppConfig.SqlConnection))
            {
                var time = DateTime.Now.AddMilliseconds(0 - JobReferralQueueDuration);
                var jobReferralList = dataContext.EnterpriseJobRequestQueue.Where(
                    it => !it.IsProcessed && !it.IsSendToEnterprise && it.ReferralState == (int) ReferralState.Pending &&
                          time >= it.CreateTime).ToList();
                foreach (var jobReferral in jobReferralList)
                {
                    jobRequestQueueServer.ChangeReferralJobQueueState(jobReferral.JobCode, jobReferral.ID);
                }
            }
        }
    }
}
