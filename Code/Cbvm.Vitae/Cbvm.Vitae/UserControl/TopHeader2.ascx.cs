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
    public partial class TopHeader2 : BaseUserControl
    {
        protected override void InitData()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && CurrentUser.UserType != UserType.Guest)
            {
                switch (CurrentUser.UserType)
                {
                    case UserType.DepartAdmin:
                        link2Backend.NavigateUrl = "~/Manage/DepartAdmin/";
                        break;
                    case UserType.Enterprise:
                        link2Backend.NavigateUrl = "~/Manage/Enterprise/";
                        break;
                    case UserType.Student:
                        link2Backend.NavigateUrl = "~/Manage/Student/";
                        break;
                    case UserType.Teacher:
                        link2Backend.NavigateUrl = "~/Manage/Teacher/";
                        break;
                    case UserType.Family:
                        link2Backend.NavigateUrl = "~/Manage/Family/";
                        break;
                    case UserType.College:
                        link2Backend.NavigateUrl = "~/Manage/College/";
                        break;
                    case UserType.University:
                        link2Backend.NavigateUrl = "~/Manage/University/";
                        break;
                }
            }

            base.InitData();
        }
    }
}