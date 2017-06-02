using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using WebLibrary;
using WebLibrary.Helper;
using Presentation.Cache;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class StudentProfessionalDetail : BaseTeacherDetailPage
    {
        #region property
        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Professional;
        }

        public ProfessionalType SourceType
        {
            get
            {
                ProfessionalType type;

                Enum.TryParse(Request.QueryString["SourceType"], out type);

                return type;
            }
        }

        public string NameTitle
        {
            get
            {
                if (SourceType == ProfessionalType.Skill)
                {
                    return "证书";
                }
                else
                {
                    return "奖励";
                }
            }
        }

        private StudentProfessionalPresentation CurrentProfessional
        {
            get
            {
                var _CurrentProfessional = this.ViewState["CurrentProfessional"] as StudentProfessionalPresentation;
                if (_CurrentProfessional == null)
                {
                    _CurrentProfessional = Service.Get(new StudentProfessionalCriteria()
                    {
                        Id = CurrentID,
                        TeacherNum = TeacherNum,
                        IncludeRelativeData = true,
                        Type = SourceType
                    });

                    this.ViewState["CurrentProfessional"] = _CurrentProfessional;
                }
                return _CurrentProfessional;
            }
        }
        #endregion

        private IStudentProfessionalService Service
        {
            get { return new StudentProfessionalService(); }
        }

        protected override void InitData()
        {
            if (CurrentProfessional == null)
            {
                Response.Redirect("StudentProfessionalList.aspx", true); return;
            }

            radTabs.Tabs.FirstOrDefault().Text = NameTitle + "信息";            

            txt_Description_.Text = (CurrentProfessional.Description);
            txt_Name_.Text = CurrentProfessional.Name;
            if (CurrentProfessional.ObtainTime != DateTime.MinValue)
            {
                dtp_ObtainTime_.SelectedDate = CurrentProfessional.ObtainTime;
            }
            chk_IsOnline_.Checked = CurrentProfessional.IsOnline;
            txt_EvaluateFromTeacher_.Text = CurrentProfessional.EvaluateFromTeacher;
            txt_VerifyStatusReason_.Text = CurrentProfessional.VerifyStatusReason;
            rdoVerify.SelectedValue = (CurrentProfessional.VerfyStatus==VerifyStatus.Passed?"2":"1");

            BindAttachmentList();
        }

        private void BindAttachmentList()
        {
            this.attachmentList.LoadData(CurrentProfessional.AttachmentPresentations.ToList());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            var result = Service.ChangeVerifyStatus(CurrentID, (VerifyStatus)(int.Parse(rdoVerify.SelectedValue)),SourceType,
                txt_VerifyStatusReason_.Text, txt_EvaluateFromTeacher_.Text, CurrentUser.UserName);
            if (result.IsSucess)
            {
                Response.Redirect("StudentProfessionalList.aspx");
            }
            else
            {
                ShowMsg(result.IsSucess, result.Message);
            }

            InitData();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }
    }
}