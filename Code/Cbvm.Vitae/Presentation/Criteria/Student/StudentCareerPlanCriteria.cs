using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;
using Presentation.Criteria.Base;

namespace Presentation.Criteria.Student
{
    public class StudentCareerPlanCriteria : BaseStudentCriteria
    {
        public string Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
