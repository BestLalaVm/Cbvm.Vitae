using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Cbvm.Vitae.Manage.DepartAdmin
{
    public partial class Default : BaseDepartAdminDetailPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            (this.Master as Master).DefaultPage = "EnterpriseManage.aspx";

            if (CurrentUser != null)
            {
                ltlUserName.Text = CurrentUser.Name;
            }
            ltlWelCome.Text = string.Format(Resources.XmcaResource.WelcomeBackendLabel, Resources.XmcaResource.Organization);

            Cbvm.Vitae.Manage.Master master = this.Master as Cbvm.Vitae.Manage.Master;
            master.DefaultPage = "EnterpriseManage.aspx";
        }
    }
}