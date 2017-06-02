using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Family
{
    public class FamilyCriteria : BaseCriteria
    {
        public string Name { get; set; }

        public string StudentName { get; set; }

        public string StudentNum
        {
            get; set;
        }

        public string CollegeCode { get; set; }
    }
}
