using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Student.View
{
    public class StudentCommonPresentation:BasePresentation
    {
        public string StudentNum
        {
            get; set;
        }

        public string NameZh
        {
            get; set;
        }

        public string SexLabel
        {
            get; set;
        }

        public string Period
        {
            get; set;
        }

        public string MarjorName
        {
            get; set;
        }

        public string Class
        {
            get; set;
        }

        public bool Selected
        {
            get; set;
        }

        public DateTime CreayeTime
        {
            get; set;
        }
    }
}
