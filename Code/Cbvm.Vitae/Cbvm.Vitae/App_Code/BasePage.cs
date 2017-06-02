using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Presentation.Enum;
using Presentation.UIView;
using System.Web.Security;
using System.Drawing;
using WebLibrary;
using WebLibrary.Helper;

public abstract class BasePage: BaseAccountPage
{
    public Size MaxThumbSize
    {
        get
        {
            return new Size
            {
                Width = 160,
                Height = 140
            };
        }
    }

    public abstract UserType InitUserType();

    public abstract AttachmentType InitAttachmentType();

    protected override void OnInit(EventArgs e)
    {
        _CurrentUserType = InitUserType();
        _AttachmentType = InitAttachmentType();

        EnsureAuthorized();
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitBindData();
            InitData();
        }
        InitLoadedData();
    }

    protected virtual void InitBindData()
    {

    }

    protected virtual void InitData()
    {

    }

    protected virtual void InitLoadedData()
    {

    }

    protected override void EnsureAuthorized()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            var service = new Business.Service.AutoLoginService();
            var ipAddress = HttpContext.Current.Request.UserHostAddress;
            if (service.IsForce2Logout(ipAddress, CurrentUser.UserName, CurrentUser.UserType))
            {
                AuthorizeHelper.LogOut();
                this.RedirectToLoginPage();
                return;
            }

            Page.EnsureAuthorized(CurrentUser, this.UserType);
        }
        else
        {
            this.RedirectToLoginPage();
        }
        
    }

    public override void ShowMsg(bool isSucess, string msg)
    {
        Page.ShowMsg(isSucess,msg);
    }

    protected void RedirectToLoginPage()
    {
        //Page.RedirectToLoginPage();

        HttpContext.Current.RedirectToLoginPage();
    }

    private LkDataContext.CVAcademicianDataContext _DataContext;
    protected LkDataContext.CVAcademicianDataContext DataContext
    {
        get
        {
            if (_DataContext == null)
            {
                _DataContext = new LkDataContext.CVAcademicianDataContext();
            }

            return _DataContext;
        }
    }
}