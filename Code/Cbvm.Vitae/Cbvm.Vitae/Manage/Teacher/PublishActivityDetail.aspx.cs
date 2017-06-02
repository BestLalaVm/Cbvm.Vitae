using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class PublishActivityDetail : BaseTeacherDetailPage
    {
        #region properties
        private IPublishActivityService Service
        {
            get
            {
                return new PublishActivityService();
            }
        }

        private PublishActivityPresentation CurrentActivity
        {
            get
            {
                var _CurrentActivity = this.ViewState["CurrentActivity"] as PublishActivityPresentation;
                if (_CurrentActivity == null)
                {
                    _CurrentActivity = Service.Get(new PublishActivityCriteria()
                    {
                        Id = CurrentID,
                        IncludeRelativeData = true
                    });
                    if (_CurrentActivity == null)
                    {
                        _CurrentActivity = new PublishActivityPresentation()
                        {
                            PublisherType = UserType.Teacher,
                            Publisher = TeacherNum
                        };
                    }
                    this.ViewState["CurrentActivity"] = _CurrentActivity;
                }
                return _CurrentActivity;
            }
        }

        private IList<int> DeletedAttachmentIDList
        {
            get
            {
                var attachmentList = this.ViewState["DeleteAttachmentList"] as List<int>;
                if (attachmentList == null)
                {
                    attachmentList = new List<int>();
                    this.ViewState["DeleteAttachmentList"] = attachmentList;
                }
                return attachmentList;
            }
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Activity;
        }
        #endregion

        protected override void InitBindData()
        {
            drp_ActivityType_.BindSource(BindingSourceType.ActivityTypeInfo, false);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }

        private PublishActivityPresentation GetActivityData()
        {
            CurrentActivity.ActivityType = drp_ActivityType_.SelectedValue;
            CurrentActivity.Address = txt_Address_.Text;
            CurrentActivity.BeginTime = dtp_BeginTime_.SelectedDate.Value;
            CurrentActivity.EndTime = dtp_EndTime_.SelectedDate.Value;
            CurrentActivity.Content = txt_Content_.SaveData();
            CurrentActivity.Title = txt_Title_.Text;
            CurrentActivity.IsOnline = chkIsOnline.Checked;
            return CurrentActivity;
        }

        protected override void InitData()
        {
            if (CurrentActivity != null)
            {
                SetEnavle(!CurrentActivity.IsReferenced);

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
                txt_Title_.Text = CurrentActivity.Title;
                chkIsOnline.Checked = CurrentActivity.IsOnline;
                BindAttachmentList();
            }
        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentActivity.AttachmentPresentations.Where(it => !DeletedAttachmentIDList.Contains(it.ID)).ToList());
        }

        protected void grdAttList_AttachmentDeleteCommmandEventHandler(int attachID)
        {
            DeletedAttachmentIDList.Add(attachID);
            BindAttachmentList();
        }

        protected void grdAttList_AttachmentInsertCommmandEventHandler(AttachmentPresentation viewItem)
        {
            var currentAttachID = 0;
            if (CurrentActivity.AttachmentPresentations.Any())
            {
                currentAttachID = CurrentActivity.AttachmentPresentations.Max(it => it.ID);
            }

            var attachmentItem = new AttachmentPresentation
            {
                IsMain = viewItem.IsMain,
                FileLabel = viewItem.FileLabel,
                DisplayOrder = viewItem.DisplayOrder,
                DocumentType = viewItem.DocumentType,
                Path = viewItem.Path,
                Index = viewItem.Index,
                ID = currentAttachID + 1,
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

                Response.Redirect("PublishActivityList.aspx");
                return;
            }

            InitData();
        }

        private void SetEnavle(bool isEnable)
        {
            drp_ActivityType_.Enabled = isEnable;
            dtp_BeginTime_.Enabled = isEnable;
            dtp_EndTime_.Enabled = isEnable;
            txt_Title_.Enabled = isEnable;
            txt_Content_.EditorEnabled = isEnable;
            txt_Address_.Enabled = isEnable;
            chkIsOnline.Enabled = isEnable;
            grdAttList.Enable = isEnable;
            btnSave.Visible = isEnable;
        }
    }
}