using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service.Student;
using LkHelper;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class DailyEssayList : BaseFrontStudentUserControl
    {
        protected override void InitData()
        {
            Search(null);
        }

        private void Search(string keyword)
        {
            var server = new StudentDailyEssayService();
            var list = server.GetFrontDailyEssayList(StudentInfo.StudentNum, keyword);
            if (!list.Any())
            {
                //this.Visible = false;
            }
            else
            {
                rptDailyEssay.DataSource = list.Select(it => new
                    {
                        Title = it.Name.Cut(40, "...").HtmlEncode(),
                        Url =
                                                                 UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum,
                                                                                           it.Identity,
                                                                                           it.Name.Cut(200, ""),
                                                                                           StudentRulePathType
                                                                                               .DailyEssay)
                    }).ToList();
                rptDailyEssay.DataBind();
            }
        }
        protected void CommonSearchWidget_SearchClicked(string keyword)
        {
            Search(keyword);
        }
    }
}