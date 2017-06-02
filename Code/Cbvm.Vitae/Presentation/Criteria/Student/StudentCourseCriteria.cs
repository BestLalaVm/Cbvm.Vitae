using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Presentation.Criteria.Student
{
    public class StudentCourseCriteria:BaseStudentCriteria
    {
        public string CourseName
        {
            get; set;
        }

        public string TeacherName
        {
            get;
            set;
        }

        public string CourseTypeCode
        {
            get; set;
        }

        public string MarjorCode
        {
            get; set;
        }
    }
}
