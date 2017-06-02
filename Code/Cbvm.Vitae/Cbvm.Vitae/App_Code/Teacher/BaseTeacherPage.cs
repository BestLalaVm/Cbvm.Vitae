using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Interface.Teacher;
using Business.Service.Teacher;
using Presentation.Enum;


public class BaseTeacherPage : BasePage
{
    public override UserType InitUserType()
    {
        return UserType.Teacher;
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }

    private ITeacherService Service
    {
        get { return new TeacherService(); }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "TeacherManage";
        base.OnPreInit(e);
    }

    public string TeacherNum
    {
        get
        {
            return CurrentUser.UserName;
        }
    }
}