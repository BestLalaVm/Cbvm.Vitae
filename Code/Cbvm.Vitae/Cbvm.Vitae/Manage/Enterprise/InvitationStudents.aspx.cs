using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using Telerik.Web.UI;
using Presentation.UIView.Student;
using Presentation.Criteria.Student;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class InvitationStudents : BaseEnterpriseListPage<EnterpriseInvitationPresentation, EnterpriseInvitationCriteria>
    {
        private IEnterpriseInvitationService Service
        {
            get
            {
                return new EnterpriseInvitationService();
            }

        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdEnterpriseJob; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override EntityCollection<EnterpriseInvitationPresentation> GetSearchResultList(EnterpriseInvitationCriteria criteria)
        {
            criteria.EnterpriseCode = CurrentUser.Identity;
            return Service.GetInvivations(criteria);
        }
    }
}