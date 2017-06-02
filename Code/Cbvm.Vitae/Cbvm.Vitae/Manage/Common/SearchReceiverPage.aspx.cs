using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Business.Interface.Teacher;
using Business.Service.Teacher;
using Presentation;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.Cache;
using Presentation.Criteria.Teacher;
using Presentation.UIView.Teacher;
using Presentation.UIView.Teacher.View;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Presentation.Criteria;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class SearchReceiverPage : BaseCommonListPage<UserCommandPresentation, UserCommandCriteria>
    {
        private ITeacherService Service
        {
            get
            {
                return new TeacherService();
            }
        }

        private List<UserCommandPresentation> CurrentSearchUsers
        {
            get
            {
                var list = this.ViewState["CurrentSearchUsers"] as List<UserCommandPresentation>;
                if (list == null)
                {
                    list = new List<UserCommandPresentation>();
                }

                return list;
            }
            set
            {
                this.ViewState["CurrentSearchUsers"] = value;
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {
            var btnSelected = sender as Button;
            if (btnSelected != null)
            {
                var item = btnSelected.NamingContainer as GridDataItem;
                var selectedUserName = item.GetDataKeyValue("UserName");

                var user = CurrentSearchUsers.Where(ic => ic.UserName == selectedUserName.ToString()).FirstOrDefault();

                SelectedAndClose(user.UserName, user.Name, user.UserType.ToString());
            }
        }

        protected override void InitBindData()
        {
            base.InitBindData();

            prm_UserType_.Items.Add(new ListItem
            {
                Text = "学校",
                Value = UserType.University.ToString()
            });

            prm_UserType_.Items.Add(new ListItem
            {
                Text = "学院",
                Value = UserType.College.ToString()
            });

            prm_UserType_.Items.Add(new ListItem
            {
                Text = "企业",
                Value = UserType.Enterprise.ToString()
            });

            prm_UserType_.Items.Add(new ListItem
            {
                Text = "家长",
                Value = UserType.Family.ToString()
            });

            prm_UserType_.Items.Add(new ListItem
            {
                Text = "学生",
                Value = UserType.Student.ToString()
            });

            prm_UserType_.Items.Add(new ListItem
            {
                Text = "教师",
                Value = UserType.Teacher.ToString()
            });
        }

        protected override void InitData()
        {
            base.InitData();

            RadGridControl.ClientSettings.Scrolling.ScrollHeight = 332;
        }

        protected override RadGrid RadGridControl
        {
            get { return grdTeacher; }
        }

        protected override Presentation.UIView.EntityCollection<UserCommandPresentation> GetSearchResultList(UserCommandCriteria criteria)
        {
            //UserType userType = (UserType)Enum.Parse(typeof(UserType), this.prm_UserType_.Text);
            UserType userType = (UserType)Enum.Parse(typeof(UserType), criteria.UserType);

            IQueryable<UserCommandPresentation> query = null;

            switch (userType)
            {
                case UserType.College:
                    query = DataContext.College.Select(it => new UserCommandPresentation
                    {
                        Name = it.Name,
                        UserName = it.CollegeAdmin.Select(ii => ii.UserName).FirstOrDefault(),
                        UserType = userType
                    }).AsQueryable();
                    break;
                case Presentation.Enum.UserType.Enterprise:
                    query = DataContext.Enterprise.Select(it => new UserCommandPresentation
                    {
                        Name = it.Name,
                        UserName = it.UserName,
                        UserType = userType
                    }).AsQueryable();
                    break;
                case Presentation.Enum.UserType.Family:
                    query = DataContext.StudentFamilyAccount.Select(it => new UserCommandPresentation
                    {
                        Name = it.NameZh,
                        UserName = it.UserName,
                        UserType = userType
                    }).AsQueryable();
                    break;
                case Presentation.Enum.UserType.Student:
                    query = DataContext.Student.Select(it => new UserCommandPresentation
                    {
                        Name = it.NameZh,
                        UserName = it.StudentNum,
                        UserType = userType
                    }).AsQueryable();
                    break;
                case Presentation.Enum.UserType.Teacher:
                    query = DataContext.Teacher.Select(it => new UserCommandPresentation
                    {
                        Name = it.NameZh,
                        UserName = it.TeacherNum,
                        UserType = userType
                    }).AsQueryable();
                    break;
                case Presentation.Enum.UserType.University:
                    query = DataContext.University.Select(it => new UserCommandPresentation
                    {
                        Name = it.Name,
                        UserName = it.UniversityAdmin.Select(ii => ii.UserName).FirstOrDefault(),
                        UserType = userType
                    }).AsQueryable();
                    break;
            }

            if (!string.IsNullOrEmpty(criteria.Keyword))
            {
                query = query.Where(it => it.UserName.Contains(criteria.Keyword) || it.Name.Contains(criteria.Keyword));
            }

            var count = query.Count();
            var list = query.OrderBy(it => it.Name).Skip(criteria.PageIndex * criteria.PageSize).Take(criteria.PageSize).ToList();

            var entityList = ((Business.Service.BaseService)this.Service).Translate2Presentations<UserCommandPresentation>(list);

            entityList.TotalCount = count;

            CurrentSearchUsers = list;

            return entityList;
        }

        public virtual void SelectedAndClose(string userName, string name,string userType)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = userName;
            }

            var script = new StringBuilder();
            script.Append("var contentWindow = window.parent.$('#frame')[0].contentWindow;");
            script.AppendLine();
            script.Append("if(typeof contentWindow.setSelectedUser =='function'){");
            script.AppendLine();
            script.Append("     contentWindow.setSelectedUser('" + userName + "','" + name + "','" + userType + "');");
            script.AppendLine();
            script.Append("}");
            script.AppendLine();
            script.Append("window.parent.removeFrameDialog();");

            if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "PopMsg",
                                                            "$(function(){" + script.ToString() + "});", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AsyncPopMsg", script.ToString(),
                                                        true);
            }
        }
    }
}