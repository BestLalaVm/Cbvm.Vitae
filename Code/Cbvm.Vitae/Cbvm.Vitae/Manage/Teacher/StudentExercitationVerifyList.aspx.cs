using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class StudentExercitationVerifyList : BaseTeacherListPage<StudentExercitationPresentation, StudentExercitationCriteria>
    {
        private IStudentExercitationService Service
        {
            get
            {
                return new StudentExercitationService();
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
            var activityID = (int) gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["ID"];
            var result = Service.ChangeVerifyStatus(activityID,
                (chkIsPassed.Checked ? VerifyStatus.Passed : VerifyStatus.UnPassed),
                "", "", CurrentUser.UserName);

            //ShowMsg(result.IsSucess, result.Message);
            RadGridControl.Rebind();
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo,true);
        }

        protected override RadGrid RadGridControl
        {
            get { return grdActivity; }
        }

        protected override EntityCollection<StudentExercitationPresentation> GetSearchResultList(StudentExercitationCriteria criteria)
        {
            return Service.GetVerifyAll(criteria,TeacherNum);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentExercitationPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Index,
                it.Title,
                ActivityType = GlobalBaseDataCache.GetActivityTypeLabel(it.ActivityType),
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(it.VerfyStatus),
                BeginTime = it.BeginTime.ToString("yyyy-MM-dd"),
                EndTime = it.EndTime.ToString("yyyy-MM-dd"),
                IsPassed = (it.VerfyStatus == VerifyStatus.Passed),
                ID = it.Id,
                StudentName = it.StudentNameZh
            }).ToList();
        }
    }
}