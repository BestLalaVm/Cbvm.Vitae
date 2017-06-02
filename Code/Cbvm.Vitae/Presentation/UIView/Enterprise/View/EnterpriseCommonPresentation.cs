using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Enterprise.View
{
    [Serializable]
    public class EnterpriseCommonPresentation:BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
