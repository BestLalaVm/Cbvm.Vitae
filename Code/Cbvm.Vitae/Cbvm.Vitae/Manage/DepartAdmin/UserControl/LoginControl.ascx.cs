using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Business.Interface;
using Presentation.UIView;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Manage.DepartAdmin.UserControl
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            IAuthenticateService server = new DepartAdminService();
            var loginUser = server.Login(txt_UserName_.Text, txt_Password_.Text);
            if (loginUser!=null)
            {
                AuthorizeHelper.SetCurrentUser(loginUser);
                Response.Redirect("~/Manage/DepartAdmin/");
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