using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using Presentation.Criteria;
using Presentation.UIView;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Presentation.Criteria.Enterprise;


public abstract class BaseEnterpriseListPage<T, TCriteria> : BaseEnterprisePage where T : BasePresentation, new()
    where TCriteria:BaseCriteria,new()
{
    protected abstract Panel PnlConditionControl
    {
        get;
    }

    protected abstract RadGrid RadGridControl
    {
        get;
    }

    protected void radGrid_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        var radGrid = source as RadGrid;
        radGrid.CurrentPageIndex = e.NewPageIndex;
    }

    protected void radGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        var criteria = PnlConditionControl.ExtractCriteriaFromPanel<TCriteria>();
        criteria.PageIndex = RadGridControl.MasterTableView.CurrentPageIndex;
        criteria.PageSize = RadGridControl.MasterTableView.PageSize;
        var enterpriseCriteria = criteria as BaseEnterpriseCriteria;
        if (enterpriseCriteria != null)
        {
            enterpriseCriteria.EnterpriseCode = this.EnterpriseCode;
        }

        if (criteria.PageSize > 0)
        {
            criteria.NeedPaging = true;
        }

        var list = GetSearchResultList(criteria);
        BindSearchResultList(RadGridControl, list);
        RadGridControl.MasterTableView.VirtualItemCount = list.TotalCount;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        PnlConditionControl.Reset(RadGridControl);
    }

    protected abstract EntityCollection<T> GetSearchResultList(TCriteria criteria);

    protected virtual void BindSearchResultList(RadGrid radGrid, IList<T> list)
    {
        radGrid.DataSource = list;
    }
}