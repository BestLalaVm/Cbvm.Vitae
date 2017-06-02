using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.Criteria
{
    public class SystemLogCriteria:BaseCriteria
    {
        public string LogType
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Message
        {
            get;
            set;
        }

        public DateTime? DateFrom
        {
            get;
            set;
        }

        public DateTime? DateTo
        {
            get;
            set;
        }
    }
}
