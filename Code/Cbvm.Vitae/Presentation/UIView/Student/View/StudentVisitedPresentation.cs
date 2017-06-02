using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student.View
{
    [Serializable]
    public class StudentVisitedPresentation:BasePresentation
    {
        public string StudentNum { get; set; }

        public string NameZh { get; set; }

        public SexType Sex { get; set; }

        public string ThumbPath { get; set; }

        public DateTime? VisitTime { get; set; }

        public string MarjorCode { get; set; }

        public string DepartCode
        {
            get; set;
        }
    }
}
