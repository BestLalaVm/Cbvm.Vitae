using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class SearchStudentPage : BaseCommonListPage<StudentCommonPresentation,StudentCriteria>
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
            List<string> studentNumList = new List<string>();

            for (var index = 0; index < RadGridControl.MasterTableView.Items.Count; index++)
            {
                var grdItem = RadGridControl.MasterTableView.Items[index];
                var chkStudent = grdItem.FindControl("chkStudent") as CheckBox;
                if (chkStudent.Checked)
                {
                    studentNumList.Add(grdItem.GetDataKeyValue("StudentNum").ToString());
                }
            }

            Session.AddRecommendStudentToSession(
                GlobalBaseDataCache.StudentPresentationList.Where(ic => studentNumList.Contains(ic.StudentNum)).ToList());
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
    }
}