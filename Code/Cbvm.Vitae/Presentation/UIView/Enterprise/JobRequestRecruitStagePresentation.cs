using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class JobRequestRecruitStagePresentation : BasePresentation
    {
        public int JobRequestId
        {
            get; set;
        }

        public int RecruitFlowId
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public bool IsNewest
        {
            get; set;
        }
    }
}
