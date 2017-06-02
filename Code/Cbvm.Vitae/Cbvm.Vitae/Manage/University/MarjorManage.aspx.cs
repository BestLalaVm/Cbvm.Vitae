using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Base;
using Business.Service.Base;
using Presentation.Criteria.Base;
using Presentation.Criteria.College;
using Presentation.UIView;
using Presentation.UIView.Base;
using Business.Interface.College;
using Business.Service.College;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Business;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.College
{
    public partial class MarjorManage : BaseUniversityListPage<MarjorPresentation, MarjorCriteria>
    {
        private IMarjorService Service
        {
            get
            {
                return new MarjorService();
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
            var drpDepartName = e.Item.FindControl("drpDepartName") as DropDownList;
            string code = null;
            if (e.Item.ItemIndex >= 0){
                code = (string)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
            }


            var result = Service.Save(new MarjorPresentation()
            {
                Code = code,
                Name = txtName.Text,
                Description = txtDescription.Text,
                UniversityCode=this.UniversityCode,
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
                try
                {
                    var code = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"].ToString();
                    var result = Service.DeleteByTKey(code);
                    ShowMsg(result.IsSucess, result.Message);
                }
                catch (Exception ex)
                {
                    ShowMsg(false, "无法删除该专业信息!");
                }
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
                    var Marjor = e.Item.DataItem as MarjorPresentation;
                    txtName.Text = Marjor.Name;
                    txtDescription.Text = Marjor.Description;
                }
            }
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdTeacherGroup; }
        }

        protected override EntityCollection<MarjorPresentation> GetSearchResultList(MarjorCriteria criteria)
        {
            criteria.UniversityCode = this.UniversityCode;
            return Service.GetAll(criteria);
        }
    }
}