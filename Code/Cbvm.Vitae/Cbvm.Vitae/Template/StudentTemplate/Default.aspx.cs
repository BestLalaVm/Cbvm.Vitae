using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Template.StudentTemplate
{
    public partial class Default : BaseFrontStudentPage
    {
        public override string PageName
        {
            get
            {
                return "首页";
            }
        }

        protected override bool IsRecordVisisted()
        {
            return true;
        }
    }
}