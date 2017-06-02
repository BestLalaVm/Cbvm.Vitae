using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.UIView;

namespace Presentation.UIView.Teacher
{
    [Serializable]
    public class TeacherRelativeCoursePresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string CourseCode
        {
            get; set;
        }

        public string MarjorCode
        {
            get; set;
        }

        public bool IsAdviserTeacher
        {
            get; set;
        }

        public DateTime BeginTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public bool IsBroken
        {
            get; set;
        }

        public string TeacherNum
        {
            get; set;
        }

        public DateTime? BrokenTime
        {
            get; set;
        }

        public string MarjorName
        {
            get; set;
        }
    }
}
