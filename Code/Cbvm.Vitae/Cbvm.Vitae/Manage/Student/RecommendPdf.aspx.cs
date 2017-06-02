using System;
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

namespace Cbvm.Vitae.Manage.Student
{
    public partial class RecommendPdf : System.Web.UI.Page
    {
        private LkDataContext.CVAcademicianDataContext _DataContext;
        protected LkDataContext.CVAcademicianDataContext DataContext
        {
            get
            {
                if (_DataContext == null)
                {
                    _DataContext = new LkDataContext.CVAcademicianDataContext();
                }

                return _DataContext;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var studentNum = Request.QueryString["StudentNum"];

            if (String.IsNullOrEmpty(studentNum))
            {
                return;
            }

            var data = DataContext.Student.Where(it => it.StudentNum == studentNum).Select(it => new
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
                }
            }).FirstOrDefault();

            if (data == null) return;

            txt_StudentName_.Text = data.NameZh;
            txt_Sex_.Text = GlobalBaseDataCache.GetSexLabel(data.Sex);
            txt_Birthday_.Text = data.Birthday.HasValue ? String.Format("{0}年{1}月{0}日", data.Birthday.Value.Year, data.Birthday.Value.Month.ToString().PadLeft(2, '0'), data.Birthday.Value.Day.ToString().PadLeft(2, '0')) : "";
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

            imgSource.ImageUrl = data.Recommendation.RecommendImage;
        }
    }
}