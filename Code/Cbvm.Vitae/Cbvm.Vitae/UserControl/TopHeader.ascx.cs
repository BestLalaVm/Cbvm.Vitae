using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LkHelper;
using Presentation.Enum;
using WebLibrary.Helper;
namespace Cbvm.Vitae.UserControl
{
    public partial class TopHeader : BaseUserControl
    {
        protected override void InitData()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && CurrentUser.UserType != UserType.Guest)
            {
                //ltlLoginTime.Text = CurrentUser.LogTime.ToCustomerShortDateString();
                //ltlUserName.Text = String.Format("{0}({1})", CurrentUser.Name, CurrentUser.FullName);
                switch (CurrentUser.UserType)
                {
                    case UserType.DepartAdmin:
                        //ltlRoleName.Text = "系管理员";
                        link2Backend.NavigateUrl = "~/Manage/DepartAdmin/";
                        break;
                    case UserType.Enterprise:
                        //ltlRoleName.Text = "企业用户";
                        link2Backend.NavigateUrl = "~/Manage/Enterprise/";
                        break;
                    case UserType.Student:
                        //ltlRoleName.Text = "学生";
                        link2Backend.NavigateUrl = "~/Manage/Student/";
                        //if (!(Page is BaseFrontStudentPage))
                        //{
                        //    phHomePage.Visible = true;
                        //    link2Home.NavigateUrl = UrlRuleHelper.GenerateUrl(CurrentUser.UserName.ToString(), "",
                        //                                                      RulePathType.StudentInfo);
                        //}
                        break;
                    case UserType.Teacher:
                        //ltlRoleName.Text = "老师";
                        link2Backend.NavigateUrl = "~/Manage/Teacher/";
                        break;
                    case UserType.Family:
                        //ltlRoleName.Text = "家长";
                        link2Backend.NavigateUrl = "~/Manage/Family/";
                        break;
                    case UserType.College:
                        //ltlRoleName.Text = "学院";
                        link2Backend.NavigateUrl = "~/Manage/College/";
                        break;
                    case UserType.University:
                        //ltlRoleName.Text = "学校";
                        link2Backend.NavigateUrl = "~/Manage/University/";
                        break;
                }
                //phUserContainer.Visible = true;
                //phRegisterContainer.Visible = false;
            }

            base.InitData();
        }
    }
}