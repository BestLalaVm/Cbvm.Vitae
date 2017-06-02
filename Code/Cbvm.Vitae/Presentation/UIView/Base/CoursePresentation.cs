using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Base
{
    [Serializable]
    public class CoursePresentation:BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TypeCode { get; set; }

        public string MarjorCode
        {
            get; set;
        }
    }
}
