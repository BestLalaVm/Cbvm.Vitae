using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;


namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudenyDailyEssayDetail : BaseStudentDetailPage
    {
        private IStudentDailyEssayService Service
        {
            get
            {
                return new StudentDailyEssayService();
            }
        }

        private StudentDailyEssayPresentation _DailyEssay;
        private StudentDailyEssayPresentation DailyEssay
        {
            get
            {
                if (_DailyEssay == null)
                {
                    _DailyEssay = Service.Get(new StudentDailyEssayCriteria()
                    {
                        Id = CurrentID,
                        StudentNum = StudentNum,
                        IncludeRelativeData = true
                    });
                }
                return _DailyEssay;
            }
        }

        protected override void InitData()
        {
            if (DailyEssay != null)
            {
                txt_Content_.LoadData(DailyEssay.Content);
                txt_Title_.Text = DailyEssay.Title;
                chk_IsOnline_.Checked = DailyEssay.IsOnline;

                cmtDailyEssayList.LoadData(DailyEssay.CommentPresentations);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var result = Service.Save(new StudentDailyEssayPresentation()
            {
                Title = txt_Title_.Text,
                Content = txt_Content_.SaveData(),
                IsOnline = chk_IsOnline_.Checked,
                StudentNum = CurrentUser.UserName,
                Id = CurrentID
            });

            ShowMsg(result.IsSucess, result.Message);
        }

        public override void ShowMsg(bool isSucess, string msg)
        {
            if (isSucess)
            {
                Response.Redirect("StudenyDailyEssayList.aspx");
                return;
            }
            base.ShowMsg(isSucess, msg);
        }
    }
}