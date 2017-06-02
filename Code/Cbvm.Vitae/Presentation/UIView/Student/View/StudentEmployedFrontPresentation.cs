using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;
using Presentation.UIView.Enterprise;

namespace Presentation.UIView.Student.View
{
    [Serializable]
    public class StudentEmployedFrontPresentation:StudentPresentation
    {
        public int JobRequestId
        {
            get; set;
        }

        public int? ReferralsQueueId
        {
            get; set;
        }

        public string JobName { get; set; }

        public string EnterpriseName { get; set; }

        public string EnterpriseCode { get; set; }

        public DateTime RequestTime { get; set; }

        public string ReferralerNames
        {
            get
            {
                if (JobReferralers == null)
                {
                    return "";
                }
                return String.Join(",",
                                   JobReferralers.Where(it => it.ReferralState == ReferralState.Passed)
                                                               .Select(it => it.NameZh));
            }
        }

        public DateTime EmployedTime { get; set; }

        public DateTime? RecruitFlowChangedTime
        {
            get; set;
        }

        public List<EnterpriseJobReferralerPresentation> JobReferralers { get; set; }
    }
}
