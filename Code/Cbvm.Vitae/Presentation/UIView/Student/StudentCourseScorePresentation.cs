using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentCourseScorePresentation:BasePresentation
    {
        public string StudentCourseCode
        {
            get; set;
        }

        public string CourseCode
        {
            get; set;
        }

        public decimal Score
        {
            get; set;
        }

        public DateTime ExamineTime
        {
            get; set;
        }

        public CourseScoreType CourseScoreType
        {
            get; set;
        }

        public string CourseName
        {
            get; set;
        }
    }
}
