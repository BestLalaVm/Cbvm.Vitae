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
    public partial class ProfessionalDetailControl : BaseFrontStudentUserControl
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
            var professionalId = 0;
            if (!int.TryParse(base.KeyCode, out professionalId))
            {
                this.Visible = false;
                return;
            }


            phAttachmentList.Visible = false;
            var professional = Service.GetFrontProfessionalById(professionalId, StudentNum);
            if (phContainer.PresentationEmptyCheck(professional))
            {

                ltlName.Text = professional.Name.HtmlEncode();
                ltlDescription.Text = professional.Description.HtmlEncode();
                ltlObtainTime.Text = professional.ObtainTime.ToCustomerShortDateString();
                var index = 0;
                if (professional.AttachmentPresentations.Any())
                {
                    rptAttachment.DataSource = professional.AttachmentPresentations.Select(it => new
                    {
                        it.ThumbPath,
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