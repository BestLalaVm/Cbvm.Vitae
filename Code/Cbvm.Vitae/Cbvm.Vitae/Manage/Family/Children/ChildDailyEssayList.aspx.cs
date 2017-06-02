using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;


namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildDailyEssayList : BaseFamilyListPage<StudentDailyEssayPresentation, StudentDailyEssayCriteria>
    {
        private IStudentDailyEssayService Service
        {
            get
            {
                return new StudentDailyEssayService();
            }
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


        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdDailyEssay; }
        }

        protected override EntityCollection<StudentDailyEssayPresentation> GetSearchResultList(
            StudentDailyEssayCriteria criteria)
        {
            criteria.StudentNum = StudentNum;
            return Service.GetAll(criteria);
        }
    }
}