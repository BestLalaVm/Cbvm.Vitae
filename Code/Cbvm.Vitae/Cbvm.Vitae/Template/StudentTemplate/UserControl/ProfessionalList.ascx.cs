using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service.Student;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class ProfessionalList : BaseFrontStudentUserControl
    {
        protected override void InitData()
        {
            Search(null);
        }
        private void Search(string keyword)
        {
            var server = new StudentProfessionalService();
            var list = server.GetFrontProfessionalList(StudentInfo.StudentNum,keyword);
            if (!list.Any())
            {
                //this.Visible = false;
            }
            else
            {
                rptProfessional.DataSource = list.Select(it => new
                {
                    Name = it.Name.HtmlEncode(),
                    Url =
                        UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, it.Identity, it.Name,
                            StudentRulePathType.Professional)
                }).ToList();
                rptProfessional.DataBind();
            }
        }

        protected void CommonSearchWidget_SearchClicked(string keyword)
        {
            Search(keyword);
        }
    }
}