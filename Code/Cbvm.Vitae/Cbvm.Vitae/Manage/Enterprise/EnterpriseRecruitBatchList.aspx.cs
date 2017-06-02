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
using Telerik.Web.UI;


namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class EnterpriseRecruitBatchList : BaseEnterpriseListPage<EnterpriseRecruitBatchStatisticsPresentation, EnterpriseRecruitBatchCriteria>
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

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var recruitBatchID = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
            var result = Service.Delete(recruitBatchID);
            ShowMsg(result.IsSucess, result.Message);
        }

        protected override RadGrid RadGridControl
        {
            get { return grdRecruitBatch; }
        }

        protected override EntityCollection<EnterpriseRecruitBatchStatisticsPresentation> GetSearchResultList(EnterpriseRecruitBatchCriteria criteria)
        {
            criteria.EnterpriseCode = EnterpriseCode;
            return Service.GetAllStatistics(criteria);
        }
    }
}