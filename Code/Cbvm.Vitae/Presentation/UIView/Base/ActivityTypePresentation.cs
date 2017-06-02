using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Base
{
    [Serializable]
    public class ActivityTypePresentation : BasePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ActivityDataType ActivityDataType { get; set; }
    }
}
