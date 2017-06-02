using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Criteria.Student
{
    public class StudentScoreNewCriteria : BaseStudentCriteria
    {
        public string CourseName
        {
            get;
            set;
        }

        public string CourseCode
        {
            get;
            set;
        }
    }
}
