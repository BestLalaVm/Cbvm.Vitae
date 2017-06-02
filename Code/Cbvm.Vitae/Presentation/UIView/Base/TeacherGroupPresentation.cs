using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Base
{
    [Serializable]
    public class TeacherGroupPresentation:BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime LastUpdateTime
        {
            get; set;
        }

        public TeacherGroupType TeacherGroupType { get; set; }

        public string UniversityCode { get; set; }
    }
}
