using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;
using Business.Interface;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.Cache;
using LkDataContext;
using LkHelper;

namespace Cbvm.Vitae.Manage.College
{
    public partial class VoluntaryServiceDetail : BaseCollegeDetailPage
    {
        private CVAcademicianDataContext _DataContext;
        private LkDataContext.CVAcademicianDataContext DataContext
        {
            get
            {
                if (_DataContext == null)
                {
                    _DataContext = (new StudentVoluntaryServiceService()).DataContext;
                }

                return _DataContext;
            }
        }

        protected override void InitData()
        {
            var voluntService = DataContext.StudentVoluntaryService.Where(it => it.Student.CollegeCode == CurrentUser.Identity && it.ID == CurrentId).Select(it => new
            {
                it.Title,
                it.StartDate,
                it.EndDate,
                it.VerifyStatus,
                it.Content,
                it.VerifyComment,
                StudentName = it.Student.NameZh+" "+it.Student.NameEn,
                StudentNum = it.StudentNum,
                MarjorName = it.Student.Marjor.Name
            }).FirstOrDefault();

            if (voluntService == null)
            {
                Response.Redirect("VoluntaryServiceList.aspx");
                return;
            }

            txt_Title_.Text = voluntService.Title;
            dtp_BeginTime_.SelectedDate = voluntService.StartDate;
            dtp_EndTime_.SelectedDate = voluntService.EndDate;
            txt_VerifyComment_.Text = voluntService.VerifyComment;
            rdoVerify.SelectedValue = voluntService.VerifyStatus == (int)VerifyStatus.Passed ? "2" : "1";
            ltlDescription.Text = voluntService.Content;

            txt_StudentName_.Text = voluntService.StudentName;
            txt_StudentNum_.Text = voluntService.StudentNum;
            txt_MarjorName_.Text = voluntService.MarjorName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            var voluntary = DataContext.StudentVoluntaryService.Where(it => it.Student.CollegeCode == CurrentUser.Identity && it.ID == CurrentId).FirstOrDefault();
            if (voluntary != null)
            {
                voluntary.VerifyStatus = (int)(rdoVerify.SelectedValue == "2" ? VerifyStatus.Passed : VerifyStatus.UnPassed);
                voluntary.VerifyComment = txt_VerifyComment_.Text;
                voluntary.LastUpdateDate = DateTime.Now;

                DataContext.SubmitChanges();
            }

            Response.Redirect("VoluntaryServiceList.aspx");
        }
    }
}