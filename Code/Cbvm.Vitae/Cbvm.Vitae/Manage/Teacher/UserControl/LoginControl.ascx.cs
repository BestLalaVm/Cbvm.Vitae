using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Business.Interface;
using Business.Service.Teacher;
using Presentation.UIView;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher.UserControl
{
    public partial class LoginControl : BaseUserControl
    {
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            IAuthenticateService server = new TeacherService();
            var loginUser = server.Login(txt_TeacherNum_.Text, txt_Password_.Text);
            if (loginUser!=null)
            {
                AuthorizeHelper.SetCurrentUser(loginUser);
                Response.Redirect("~/Manage/Teacher/");
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