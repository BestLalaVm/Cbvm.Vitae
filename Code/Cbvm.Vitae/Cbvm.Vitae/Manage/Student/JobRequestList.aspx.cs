using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class JobRequestList : BaseStudentListPage<EnterpriseJobRequestForStudentPresentation, EnterpriseJobRequestCriteria>
    {
        private IEnterpriseJobRequestService Service
        {
            get
            {
                return new EnterpriseJobRequestService();
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

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdJobRequest; }
        }

        protected override EntityCollection<EnterpriseJobRequestForStudentPresentation> GetSearchResultList(
            EnterpriseJobRequestCriteria criteria)
        {
            return Service.GetAllForStudent(criteria, StudentNum);
        }
    }
}