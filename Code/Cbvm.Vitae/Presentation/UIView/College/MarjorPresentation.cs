using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Base
{
     [Serializable]
    public class MarjorPresentation : BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UniversityCode { get; set; }
    }
}
