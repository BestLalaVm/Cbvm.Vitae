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
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildProjectList : BaseFamilyListPage<StudentProjectPresentation, StudentProjectCriteria>
    {
        private IStudentProjectService Service
        {
            get
            {
                return new StudentProjectService();
            }
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


        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }


        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
        }

        protected override RadGrid RadGridControl
        {
            get { return grdProject; }
        }

        protected override Presentation.UIView.EntityCollection<StudentProjectPresentation> GetSearchResultList(
            StudentProjectCriteria criteria)
        {
            criteria.StudentNum = StudentNum;
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentProjectPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Id,
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
            }).ToList();
        }
    }
}