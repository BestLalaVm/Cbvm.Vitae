using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Presentation.Criteria.Student;

using Presentation.Enum;
using Presentation.UIView.Student;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class StudentProjectVerifyDetail : BaseTeacherDetailPage
    {
        private IStudentProjectService Service
        {
            get
            {
                return new StudentProjectService();
            }
        }

        protected StudentProjectPresentation CurrentProject
        {
            get
            {
                var project = this.ViewState["CurrentProject"] as StudentProjectPresentation;
                if (project == null)
                {

                    project = Service.Get(new StudentProjectCriteria()
                    {
                        Id = CurrentID,
                        IncludeRelativeData = true
                    });
                    this.ViewState["CurrentProject"] = project;
                }
                return project;
            }
        }

        protected override void InitData()
        {
            base.InitData();
            if (CurrentProject == null)
            {
                Response.Redirect("StudentProjectVerifyList.aspx"); return;
            }
            txt_Name_.Text = CurrentProject.Name;
            txt_EvaluateFromTeacher_.Text = CurrentProject.EvaluateFromTeacher;
            txt_VerifyStatusReason_.Text = CurrentProject.VerifyStatusReason;
            txtBeginTime.Text = CurrentProject.BeginTime.ToCustomerShortDateString();
            txtEndTime.Text = CurrentProject.EndTime.ToCustomerShortDateString();

            ltl_Description_.Text = CurrentProject.Description;
            ltl_Evaluate_.Text = CurrentProject.Evaluate;
            ltl_Position_.Text = CurrentProject.Position;
            ltl_PositionDescrition_.Text = CurrentProject.PositionDescrition;
            ltl_TeamDescription_.Text = CurrentProject.TeamDescription;
            gdcDreativeLevel.RadRate = CurrentProject.DreativeLevel;
            gdcSkillLevel.RadRate = CurrentProject.SkillLevel;
            gdcUsableLevel.RadRate = CurrentProject.UsableLevel;
            rdoVerify.SelectedValue = (CurrentProject.VerfyStatus == VerifyStatus.Passed ? "2" : "1");
            attachmentList.LoadData(CurrentProject.AttachmentPresentations);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var evaluateFromTeacher = txt_EvaluateFromTeacher_.Text;
            var verfyStatus = (VerifyStatus) (int.Parse(rdoVerify.SelectedValue));
            var verifyStatusReason = txt_VerifyStatusReason_.Text;
            var skillLevel = (int) gdcSkillLevel.RadRate;
            var dreativeLevel = (int) gdcDreativeLevel.RadRate;
            var usableLevel = (int) gdcUsableLevel.RadRate;
            var result = Service.ChangeVerifyStatus(CurrentID, verfyStatus, verifyStatusReason, evaluateFromTeacher,
                skillLevel, dreativeLevel, usableLevel, TeacherNum);
            if (result.IsSucess)
            {
                Response.Redirect("StudentProjectVerifyList.aspx");
            }
            else
            {
                ShowMsg(result.IsSucess, result.Message);
            }
        }
    }
}