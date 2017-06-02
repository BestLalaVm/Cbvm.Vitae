using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;
using Presentation.Criteria.Base;

namespace Presentation.Criteria.College
{
    public class AssessmentCriteria : CollegeCriteriaBase
    {
        public string Title { get; set; }

        public AssessType? AssessType { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string StudentNum { get; set; }
    }
}
