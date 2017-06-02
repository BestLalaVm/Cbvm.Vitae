using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Criteria.Universoty
{
    public class UniversityMessageCategoryCriteria : BaseCriteria
    {
        public string Name
        {
            get;
            set;
        }

        public string UniversityCode { get; set; }
    }
}
