using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;
using Presentation.UIView.Base;

namespace Presentation.UIView.Teacher
{
    [Serializable]
    public class TeacherPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string TeacherNum
        {
            get; set;
        }

        public string NameZh
        {
            get;
            set;
        }
        
        public string NameEn
        {
            get;
            set;
        }
        
        public string NativePlace
        {
            get;
            set;
        }
        
        public string School
        {
            get;
            set;
        }
        
        public string MarjorName
        {
            get;
            set;
        }
        
        public string Telephone
        {
            get;
            set;
        }
        
        public SexType Sex
        {
            get;
            set;
        }
        
        public bool IsMarried
        {
            get;
            set;
        }
        
        public string Email
        {
            get;
            set;
        }
        
        public string ContactPhone
        {
            get;
            set;
        }
        
        public string WorkingYear
        {
            get;
            set;
        }
        
        public bool IsOnline
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string EducationCode
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Photo
        {
            get;
            set;
        }

        public string ThumbPath
        {
            get; set;
        }

        public string DepartName
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string CollegeCode { get; set; }

        private List<TeacherGroupPresentation> _RelativeGroups;
        public List<TeacherGroupPresentation> RelativeGroups
        {
            get
            {
                if (_RelativeGroups == null)
                {
                    _RelativeGroups = new List<TeacherGroupPresentation>();
                }
                return _RelativeGroups;
            }
            set
            {
                _RelativeGroups = value;
            }
        }

        private List<TeacherRelativeCoursePresentation> _RelativeCourses;
        public List<TeacherRelativeCoursePresentation> RelativeCourses
        {
            get
            {
                if (_RelativeCourses == null)
                {
                    _RelativeCourses = new List<TeacherRelativeCoursePresentation>();
                }
                return _RelativeCourses;
            }
            set
            {
                _RelativeCourses = value;
            }
        }
    }
}
