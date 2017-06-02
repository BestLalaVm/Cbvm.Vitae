using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Front
{
    public class EnterpriseViewPresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string TypeName { get; set; }

        public string IndustryName { get; set; }

        public string ScopeName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public IList<EnterpriseJobViewPresentation> Jobs { get; set; }
    }

    public class EnterpriseJobViewPresentation
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
