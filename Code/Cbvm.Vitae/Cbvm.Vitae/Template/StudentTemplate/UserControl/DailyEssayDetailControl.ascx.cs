using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class DailyEssayDetailControl : BaseFrontStudentUserControl
    {
        private IStudentDailyEssayService Service
        {
            get
            {
                return new StudentDailyEssayService();
            }
        }

        protected override void InitData()
        {
            base.InitData();

            var dailyEssayID = 0;
            if (!int.TryParse(base.KeyCode, out dailyEssayID))
            {
                this.Visible = false;
                return;
            }
            var dailyEssay = Service.GetFrontDailyEssayById(dailyEssayID, base.StudentNum);
            if (phContainer.PresentationEmptyCheck(dailyEssay))
            {
                ltlTitle.Text = dailyEssay.Title.HtmlEncode();
                ltlContent.Text = dailyEssay.Content;
                ltlTime.Text = string.Format("<span class='label'>发表时间</span>: {0}",
                    dailyEssay.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }
    }
}