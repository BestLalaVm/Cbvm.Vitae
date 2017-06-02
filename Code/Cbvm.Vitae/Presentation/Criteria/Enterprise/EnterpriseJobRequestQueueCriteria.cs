using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Enterprise
{
    public class EnterpriseJobRequestQueueCriteria : BaseEnterpriseCriteria
    {
        public string EnterpriseName
        {
            get; set;
        }

        public string JobName
        {
            get; set;
        }

        public string WorkPlace
        {
            get; set;
        }

        public bool IncludeRelativeData
        {
            get; set;
        }

        public string TeacherNum
        {
            get; set;
        }

        public string MarjorCode { get; set; }

        public string StudentName { get; set; }

        public string StudentNum { get; set; }

        public string ReferralStateFromReferraler { get; set; }
    }
}
