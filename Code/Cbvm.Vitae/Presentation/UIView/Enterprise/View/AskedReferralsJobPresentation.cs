using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise.View
{
    public class AskedReferralsJobPresentation : BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string JobCode
        {
            get; set;
        }

        public string JobName
        {
            get; set;
        }

        public ReferralState ReferralState
        {
            get; set;
        }

        public string AskedReferralNote
        {
            get; set;
        }

        public DateTime RequestTime
        {
            get; set;
        }

        public string StudentNum
        {
            get; set;
        }

        public string StudentName
        {
            get; set;
        }

        public string StudentMarjorName
        {
            get; set;
        }

        public string StudentClass
        {
            get; set;
        }

        public string SexRequired
        {
            get; set;
        }

        public string Education
        {
            get; set;
        }

        public string WorkPlace
        {
            get; set;
        }

        public string SalaryScope
        {
            get; set;
        }

        public DateTime? StartTime
        {
            get; set;
        }

        public DateTime? EndTime
        {
            get; set;
        }

        public string EnterpriseName
        {
            get; set;
        }

        public RequestQueueType RequestQueueType
        {
            get; set;
        }
    }
}
