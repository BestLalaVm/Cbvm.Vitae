using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using WebLibrary.Helper;
using Cbvm.Vitae.Front;
using WebLibrary;
using Presentation.Enum;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControlV2
{
    public partial class NewestTop10ProfessionalWidget : BaseFrontStudentUserControl, IShortWidget
    {
        private IStudentProfessionalService Service
        {
            get
            {
                return new StudentProfessionalService();
            }
        }

        protected override void InitData()
        {
            int totalCount;
            var list = Service.GetNewestFrontProfessionalList(StudentInfo.StudentNum, out totalCount);
            if (!list.Any())
            {
                ltlEmptyMessage.Text = HtmlContentHelper.GetEmptyContent();
            }
            else
            {
                rptProfessional.DataSource = list.Select(it => new
                {
                    Name = it.Name.Cut(EnableShortContent, MaxContentLength).HtmlEncode(),
                    Url = UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, it.Identity, it.Name, StudentRulePathType.Professional)
                }).ToList();
                rptProfessional.DataBind();
            }
        }

        public int MaxContentLength
        {
            get
            {
                if (this.ViewState["MaxContentLength"] == null)
                {
                    return 15;
                }
                return (int)this.ViewState["MaxContentLength"];
            }
            set { this.ViewState["MaxContentLength"] = value; }
        }

        public bool EnableShortContent
        {
            get
            {
                if (this.ViewState["EnableShortContent"] == null)
                {
                    return true;
                }
                return (bool)this.ViewState["EnableShortContent"];
            }
            set { this.ViewState["EnableShortContent"] = value; }
        }
    }
}