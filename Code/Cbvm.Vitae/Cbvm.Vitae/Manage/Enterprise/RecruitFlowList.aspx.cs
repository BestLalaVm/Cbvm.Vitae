using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class RecruitFlowList : BaseEnterpriseListPage<EnterpriseRecruitBatchStatisticsPresentation, EnterpriseRecruitBatchCriteria>
    {
        private IEnterpriseRecruitBatchService Service
        {
            get
            {
                return new EnterpriseRecruitBatchService();
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
            get { return grdRecruitBatch; }
        }

        protected override Presentation.UIView.EntityCollection<EnterpriseRecruitBatchStatisticsPresentation> GetSearchResultList(EnterpriseRecruitBatchCriteria criteria)
        {
            criteria.EnterpriseCode = EnterpriseCode;
            return Service.GetAllStatistics(criteria);
        }
    }
}