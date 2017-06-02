using System;
using System.Text;

using Presentation.Enum;
using Presentation.UIView;
using System.Web.UI;
using WebLibrary;

public class BaseAccountPage : System.Web.UI.Page, IBasePageControl
{
    public virtual void ShowMsg(bool isSucess, string msg)
    {
        if (String.IsNullOrEmpty(msg))
        {
            msg = isSucess ? "操作成功!" : "操作失败!";
        }

        var script = new StringBuilder();
        script.AppendFormat("showAlterResultMsg({0},'{1}');", isSucess ? "true" : "false", msg);
        if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "PopMsg",
                                                        "$(function(){" + script.ToString() + "});", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AsyncPopMsg", script.ToString(),
                                                    true);
        }
    }

    public virtual void ShowSelfMsg(bool isSucess, string msg)
    {
        var script = new StringBuilder();
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
        script.AppendFormat("showAlterResultMsg({0},'{1}');", isSucess ? "true" : "false", msg);
        if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "PopMsg",
                                                        "$(function(){" + script.ToString() + "});", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AsyncPopMsg", script.ToString(),
                                                    true);
        }
    }

    public virtual void Close()
    {
        var script = new StringBuilder();
        script.AppendFormat("window.parent.removeFrameDialog();");
        if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "PopMsg",
                                                        "$(function(){" + script.ToString() + "});", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AsyncPopMsg", script.ToString(),
                                                    true);
        }
    }

    public virtual void WriteLog(string message)
    {

    }

    public virtual void WriteLog(Exception ex)
    {

    }

    protected UserType? _CurrentUserType;
    public virtual UserType UserType
    {
        get
        {
            if (!_CurrentUserType.HasValue)
            {
                if (CurrentUser != null)
                {
                    _CurrentUserType = CurrentUser.UserType;
                }
                else
                {
                    throw new Exception("用户类型无效!");
                }
            }
            return _CurrentUserType.Value;
        }
    }

    public LoginUserPresentation CurrentUser
    {
        get
        {
            return HaddockContext.Current.CurrentUser;
        }
    }

    //private LoginUserPresentation _CurrentAccount;

    //public LoginUserPresentation CurrentAccount
    //{
    //    get
    //    {
    //        if (_CurrentAccount == null)
    //        {
    //            EnsureAuthorized();
    //        }
    //        return _CurrentAccount;
    //    }
    //    protected set { _CurrentAccount = value; }
    //}

    public int MemberID
    {
        get { return CurrentUser.Id; }
    }

    protected virtual void EnsureAuthorized()
    {
        
    }

    protected AttachmentType? _AttachmentType;
    public AttachmentType AttachmentType
    {
        get
        {
            if (!_AttachmentType.HasValue)
            {
                throw new Exception("附件类型无效!");
            }
            return _AttachmentType.Value;
        }
    }
}