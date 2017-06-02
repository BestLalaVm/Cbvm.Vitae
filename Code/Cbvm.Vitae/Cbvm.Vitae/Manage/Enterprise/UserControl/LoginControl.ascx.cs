using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Business.Interface;
using Business.Service.Enterprise;
using Presentation.UIView;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Enterprise.UserControl
{
    public partial class LoginControl :BaseUserControl
    {
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            IAuthenticateService server = new EnterpriseService();
            var loginUser = server.Login(txt_UserName_.Text, txt_Password_.Text);
            if (loginUser!=null)
            {
                AuthorizeHelper.SetCurrentUser(loginUser);
                Response.Redirect("~/Manage/Enterprise/");
            }
        }

        public bool ShowChangePasswordControl
        {
            set
            {
                this.linkResetPassword.Visible = value;
            }
        }
    }
}