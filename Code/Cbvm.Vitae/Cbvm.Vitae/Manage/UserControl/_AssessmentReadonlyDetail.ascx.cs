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

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class _AssessmentReadonlyDetail : BaseUserControl
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
                this.txt_Title_.Text = presentation.Title;
                this.ltl_Description_.Text = presentation.Description;
                this.dtp_BeginTime_.SelectedDate = presentation.StartDate;
                this.dtp_EndTime_.SelectedDate = presentation.EndDate;
            }
        }
    }
}