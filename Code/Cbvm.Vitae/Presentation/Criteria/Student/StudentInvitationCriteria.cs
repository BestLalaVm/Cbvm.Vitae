using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Criteria.Student
{
    public class StudentInvitationCriteria:BaseStudentCriteria
    {
        public string EnterpriseName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
