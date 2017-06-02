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
using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class StudentActivityVerifyList : BaseTeacherListPage<StudentActivityPresentation, StudentActivityCriteria>
    {
        private IStudentActivityService Service
        {
            get
            {
                return new StudentActivityService();
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

        protected void chkIsPassed_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsPassed = sender as CheckBox;
            var gridItem = chkIsPassed.NamingContainer as GridItem;
            var activityID = (int)gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["ID"];
            var result = Service.ChangeVerifyStatus(activityID,
                (chkIsPassed.Checked ? VerifyStatus.Passed : VerifyStatus.UnPassed), "", "", TeacherNum);
            RadGridControl.Rebind();
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
        }

        protected override RadGrid RadGridControl
        {
            get { return grdActivity; }
        }

        protected override EntityCollection<StudentActivityPresentation> GetSearchResultList(StudentActivityCriteria criteria)
        {
            return Service.GetVerifyAll(criteria,TeacherNum);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentActivityPresentation> list)
        {
            radGrid.DataSource = list.Select(ix => new
            {
                ix.Index,
                ix.Address,
                BeginTime = ix.BeginTime.ToCustomerDateString(),
                ix.Content,
                EndTime = ix.EndTime.ToCustomerDateString(),
                ix.EvaluateFromTeacher,
                ix.Id,
                ix.IsOnline,
                ix.ReferenceID,
                ix.StudentNameEn,
                ix.StudentNameZh,
                ix.StudentNum,
                ix.TeacherNum,
                ix.Title,
                IsPassed = (ix.VerfyStatus == VerifyStatus.Passed),
                StudentName = ix.StudentNameZh,
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(ix.VerfyStatus),
                ix.VerifyStatusReason,
                ActivityType = GlobalBaseDataCache.GetActivityTypeLabel(ix.ActivityType)
            }).ToList();
        }
    }
}