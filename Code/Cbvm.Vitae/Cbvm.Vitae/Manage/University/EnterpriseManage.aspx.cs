using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Cache;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Telerik.Web.UI;

using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.University
{
    public partial class EnterpriseManage : BaseUniversityListPage<EnterprisePresentation, EnterpriseCriteria>
    {
        private IEnterpriseService Service
        {
            get
            {
                return new EnterpriseService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void grdEnterprise_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var cmbVerifyStatus = e.Item.FindControl("cmbVerifyStatus") as DropDownList;
                cmbVerifyStatus.BindSource(BindingSourceType.VerifyStatusInfo,false);

                var data = e.Item.DataItem as EnterprisePresentationView;
                var txtName = e.Item.FindControl("txtName") as TextBox;
                var txtLicenseNo = e.Item.FindControl("txtLicenseNo") as TextBox;
                var txtContactName = e.Item.FindControl("txtContactName") as TextBox;
                var txtContactPhone = e.Item.FindControl("txtContactPhone") as TextBox;
                var txtUserName = e.Item.FindControl("txtUserName") as TextBox;
                var txtEmail = e.Item.FindControl("txtEmail") as TextBox;
                var ltlDescription = e.Item.FindControl("ltlDescription") as Literal;
                var chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
                var chkIsFamous = e.Item.FindControl("chkIsFamous") as CheckBox;
                var imgLicenseNoImage = e.Item.FindControl("imgLicenseNoImage") as Image;
                var imgOrganizationCodeImage = e.Item.FindControl("imgOrganizationCodeImage") as Image;
                var lnkOrganizationCodeImage = e.Item.FindControl("lnkOrganizationCodeImage") as HyperLink;
                var lnkLicenseNoImage = e.Item.FindControl("lnkLicenseNoImage") as HyperLink;
                var lnkLicenseNoImageSource = e.Item.FindControl("lnkLicenseNoImageSource") as HyperLink;
                var lnkOrganizationCodeImageSource = e.Item.FindControl("lnkOrganizationCodeImageSource") as HyperLink;

                var pnlLicenseNoImage = e.Item.FindControl("pnlLicenseNoImage") as Panel;
                var pnlOrganizationCodeImage = e.Item.FindControl("pnlOrganizationCodeImage") as Panel;

                txtName.Text = data.Name;
                txtLicenseNo.Text = data.LicenseNoImage;
                txtContactName.Text = data.ContactName;
                txtContactPhone.Text = data.ContactPhone;
                txtUserName.Text = data.UserName;
                txtEmail.Text = data.Email;
                chkIsFamous.Checked = data.IsFamous;

                ltlDescription.Text = data.Description;
                chkIsOnline.Checked = data.IsOnline;
                cmbVerifyStatus.SelectedValue = ((int) data.VerifyStatus).ToString();

                imgLicenseNoImage.ImageUrl = data.LicenseNoImage;
                lnkLicenseNoImage.NavigateUrl = data.LicenseNoImage;
                imgOrganizationCodeImage.ImageUrl = data.OrganizationCodeImage;

                imgLicenseNoImage.Visible = !string.IsNullOrEmpty(data.LicenseNoImage);
                imgOrganizationCodeImage.Visible = !string.IsNullOrEmpty(data.OrganizationCodeImage);
                lnkOrganizationCodeImage.NavigateUrl = data.OrganizationCodeImage;

                lnkOrganizationCodeImageSource.NavigateUrl = data.OrganizationCodeImage;
                lnkLicenseNoImageSource.NavigateUrl = data.LicenseNoImage;
                lnkOrganizationCodeImageSource.Visible = true;
                lnkLicenseNoImageSource.Visible = true;

                pnlOrganizationCodeImage.Visible= lnkOrganizationCodeImage.Visible = false;
                pnlLicenseNoImage.Visible = lnkLicenseNoImage.Visible = false;

                if (!String.IsNullOrEmpty(data.OrganizationCodeImage))
                {
                    if ((new string[] { ".png", ".jpg", ".jpeg" }).Any(ii => data.OrganizationCodeImage.EndsWith(ii, StringComparison.OrdinalIgnoreCase)))
                    {
                        lnkOrganizationCodeImageSource.Visible = false;
                        pnlOrganizationCodeImage.Visible = lnkOrganizationCodeImage.Visible = true;
                    }
                }

                if (!String.IsNullOrEmpty(data.LicenseNoImage))
                {
                    if ((new string[] { ".png", ".jpg", ".jpeg" }).Any(ii => data.LicenseNoImage.EndsWith(ii, StringComparison.OrdinalIgnoreCase)))
                    {
                        lnkLicenseNoImageSource.Visible = false;
                        pnlLicenseNoImage.Visible = lnkLicenseNoImage.Visible = true;
                    }
                }
            }
        }

        protected void grdEnterprise_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var hdfID = e.Item.FindControl("hdfID") as HiddenField;
            var enterpriseID = int.Parse(hdfID.Value);
            DropDownList cmbVerifyStatus = e.Item.FindControl("cmbVerifyStatus") as DropDownList;
            var chkIsFamous = e.Item.FindControl("chkIsFamous") as CheckBox;

            var actionResult = Service.SetVerifyStatus(enterpriseID,
                                                       (VerifyStatus)int.Parse(cmbVerifyStatus.SelectedValue), chkIsFamous.Checked);
            ShowMsg(actionResult.IsSucess, actionResult.Message);
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var enterpriseId = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(enterpriseId);

                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdEnterprise; }
        }

        protected override EntityCollection<EnterprisePresentation> GetSearchResultList(EnterpriseCriteria criteria)
        {
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<EnterprisePresentation> list)
        {
            radGrid.DataSource = list.Select(it => new EnterprisePresentationView
            {
                Index =it.Index,
                Id = it.Id,
                Name = it.Name,
                LicenseNoImage = it.LicenseNoImage,
                ContactName = it.ContactName,
                ContactPhone = it.ContactPhone,
                Email = it.Email,
                UserName = it.UserName,
                IsOnline = it.IsOnline,
                VerifyStatus = it.VerifyStatus,
                Code = it.Code,
                IsFamous=it.IsFamous,
                OrganizationCodeImage = it.OrganizationCodeImage
            }).ToList();
        }
    }

    internal class EnterprisePresentationView : EnterprisePresentation
    {
        public string VerifyStatusName
        {
            get
            {
                return GlobalBaseDataCache.GetVerifityStatusLabel(VerifyStatus);
            }
        }
    }
}