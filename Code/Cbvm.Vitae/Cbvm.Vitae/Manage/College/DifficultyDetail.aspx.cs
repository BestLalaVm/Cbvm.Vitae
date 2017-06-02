using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.College
{
    public partial class DifficultyDetail : BaseCollegeDetailPage
    {
        protected override void InitData()
        {
            this.uc_assessment_.LoadData(CurrentId);

            base.InitData();
        }
    }
}