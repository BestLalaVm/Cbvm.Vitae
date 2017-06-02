using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presentation.Enum;
using WebLibrary;
using Business.Interface.College;
using Business.Service.College;

public class BaseCollegePage : BasePage
{
    public override UserType InitUserType()
    {
        return UserType.College;
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }

    private ICollegeService Service
    {
        get
        {
            return new CollegeService();
        }
    }

    protected string CollegeCode
    {
        get
        {
            return HaddockContext.Current.CurrentUser.Identity;
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "CollegeManage";
        base.OnPreInit(e);
    }

    public int CollegeAdminId
    {
        get
        {
            return CurrentUser.Id;
        }
    }
}