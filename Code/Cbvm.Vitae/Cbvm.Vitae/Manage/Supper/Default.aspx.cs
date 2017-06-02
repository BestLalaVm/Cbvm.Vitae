using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.Supper
{
    public partial class Default : BaseSupperDetailPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            (this.Master as Master).DefaultPage = "SysLogList.aspx";

            if (CurrentUser != null)
            {
                ltlUserName.Text = CurrentUser.Name;
            }
            ltlWelCome.Text = string.Format(Resources.XmcaResource.WelcomeBackendLabel, Resources.XmcaResource.Organization);

            Cbvm.Vitae.Manage.Master master = this.Master as Cbvm.Vitae.Manage.Master;
            master.DefaultPage = "SysLogList.aspx";
        }
    }
}