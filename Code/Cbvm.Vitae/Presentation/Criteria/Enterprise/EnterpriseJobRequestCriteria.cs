using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Enterprise
{
    public class EnterpriseJobRequestCriteria:BaseCriteria
    {
        public string EnterpriseName
        {
            get; set;
        }

        public string JobName
        {
            get; set;
        }

        public string EnterpriseCode
        {
            get; set;
        }

        public int? BatchId
        {
            get; set;
        }

        public DateTime? RequestTimeFrom
        {
            get; set;
        }

        public DateTime? RequestTimeTo
        {
            get; set;
        }

        public string StudentName
        {
            get; set;
        }

        public bool IsReferralsQueueIDNotNull
        {
            get; set;
        }

        public string Referraler
        {
            get; set;
        }

        public string RecruitFlowID
        {
            get; set;
        }
    }
}
