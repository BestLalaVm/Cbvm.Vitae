using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.UserControlV2
{
    public partial class EnterpriseRegister : BaseFrontUserControl
    {
        private IEnterpriseService Service
        {
            get
            {
                return new EnterpriseService();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!CheckCodeHelper.ValidateCheckCode(UserType.Enterprise.ToString(), txt_CheckCode_.Text))
            {
                ShowMsg(false, "验证码错误!");
                return;
            }
            if (this.txt_Password_.Text != this.txt_Password2_.Text)
            {
                cvcCompare.IsValid = false;
                ShowMsg(false, "确认密码和密码不一致!");
                return;
            }

            if (Page.IsValid)
            {
                var thumbPath = FileHelper.GenerateRelativeThumbFilePath("EnterpriseRegister", UserType.Enterprise,
                                                                         AttachmentType.BaseInfo, txt_LicenseNo_.FileName);
                FileHelper.DrawingUploadFile(txt_LicenseNo_.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), int.MaxValue, int.MaxValue);

                int enterpriseID = 0;
                var enterprise = new EnterprisePresentation()
                {
                    ContactName = txt_ContactName_.Text,
                    ContactPhone = txt_ContactPhoto_.Text,
                    CreateTime = DateTime.Now,
                    Email = txt_Email_.Text,
                    //Address = txt_Address_.Text,
                    //LicenseNo = txt_LicenseNo_.Text,
                    LicenseNoImage = thumbPath,
                    Name = txt_Name_.Text,
                    UserName = txt_UserName_.Text,
                    Password = txt_Password_.Text,
                    IsOnline = true
                };
                var result = Service.Register(enterprise, out enterpriseID);
                if (result.IsSucess)
                {
                    Response.Redirect("~/Template/Login.aspx");
                }

                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void cvcCheckCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!CheckCodeHelper.ValidateCheckCode(UserType.Enterprise.ToString(), args.Value))
            {
                args.IsValid = false;
            }
        }
    }
}