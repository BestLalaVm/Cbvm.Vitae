using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView.Student;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildProjectDetail : BaseFamilyDetailPage
    {
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
                        Id = CurrentID
                    });
                    this.ViewState["CurrentPorject"] = project;
                }
                return project;
            }
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Project;
        }

        protected override void InitData()
        {
            txt_Evaluate_.Text = CurrentPorject.Evaluate;
            txt_Name_.Text = CurrentPorject.Name;
            txt_Position_.Text = CurrentPorject.Position;
            txt_PositionDescrition_.Text = CurrentPorject.PositionDescrition;
            txt_TeamDescription_.Text = CurrentPorject.TeamDescription;
            txt_BeginTime_.Text = CurrentPorject.BeginTime.ToCustomerShortDateString();
            txt_EndTime_.Text = CurrentPorject.EndTime.ToCustomerShortDateString();
            ltl_VerifiedStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel(CurrentPorject.VerfyStatus);
            gdcUsableLevel.RadRate = CurrentPorject.UsableLevel;
            gdcSkillLevel.RadRate = CurrentPorject.SkillLevel;
            gdcDreativeLevel.RadRate = CurrentPorject.DreativeLevel;
            txt_Description_.Text = CurrentPorject.Description;
            chk_IsOnline_.Checked = CurrentPorject.IsOnline;
            ltl_TeacherNum_.Text = GlobalBaseDataCache.GetTeacherName(CurrentPorject.TeacherNum);

            BindAttachmentList();
            BindCommentList();

        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentPorject.AttachmentPresentations);
        }

        private void BindCommentList()
        {
            cmtProjectCommentList.LoadData(CurrentPorject.CommentPresentations);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }
    }
}