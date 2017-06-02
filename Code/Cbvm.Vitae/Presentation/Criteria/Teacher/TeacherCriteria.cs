using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Teacher
{
    public class TeacherCriteria:BaseTeacherCriteria
    {
        public string Name
        {
            get; set;
        }

        public string TeacherGroupCode
        {
            get; set;
        }

        public bool IncludeCourse
        {
            get; set;
        }

        public bool IncludeGroup
        {
            get;
            set;
        }

        public string CollegeCode { get; set; }
    }
}
