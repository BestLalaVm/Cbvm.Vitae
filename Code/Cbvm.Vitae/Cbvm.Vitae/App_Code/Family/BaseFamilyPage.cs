using System;
using Presentation.Enum;

public class BaseFamilyPage:BasePage
{
    public override UserType InitUserType()
    {
        return UserType.Family;        
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "FamilyManage";
        base.OnPreInit(e);
    }

    public string FamilyId
    {
        get
        {
            return CurrentUser.Identity;
        }
    }

    public string StudentNum
    {
        get
        {
            return CurrentUser.AddtionalUser["StudentNum"];
        }
    }
}