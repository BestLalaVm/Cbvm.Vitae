using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Base
{
     [Serializable]
    public class DepartPresentation : BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CollegeCode { get; set; }
    }
}
