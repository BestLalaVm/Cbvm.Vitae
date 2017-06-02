using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Business.Interface.Base;
using Business.Service.Base;
using Presentation.Criteria;
using Presentation.Criteria.Base;
using Presentation.UIView;
using Presentation.UIView.Base;
using Telerik.Web.UI;

namespace Cbvm.Vitae.Manage.DepartAdmin
{
    public partial class RecruitFlowList : BaseDepartAdminListPage<RecruitFlowSettedPresentation, RecruitFlowSettedCriteria>
    {
        private IRecruitFlowSettedService Service
        {
            get
            {
                return new RecruitFlowSettedService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected int MaxDisplayOrder
        {
            get
            {
                if (this.ViewState["MaxDisplayOrder"] == null) return 0;
                return (int)this.ViewState["MaxDisplayOrder"];
            }
            set { this.ViewState["MaxDisplayOrder"] = value; }
        }

        protected int MinDisplayOrder
        {
            get
            {
                if (this.ViewState["MinDisplayOrder"] == null) return 0;
                return (int)this.ViewState["MinDisplayOrder"];
            }
            set { this.ViewState["MinDisplayOrder"] = value; }
        }

        protected void radGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "PerformInsert":
                    Insert(e.Item);
                    break;
                case "Update":
                    Update(e.Item);
                    break;
                case "delete":
                    Delete(e.Item);
                    break;
                case "subUp":
                case "subDown":
                    UpDownItem(e);
                    break;
            }
        }

        protected void radGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var imgSubUp = e.Item.FindControl("imgSubUp");
                var imgSubDown = e.Item.FindControl("imgSubDown");
                var dataItem = e.Item.DataItem as RecruitFlowSettedPresentation;
                if (dataItem.DisplayOrder == MinDisplayOrder)
                {
                    imgSubUp.Visible = false;
                }

                if (dataItem.DisplayOrder == MaxDisplayOrder)
                {
                    imgSubDown.Visible = false;
                }
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode &&
                !(e.Item is GridEditFormInsertItem))
            {
                var dataItem = e.Item.DataItem as RecruitFlowSettedPresentation;
                var txtName = e.Item.FindControl("txtName") as TextBox;
                var txtDescription = e.Item.FindControl("txtDescription") as TextBox;
                txtName.Text = dataItem.Name;
                txtDescription.Text = dataItem.Description;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            RadGridControl.MasterTableView.InsertItem();
        }

        private void Insert(GridItem item)
        {
            var txtName = item.FindControl("txtName") as TextBox;
            var txtDescription = item.FindControl("txtDescription") as TextBox;

            var result= Service.Save(new RecruitFlowSettedPresentation()
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                DisplayOrder = MaxDisplayOrder + 1
            });
            if (result.IsSucess)
            {
                MaxDisplayOrder = MaxDisplayOrder + 1;
            }
        }

        private void Update(GridItem item)
        {
            var txtName = item.FindControl("txtName") as TextBox;
            var txtDescription = item.FindControl("txtDescription") as TextBox;

            var recruitID = (int)item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"];
            var result = Service.Save(new RecruitFlowSettedPresentation()
            {
                Id = recruitID,
                Name = txtName.Text,
                Description = txtDescription.Text,
                DisplayOrder = MaxDisplayOrder + 1
            });

        }

        private void Delete(GridItem item)
        {
            var recruitId = (int)item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"];
            var actionResult = Service.Delete(recruitId);
            ShowMsg(actionResult.IsSucess, actionResult.Message);
            if (actionResult.IsSucess)
            {
                RadGridControl.Rebind();
            }
        }

        private void UpDownItem(GridCommandEventArgs e)
        {
            if (e.CommandName == "subUp" || e.CommandName == "subDown")
            {
                var recruitID = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var currentIndex = e.Item.ItemIndex;
                var nextIndex = currentIndex + 1;
                if (e.CommandName == "subUp")
                {
                    nextIndex = currentIndex - 1;
                }
                var nextRecruitID = (int)e.Item.OwnerTableView.DataKeyValues[nextIndex]["ID"];

                var actionResult = Service.SetRecruitFlowOrder(recruitID, nextRecruitID);
                if (actionResult.IsSucess)
                {
                    RadGridControl.Rebind();
                }
            }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdRecruitFlowSetted; }
        }

        protected override EntityCollection<RecruitFlowSettedPresentation> GetSearchResultList(RecruitFlowSettedCriteria criteria)
        {
            var list= Service.GetAll(criteria);
            if (list.Any())
            {
                MaxDisplayOrder = list.Max(it => it.DisplayOrder);
                MinDisplayOrder = list.Min(it => it.DisplayOrder);
            }
            return list;
        }


    }
}