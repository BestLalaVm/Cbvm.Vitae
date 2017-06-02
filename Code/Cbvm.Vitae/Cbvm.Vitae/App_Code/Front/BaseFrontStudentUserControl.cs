using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.UIView.Student;
using Presentation.UIView.Student.View;

public class BaseFrontStudentUserControl : BaseFrontUserControl
{
    protected string StudentNum
    {
        get
        {
            return (this.Page as BaseFrontStudentPage).StudentNum;
        }
    }

    protected string KeyCode
    {
        get
        {
            return (this.Page as BaseFrontStudentPage).KeyCode;
        }
    }

    private IStudentService Service
    {
        get
        {
            return new StudentService();
        }
    }

    protected StudentFrontPresentation StudentInfo
    {
        get
        {
            var student = this.ViewState["StudentInfo"] as StudentFrontPresentation;
            if (student == null)
            {
                student = Service.GetFront(StudentNum);
                this.ViewState["StudentInfo"] = student;
            }
            return student;
        }
    }
}