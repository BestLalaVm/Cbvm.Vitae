using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentProjectList : BaseStudentListPage<StudentProjectPresentation,StudentProjectCriteria>
    {
        private IStudentProjectService Service
        {
            get { return new StudentProjectService(); }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentProjectDetail.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsOnline = sender as CheckBox;
            var dataItem = chkIsOnline.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            Service.SetStatus(StudentNum, id, chkIsOnline.Checked);
            RadGridControl.Rebind();
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(CurrentUser.UserName, id);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdProject; }
        }

        protected override EntityCollection<StudentProjectPresentation> GetSearchResultList(StudentProjectCriteria criteria)
        {
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentProjectPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
                {
                    ID=it.Id,
                    it.Index,
                    it.Name,
                    BeginTime = it.BeginTime.ToCustomerShortDateString(),
                    EndTime = it.EndTime.ToCustomerShortDateString(),
                    it.IsOnline,
                    it.Position,
                    it.Description,
                    it.PositionDescrition,
                    it.Evaluate,
                    it.TeamDescription,
                    it.StudentNum,
                    it.TeacherNum,
                    it.EvaluateFromTeacher,
                    it.VerifyStatusReason,
                    it.UsableLevel,
                    it.SkillLevel,
                    it.DreativeLevel,
                    VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(it.VerfyStatus),
                });
        }
    }
}