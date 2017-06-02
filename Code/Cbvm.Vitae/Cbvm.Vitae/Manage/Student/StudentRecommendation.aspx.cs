using System;
using System.IO;
using System.Data.Linq;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation;
using Presentation.Cache;
using Presentation.Enum;
using WebLibrary.Helper;
using Presentation.Criteria;
using Presentation.UIView;
using System.Collections.Generic;
using Codaxy.WkHtmlToPdf;
using WebLibrary.Helper;
using WebLibrary.Extensions;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentRecommendation : BaseStudentDetailPage
    {
        protected UploadFileItemPresentation Filedata
        {
            get { return this.ViewState["Filedata"] as UploadFileItemPresentation; }
            set { this.ViewState["Filedata"] = value; }
        }

        protected override void InitData()
        {
            var data = DataContext.Student.Where(it => it.StudentNum == StudentNum).Select(it => new
            {
                it.StudentNum,
                it.Sex,
                it.Birthday,
                it.Telephone,
                it.Email,
                it.IDentityNum,
                it.Politics,
                it.NameZh,
                CollegeName = it.College.Name,
                MarjorName = it.Marjor.Name,
                Recommendation = new
                {
                    ComputerLevel = it.StudentRecommendation.ComputerLevel,
                    FromArea = it.StudentRecommendation.FromArea,
                    Health = it.StudentRecommendation.Health,
                    HomeAddress = it.StudentRecommendation.HomeAddress,
                    HomePhone = it.StudentRecommendation.HomePhone,
                    InnovativePracticeExperience = it.StudentRecommendation.InnovativePracticeExperience,
                    LanguageLevel = it.StudentRecommendation.LanguageLevel,
                    PostCode = it.StudentRecommendation.PostCode,
                    PunishmentDesc = it.StudentRecommendation.PunishmentDesc,
                    RecommendImage = it.StudentRecommendation.RecommendImage,
                    ScientificResearchProject = it.StudentRecommendation.ScientificResearchProject,
                    SecondDegree = it.StudentRecommendation.SecondDegree,
                    SelfEvaluation = it.StudentRecommendation.SelfEvaluation,
                    SocialWorkPosition = it.StudentRecommendation.SocialWorkPosition,
                    Specialty = it.StudentRecommendation.Specialty,
                    TrainingMode = it.StudentRecommendation.TrainingMode,
                    //ToVerify = it.StudentRecommendation.ToVerify,
                    //VerifyStatus = it.StudentRecommendation.VerifyStatus,
                    //VerifyComment = it.StudentRecommendation.VerifyComment,
                    GraduateYear = it.StudentRecommendation.GraduateYear
                }
            }).FirstOrDefault();

            txt_StudentName_.Text = data.NameZh;
            txt_Sex_.Text = GlobalBaseDataCache.GetSexLabel(data.Sex);
            txt_Birthday_.Text = data.Birthday.HasValue ? String.Format("{0}年{1}月{2}日", data.Birthday.Value.Year, data.Birthday.Value.Month.ToString().PadLeft(2, '0'), data.Birthday.Value.Day.ToString().PadLeft(2, '0')) : "";
            txt_Telephone_.Text = data.Telephone;
            txt_Email_.Text = data.Email;
            txt_Identity_.Text = data.IDentityNum;
            txt_Politics_.Text = GlobalBaseDataCache.GetPoliticsLabel((PoliticsType)data.Politics);
            txt_CollegeName_.Text = data.CollegeName;
            txt_MarjorName_.Text = data.MarjorName;

            txt_ComputerLevel_.Text = data.Recommendation.ComputerLevel;
            txt_FromArea_.Text = data.Recommendation.FromArea;
            txt_Health_.Text = data.Recommendation.Health;
            txt_HomeAddress_.Text = data.Recommendation.HomeAddress;
            txt_HomePhone_.Text = data.Recommendation.HomePhone;
            txt_InnovativePracticeExperience_.Text = data.Recommendation.InnovativePracticeExperience;
            txt_LanguageLevel_.Text = data.Recommendation.LanguageLevel;
            txt_PostCode_.Text = data.Recommendation.PostCode;
            txt_PunishmentDesc_.Text = data.Recommendation.PunishmentDesc;
            txt_ScientificResearchProject_.Text = data.Recommendation.ScientificResearchProject;
            txt_SecondDegree_.Text = data.Recommendation.SecondDegree;
            txt_SelfEvaluation_.Text = data.Recommendation.SelfEvaluation;
            txt_SocialWorkPosition_.Text = data.Recommendation.SocialWorkPosition;
            txt_Specialty_.Text = data.Recommendation.Specialty;
            txt_TrainingMode_.Text = data.Recommendation.TrainingMode;
            txt_GraduateYear_.Text = data.Recommendation.GraduateYear;
            if (String.IsNullOrEmpty(txt_GraduateYear_.Text))
            {
                txt_GraduateYear_.Text = DateTime.Now.AddYears(1).Year.ToString();
            }

            //chk_ToVerify_.Checked = data.Recommendation.ToVerify;
            //txt_VerifyStatus_.Text = ((VerifyStatus)data.Recommendation.VerifyStatus) == VerifyStatus.WaitAudited ? "等待审核" : (((VerifyStatus)data.Recommendation.VerifyStatus) == VerifyStatus.UnPassed ? "审核未通过" : "审核通过");
            //txt_VerifyComment_.Text = data.Recommendation.VerifyComment;

            imgSource.ImageUrl = data.Recommendation.RecommendImage;

            Filedata = new UploadFileItemPresentation
            {
                Path = data.Recommendation.RecommendImage
            };

            //btnGeneratePdf.Visible = ((VerifyStatus)data.Recommendation.VerifyStatus == VerifyStatus.Passed);

            //if (btnGeneratePdf.Visible)
            //{
                //txt_StudentName_.Enabled = false;
                //txt_Sex_.Enabled = false;
                //txt_Birthday_.Enabled = false;
                //txt_Telephone_.Enabled = false;
                //txt_Email_.Enabled = false;
                //txt_Identity_.Enabled = false;
                //txt_Politics_.Enabled = false;
                //txt_CollegeName_.Enabled = false;
                //txt_MarjorName_.Enabled = false;
                //txt_ComputerLevel_.Enabled = false;
                //txt_FromArea_.Enabled = false;
                //txt_Health_.Enabled = false;
                //txt_HomeAddress_.Enabled = false;
                //txt_HomePhone_.Enabled = false;
                //txt_InnovativePracticeExperience_.Enabled = false;
                //txt_LanguageLevel_.Enabled = false;
                //txt_PostCode_.Enabled = false;
                //txt_PunishmentDesc_.Enabled = false;
                //txt_ScientificResearchProject_.Enabled = false;
                //txt_SecondDegree_.Enabled = false;
                //txt_SelfEvaluation_.Enabled = false;
                //txt_SocialWorkPosition_.Enabled = false;
                //txt_Specialty_.Enabled = false;
                //txt_TrainingMode_.Enabled = false;
                //txt_GraduateYear_.Enabled = false;
            //    //chk_ToVerify_.Enabled = false;
            //}

            base.InitData();
        }

        protected void btnSave_Click(object sender,EventArgs e)
        {
            var recommendation = DataContext.StudentRecommendation.Where(it => it.StudentNum == StudentNum).FirstOrDefault();
            if (recommendation == null)
            {
                recommendation = new LkDataContext.StudentRecommendation();
                DataContext.StudentRecommendation.InsertOnSubmit(recommendation);
                recommendation.CreateTime = DateTime.Now;
                recommendation.StudentNum = StudentNum;
            }
            else
            {
                recommendation.UpdateTime = DateTime.Now;
            }

            recommendation.FromArea = txt_FromArea_.Text;
            recommendation.ComputerLevel = txt_ComputerLevel_.Text;
            recommendation.Health = txt_Health_.Text;
            recommendation.HomeAddress = txt_HomeAddress_.Text;
            recommendation.HomePhone = txt_HomePhone_.Text;
            recommendation.InnovativePracticeExperience = txt_InnovativePracticeExperience_.Text;
            recommendation.LanguageLevel = txt_LanguageLevel_.Text;
            recommendation.PostCode = txt_PostCode_.Text;
            recommendation.PunishmentDesc = txt_PunishmentDesc_.Text;
            recommendation.ScientificResearchProject = txt_ScientificResearchProject_.Text;
            recommendation.SecondDegree = txt_SecondDegree_.Text;
            recommendation.SelfEvaluation = txt_SelfEvaluation_.Text;
            recommendation.SocialWorkPosition = txt_SocialWorkPosition_.Text;
            recommendation.Specialty = txt_Specialty_.Text;
            recommendation.TrainingMode = txt_TrainingMode_.Text;
            recommendation.RecommendImage = Filedata.Path;
            recommendation.GraduateYear = txt_GraduateYear_.Text;

            if (String.IsNullOrEmpty(txt_GraduateYear_.Text))
            {
                recommendation.RecommendImage = DateTime.Now.AddYears(1).Year.ToString();
            }

            DataContext.SubmitChanges();

            ShowMsg(true, "保存成功!");
        }

        protected override void OnInit(EventArgs e)
        {
            upLoadControl.FinishUploadingImageEventHandler +=
                new BaseUploadControl.FinishUploadingImageEvent(upLoadControl_FinishUploadingImageEventHandler);
            base.OnInit(e);
        }

        protected void upLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgSource.ImageUrl = fileItem.FilePath;
            var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Student,
                                                                     AttachmentType.BaseInfo, fileItem.FileName);
            FileHelper.DrawingUploadFile(fileItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), 60, 50);
            Filedata = new UploadFileItemPresentation
            {
                Path = fileItem.FilePath,
                ThumbPath = thumbPath
            };
        }

        protected void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneratePdf.aspx");
        }
    }
}