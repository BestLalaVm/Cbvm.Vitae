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
using Presentation.Enum;
using Presentation.UIView.Student;
using Presentation.UIView.Student.View;
using WebLibrary.Helper;

namespace Cbvm.Vitae.UserControl
{
    public partial class StudentResumeControl : System.Web.UI.UserControl
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
            }
        }

        private StudentFrontPresentation _StudentInfo;
        private StudentFrontPresentation StudentInfo
        {
            get
            {
                if (_StudentInfo == null)
                {
                    var studentNum = Request.QueryString["StudentNum"];
                    if (string.IsNullOrEmpty(studentNum))
                    {
                        RedirectToDefaultPage();
                    }

                    _StudentInfo = Service.GetFront(studentNum);
                    if (_StudentInfo == null)
                    {
                        RedirectToDefaultPage();
                    }
                }
                return _StudentInfo;
            }
        }

        private void RedirectToDefaultPage()
        {
            Response.Redirect("~/Template/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            ltl_NameZh_.Text = StudentInfo.NameZh;
            ltl_NameEn_.Text = StudentInfo.NameEn;
            if (StudentInfo.Birthday.HasValue)
            {
                ltl_Birthday_.Text = StudentInfo.Birthday.Value.ToString("yyyy-MM-dd");
            }
            ltl_IDentityNum_.Text = StudentInfo.IDentityNum;
            //ltl_DepartName_.Text = GlobalBaseDataCache.GetDepartName(StudentInfo.DepartCode);
            ltl_MarjorName_.Text = GlobalBaseDataCache.GetMarjorName(StudentInfo.MarjorCode);
            ltl_Politics_.Text = GlobalBaseDataCache.GetPoliticsLabel(StudentInfo.Politics);
            ltl_Marriage_.Text = GlobalBaseDataCache.GetMarriedLabel(StudentInfo.IsMarried);
            ltl_Sex_.Text = GlobalBaseDataCache.GetSexLabel(StudentInfo.Sex);
            ltl_NativePlace_.Text = StudentInfo.NativePlace;
                ltl_Tall_.Text = StudentInfo.Tall;
            ltl_Interested_.Text = StudentInfo.Interested;
            ltl_Activity_.Text = StudentInfo.Activity;
            ltl_Music_.Text = StudentInfo.Music;
            ltl_Movie_.Text = StudentInfo.Movie;
            ltl_Program_.Text = StudentInfo.Program;
            ltl_Book_.Text = StudentInfo.Book;
            imgPhoto.ImageUrl = FileHelper.GetPersonAbsoluatePath(StudentInfo.Sex, StudentInfo.Photo, false); 
            ltlDescription.Text = StudentInfo.Description;

            BindProfessionalData();
            BindProjectData();
            BindActivityData();
            BindExercitationData();
        }

        private void BindProfessionalData()
        {
            var professionalServer = new StudentProfessionalService();
            rptProfessional.DataSource = professionalServer.GetFrontResumeProfessionalList(StudentInfo.StudentNum, null).Select(it => new
            {
                it.LastUpdateTime,
                it.Description,
                it.Id,
                it.Index,
                it.Name,
                ObtainTime = it.ObtainTime.ToCustomerShortDateString(),
                it.StudentNum,
                AttachmentList = it.AttachmentPresentations.Select(ic => new
                {
                    Description = ic.FileLabel,
                    ic.DisplayOrder,
                    ic.ID,
                    ic.Index,
                    ic.IsMain,
                    ic.Path,
                    ThumbPath = FileHelper.GetImagePathByDocumentType(ic.DocumentType, ic.ThumbPath)
                }).ToList()
            });
            rptProfessional.DataBind();
        }

        private void BindProjectData()
        {
            var projectServer = new StudentProjectService();
            rptProjet.DataSource = projectServer.GetResumeProjectList(StudentInfo.StudentNum, "").Select(it => new
            {
                BeginTime = it.BeginTime.ToString("yyyy-MM-dd"),
                it.Description,

                EndTime = it.EndTime.ToString("yyyy-MM-dd"),
                it.Evaluate,
                it.EvaluateFromTeacher,
                it.Id,
                it.Index,
                it.IsOnline,
                it.Name,
                it.Position,
                it.PositionDescrition,
                
                it.StudentNum,
                it.TeacherNum,
                it.TeamDescription,
                UsableLevel = (double)it.UsableLevel,
                SkillLevel = (double)it.SkillLevel,
                DreativeLevel = (double)it.DreativeLevel,
                it.VerifyStatusReason,
                it.VerfyStatus,
                AttachmentList = it.AttachmentPresentations.Select(ic => new
                {
                    Description = ic.FileLabel,
                    ic.DisplayOrder,
                    ic.ID,
                    ic.Index,
                    ic.IsMain,
                    ic.Path,
                    ThumbPath = FileHelper.GetImagePathByDocumentType(ic.DocumentType, ic.ThumbPath)
                }).ToList()
            });
            rptProjet.DataBind();
        }

        private void BindActivityData()
        {
            var activityServer = new StudentActivityService();
            rptActivity.DataSource = activityServer.GetResumeActivityList(StudentInfo.StudentNum, "").Select(it => new { 
              it.ActivityType,
              it.Address,
              BeginTime = it.BeginTime.ToString("yyyy-MM-dd hh:mm"),
              it.Content,
              EndTime = it.EndTime.ToString("yyyy-MM-dd hh:mm"),
              it.EvaluateFromTeacher,
              it.Id,
              it.IsOnline,
              it.StudentNum,
              it.TeacherNum,
              it.Title,
              it.VerfyStatus,
              it.VerifyStatusReason,
              AttachmentList = it.AttachmentPresentations.Select(ic => new
              {
                  Description = ic.FileLabel,
                  ic.DisplayOrder,
                  ic.ID,
                  ic.Index,
                  ic.IsMain,
                  ic.Path,
                  ThumbPath = FileHelper.GetImagePathByDocumentType(ic.DocumentType, ic.ThumbPath)
              }).ToList()            
            });
            rptActivity.DataBind();
        }

        private void BindExercitationData()
        {
            var activityServer = new StudentExercitationService();
            rptExercitation.DataSource = activityServer.GetFrontResumeExercitationList(StudentInfo.StudentNum, "").Select(it => new
            {
                it.ActivityType,
                it.Address,
                BeginTime = it.BeginTime.ToString("yyyy-MM-dd hh:mm"),
                it.Content,
                EndTime = it.EndTime.ToString("yyyy-MM-dd hh:mm"),
                it.EvaluateFromTeacher,
                it.Id,
                it.IsOnline,
                it.StudentNum,
                it.TeacherNum,
                it.Title,
                it.VerfyStatus,
                it.VerifyStatusReason,
                AttachmentList = it.AttachmentPresentations.Select(ic => new
                {
                    Description = ic.FileLabel,
                    ic.DisplayOrder,
                    ic.ID,
                    ic.Index,
                    ic.IsMain,
                    ic.Path,
                    ThumbPath = FileHelper.GetImagePathByDocumentType(ic.DocumentType, ic.ThumbPath)
                }).ToList()
            });
            rptExercitation.DataBind();
        }
    }
}