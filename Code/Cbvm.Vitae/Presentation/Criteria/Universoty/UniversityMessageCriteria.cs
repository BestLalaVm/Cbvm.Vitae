using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Universoty
{
    public class UniversityMessageCriteria:BaseCriteria
    {
        public string Title
        {
            get; set;
        }

        public string UniversityCode { get; set; }

        public string CategoryCode { get; set; }
    }
}
