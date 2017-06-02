using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentActivityList : BaseStudentListPage<StudentActivityPresentation,StudentActivityCriteria>
    {
        private IStudentActivityService Service
        {
            get
            {
                return new StudentActivityService();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentActivityDetail.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            grdActivity.Rebind();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdActivity; }
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(CurrentUser.UserName, id);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsOnline = sender as CheckBox;
            var dataItem = chkIsOnline.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            Service.SetStatus(StudentNum, id, chkIsOnline.Checked);
            RadGridControl.Rebind();
        }

        protected override EntityCollection<StudentActivityPresentation> GetSearchResultList(StudentActivityCriteria criteria)
        {
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentActivityPresentation> list)
        {
            radGrid.DataSource = list.Select(ix => new
            {
                ix.Index,
                ix.Address,
                ix.BeginTime,
                ix.Content,
                ix.EndTime,
                ix.EvaluateFromTeacher,
                ix.Id,
                ix.IsOnline,
                ix.ReferenceID,
                ix.StudentNameEn,
                ix.StudentNameZh,
                ix.StudentNum,
                ix.TeacherNum,
                ix.Title,
                StudentName = String.Format("{0} {1}", ix.StudentNameZh, ix.StudentNameEn),
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(ix.VerfyStatus),
                ix.VerifyStatusReason,
                ActivityType = GlobalBaseDataCache.GetActivityTypeLabel(ix.ActivityType)
            }).ToList();
        }
    }
}