using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Presentation.Criteria;
using Presentation.UIView;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Presentation.Criteria.Teacher;

public abstract class BaseTeacherListPage<T, TCriteria> : BaseTeacherPage
    where T : BasePresentation, new()
    where TCriteria : BaseCriteria, new()
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

        var teacherCriteria = criteria as BaseTeacherCriteria;
        if (teacherCriteria != null)
        {
            teacherCriteria.TeacherNum = this.TeacherNum;
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