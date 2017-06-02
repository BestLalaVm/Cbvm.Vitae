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


namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildDailyEssayDetail : BaseFamilyDetailPage
    {
        private IStudentDailyEssayService Service
        {
            get { return new StudentDailyEssayService(); }
        }

        private StudentDailyEssayPresentation _CurrentDailyEssay;
        private StudentDailyEssayPresentation CurrentDailyEssay
        {
            get
            {
                if (_CurrentDailyEssay == null)
                {
                    _CurrentDailyEssay = Service.Get(new StudentDailyEssayCriteria()
                    {
                        Id = CurrentID
                    });
                }
                return _CurrentDailyEssay;
            }
        }

        protected override void InitData()
        {
            _CurrentDailyEssay = null;
            txt_Content_.LoadData(CurrentDailyEssay.Content);
            txt_Title_.Text = CurrentDailyEssay.Title;
            chk_IsOnline_.Checked = CurrentDailyEssay.IsOnline;

            cmtDailyEssayList.LoadData(CurrentDailyEssay.CommentPresentations);
        }
    }
}