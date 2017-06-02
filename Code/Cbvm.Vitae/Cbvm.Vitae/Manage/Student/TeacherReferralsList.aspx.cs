using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.UIView.Enterprise.View;
using Telerik.Web.UI;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class TeacherReferralsList : BaseStudentListPage<TeacherReferralJobForStudentPresentation, EnterpriseJobRequestQueueCriteria>
    {
        private IEnterpriseJobRequestQueueService Service
        {
            get
            {
                return new EnterpriseJobRequestQueueService();
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

        protected override RadGrid RadGridControl
        {
            get { return grdEnterpriseJob; }
        }

        protected override Presentation.UIView.EntityCollection<TeacherReferralJobForStudentPresentation>
            GetSearchResultList(EnterpriseJobRequestQueueCriteria criteria)
        {
            return Service.GetAllTeacherReferralForStudents(criteria, StudentNum);
        }
    }

}