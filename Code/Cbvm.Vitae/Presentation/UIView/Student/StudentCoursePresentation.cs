using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentCoursePresentation:BasePresentation
    {
        public string Code
        {
            get;
            set;
        }

        public string CourseCode
        {
            get;
            set;
        }

        public string StudentNum
        {
            get;
            set;
        }

        public string TeacherNum
        {
            get;
            set;
        }

        public string CourseTypeName { get; set; }

        public string CourseName { get; set; }

        public decimal Score { get; set; }

        public DateTime ExamineTime { get; set; }

        
    }
}
