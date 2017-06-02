using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterpriseEmailTemplatePresentation:BasePresentation
    {
        public string Subject
        {
            get; set;
        }

        public string Body
        {
            get; set;
        }

        public EnterpriseEmailType EmailType
        {
            get; set;
        }

        public string Sender
        {
            get; set;
        }

        public string SenderName
        {
            get; set;
        }

        public string Cc
        {
            get; set;
        }

        public string EnterpriseCode
        {
            get; set;
        }

        public string HelpTip
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }
    }
}
