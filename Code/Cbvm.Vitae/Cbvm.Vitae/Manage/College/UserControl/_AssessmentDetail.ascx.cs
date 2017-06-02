using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Interface.College;
using Business.Service.College;
using Presentation.Criteria.College;
using Presentation.Enum;
using Presentation.UIView.Family;
using WebLibrary;
using WebLibrary.Helper;
using Presentation.UIView.College;

namespace Cbvm.Vitae.Manage.College.UserControl
{
    public partial class _AssessmentDetail : BaseUserControl
    {
        private int AssessmentId
        {
            get
            {
                if (this.ViewState["AssessmentId"] == null)
                {
                    return 0;
                }

                return (int)this.ViewState["AssessmentId"];

            }
            set
            {
                this.ViewState["AssessmentId"] = value;
            }
        }

        public AssessType SourceAssessType
        {
            get
            {
                if (this.ViewState["SourceAssessType"] == null)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return (AssessType)this.ViewState["SourceAssessType"];

            }
            set
            {
                this.ViewState["SourceAssessType"] = value;
            }
        }

        public string Caption { get; set; }

        private IAssessmentService Service
        {
            get
            {
                return new AssessmentService();
            }
        }

        public string RedirectListPage { get; set; }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var result = this.Service.Save(new AssessmentPresentation
            {
                AssessType = SourceAssessType,
                Title = txt_Title_.Text,
                Description = txt_Content_.SaveData(),
                StartDate = dtp_BeginTime_.SelectedDate,
                EndDate = dtp_EndTime_.SelectedDate,
                StudentNum = txt_StudentNum_.Value,
                LastUpdator = CurrentUser.UserName,
                CollegeCode=CurrentUser.Identity,
                ID=AssessmentId
            });

            if (result.IsSucess)
            {
                Response.Redirect(RedirectListPage);
                return;
            }

            ShowMsg(result.IsSucess, result.Message);
        }


        public void LoadData(int id)
        {
            AssessmentId = id;

            var presentation = this.Service.GetOrigianlQuery<LkDataContext.Assessment>().Where(it => it.ID == id && it.AssessType == (int)SourceAssessType).Select(it => new
            {
                it.ID,
                it.Title,
                it.Description,
                it.StartDate,
                it.EndDate,
                it.StudentNum,
                StudnetName = it.Student.NameZh
            }).FirstOrDefault();

            if (presentation != null)
            {
                this.txt_StudentName_.Text = presentation.StudnetName;
                this.txt_StudentNum_.Value = presentation.StudentNum;
                this.txt_Title_.Text = presentation.Title;
                this.txt_Content_.LoadData(presentation.Description);
                this.dtp_BeginTime_.SelectedDate = presentation.StartDate;
                this.dtp_EndTime_.SelectedDate = presentation.EndDate;
            }
        }
    }
}