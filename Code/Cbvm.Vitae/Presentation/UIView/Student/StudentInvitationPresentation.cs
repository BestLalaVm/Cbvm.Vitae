using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Student
{
    public class StudentInvitationPresentation:BasePresentation
    {
        public string EnterpriseCode { get; set; }

        public string EnterpriseName { get; set; }

        public DateTime InvitedDate { get; set; }

        public bool IsSendJobRequest { get; set; }

        public bool IsViewed { get; set; }

        public int ID { get; set; }

        public IList<string> JobNameList { get; set; }

        public string JobNames
        {
            get
            {
                if (JobNameList == null) return "";

                return String.Join(",",JobNameList);
            }
        }
    }
}
