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
    public partial class StudentProjectDetail : BaseStudentDetailPage
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
            return AttachmentType.Project;
        }

        private IStudentProjectService Service
        {
            get { return new StudentProjectService(); }
        }

        private StudentProjectPresentation CurrentPorject
        {
            get
            {
                var project = this.ViewState["CurrentPorject"] as StudentProjectPresentation;
                if (project == null)
                {
                    project = Service.Get(new StudentProjectCriteria()
                    {
                        Id = CurrentID,
                        StudentNum = StudentNum,
                        IncludeRelativeData = true
                    });
                    if (project == null)
                    {
                        project = new StudentProjectPresentation()
                        {
                            StudentNum = this.StudentNum
                        };
                    }
                    this.ViewState["CurrentPorject"] = project;
                }
                return project;
            }
        }

        protected override void InitData()
        {
            if (CurrentPorject.Id == 0) return;
            SetEnable(CurrentPorject.VerfyStatus != VerifyStatus.Passed);
            txt_Evaluate_.Text = CurrentPorject.Evaluate;
            txt_Name_.Text = CurrentPorject.Name;
            txt_Position_.Text = CurrentPorject.Position;
            txt_PositionDescrition_.Text = CurrentPorject.PositionDescrition;
            txt_TeamDescription_.Text = CurrentPorject.TeamDescription;
            dtp_BeginTime_.SelectedDate = CurrentPorject.BeginTime;
            dtp_EndTime_.SelectedDate = CurrentPorject.EndTime;
            //drp_TeacherNum_.SelectedValue = CurrentPorject.TeacherNum;
            hdf_TeacherNum_.Value = CurrentPorject.TeacherNum;
            txt_TeacherName_.Text = CurrentPorject.TeacherName;

            ltl_VerifiedStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel(CurrentPorject.VerfyStatus);
            gdcUsableLevel.RadRate = CurrentPorject.UsableLevel;
            gdcSkillLevel.RadRate = CurrentPorject.SkillLevel;
            gdcDreativeLevel.RadRate = CurrentPorject.DreativeLevel;
            txt_Description_.Text = CurrentPorject.Description;
            chk_IsOnline_.Checked = CurrentPorject.IsOnline;

            BindAttachmentList();
            BindCommentList();

        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentPorject.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Any(ic => ic == it.ID)).ToList());
        }

        private void BindCommentList()
        {
            cmtProjectCommentList.LoadData(CurrentPorject.CommentPresentations.ToList());
        }

        protected void grdAttList_AttachmentDeleteCommmandEventHandler(int attachID)
        {
            DeletedAttachmentIDList.Add(attachID);
            BindAttachmentList();
        }

        protected void grdAttList_AttachmentInsertCommmandEventHandler(AttachmentPresentation viewItem)
        {
            int currentAttachmentID = 0;
            if (CurrentPorject.AttachmentPresentations.Any())
            {
                currentAttachmentID = CurrentPorject.AttachmentPresentations.Max(it => it.ID);
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
            CurrentPorject.AttachmentPresentations.Add(attachmentItem);
            BindAttachmentList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GetProjectData();
            SaveData();
        }

        private void SaveData()
        {
            CurrentPorject.AttachmentPresentations =
                CurrentPorject.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Contains(it.ID)).ToList();
            var result = Service.Save(CurrentPorject);
            if (result.IsSucess)
            {
                CurrentPorject.AttachmentPresentations.Where(ic => DeletedAttachmentIDList.Contains(ic.ID)).ToList()
                    .ForEach(it =>
                    {
                        FileHelper.DeleteAttachmentFile(it);
                    });

                Response.Redirect("StudentProjectList.aspx");
                return;
            }
            ShowMsg(result.IsSucess, result.Message);
        }

        private StudentProjectPresentation GetProjectData()
        {
            CurrentPorject.Name = txt_Name_.Text;
            CurrentPorject.Position = txt_Position_.Text;
            CurrentPorject.PositionDescrition = txt_PositionDescrition_.Text;
            CurrentPorject.Evaluate = txt_Evaluate_.Text;
            CurrentPorject.BeginTime = dtp_BeginTime_.SelectedDate.Value;
            CurrentPorject.EndTime = dtp_EndTime_.SelectedDate.Value;
            CurrentPorject.IsOnline = chk_IsOnline_.Checked;
            CurrentPorject.TeamDescription = txt_TeamDescription_.Text;
            CurrentPorject.TeacherNum = hdf_TeacherNum_.Value;
            CurrentPorject.Description = txt_Description_.Text;

            if (CurrentPorject.VerfyStatus != VerifyStatus.Passed)
            {
                CurrentPorject.VerfyStatus = VerifyStatus.WaitAudited;
            }
            return CurrentPorject;
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }


        private void SetEnable(bool isEnable)
        {
            grdAttList.Enable = isEnable;
            txt_Description_.Enabled = isEnable;
            dtp_BeginTime_.Enabled = isEnable;
            dtp_EndTime_.Enabled = isEnable;
            txt_Evaluate_.Enabled = isEnable;
            txt_Name_.Enabled = isEnable;
            txt_Position_.Enabled = isEnable;
            txt_PositionDescrition_.Enabled = isEnable;
            btnSelectTeacher.Visible = isEnable;

            txt_TeamDescription_.Enabled = isEnable;
        }
    }
}