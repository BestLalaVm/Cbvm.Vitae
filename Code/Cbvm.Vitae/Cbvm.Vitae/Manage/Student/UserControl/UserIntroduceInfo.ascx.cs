using System;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.UIView.Student;

namespace Cbvm.Vitae.Manage.Student.UserControl
{
    public partial class UserIntroduceInfo : BaseUserControl
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
            }
        }

        protected override void InitData()
        {
            var studentIntroduce = Service.GetIntroduce(CurrentUser.UserName);
            txt_AboutMe_.Text = studentIntroduce.AboutMe;
            txt_Activity_.Text = studentIntroduce.Activity;
            txt_Interest_.Text = studentIntroduce.Interested;
            txt_Music_.Text = studentIntroduce.Music;
            txt_Movie_.Text = studentIntroduce.Movie;
            txt_Program_.Text = studentIntroduce.Program;
            txt_Book_.Text = studentIntroduce.Book;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveData();
            }
        }

        public void SaveData()
        {
            var studentIntroduce = new StudentIntroducePresentation()
            {
                AboutMe = txt_AboutMe_.Text,
                Activity = txt_Activity_.Text,
                Interested = txt_Interest_.Text,
                Music = txt_Music_.Text,
                Movie = txt_Movie_.Text,
                Program = txt_Program_.Text,
                Book = txt_Book_.Text,
                StudentNum = CurrentUser.UserName
            };
            var result = Service.SaveIntroduce(studentIntroduce);

            ShowMsg(result.IsSucess, result.Message);
        }
    }
}