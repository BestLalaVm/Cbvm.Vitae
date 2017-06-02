using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service.Enterprise;
using Business.Service.Family;
using Business.Service.Student;
using Business.Service.Teacher;
using Presentation.Enum;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class ResetPasswordControl : System.Web.UI.UserControl
    {
        public UserType UserType
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch(UserType)
                {
                    case UserType.Admin:
                        ltlCaption.Text = string.Format("忘记密码-{0}", "管理员");
                        break;
                    case UserType.DepartAdmin:
                        ltlCaption.Text = string.Format("忘记密码-{0}", "系管理员");
                        break;
                    case UserType.Enterprise:
                        ltlCaption.Text = string.Format("忘记密码-{0}", "企业用户");
                        break;
                    case UserType.Student:
                        ltlCaption.Text = string.Format("忘记密码-{0}", "学生");
                        break;
                    case UserType.Teacher:
                        ltlCaption.Text = string.Format("忘记密码-{0}", "教师");
                        break;
                    case UserType.Family:
                        ltlCaption.Text = string.Format("忘记密码-{0}", "家长");
                        break;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IAuthenticateService server = null;
            switch (UserType)
            {
                //case UserType.DepartAdmin:
                //    server = new DepartAdminService();
                //    break;
                case UserType.Enterprise:
                    server = new EnterpriseService();
                    break;
                case UserType.Student:
                    server = new StudentService();
                    break;
                case UserType.Teacher:
                    server = new TeacherService();
                    break;
                case UserType.Family:
                    server = new FamilyService();
                    break;
            }
            var result = server.ResetPassword(txtUserName.Text, UserType, txtCheckCode.Text);
            (this.Page as BaseAccountPage).ShowMsg(result.IsSucess, result.Message);
            if (result.IsSucess)
            {
                txtCheckCode.Text = "";
                txtUserName.Text = "";
            }
        }
    }
}