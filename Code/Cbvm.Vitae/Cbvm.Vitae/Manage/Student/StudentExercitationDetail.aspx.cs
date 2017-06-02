using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentExercitationDetail : BaseStudentDetailPage
    {
        private IList<int> DeletedAttachmentIDList
        {
            get
            {
                var list = this.ViewState["DeletedAttachmentList"] as IList<int>;
                if (list == null)
                {
                    list = new List<int>();
                    this.ViewState["DeletedAttachmentList"] = list;
                }
                return list;
            }
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Activity;
        }

        private IStudentExercitationService Service
        {
            get
            {
                return new StudentExercitationService();
            }
        }

        private StudentExercitationPresentation CurrentExercitation
        {
            get
            {
                var activity = this.ViewState["CurrentExercitation"] as StudentExercitationPresentation;
                if (activity == null)
                {
                    activity = Service.Get(new StudentExercitationCriteria()
                    {
                        Id = CurrentID,
                        StudentNum = StudentNum,
                        IncludeRelativeData = true
                    });
                    if (activity == null)
                    {
                        activity = new StudentExercitationPresentation()
                        {
                            StudentNum = StudentNum
                        };
                    }
                    this.ViewState["CurrentExercitation"] = activity;
                }
                return activity;
            }
        }

        private StudentExercitationPresentation GetExercitationData()
        {
            CurrentExercitation.Address = txt_Address_.Text;
            if (dtp_BeginTime_.SelectedDate.HasValue)
            {
                CurrentExercitation.BeginTime = dtp_BeginTime_.SelectedDate.Value;
            }

            if (dtp_EndTime_.SelectedDate.HasValue)
            {
                CurrentExercitation.EndTime = dtp_EndTime_.SelectedDate.Value;
            }

            CurrentExercitation.Content = txt_Content_.SaveData();
            CurrentExercitation.IsOnline = chk_IsOnline_.Checked;
            CurrentExercitation.TeacherNum = hdf_TeacherNum_.Value;
            CurrentExercitation.VerifyStatusReason = lbl_VerifyStatusReason_.Text;
            CurrentExercitation.EvaluateFromTeacher = lbl_EvaluateFromTeacher_.Text;
            CurrentExercitation.Title = txt_Title_.Text;

            if (CurrentExercitation.VerfyStatus != VerifyStatus.Passed)
            {
                CurrentExercitation.VerfyStatus = VerifyStatus.WaitAudited;
            }
            return CurrentExercitation;
        }

        protected override void InitData()
        {
            txt_Address_.Text = CurrentExercitation.Address;

            if (!CurrentExercitation.BeginTime.Equals(DateTime.MinValue))
            {
                dtp_BeginTime_.SelectedDate = CurrentExercitation.BeginTime;
            }

            if (!CurrentExercitation.EndTime.Equals(DateTime.MinValue))
            {
                dtp_EndTime_.SelectedDate = CurrentExercitation.EndTime;
            }

            txt_Content_.LoadData(CurrentExercitation.Content);
            chk_IsOnline_.Checked = CurrentExercitation.IsOnline;
            hdf_TeacherNum_.Value = CurrentExercitation.TeacherNum;
            txt_TeacherName_.Text = CurrentExercitation.TeacherName;
            ltl_VerifiedStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel(CurrentExercitation.VerfyStatus);
            lbl_EvaluateFromTeacher_.Text = CurrentExercitation.EvaluateFromTeacher;
            lbl_VerifyStatusReason_.Text = CurrentExercitation.VerifyStatusReason;
            txt_Title_.Text = CurrentExercitation.Title;
            SetEnable((!CurrentExercitation.ReferenceID.HasValue && CurrentExercitation.VerfyStatus != VerifyStatus.Passed));

            BindAttachmentList();
            BindCommentList();
        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentExercitation.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Any(ic => ic == it.ID)).ToList());
        }

        private void BindCommentList()
        {
            cmtActivityList.LoadData(CurrentExercitation.CommentPresentations.ToList());
        }

        protected void grdAttList_AttachmentDeleteCommmandEventHandler(int attachID)
        {
            this.DeletedAttachmentIDList.Add(attachID);
            BindAttachmentList();
        }

        protected void grdAttList_AttachmentInsertCommmandEventHandler(AttachmentPresentation viewItem)
        {
            int currentAttachmentID = 0;
            if (CurrentExercitation.AttachmentPresentations.Any())
            {
                currentAttachmentID = CurrentExercitation.AttachmentPresentations.Max(it => it.ID);
            }
            var attachmentItem = new AttachmentPresentation()
            {
                IsMain = viewItem.IsMain,
                FileLabel = viewItem.FileLabel,
                DisplayOrder = viewItem.DisplayOrder,
                DocumentType = viewItem.DocumentType,
                Path = viewItem.Path,
                Index = viewItem.Index,
                ID = currentAttachmentID + 1,
                IsNew = true
            };
            string thumbPath = viewItem.Path;
            if (viewItem.DocumentType == DocumentType.Image)
            {
                thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType, AttachmentType, viewItem.FileName);
                FileHelper.DrawingUploadFile(viewItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), MaxThumbSize.Width, MaxThumbSize.Height);
            }
            attachmentItem.ThumbPath = thumbPath;
            CurrentExercitation.AttachmentPresentations.Add(attachmentItem);
            BindAttachmentList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GetExercitationData();
            SaveData();
        }

        private void SaveData()
        {
            CurrentExercitation.AttachmentPresentations =
                CurrentExercitation.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Contains(it.ID)).ToList();
            var result = Service.Save(CurrentExercitation);
            if (result.IsSucess)
            {
                CurrentExercitation.AttachmentPresentations.Where(ic => DeletedAttachmentIDList.Contains(ic.ID)).ToList()
                    .ForEach(it =>
                    {
                        FileHelper.DeleteAttachmentFile(it);
                    });

                Response.Redirect("StudentExercitationList.aspx");
                return;
            }

            ShowMsg(result.IsSucess, result.Message);
            InitData();
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }

        private void SetEnable(bool isEnable)
        {
            grdAttList.Enable = isEnable;
            dtp_BeginTime_.Enabled = isEnable;
            dtp_EndTime_.Enabled = isEnable;
            txt_Title_.Enabled = isEnable;
            txt_Address_.Enabled = isEnable;
            txt_Content_.EditorEnabled = isEnable;
            btnSelectTeacher.Visible = isEnable;
        }
    }
}