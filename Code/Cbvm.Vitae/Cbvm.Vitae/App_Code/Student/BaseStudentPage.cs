using System;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Enum;


public class BaseStudentPage : BasePage
{
    public override UserType InitUserType()
    {
        return UserType.Student;
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }

    private IStudentService Service
    {
        get
        {
           return new StudentService();
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "StudentManage";
        base.OnPreInit(e);
    }

    public string StudentNum
    {
        get
        {
            return CurrentUser.UserName;
        }
    }
}