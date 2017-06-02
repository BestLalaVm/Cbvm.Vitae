using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Business.Service;
using Presentation.UIView;
using System.Globalization;
using System.Text;
using WebLibrary;
using WebLibrary.Helper;

public class BaseFrontPage : Page
{
    protected override void OnInit(EventArgs e)
    {
        if (CurrentUser == null)
        {
            var user = ThirdUserService.LoginViaGuest();
            AuthorizeHelper.SetCurrentUser(user);
        }
        else
        {
            var service = new Business.Service.AutoLoginService();
            var ipAddress = HttpContext.Current.Request.UserHostAddress;
            if (service.IsForce2Logout(ipAddress, CurrentUser.UserName, CurrentUser.UserType))
            {
                AuthorizeHelper.LogOut();
                this.RedirectToLoginPage();
            }
        }
        base.OnInit(e);
    }

    internal CultureInfo GetCultureInfo()
    {
        return CultureInfo.CurrentCulture;
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "BaseFront";
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();
        }
    }

    protected virtual void InitData()
    {

    }

    public void RedirectToDefaultPage()
    {

    }

    protected LoginUserPresentation CurrentUser
    {
        get
        {
            return HaddockContext.Current.CurrentUser;
        }
    }

    public void ShowMsg(bool isSucess, string msg)
    {
        if (String.IsNullOrEmpty(msg))
        {
            if (isSucess)
            {
                msg = "操作成功!";
            }
            else
            {
                msg = "操作失败!";
            }
        }
        var script = new StringBuilder();
        script.AppendFormat("showAlterResultMsg({0},'{1}');", isSucess ? "true" : "false", msg);
        //var isAsyncPostBack = ScriptManager.GetCurrent(Page).IsInAsyncPostBack;
        if (!Page.IsAsync)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "PopMsg", "$(function(){" + script.ToString() + "});", true);
        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AsyncPopMsg", script.ToString(), true);
        }   
    }
}