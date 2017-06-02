using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.College
{
    public partial class ChangePassword : BaseCollegeDetailPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            (this.Master as MasterDetail).EnableAJAX = false;
        }
    }
}