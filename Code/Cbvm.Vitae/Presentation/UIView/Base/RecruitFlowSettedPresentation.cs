using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Base
{
    [Serializable]
    public class RecruitFlowSettedPresentation : BasePresentation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public RecruitType RecruitType { get; set; }
    }
}
