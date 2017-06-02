using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria
{
    public class PublishActivityCriteria:BaseCriteria
    {
        public int Id
        {
            get; set;
        }

        public bool IncludeRelativeData
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public DateTime? DateFrom
        {
            get; set;
        }

        public DateTime? DateTo
        {
            get; set;
        }

        public string StudentNum
        {
            get; set;
        }
    }
}
