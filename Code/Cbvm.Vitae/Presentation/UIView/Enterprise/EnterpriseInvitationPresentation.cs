using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Enterprise
{
    public class EnterpriseInvitationPresentation : BasePresentation
    {
        public string CollegeName { get; set; }

        public string MarjorName { get; set; }

        public string StudentNum { get; set; }

        public string StudentName { get; set; }

        public DateTime InvitedDate { get; set; }

        public bool HasRequested { get; set; }

        public bool IsViewed { get; set; }

        public int ID { get; set; }
    }
}
