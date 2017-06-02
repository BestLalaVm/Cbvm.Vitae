using System;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Cache;
using Presentation.UIView.Enterprise;
using WebLibrary.Helper;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.Criteria;
using System.Web.UI.WebControls;
using System.Linq;

namespace Cbvm.Vitae.Manage.Enterprise.UserControl
{
    public partial class UserInfo : BaseUserControl
    {
        private IEnterpriseService Service
        {
            get
            {
                return new EnterpriseService();
            }
        }

        private string EnterpriseCode
        {
            get
            {
                return CurrentUser.Identity;
            }
        }

        protected UploadFileItemPresentation Filedata
        {
            get { return this.ViewState["Filedata"] as UploadFileItemPresentation; }
            set { this.ViewState["Filedata"] = value; }
        }

        protected UploadFileItemPresentation LicenseNoImageFiledata
        {
            get { return this.ViewState["LicenseNoImageFiledata"] as UploadFileItemPresentation; }
            set { this.ViewState["LicenseNoImageFiledata"] = value; }
        }

        protected UploadFileItemPresentation OrganizationCodeImageFiledata
        {
            get { return this.ViewState["OrganizationCodeImageFiledata"] as UploadFileItemPresentation; }
            set { this.ViewState["OrganizationCodeImageFiledata"] = value; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected override void BindData()
        {
            drp_CdIndustryCode_.BindSource(BindingSourceType.IndustryCodeInfo, false);
            //drp_CdRegionCode_.BindSource(BindingSourceType.RegionCodeInfo, false);
            drp_EnterpriseTypeCode_.BindSource(BindingSourceType.EnterpriseTypeCodeInfo, false);
            drp_ScopeCode_.BindSource(BindingSourceType.EnterpriseScopeCodeInfo, false);

            drp_ProvinceName_.BindSource(BindingSourceType.ProvinceInfo, true);
        }

        protected override void InitData()
        {
            var presentation = Service.Get(EnterpriseCode);

            txt_Code_.Text = presentation.Code;
            txt_Address_.Text = presentation.Address;
            txt_ContactName_.Text = presentation.ContactName;
            txt_ContactPhone_.Text = presentation.ContactPhone;
            txt_Corporation_.Text = presentation.Corporation;
            txt_Email_.Text = presentation.Email;
            txt_Name_.Text = presentation.Name;
            txt_UserName_.Text = presentation.UserName;
            txt_WebSite_.Text = presentation.WebSite;

            drp_CdIndustryCode_.SelectedValue = presentation.IndustryCode;
            //drp_CdRegionCode_.SelectedValue = presentation.RegionCode;
            drp_EnterpriseTypeCode_.SelectedValue = presentation.EnterpriseTypeCode;
            drp_ScopeCode_.SelectedValue = presentation.ScopeCode;
            lbl_VerifyStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel(presentation.VerifyStatus);
            chk_IsOnline_.Checked = presentation.IsOnline;
            imgSource.ImageUrl = presentation.Photo;

            if (!String.IsNullOrEmpty(presentation.ProvinceCode))
            {
                drp_CityName_.Enabled = true;
                drp_CityName_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
                {
                    Code = presentation.ProvinceCode
                });
            }

            if (!String.IsNullOrEmpty(presentation.CityCode))
            {
                drp_DistrictName_.Enabled = true;
                drp_DistrictName_.BindSource(BindingSourceType.DistrictInfo, true, new AddressCriteria()
                {
                    Code = presentation.CityCode
                });
            }

            drp_ProvinceName_.SelectedValue = presentation.ProvinceCode;
            drp_CityName_.SelectedValue = presentation.CityCode;
            drp_DistrictName_.SelectedValue = presentation.DistrictCode;

            editDescription.LoadData(presentation.Description);

            Filedata = new UploadFileItemPresentation()
            {
                Path = presentation.Photo,
                ThumbPath = presentation.ThumbPath
            };

            imgLicenseNoImageSource.ImageUrl = presentation.LicenseNoImage;
            SwitchImageLink(imgLicenseNoImageSource, lnkLicenseNoImageSource, presentation.LicenseNoImage);
            imgOrganizationCodeImageSource.ImageUrl = presentation.OrganizationCodeImage;
            SwitchImageLink(imgOrganizationCodeImageSource, lnkOrganizationCodeImageSource, presentation.OrganizationCodeImage);

            LicenseNoImageFiledata = new UploadFileItemPresentation()
            {
                Path = presentation.LicenseNoImage
            };
            OrganizationCodeImageFiledata = new UploadFileItemPresentation()
            {
                Path = presentation.OrganizationCodeImage
            };

        }


        protected override void OnInit(EventArgs e)
        {
            upLoadControl.FinishUploadingImageEventHandler +=
                new BaseUploadControl.FinishUploadingImageEvent(upLoadControl_FinishUploadingImageEventHandler);

            upLicenseNoImageLoadControl.FinishUploadingImageEventHandler += upLicenseNoImageLoadControl_FinishUploadingImageEventHandler;

            upOrganizationCodeImageLoadControl.FinishUploadingImageEventHandler += upOrganizationCodeImageLoadControl_FinishUploadingImageEventHandler;

            base.OnInit(e);
        }

        protected void upLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgSource.ImageUrl = fileItem.FilePath;
            var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Enterprise,
                                                                     AttachmentType.BaseInfo, fileItem.FileName);
            FileHelper.DrawingUploadFile(fileItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), 60, 50);
            Filedata = new UploadFileItemPresentation
            {
                Path = fileItem.FilePath,
                ThumbPath = thumbPath
            };

        }

        void upOrganizationCodeImageLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgOrganizationCodeImageSource.ImageUrl = fileItem.FilePath;
            //var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Enterprise,
            //                                                         AttachmentType.BaseInfo, fileItem.FileName);
            //FileHelper.DrawingUploadFile(fileItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), 60, 50);
            OrganizationCodeImageFiledata = new UploadFileItemPresentation
            {
                Path = fileItem.FilePath,
                //ThumbPath = thumbPath
            };

            SwitchImageLink(imgOrganizationCodeImageSource, lnkOrganizationCodeImageSource, fileItem.FilePath);
        }

        void upLicenseNoImageLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgLicenseNoImageSource.ImageUrl = fileItem.FilePath;
            //var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Enterprise,
            //                                                         AttachmentType.BaseInfo, fileItem.FileName);
            //FileHelper.DrawingUploadFile(fileItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), 60, 50);
            LicenseNoImageFiledata = new UploadFileItemPresentation
            {
                Path = fileItem.FilePath,
                //ThumbPath = thumbPath
            };


            SwitchImageLink(imgLicenseNoImageSource, lnkLicenseNoImageSource, fileItem.FilePath);
        }
        private void SaveData()
        {
            var presentation = new EnterprisePresentation()
            {
                Code = EnterpriseCode,
                Address = txt_Address_.Text,
                ContactName = txt_ContactName_.Text,
                ContactPhone = txt_ContactPhone_.Text,
                Corporation = txt_Corporation_.Text,
                Email = txt_Email_.Text,
                Name = txt_Name_.Text,
                WebSite = txt_WebSite_.Text,
                IndustryCode = drp_CdIndustryCode_.SelectedValue,
                //RegionCode = drp_CdRegionCode_.SelectedValue,
                EnterpriseTypeCode = drp_EnterpriseTypeCode_.SelectedValue,
                ScopeCode = drp_ScopeCode_.SelectedValue,
                IsOnline = chk_IsOnline_.Checked,
                Description = editDescription.SaveData(),
                Photo = Filedata.Path,
                ThumbPath = Filedata.ThumbPath,
                LicenseNoImage=LicenseNoImageFiledata.Path,
                OrganizationCodeImage=OrganizationCodeImageFiledata.Path
            };

            if (drp_ProvinceName_.SelectedItem != null)
            {
                presentation.ProvinceCode = drp_ProvinceName_.SelectedValue;
                presentation.ProvinceName = drp_ProvinceName_.SelectedItem.Text;
            }

            if (drp_CityName_.SelectedItem != null)
            {
                presentation.CityCode = drp_CityName_.SelectedValue;
                presentation.CityName = drp_CityName_.SelectedItem.Text;
            }

            if (drp_DistrictName_.SelectedItem != null)
            {
                presentation.DistrictCode = drp_DistrictName_.SelectedValue;
                presentation.DistrictName = drp_DistrictName_.SelectedItem.Text;
            }


            if (presentation.VerifyStatus != Presentation.Enum.VerifyStatus.Passed)
            {
                presentation.VerifyStatus = Presentation.Enum.VerifyStatus.WaitAudited;
            }

            var result = Service.Save(presentation);

            ShowMsg(result.IsSucess, result.Message);

        }

        protected void drp_ProvinceName__SelectedIndexChanged(object sender, EventArgs e)
        {
            drp_CityName_.Enabled = true;
            drp_DistrictName_.Enabled = false;
            drp_CityName_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
            {
                Code = drp_ProvinceName_.SelectedValue
            });

            drp_DistrictName_.SelectedValue = "";
        }

        protected void drp_CityName__SelectedIndexChanged(object sender, EventArgs e)
        {
            drp_DistrictName_.Enabled = true;
            drp_DistrictName_.BindSource(BindingSourceType.DistrictInfo, true, new AddressCriteria()
            {
                Code = drp_CityName_.SelectedValue
            });
        }

        private void SwitchImageLink(Image image,HyperLink link,string imagePath)
        {
            image.ImageUrl = imagePath;
            link.NavigateUrl = imagePath;
            link.Visible = true;
            image.Visible = false;

            if (!String.IsNullOrEmpty(imagePath))
            {
                if ((new string[] { ".png", ".jpg", ".jpeg" }).Any(ii => imagePath.EndsWith(ii, StringComparison.OrdinalIgnoreCase)))
                {
                    link.Visible = false;
                    image.Visible = true;
                }
            }
        }
    }
}