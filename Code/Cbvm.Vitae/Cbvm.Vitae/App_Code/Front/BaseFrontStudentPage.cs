using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Service.Student;

public abstract class BaseFrontStudentPage : BaseFrontPage
{
    public string StudentNum
    {
        get
        {
            var studentNum = Request.QueryString["StudentNum"];
            if (string.IsNullOrEmpty(studentNum)) return "";
            return studentNum.Split(',')[0];
        }
    }

    public string KeyCode
    {
        get
        {
            var keyCode = Request.QueryString["KeyCode"];
            if (string.IsNullOrEmpty(keyCode)) return "";
            return keyCode.Split(',')[0];
        }
    }

    public string KeyWord
    {
        get
        {
            var keyword = Request.QueryString["Keyword"];
            if (string.IsNullOrEmpty(keyword)) return "";
            return keyword.Split(',')[0];
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Request.UrlReferrer != null &&
            !Request.UrlReferrer.AbsolutePath.Contains("/" + this.StudentNum))
        {
            if (IsRecordVisisted())
            {
                if (CurrentUser.UserName != this.StudentNum)
                {
                    StudentService.AddVisitedRecord(this.StudentNum, HttpContext.Current.Request.UserHostAddress,
                        CurrentUser.UserName, CurrentUser.UserType);
                }
            }
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "FrontStudent";
    }

    public abstract string PageName
    {
        get;
    }

    protected virtual bool IsRecordVisisted()
    {
        return true;
    }
}