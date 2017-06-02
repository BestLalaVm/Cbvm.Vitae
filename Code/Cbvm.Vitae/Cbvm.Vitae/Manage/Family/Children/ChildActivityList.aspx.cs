using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildActivityList : BaseFamilyListPage<StudentActivityPresentation, StudentActivityCriteria>
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
            criteria.StudentNum = StudentNum;
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