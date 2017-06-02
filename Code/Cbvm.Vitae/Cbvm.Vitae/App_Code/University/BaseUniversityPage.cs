using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presentation.Enum;
using WebLibrary;
using Business.Interface.University;
using Business.Service.University;

public class BaseUniversityPage : BasePage
{
    public override UserType InitUserType()
    {
        return UserType.University;
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }

    private IUniversityService Service
    {
        get
        {
            return new UniversityService();
        }
    }

    protected string UniversityCode
    {
        get
        {
            return HaddockContext.Current.CurrentUser.Identity;
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "UniversityManage";
        base.OnPreInit(e);
    }


    public int UniversityAdminId
    {
        get
        {
            return CurrentUser.Id;
        }
    }
}