using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Presentation.UIView;
using WebLibrary;

public class BaseFrontUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPostData();
        if (!IsPostBack)
        {
            InitData();
        }
    }

    protected virtual void InitPostData()
    {

    }

    protected virtual void InitData()
    {

    }

    protected CultureInfo GetCultureInfo()
    {
        return (Page as BaseFrontPage).GetCultureInfo();
    }

    protected void RedirectToDefaultPage()
    {
        Response.Redirect("~/Template/Default.aspx");
    }

    protected void ShowMsg(bool isSucess, string msg)
    {
        (Page as BaseFrontPage).ShowMsg(isSucess, msg);
    }

    protected LoginUserPresentation CurrentUser
    {
        get
        {
            return HaddockContext.Current.CurrentUser;
        }
    }

    protected bool IsNeedDivClosed
    {
        get
        {
            var flag = this.ViewState["IsNeedDivClosed"];
            if (flag != null && flag.ToString() == true.ToString())
            {
                return true;
            }

            return false;
        }

        set
        {
            this.ViewState["IsNeedDivClosed"] = value;
        }
    }
}