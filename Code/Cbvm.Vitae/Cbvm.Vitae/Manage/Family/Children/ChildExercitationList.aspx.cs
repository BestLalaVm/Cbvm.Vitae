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


namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildExercitationList : BaseFamilyListPage<StudentExercitationPresentation, StudentExercitationCriteria>
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
            get { return this.pnlCondition; }
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdActivity; }
        }

        protected override EntityCollection<StudentExercitationPresentation> GetSearchResultList(StudentExercitationCriteria criteria)
        {
            criteria.StudentNum = StudentNum;
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentExercitationPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Id,
                it.Index,
                it.Title,
                ActivityType = GlobalBaseDataCache.GetActivityTypeLabel(it.ActivityType),
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(it.VerfyStatus),
                it.BeginTime,
                it.EndTime,
                it.IsOnline
            });
        }
    }
}