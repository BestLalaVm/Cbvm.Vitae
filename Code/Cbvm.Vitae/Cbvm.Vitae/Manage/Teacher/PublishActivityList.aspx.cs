using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation.Cache;
using Presentation.Criteria;
using Presentation.UIView;
using Telerik.Web.UI;


namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class PublishActivityList : BaseTeacherListPage<PublishActivityPresentation,PublishActivityCriteria>
    {
        private IPublishActivityService Service
        {
            get
            {
                return new PublishActivityService();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("PublishActivityDetail.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsOnline = sender as CheckBox;
            var dataItem = chkIsOnline.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            Service.SetStatus(TeacherNum, id, chkIsOnline.Checked);
            RadGridControl.Rebind();
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(id);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void grdPublishActivity_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                var isReferenced = (bool)e.Item.DataItem.GetType().GetProperty("IsReferenced").GetValue(e.Item.DataItem, null);
                if (isReferenced)
                {
                    e.Item.FindControl("btnDelete").Visible = false;
                }
            }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdPublishActivity; }
        }

        protected override EntityCollection<PublishActivityPresentation> GetSearchResultList(
            PublishActivityCriteria criteria)
        {
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<PublishActivityPresentation> list)
        {
            radGrid.DataSource = list.Select(ic => new
            {
                ic.Index,
                ic.Id,
                ActivityType = GlobalBaseDataCache.GetActivityTypeLabel(ic.ActivityType),
                ic.Address,
                ic.BeginTime,
                ic.Content,
                ic.CreateTime,
                ic.EndTime,
                ic.IsOnline,
                ic.IsReferenced,
                ic.Publisher,
                ic.PublisherType,
                ic.Status,
                ic.Title
            }).ToList();
        }
    }
}