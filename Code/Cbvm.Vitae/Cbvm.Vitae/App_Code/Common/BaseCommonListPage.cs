using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Business.Service.Student;
using System.Web.UI.WebControls;
using Presentation.Criteria;
using Presentation.UIView;
using Telerik.Web.UI;
using WebLibrary.Helper;


public abstract class BaseCommonListPage<T, TCriteria> : BaseAccountPage
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
        if (criteria.PageSize > 0)
        {
            criteria.NeedPaging = true;
        }

        var list = GetSearchResultList(criteria);
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

    protected abstract EntityCollection<T> GetSearchResultList(TCriteria criteria);

    protected virtual void BindSearchResultList(RadGrid radGrid, IList<T> list)
    {
        radGrid.DataSource = list;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitBindData();
            InitData();
        }
        InitLoadedData();
    }

    protected virtual void InitBindData()
    {

    }

    protected virtual void InitData()
    {

    }

    protected virtual void InitLoadedData()
    {

    }

    private LkDataContext.CVAcademicianDataContext _DataContext;
    protected LkDataContext.CVAcademicianDataContext DataContext
    {
        get
        {
            if (_DataContext == null)
            {
                _DataContext = new LkDataContext.CVAcademicianDataContext();
            }

            return _DataContext;
        }
    }

    public EntityCollection<T> Translate2Presentations<T>(List<T> list) where T : BasePresentation, new()
    {
        int index = 0;
        EntityCollection<T> entityCollection =
            new EntityCollection<T>();
        list.ForEach(it =>
        {
            it.Index = ++index;
            entityCollection.Add(it);
        });

        return entityCollection;
    }
}