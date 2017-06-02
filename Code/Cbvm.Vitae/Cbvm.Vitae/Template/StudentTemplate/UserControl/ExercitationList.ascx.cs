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
    public partial class ExercitationList : BaseFrontStudentUserControl
    {
        protected override void InitData()
        {
            Search(null);
        }

        private void Search(string keyword)
        {
            var server = new StudentExercitationService();
            var list = server.GetFrontExercitationList(StudentInfo.StudentNum, keyword);
            if (!list.Any())
            {
                //this.Visible = false;
            }
            else
            {
                rptActivity.DataSource = list.Select(it => new
                {
                    Name = it.Name.HtmlEncode(),
                    Url =
                        UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, it.Identity, it.Name.Cut(200, ""),
                            StudentRulePathType.Exercitation)
                }).ToList();
                rptActivity.DataBind();
            }
        }

        protected void CommonSearchWidget_SearchClicked(string keyword)
        {
            Search(keyword);
        }
    }
}