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
    public partial class TruthAuthenticatedDetail : BaseCollegeDetailPage
    {
        private CVAcademicianDataContext _DataContext;
        private LkDataContext.CVAcademicianDataContext DataContext
        {
            get
            {
                if (_DataContext == null)
                {
                    _DataContext = (new StudentTruthAuthenticatedService()).DataContext;
                }

                return _DataContext;
            }
        }

        protected override void InitData()
        {
            var truthAuthenticated = DataContext.StudentTruthAuthenticated.Where(it => it.Student.CollegeCode == CurrentUser.Identity && it.ID == CurrentId).Select(it => new
            {
                EnterpriseName = it.Enterprise.Name,
                RequestDate=it.RequestDate,
                it.IsAuthenticated,
                it.AuthenticatedComment,
                StudentName = it.Student.NameZh + " " + it.Student.NameEn,
                StudentNum = it.StudentNum,
                MarjorName = it.Student.Marjor.Name,
                EnterpriseCode=it.Enterprise.Code
            }).FirstOrDefault();

            if (truthAuthenticated == null)
            {
                Response.Redirect("TruthAuthenticatedList.aspx");
                return;
            }

            rdoVerify.SelectedValue = truthAuthenticated.IsAuthenticated ? "2" : "1";
            txt_AuthenticatedComment_.Text = truthAuthenticated.AuthenticatedComment;

            txt_StudentNum_.Text = truthAuthenticated.StudentNum;
            //txt_DepartName_.Text = truthAuthenticated.DepartName;
            txt_MarjorName_.Text = truthAuthenticated.MarjorName;
            dtp_RequestDate_.SelectedDate = truthAuthenticated.RequestDate;

            lnkStudentName.Text = truthAuthenticated.StudentName;
            lnkStudentName.NavigateUrl = String.Format("/Template/StudentTemplate/ViewStudentResume.aspx?StudentNum={0}", truthAuthenticated.StudentNum);
            
            lnkEnterpriseName.Text = truthAuthenticated.EnterpriseName;
            lnkEnterpriseName.NavigateUrl = String.Format("/Template/EnterpriseDetail.aspx?KeyCode={0}", truthAuthenticated.EnterpriseCode);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var truthAuthenticated = DataContext.StudentTruthAuthenticated.Where(it => it.Student.CollegeCode == CurrentUser.Identity && it.ID == CurrentId).FirstOrDefault();
            if (truthAuthenticated != null)
            {
                truthAuthenticated.IsAuthenticated = rdoVerify.SelectedValue == "2";
                truthAuthenticated.AuthenticatedComment = txt_AuthenticatedComment_.Text;

                DataContext.SubmitChanges();
            }

            Response.Redirect("TruthAuthenticatedList.aspx");
        }
    }
}