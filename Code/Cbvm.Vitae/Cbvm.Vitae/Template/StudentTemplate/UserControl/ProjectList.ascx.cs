using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service.Student;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class ProjectList : BaseFrontStudentUserControl
    {
        private void Search(string keyword)
        {
            var server = new StudentProjectService();
            var list = server.GetFrontProjectList(StudentInfo.StudentNum,keyword);
            if (!list.Any())
            {
                //this.Visible = false;
            }
            else
            {
                rptProject.DataSource = list.Select(it => new
                {
                    Name=it.Name.HtmlEncode(),
                    Url = UrlRuleHelper.GenerateUrl(it.ReferenceCode, it.Identity, it.Name, StudentRulePathType.Project)
                }).ToList();
                rptProject.DataBind();
            }
        }

        protected override void InitData()
        {
            Search(null);
        }

        protected void CommonSearchWidget_SearchClicked(string keyword)
        {
            Search(keyword);
        }
    }
}