using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class FellowshipDetail : BaseStudentDetailPage
    {
        protected override void InitData()
        {
            this.uc_assessment_.LoadData(CurrentID);

            base.InitData();
        }
    }
}