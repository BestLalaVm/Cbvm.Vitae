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
using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class StudentProjectVerifyList : BaseTeacherListPage<StudentProjectPresentation, StudentProjectCriteria>
    {
        private IStudentProjectService Service
        {
            get
            {
                return new StudentProjectService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get
            {
                return pnlCondition;
            }
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
            base.InitBindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void chkIsPassed_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsPassword = sender as CheckBox;
            var gridItem = chkIsPassword.NamingContainer as GridItem;
            var projectID = (int)gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["ID"];
            var result = Service.ChangeVerifyStatus(projectID,
                (chkIsPassword.Checked ? VerifyStatus.Passed : VerifyStatus.UnPassed), "", "", (int?)null, (int?)null, (int?)null, CurrentUser.UserName);
            //ShowMsg(result.IsSucess, result.Message);
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdProject; }
        }

        protected override EntityCollection<StudentProjectPresentation> GetSearchResultList(StudentProjectCriteria criteria)
        {
            return Service.GetVerifyAll(criteria, TeacherNum);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentProjectPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Index,
                it.Name,
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(it.VerfyStatus),
                IsPassed = (it.VerfyStatus == VerifyStatus.Passed),
                it.Id,
                StudentName=it.StudentNameZh,
                it.Position,
                BeginTime = it.BeginTime.ToString("yyyy-MM-dd"),
                EndTime = it.EndTime.ToString("yyyy-MM-dd")
            }).ToList();
        }
    }
}