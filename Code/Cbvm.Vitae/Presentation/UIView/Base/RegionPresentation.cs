using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Base
{
    [Serializable]
    public class RegionPresentation : BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string ParentCode { get; set; }

        public int DisplayOrder { get; set; }
    }
}
