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
using Presentation.Cache;
using Presentation.Criteria.Teacher;
using Presentation.UIView.Teacher;
using Presentation.UIView.Teacher.View;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class SearchTeacherPage : BaseCommonListPage<TeacherCommandPresentation, TeacherCriteria>
    {
        private ITeacherService Service
        {
            get
            {
                return new TeacherService();
            }
        }

        private List<TeacherCommandPresentation> CurrentSearchTeachers
        {
            get
            {
                var list = this.ViewState["CurrentSearchTeachers"] as List<TeacherCommandPresentation>;
                if (list == null)
                {
                    list = new List<TeacherCommandPresentation>();
                }

                return list;
            }
            set
            {
                this.ViewState["CurrentSearchTeachers"] = value;
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
                var selectedTeacherNum = item.GetDataKeyValue("TeacherNum");

                var teacher = CurrentSearchTeachers.Where(ic => ic.TeacherNum == selectedTeacherNum.ToString()).FirstOrDefault();
                Session.AddRecommendSingleTeacherToSession(teacher);

                SelectedAndClose(teacher.TeacherNum, teacher.NameZh);
            }
        }

        protected override void InitBindData()
        {
            base.InitBindData();

            prm_CollegeCode_.BindSource(BindingSourceType.College, true);
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

        protected override Presentation.UIView.EntityCollection<TeacherCommandPresentation> GetSearchResultList(TeacherCriteria criteria)
        {
            var query = Service.GetQuery();
           
            if (!String.IsNullOrEmpty(criteria.CollegeCode))
            {
                query = query.Where(it => it.CollegeCode == criteria.CollegeCode);
            }

            if (!String.IsNullOrEmpty(criteria.TeacherNum))
            {
                query = query.Where(it => it.TeacherNum.Contains(criteria.TeacherNum));
            }

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.NameZh.Contains(criteria.Name) || it.NameEn.Contains(criteria.Name));
            }

            var pageIndex = criteria.PageIndex;
            if (pageIndex > 0)
            {
                pageIndex = pageIndex - 1;
            }
            this.CurrentSearchTeachers = query.OrderByDescending(it => it.NameZh).Skip(pageIndex * criteria.PageSize).Take(criteria.PageSize).Select(it => new TeacherCommandPresentation
            {
                CollegeName = it.College.Name,
                Email = it.Email,
                NameEn = it.NameEn,
                NameZh = it.NameZh,
                Photo = it.Photo,
                TeacherNum = it.TeacherNum,
                Sex = it.Sex
            }).ToList();

            var entities = ((Business.Service.BaseService)this.Service).Translate2Presentations<TeacherCommandPresentation>(this.CurrentSearchTeachers);

            entities.TotalCount = query.Count();

            return entities;
        }

        public virtual void SelectedAndClose(string teacherNum, string teacherName)
        {
            var script = new StringBuilder();
            script.Append("var contentWindow = window.parent.$('#frame')[0].contentWindow;");
            script.AppendLine();
            script.Append("if(typeof contentWindow.setSelectedTeacher =='function'){");
            script.AppendLine();
            script.Append("     contentWindow.setSelectedTeacher('" + teacherNum + "','" + teacherName + "');");
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