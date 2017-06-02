using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterpriseJobReferralerPresentation : BasePresentation
    {
        public string UserName
        {
            get; set;
        }

        public UserType UserType
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public ReferralState ReferralState
        {
            get; set;
        }

        public string NameZh
        {
            get; set;
        }
    }
}
