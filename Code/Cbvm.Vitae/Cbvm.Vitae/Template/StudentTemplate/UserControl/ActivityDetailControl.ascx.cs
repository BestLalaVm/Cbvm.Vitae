using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Telerik.Web.UI.GridExcelBuilder;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class ActivityDetailControl : BaseFrontStudentUserControl
    {
        private IStudentActivityService Service
        {
            get
            {
                return new StudentActivityService();
            }
        }

        protected override void InitData()
        {
            var activityID = 0;
            if (!int.TryParse(base.KeyCode, out activityID))
            {
                this.Visible = false;
                return;
            }
            var activity = Service.GetFrontActivityById(activityID, base.StudentNum);
            phAttachmentList.Visible = false;
            if (phContainer.PresentationEmptyCheck(activity))
            {
                ltlTitle.Text = activity.Title.HtmlEncode();
                ltlContent.Text = activity.Content;//.HtmlEncode();
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