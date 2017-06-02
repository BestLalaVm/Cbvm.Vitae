using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.UIView;

namespace Presentation.UIView.Teacher
{
    [Serializable]
    public class TeacherRelativeGroupPresentation : BasePresentation
    {
        public string Name { get; set; }

        public string TeacherNum
        {
            get; set;
        }

        public string GroupCode
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }
    }
}
