using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Presentation.Criteria;
using Presentation.UIView;
using Telerik.Web.UI;
using WebLibrary.Helper;


public abstract class BasePageList<TPresentation, TCriteria> : BasePage
    where TCriteria : BaseCriteria, new()
    where TPresentation:BasePresentation,new()
{
    protected abstract Panel PnlConditionControl { get; }

    protected abstract RadGrid RadGridControl { get; }

    public virtual TCriteria ExtractCriteria(Panel pnlCondition)
    {
        var criteria = pnlCondition.ExtractCriteriaFromPanel<TCriteria>();

        criteria.NeedPaging = RadGridControl.AllowCustomPaging;
        criteria.PageIndex = RadGridControl.CurrentPageIndex;
        criteria.PageSize = RadGridControl.PageSize;

        return criteria;
    }

    protected void radGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        RadGridControl.CurrentPageIndex = e.NewPageIndex;
    }

    protected void radGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        var list = GetSearchResultList(PnlConditionControl.ExtractCriteriaFromPanel<TCriteria>());
        BindSearchResultList(RadGridControl, list);
        RadGridControl.MasterTableView.VirtualItemCount = list.TotalCount;
    }

    protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        PnlConditionControl.Reset(RadGridControl);
    }

    protected abstract EntityCollection<TPresentation> GetSearchResultList(TCriteria criteria);

    protected virtual void BindSearchResultList(RadGrid radGrid, IList<TPresentation> list)
    {
        radGrid.DataSource = list;
    }
}