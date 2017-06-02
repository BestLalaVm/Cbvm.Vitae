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
    public partial class ExercitationDetailControl : BaseFrontStudentUserControl
    {
        private IStudentExercitationService Service
        {
            get
            {
                return new StudentExercitationService();
            }
        }

        protected override void InitData()
        {
            var exercitationId = 0;
            if (!int.TryParse(base.KeyCode, out exercitationId))
            {
                this.Visible = false;
                return;
            }
            var activity = Service.GetFrontById(StudentNum, exercitationId);

            phAttachmentList.Visible = false;
            if (phContainer.PresentationEmptyCheck(activity))
            {
                ltlTitle.Text = activity.Title.HtmlEncode();
                ltlContent.Text = activity.Content.HtmlEncode();
                ltlAddress.Text = activity.Address.HtmlEncode();
                ltlTime.Text = string.Format("{0} 到 {1}", activity.BeginTime.ToCustomerShortDateString(),
                    activity.EndTime.ToCustomerShortDateString());
                if (activity.AttachmentPresentations.Any())
                {
                    var index = 0;
                    rptAttachment.DataSource = activity.AttachmentPresentations.Select(it => new
                    {
                        it.ThumbPath,
                        it.Path,
                        Index = index++,
                        FileTip = it.FileLabel
                    }).ToList();
                    rptAttachment.DataBind();
                }
            }
        }
    }
}