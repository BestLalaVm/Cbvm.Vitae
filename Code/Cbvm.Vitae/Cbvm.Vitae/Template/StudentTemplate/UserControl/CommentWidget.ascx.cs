using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service.Student;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class CommentWidget : BaseFrontStudentUserControl
    {
        public AttachmentType CommentType
        {
            get;
            set;
        }

        private ICommentService Service
        {
            get
            {
                switch (CommentType)
                {
                    case AttachmentType.Project:
                        return new StudentProjectCommetService();
                    case AttachmentType.Activity:
                        return new StudentActivityCommentService();
                    case AttachmentType.Exercitation:
                        return new StudentExercitationCommentService();
                    default:
                        return new StudentDailyEssayCommentService();
                }
            }
        }

        private int CommentID
        {
            get
            {
                return int.Parse(KeyCode);
            }
        }

        protected override void InitData()
        {
            rptComment.DataSource = Service.GetAll(new CommentCriteria()
            {
                ReferenceId = CommentID
            });
            rptComment.DataBind();

            this.phContainer.Visible = (this.CurrentUser.UserType != UserType.Guest);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var result = Service.Save(new CommentPresentation
                 {
                     Comment = txtComment.Text.HtmlEncode(),
                     UserName = CurrentUser.UserName,
                     UserType = CurrentUser.UserType,
                     ReferenceID = CommentID
                 });
                if (result.IsSucess)
                {
                    txtComment.Text = "";
                    InitData();
                    result.Message = "评论成功!";
                }
                //ShowMsg(result.IsSucess, result.Message);
            }
        }

    }
}