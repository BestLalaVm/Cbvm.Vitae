using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentActivityDetail : BaseStudentDetailPage
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

        private IStudentActivityService Service
        {
            get
            {
                return new StudentActivityService();
            }
        }

        private StudentActivityPresentation CurrentActivity
        {
            get
            {
                var activity = this.ViewState["CurrentActivity"] as StudentActivityPresentation;
                if (activity == null)
                {
                    activity = Service.Get(new StudentActivityCriteria()
                    {
                        IncludeRelativeData = true,
                        Id = CurrentID,
                        StudentNum = StudentNum
                    });
                    if (activity == null)
                    {
                        activity = new StudentActivityPresentation()
                        {
                            StudentNum = StudentNum
                        };
                    }
                    this.ViewState["CurrentActivity"] = activity;
                }
                return activity;
            }
        }

        protected override void InitBindData()
        {
            drp_ActivityType_.BindSource(BindingSourceType.ActivityTypeInfo, false);
        }

        private StudentActivityPresentation GetActivityData()
        {
            CurrentActivity.ActivityType = drp_ActivityType_.SelectedValue;
            CurrentActivity.Address = txt_Address_.Text;
            CurrentActivity.BeginTime = dtp_BeginTime_.SelectedDate.Value;
            CurrentActivity.EndTime = dtp_EndTime_.SelectedDate.Value;
            CurrentActivity.Content = txt_Content_.SaveData();
            CurrentActivity.IsOnline = chk_IsOnline_.Checked;
            CurrentActivity.TeacherNum = hdf_TeacherNum_.Value;
            CurrentActivity.VerifyStatusReason = lbl_VerifyStatusReason_.Text;
            CurrentActivity.EvaluateFromTeacher = lbl_EvaluateFromTeacher_.Text;
            CurrentActivity.Title = txt_Title_.Text;

            if (CurrentActivity.VerfyStatus != VerifyStatus.Passed)
            {
                CurrentActivity.VerfyStatus = (int)VerifyStatus.WaitAudited;
            }
            return CurrentActivity;
        }

        protected override void InitData()
        {
            if (CurrentActivity == null) return;
            txt_Address_.Text = CurrentActivity.Address;
            drp_ActivityType_.SelectedValue = CurrentActivity.ActivityType;
            txt_Address_.Text = CurrentActivity.Address;
            if (!CurrentActivity.BeginTime.Equals(DateTime.MinValue))
            {
                dtp_BeginTime_.SelectedDate = CurrentActivity.BeginTime;
            }

            if (!CurrentActivity.EndTime.Equals(DateTime.MinValue))
            {
                dtp_EndTime_.SelectedDate = CurrentActivity.EndTime;
            }

            txt_Content_.LoadData(CurrentActivity.Content);
            chk_IsOnline_.Checked = CurrentActivity.IsOnline;
            hdf_TeacherNum_.Value = CurrentActivity.TeacherNum;
            txt_TeacherName_.Text = CurrentActivity.TeacherName;
            ltl_VerifiedStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel((VerifyStatus)CurrentActivity.VerfyStatus);
            lbl_EvaluateFromTeacher_.Text = CurrentActivity.EvaluateFromTeacher;
            lbl_VerifyStatusReason_.Text = CurrentActivity.VerifyStatusReason;
            txt_Title_.Text = CurrentActivity.Title;
            SetEnable((!CurrentActivity.ReferenceID.HasValue && CurrentActivity.VerfyStatus != VerifyStatus.Passed));

            BindAttachmentList();
            BindCommentList();
        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentActivity.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Any(ic => ic == it.ID)).ToList());
        }

        private void BindCommentList()
        {
            cmtActivityList.LoadData(CurrentActivity.CommentPresentations.ToList());
        }

        protected void grdAttList_AttachmentDeleteCommmandEventHandler(int attachID)
        {
            this.DeletedAttachmentIDList.Add(attachID);
            BindAttachmentList();
        }

        protected void grdAttList_AttachmentInsertCommmandEventHandler(AttachmentPresentation viewItem)
        {
            int currentAttachmentID = 0;
            if (CurrentActivity.AttachmentPresentations.Any())
            {
                currentAttachmentID = CurrentActivity.AttachmentPresentations.Max(it => it.ID);
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
            CurrentActivity.AttachmentPresentations.Add(attachmentItem);
            BindAttachmentList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GetActivityData();
            SaveData();
        }

        private void SaveData()
        {
            CurrentActivity.AttachmentPresentations =
                CurrentActivity.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Contains(it.ID)).ToList();
            var result = Service.Save(CurrentActivity);
            if (result.IsSucess)
            {
                CurrentActivity.AttachmentPresentations.Where(ic => DeletedAttachmentIDList.Contains(ic.ID)).ToList()
                    .ForEach(it =>
                    {
                        FileHelper.DeleteAttachmentFile(it);
                    });

                Response.Redirect("StudentActivityList.aspx");
                return;
            }

            ShowMsg(result.IsSucess, result.Message);
            InitData();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }

        private void SetEnable(bool isEnable)
        {
            grdAttList.Enable = isEnable;
            drp_ActivityType_.Enabled = isEnable;
            dtp_BeginTime_.Enabled = isEnable;
            dtp_EndTime_.Enabled = isEnable;
            txt_Title_.Enabled = isEnable;
            txt_Title_.ReadOnly = !isEnable;
            txt_Address_.ReadOnly = !isEnable;
            txt_Address_.Enabled = isEnable;
            txt_Content_.EditorEnabled = isEnable;
            btnSelectTeacher.Visible = isEnable;
        }
    }
}