using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;
using Presentation.Criteria;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentScoreList : BaseStudentListPage<StudentCourseScorePresentation, StudentCourseScoreCriteria>
    {
        private IStudentCourseScoreService Service
        {
            get
            {
                return new StudentCourseScoreService();
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
            drp_CourseName_.BindSource(BindingSourceType.CourseInfo, true);
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdSource; }
        }

        protected override Presentation.UIView.EntityCollection<StudentCourseScorePresentation> GetSearchResultList(
            StudentCourseScoreCriteria criteria)
        {
            return Service.GetAll(criteria);
        }
    }
}