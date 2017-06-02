using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView.Student;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildExercitationDetail : BaseFamilyDetailPage
    {
        private IStudentExercitationService Service
        {
            get { return new StudentExercitationService(); }
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Activity;
        }

        private StudentExercitationPresentation CurrentActivity
        {
            get
            {
                var activity = this.ViewState["CurrentActivity"] as StudentExercitationPresentation;
                if (activity == null)
                {
                    activity = Service.Get(new StudentExercitationCriteria()
                    {
                        Id = CurrentID
                    });
                    this.ViewState["CurrentActivity"] = activity;
                }
                return activity;
            }
        }

        protected override void InitData()
        {
            if (CurrentActivity == null) return;
            txt_Address_.Text = CurrentActivity.Address;
            txt_BeginTime_.Text = CurrentActivity.BeginTime.ToCustomerShortDateString();
            txt_EndTime_.Text = CurrentActivity.EndTime.ToCustomerShortDateString();
            txt_Content_.LoadData(CurrentActivity.Content);
            chk_IsOnline_.Checked = CurrentActivity.IsOnline;

            ltl_TeacherNum_.Text = GlobalBaseDataCache.GetTeacherName(CurrentActivity.TeacherNum);

            ltl_VerifiedStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel(CurrentActivity.VerfyStatus);
            lbl_EvaluateFromTeacher_.Text = CurrentActivity.EvaluateFromTeacher;
            lbl_VerifyStatusReason_.Text = CurrentActivity.VerifyStatusReason;
            txt_Title_.Text = CurrentActivity.Title;

            BindAttachmentList();
            BindCommentList();
        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentActivity.AttachmentPresentations);
        }

        private void BindCommentList()
        {
            cmtActivityList.LoadData(CurrentActivity.CommentPresentations);
        }
    }
}