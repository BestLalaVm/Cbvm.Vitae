using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Base;
using Business.Service.Base;
using Presentation.Criteria.Base;
using Presentation.UIView;
using Presentation.UIView.Base;
using Presentation.Criteria.College;
using Business.Interface.College;
using Business.Service.College;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Business;

namespace Cbvm.Vitae.Manage.College
{
    public partial class DepartManage : BaseCollegeListPage<DepartPresentation, DepartCriteria>
    {
        private IDepartService Service
        {
            get
            {
                return new DepartService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void radGrid_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var txtName = e.Item.FindControl("txtName") as TextBox;
            var txtDescription = e.Item.FindControl("txtDescription") as TextBox;
            string code = null;
            if (e.Item.ItemIndex >= 0)
            {
                code = (string)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
            }


            var result = Service.Save(new DepartPresentation()
            {
                Code = code,
                Name = txtName.Text,
                Description = txtDescription.Text,
                CollegeCode = CurrentUser.Identity
            });

            if (!result.IsSucess)
            {
                e.Canceled = true;
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void radGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var code = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"].ToString();
                var result = Service.DeleteByTKey(code);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.RadGridControl.MasterTableView.InsertItem();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void radGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var txtName = e.Item.FindControl("txtName") as TextBox;
                var txtDescription = e.Item.FindControl("txtDescription") as TextBox;

                if (!(e.Item is GridEditFormInsertItem))
                {
                    var Depart = e.Item.DataItem as DepartPresentation;
                    txtName.Text = Depart.Name;
                    txtDescription.Text = Depart.Description;
                }
            }
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdTeacherGroup; }
        }

        protected override EntityCollection<DepartPresentation> GetSearchResultList(DepartCriteria criteria)
        {
            return Service.GetAll(criteria);
        }
    }
}