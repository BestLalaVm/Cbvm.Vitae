using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.Criteria
{
    public class UserCommandCriteria : BaseCriteria
    {
        public string UserType { get; set; }

        public string Keyword { get; set; }
    }
}
