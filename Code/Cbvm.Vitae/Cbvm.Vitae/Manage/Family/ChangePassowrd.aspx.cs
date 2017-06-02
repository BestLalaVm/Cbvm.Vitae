using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Cbvm.Vitae.Manage.Family
{
    public partial class ChangePassowrd : BaseFamilyDetailPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            (this.Master as MasterDetail).EnableAJAX = false;
        }
    }
}