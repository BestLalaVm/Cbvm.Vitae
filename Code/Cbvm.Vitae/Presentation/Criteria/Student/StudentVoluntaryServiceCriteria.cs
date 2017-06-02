using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.Criteria.Student
{
    public class StudentVoluntaryServiceCriteria : BaseStudentCriteria
    {
        public string Title { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string VerfyStatus { get; set; }

        public string MarjorCode { get; set; }

        public string CollegeCode { get; set; }

        public string StudentName { get; set; }
    }
}
