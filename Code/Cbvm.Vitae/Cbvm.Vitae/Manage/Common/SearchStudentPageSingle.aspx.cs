using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;
using Presentation.UIView.Student.View;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class SearchStudentPageSingle : BaseCommonListPage<StudentCommonPresentation, StudentCriteria>
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
            }
        }

        private string StudentNum
        {
            get
            {
                return Request.QueryString["StudentNum"];
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
                var studentNum = item.GetDataKeyValue("StudentNum");

                var student = GlobalBaseDataCache.StudentPresentationList.Where(ic => ic.StudentNum == studentNum.ToString()).FirstOrDefault();
                Session.AddRecommendSingleStudentToSession(student);

                SelectedAndClose(student.StudentNum, student.Name);
            }
        }

        protected override void InitBindData()
        {
            base.InitBindData();

            prm_MarjorCode_.BindSource(BindingSourceType.MarjorInfo, true);
        }

        protected override void InitData()
        {
            base.InitData();

            RadGridControl.ClientSettings.Scrolling.ScrollHeight = 332;
        }

        protected override RadGrid RadGridControl
        {
            get { return grdStudent; }
        }

        protected override Presentation.UIView.EntityCollection<StudentCommonPresentation> GetSearchResultList(StudentCriteria criteria)
        {
            return Service.GetCommonAll(criteria);
        }

        public virtual void SelectedAndClose(string studentNum,string studentName)
        {
            var script = new StringBuilder();
            script.Append("var contentWindow = window.parent.$('#frame')[0].contentWindow;");
            script.AppendLine();
            script.Append("if(typeof contentWindow.setSelectedStudent =='function'){");
            script.AppendLine();
            script.Append("     contentWindow.setSelectedStudent('" + studentNum + "','" + studentName + "');");
            script.AppendLine();
            script.Append("}");
            script.AppendLine();
            script.Append("window.parent.removeFrameDialog();");
            //script.Append(@" alert(window.parent.$('#frame').length);"+
            //               "debugger; "+
            //               "alert(window.parent.$('#frame')[0].contentWindow.setSelectedStudent);" +
            //                "if(typeof window.parent.setSelectedStudent =='function'){" +
            //                   " debugger;"+
            //                   " window.parent.setSelectedStudent('" + studentNum + "','" + studentName + "'); " +
            //              " } ");
            //script.AppendFormat("window.parent.removeFrameDialog();");
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