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


namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class ProjectDetailControl : BaseFrontStudentUserControl
    {
        private IStudentProjectService Service
        {
            get
            {
                return new StudentProjectService();
            }
        }

        protected override void InitData()
        {
            var projectID = 0;
            if (!int.TryParse(base.KeyCode, out projectID))
            {
                this.Visible = false;
                return;
            }
            phAttachmentList.Visible = false;
            var project = Service.GetFrontProjctById(projectID, base.StudentNum);
            if (phContainer.PresentationEmptyCheck(project))
            {
                ltlName.Text = project.Name.HtmlEncode();
                ltlPosition.Text = project.Position;
                ltlTime.Text = string.Format("{0} - {1}", project.BeginTime.ToCustomerShortDateString(),
                    project.EndTime.ToCustomerShortDateString());
                ltlPositionDescription.Text = project.PositionDescrition.HtmlEncode();
                ltlEvaluate.Text = project.Evaluate.HtmlEncode();
                ltlTeamDescription.Text = project.TeamDescription.HtmlEncode();
                ltlEvaluateFromTeacher.Text = project.EvaluateFromTeacher.HtmlEncode();
                gradeUsable.RadRate = project.UsableLevel;
                gradeSkill.RadRate = project.SkillLevel;
                gradeDreative.RadRate = project.DreativeLevel;
                ltlDescription.Text = project.Description.HtmlEncode();

                var index = 0;
                if (project.AttachmentPresentations.Any())
                {
                    rptAttachment.DataSource = project.AttachmentPresentations.Select(it => new
                    {
                        ThumbPath = FileHelper.GetImagePathByDocumentType(it.DocumentType, it.ThumbPath),
                        it.Path,
                        Index = index++,
                        FileTip = it.FileLabel
                    }).ToList();
                    rptAttachment.DataBind();
                }
            }
            base.InitData();
        }
    }
}