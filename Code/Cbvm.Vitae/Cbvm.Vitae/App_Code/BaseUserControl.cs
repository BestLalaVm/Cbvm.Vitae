using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using Presentation.Enum;
using Presentation.UIView;
using WebLibrary;

public class BaseUserControl: UserControl

{
    public int MemberID
    {
        get
        {
            return BasePageControl.MemberID;
        }
    }

    public  UserType UserType
    {
        get
        {
            return BasePageControl.UserType;
        }
    }

    public LoginUserPresentation CurrentUser
    {
        get
        {
            if (BasePageControl == null)
            {
                return HaddockContext.Current.CurrentUser;
            }

            return BasePageControl.CurrentUser;
        }
    }

    public BasePage BasePage
    {
        get { return (this.Page as BasePage); }
    }

    public IBasePageControl BasePageControl
    {
        get { return this.Page as IBasePageControl; }
    }

    protected void ShowMsg(bool isSucess, string msg)
    {
        BasePageControl.ShowMsg(isSucess, msg);
    }

    protected void ShowSelfMsg(bool isSucess, string msg)
    {
        BasePageControl.ShowSelfMsg(isSucess, msg);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            InitData();
        }
        InitLoadedData();
    }

    protected virtual void InitData()
    {

    }

    protected virtual void BindData()
    {

    }

    protected virtual void InitLoadedData()
    {

    }

    protected void WriteLog(string message)
    {
        BasePage.WriteLog(message);
    }

    protected void WriteLog(Exception ex)
    {
        BasePage.WriteLog(ex);
    }

}