using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Enterprise
{
    public class EnterpriseRecruitBatchCriteria:BaseEnterpriseCriteria
    {
        public string Title
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public bool IncludeRelativeData
        {
            get; set;
        }
    }
}
