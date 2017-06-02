using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;
using Presentation.Criteria.Base;

namespace Presentation.Criteria.College
{
    public class TruthAuthenticatedCriteria : CollegeCriteriaBase
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string VerfyStatus { get; set; }

        public string MarjorCode { get; set; }

        public string StudentName { get; set; }

        public string StudentNum { get; set; }

        public string EnterpriseName { get; set; }
    }
}
