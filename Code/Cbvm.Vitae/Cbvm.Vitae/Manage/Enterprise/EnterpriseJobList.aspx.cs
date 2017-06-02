using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Cache;
using Presentation.Criteria;
using Presentation.Criteria.Enterprise;
using Presentation.UIView.Enterprise;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class EnterpriseJobList : BaseEnterpriseListPage<EnterpriseJobPresentation,EnterpriseJobCriteria>
    {
        private IEnterpriseJobService Service
        {
            get
            {
                return new EnterpriseJobService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }


        protected void grdEnterpriseJob_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.EditFormItem && e.Item.IsInEditMode)
            {
                var data = e.Item.DataItem as EnterpriseJobPresentation;
                if (data != null)
                {
                    var hdfJobCode = e.Item.FindControl("hdfJobCode") as HiddenField;
                    hdfJobCode.Value = data.Code;
                }
                else
                {
                    var enterprise = (new EnterpriseService()).Get(CurrentUser.Identity);

                    data = new EnterpriseJobPresentation
                    {
                        ProvinceCode = enterprise.ProvinceCode,
                        ProvinceName = enterprise.ProvinceName,
                        CityCode = enterprise.CityCode,
                        CityName = enterprise.CityName,
                        DistrictCode = enterprise.DistrictCode,
                        DistrictName = enterprise.DistrictName,
                        StreetCode = enterprise.StreetCode,
                        StreetName = enterprise.StreetName,
                        ContactName=enterprise.ContactName,
                        Telephone=enterprise.ContactPhone
                    };

                    data.WorkPlace = data.Address;
                }

                var enterpriseJobDetail = e.Item.FindControl("enterpriseJobDetail") as UserControl.EnterpriseJobDetail;
                enterpriseJobDetail.LoadData(data);
            }
            else if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var ltlSex = e.Item.FindControl("ltlSex") as Literal;
                var data = e.Item.DataItem as EnterpriseJobPresentation;
                ltlSex.Text = GlobalBaseDataCache.GetSexLabel(data.Sex);
            }
        }

        protected void grdEnterpriseJob_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                string jobCode = "";
                if (e.Item.ItemIndex > -1)
                {
                    jobCode = (string) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
                }
                var enterpriseJobDetail =
                    e.Item.FindControl("enterpriseJobDetail") as UserControl.EnterpriseJobDetail;
                var data = enterpriseJobDetail.SaveData(jobCode);
                data.EnterpriseCode = EnterpriseCode;
                var result = Service.Save(data);
            }
            else if (e.CommandName == "Delete")
            {
                var jobCode = (string) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
                var result = Service.Delete(new EnterpriseJobCriteria()
                {
                    Code = jobCode,
                    EnterpriseCode = EnterpriseCode
                });
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.grdEnterpriseJob.MasterTableView.InsertItem();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdEnterpriseJob; }
        }

        protected override Presentation.UIView.EntityCollection<EnterpriseJobPresentation> GetSearchResultList(EnterpriseJobCriteria criteria)
        {
            criteria.EnterpriseCode = EnterpriseCode;
            return Service.GetAll(criteria);
        }
    }
}