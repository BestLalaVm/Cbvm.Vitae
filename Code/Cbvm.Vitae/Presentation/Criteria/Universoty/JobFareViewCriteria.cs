using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Criteria.Universoty
{
    public class JobFareViewCriteria:BaseCriteria
    {
        public string Name { get; set; }

        public string UniversityCode { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string EnterpriseName { get; set; }
    }
}
