using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria
{
    public class MessageBoxCriteria:BaseCriteria
    {
        public string Subject
        {
            get; set;
        }

        public DateTime? DateFrom
        {
            get; set;
        }

        public DateTime? DateTo
        {
            get;
            set;
        }

        public string MsgType
        {
            get; set;
        }

        public string IsReaded
        {
            get;
            set;
        }
    }
}
