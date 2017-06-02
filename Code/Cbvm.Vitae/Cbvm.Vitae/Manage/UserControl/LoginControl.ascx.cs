using System;
using Business.Interface;
using Business.Service.Enterprise;
using Business.Service.Family;
using Business.Service.Student;
using Business.Service.Teacher;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        public delegate  LoginUserPresentation GetExtandAccountInfoDelegate(LoginUserPresentation account);

        public event GetExtandAccountInfoDelegate GetExtandAccountInfoHandler;
        public string UserNameEmptyMessage
        {
            get
            {
                if (this.ViewState["UserNameEmptyMessage"] == null) return "用户名";
                return (string)this.ViewState["UserNameEmptyMessage"];
            }
            set
            {
                this.ViewState["UserNameEmptyMessage"] = value;
            }
        }

        public UserType UserType
        {
            get
            {
                if (this.ViewState["UserType"] == null) return UserType.Student;
                return (UserType)this.ViewState["UserType"];
            }
            set
            {
                this.ViewState["UserType"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txt_UserName_.Attributes.Add("EmptyMessage", UserNameEmptyMessage);
            switch (UserType)
            {
                case Presentation.Enum.UserType.Admin:
                    linkResetPassword.NavigateUrl = "~/Manage/Student/ResetPassword.aspx";
                    break;
                case Presentation.Enum.UserType.DepartAdmin:
                    linkResetPassword.NavigateUrl = "~/Manage/DepartAdmin/ResetPassword.aspx";
                    break;
                case Presentation.Enum.UserType.Enterprise:
                    linkResetPassword.NavigateUrl = "~/Manage/Enterprise/ResetPassword.aspx";
                    break;
                case Presentation.Enum.UserType.Student:
                    linkResetPassword.NavigateUrl = "~/Manage/Student/ResetPassword.aspx";
                    break;
                case Presentation.Enum.UserType.Teacher:
                    linkResetPassword.NavigateUrl = "~/Manage/Teacher/ResetPassword.aspx";
                    break;
                case UserType.Family:
                    linkResetPassword.NavigateUrl = "~/Manage/Family/ResetPassword.aspx";
                    break;
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            IAuthenticateService service = null;
            string redirectDefaultPage = null;
            switch (UserType)
            {
                case UserType.Admin:
                    service = new StudentService();
                    redirectDefaultPage = "~/Manage/Student/";
                    break;
                //case UserType.DepartAdmin:
                //    service = new DepartAdminService();
                //    redirectDefaultPage = "~/Manage/DepartAdmin/";
                //    break;
                case UserType.Enterprise:
                    service = new EnterpriseService();
                    redirectDefaultPage = "~/Manage/Enterprise/";
                    break;
                case UserType.Student:
                    service = new StudentService();
                    redirectDefaultPage = "~/Manage/Student/";
                    break;
                case UserType.Teacher:
                    service = new TeacherService();
                    redirectDefaultPage = "~/Manage/Teacher/";
                    break;
                case UserType.Family:
                    service = new FamilyService();
                    redirectDefaultPage = "~/Manage/Family/";
                    break;
            }
            var loginUser = service.Login(txt_UserName_.Text, txt_Password_.Text);
            if (loginUser!=null)
            {
                if (GetExtandAccountInfoHandler != null)
                {
                    loginUser = GetExtandAccountInfoHandler(loginUser);
                }
                AuthorizeHelper.SetCurrentUser(loginUser);
                Response.Redirect(redirectDefaultPage);
            }
        }
    }
}