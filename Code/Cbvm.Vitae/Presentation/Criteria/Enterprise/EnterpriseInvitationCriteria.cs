using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Criteria.Enterprise
{
    public class EnterpriseInvitationCriteria:BaseEnterpriseCriteria
    {
        public string StudentName { get; set; }

        public string EnterpriseName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
