using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.College
{
    public partial class Default : BaseCollegeDetailPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (CurrentUser != null)
            {
                ltlUserName.Text = String.Format("{0}({1})", CurrentUser.Name, CurrentUser.FullName);
            }
            ltlWelCome.Text = string.Format(Resources.XmcaResource.WelcomeBackendLabel, Resources.XmcaResource.Organization);

            Cbvm.Vitae.Manage.Master master = this.Master as Cbvm.Vitae.Manage.Master;
            master.DefaultPage = "DepartManage.aspx";
        }
    }
}