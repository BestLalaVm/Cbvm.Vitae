using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.University;
using Business.Service.University;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;
using Telerik.Web.UI;
using Cbvm.Vitae.Manage.UserControl;
using LkDataContext;
using Business;

namespace Cbvm.Vitae.Manage.University
{
    public partial class MessageCategoryManage : BaseUniversityListPage<UniversityMessageCategoryPresentation, UniversityMessageCategoryCriteria>
    {
        private IUniversityMessageCategoryService Service
        {
            get
            {
                return new UniversityMessageCategoryService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void grdMessage_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode && !(e.Item is GridEditFormInsertItem))
            {
                UniversityMessageCategoryPresentation category = e.Item.DataItem as UniversityMessageCategoryPresentation;
                var txtName = e.Item.FindControl("txtName") as TextBox;
                var txtDescription = e.Item.FindControl("txtDescription") as TextBox;
                HiddenField hdfKey = e.Item.FindControl("hdfKey") as HiddenField;

                txtName.Text = category.Name;
                txtDescription.Text = category.Description;
                hdfKey.Value = category.Code;
            }
        }

        protected void grdMessage_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var txtName = e.Item.FindControl("txtName") as TextBox;
            var txtDescription = e.Item.FindControl("txtDescription") as TextBox;
            HiddenField hdfKey = e.Item.FindControl("hdfKey") as HiddenField;

            string code = null;
            if (e.Item.ItemIndex >= 0)
            {
                code = (string)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
            }
            var result = Service.Save(new UniversityMessageCategoryPresentation()
            {
                Code = code,
                Description = txtDescription.Text,
                Name = txtName.Text,
                UniversityCode=CurrentUser.Identity
            });

            ShowMsg(result.IsSucess, result.Message);

            if (result.IsSucess)
            {
                this.grdMessage.Rebind();
            }
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var code = (string)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
                var result = Service.Delete(code);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.grdMessage.MasterTableView.InsertItem();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdMessage; }
        }

        protected override EntityCollection<UniversityMessageCategoryPresentation> GetSearchResultList(UniversityMessageCategoryCriteria criteria)
        {
            criteria.UniversityCode = CurrentUser.Identity;
            return Service.GetAll(criteria);
        }        
    }
}