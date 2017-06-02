using System;
using Presentation.Enum;

public class BaseSupperPage : BasePage
{
    public override UserType InitUserType()
    {
        return UserType.Supper;
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }


    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "DepartAdmin";
        base.OnPreInit(e);
    }
}