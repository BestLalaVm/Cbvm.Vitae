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

namespace Cbvm.Vitae.Template.UserControl
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
                return;
            }
            if (this.txt_Password_.Text != this.txt_Password2_.Text)
            {
                cvcCompare.IsValid = false;
                return;
            }

            if (Page.IsValid)
            {
                int enterpriseID = 0;
                var enterprise = new EnterprisePresentation()
                {
                    ContactName = txt_ContactName_.Text,
                    ContactPhone = txt_ContactPhoto_.Text,
                    CreateTime = DateTime.Now,
                    Email = txt_Email_.Text,
                    //Address = txt_Address_.Text,
                    //LicenseNoImage = txt_LicenseNo_.Text,
                    Name = txt_Name_.Text,
                    UserName = txt_UserName_.Text,
                    Password = txt_Password_.Text,
                    IsOnline = true
                };
                var result = Service.Register(enterprise, out enterpriseID);
                if (result.IsSucess)
                {
                    AuthorizeHelper.SetCurrentUser(new LoginUserPresentation()
                    {
                        Id = enterpriseID,
                        UserName = txt_UserName_.Text,
                        UserType = UserType.Enterprise,
                        Identity = enterprise.Code
                    }, true);
                    Response.Redirect("~/Template/");
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