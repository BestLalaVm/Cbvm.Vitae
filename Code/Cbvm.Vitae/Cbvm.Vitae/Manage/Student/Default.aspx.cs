using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class Default : BaseStudentPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ltlUserName.Text = String.Format("{0}({1})", CurrentUser.Name, CurrentUser.FullName);
            ltlWelCome.Text = string.Format(Resources.XmcaResource.WelcomeBackendLabel, Resources.XmcaResource.Organization);

            Master master = this.Master as Master;
            //master.DefaultPage = "UserInfo.aspx";
            master.DefaultPage = "/Manage/Common/UniversityMessageTopList.aspx";
        }
    }
}