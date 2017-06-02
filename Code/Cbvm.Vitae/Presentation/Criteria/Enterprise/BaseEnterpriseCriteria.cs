using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Enterprise
{
    public class BaseEnterpriseCriteria:BaseCriteria
    {
        public int Id
        {
            get; set;
        }

        public string EnterpriseCode
        {
            get; set;
        }
    }
}
