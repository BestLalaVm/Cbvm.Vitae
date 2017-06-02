using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service;
using Business.Interface;

namespace Cbvm.Vitae.Template
{
    public partial class Demo : BaseFrontPage
    {

        private IAutoLoginService _loginService;
        private IAutoLoginService loginService
        {
            get
            {
                if (_loginService == null)
                {
                    _loginService = new AutoLoginService();
                }

                return _loginService;
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (CurrentUser != null && CurrentUser.UserType!=Presentation.Enum.UserType.Guest)
            {
                var token = loginService.CreateToken(CurrentUser.UserName, CurrentUser.UserType, HttpContext.Current.Request.UserHostAddress);

                txtToken.Text = token;

                txtUserName.Text = CurrentUser.UserName;

                txtUserType.Text = CurrentUser.UserType.ToString();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"pupop_script","alert('请先登入!');",true);
            }
            
        }
    }
}