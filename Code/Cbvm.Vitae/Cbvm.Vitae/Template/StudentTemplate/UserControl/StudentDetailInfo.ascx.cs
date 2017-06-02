using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation;
using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class StudentDetailInfo : BaseFrontStudentUserControl
    {
        protected override void InitData()
        {
            base.InitData();

            if (!StudentInfo.IsOnline)
            {
                this.Visible = false;
                return;
            }

            ltlAboutMe.Text = StudentInfo.AboutMe.HtmlEncode();
            ltlActivity.Text = StudentInfo.Activity.HtmlEncode();
            ltlBook.Text = StudentInfo.Book.HtmlEncode();
            ltlInterested.Text = StudentInfo.Interested.HtmlEncode();
            ltlMovie.Text = StudentInfo.Movie.HtmlEncode();
            ltlMusic.Text = StudentInfo.Music.HtmlEncode();
            ltlProgram.Text = StudentInfo.Program.HtmlEncode();

            if (StudentInfo.JobExpect != null)
            {
                ltlJobAddress.Text = StudentInfo.JobExpect.JobAddress.HtmlEncode();
                ltlJobRequired.Text = StudentInfo.JobExpect.JobRequired.HtmlEncode();
                ltlJobContent.Text = StudentInfo.JobExpect.JobContent.HtmlEncode();
                ltlJobSalary.Text = StudentInfo.JobExpect.JobSalary.HtmlEncode();
                cntJobExpect.Visible =
                    ((int)StudentInfo.JobExpect.OpenType).Contain(
                        (int) GetStudentOpenType(CurrentUser.UserType));
            }
        }

        private StudentOpenType GetStudentOpenType(UserType userType)
        {
            switch (userType)
            {
                case UserType.Enterprise:
                    return StudentOpenType.Enterprise;
                case UserType.Guest:
                    return StudentOpenType.Guest;
                case UserType.Teacher:
                    return StudentOpenType.Teacher;
                case UserType.Student:
                    if (CurrentUser.UserName == StudentNum)
                    {
                        return StudentOpenType.Owner;
                    }
                    return StudentOpenType.Student;
            }
            return StudentOpenType.None;
        }
    }
}