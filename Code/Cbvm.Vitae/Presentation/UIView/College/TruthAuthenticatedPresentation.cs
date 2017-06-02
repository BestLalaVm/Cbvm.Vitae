using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.College
{
    public class TruthAuthenticatedPresentation : BasePresentation
    {

        public int ID { get; set; }

        public string MarjorName { get; set; }

        public DateTime RequestDate { get; set; }

        public string EnterpriseName { get; set; }

        public string EnterpriseCode { get; set; }

        public string StudentNum { get; set; }

        public string StudentName { get; set; }

        public bool IsAuthenticated { get; set; }

        public string AuthenticatedComment { get; set; }
    }
}
