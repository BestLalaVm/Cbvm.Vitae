using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation.Cache;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using Telerik.Web.UI;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class PublishActivityList : BaseCommonListPage<PublishActivityPresentation, PublishActivityCriteria>
    {
        private IPublishActivityService Service
        {
            get
            {
                return new PublishActivityService();
            }
        }

        private string StudentNum
        {
            get
            {
                return Request.QueryString["StudentNum"];
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.Rebind();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            List<int> publishActivityList = new List<int>();
            for (var index = 0; index < grdPublishActivity.MasterTableView.Items.Count; index++)
            {
                var item = grdPublishActivity.MasterTableView.Items[index];
                var chkSelected = item.FindControl("chkSelected") as CheckBox;
                if (chkSelected.Checked)
                {
                    publishActivityList.Add((int)item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"]);
                }
            }
        }

        protected void grdPublishActivity_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem || e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
            {
                var data = e.Item.DataItem as PublishActivityPresentation;
                var linkStatus = e.Item.FindControl("linkStatus") as LinkButton;
                if (data.Status == PublishAppliedStatus.Applied)
                {
                    linkStatus.Enabled = false;
                    linkStatus.Text = "已应用";
                }
                linkStatus.CommandArgument = data.Id.ToString();
            }
        }

        protected void linkStatus_Click(object sender, EventArgs e)
        {
            var linkStatus = sender as LinkButton;
            var activityID = int.Parse(linkStatus.CommandArgument);
            var result = Service.ApplyPublishActivity2StudentNum(activityID, StudentNum);
            ShowMsg(result.IsSucess, result.Message);
            RadGridControl.Rebind();
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdPublishActivity; }
        }

        protected override EntityCollection<PublishActivityPresentation> GetSearchResultList(
            PublishActivityCriteria criteria)
        {
            criteria.StudentNum = StudentNum;
            return Service.GetAllForStudent(criteria, StudentNum);
        }
    }
}