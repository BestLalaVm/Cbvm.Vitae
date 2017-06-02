using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Presentation.UIView.Enterprise.View
{
    public class EnterpriseJobReferralsQueueForStudentPresentation:BasePresentation
    {
        public string JobCode
        {
            get; set;
        }

        public string JobName
        {
            get; set;
        }

        public string Referralers
        {
            get; set;
        }

        public string ActualReferralers
        {
            get; set;
        }

        public string ReferralState
        {
            get; set;
        }

        public DateTime ReferralTime
        {
            get; set;
        }

        public string EnterpriseName
        {
            get; set;
        }

        public int Num
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

        public DateTime StartTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }
    }
}
