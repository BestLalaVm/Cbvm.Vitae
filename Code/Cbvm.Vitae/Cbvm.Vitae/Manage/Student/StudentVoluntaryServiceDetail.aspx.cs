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

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentVoluntaryServiceDetail : BaseStudentDetailPage
    {
        private IOrigianlBaseService _Service;
        private IOrigianlBaseService Service
        {
            get
            {
                if (_Service == null)
                {
                    _Service = new StudentVoluntaryServiceService();
                }

                return _Service;
            }
        }

        private StudentVoluntaryServicePresentation _VoluntaryService;
        private StudentVoluntaryServicePresentation VoluntaryService
        {
            get
            {
                if (_VoluntaryService == null)
                {
                    _VoluntaryService = Service.DataContext.StudentVoluntaryService.Where(it => it.StudentNum == CurrentUser.UserName && it.ID == CurrentID).Select(it => new StudentVoluntaryServicePresentation()
                    {
                        ID = it.ID,
                        IsOnline = it.IsOnline,
                        StartDate = it.StartDate,
                        EndDate = it.EndDate,
                        Status = (VerifyStatus)it.VerifyStatus,
                        StudentNum = it.StudentNum,
                        Title = it.Title,
                        VerifyComment = it.VerifyComment,
                        Content = it.Content
                    }).FirstOrDefault();
                }
                return _VoluntaryService;
            }
        }

        protected override void InitData()
        {
            if (VoluntaryService != null)
            {
                txt_Content_.LoadData(VoluntaryService.Content);
                txt_Title_.Text = VoluntaryService.Title;
                chk_IsOnline_.Checked = VoluntaryService.IsOnline;
                dtp_BeginTime_.SelectedDate = VoluntaryService.StartDate;
                dtp_EndTime_.SelectedDate = VoluntaryService.EndDate;
                ltl_VerifiedStatus_.Text = GlobalBaseDataCache.GetVerifityStatusLabel(VoluntaryService.Status);
                lbl_VerifiedComment_.Text = VoluntaryService.VerifyComment;
            }
            else
            {
                this.trVerifyComment.Visible = false;
                this.trVerifyStatrus.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var voluntary = Service.DataContext.StudentVoluntaryService.Where(it => it.StudentNum == CurrentUser.UserName && it.ID==CurrentID).FirstOrDefault();
            if (voluntary == null)
            {
                voluntary = new LkDataContext.StudentVoluntaryService
                {
                    StudentNum = CurrentUser.UserName,
                    VerifyStatus = (int)VerifyStatus.WaitAudited,
                    CreateDate = DateTime.Now
                };

                Service.DataContext.StudentVoluntaryService.InsertOnSubmit(voluntary);
            }

            voluntary.Title = txt_Title_.Text;
            voluntary.IsOnline = chk_IsOnline_.Checked;
            voluntary.LastUpdateDate = DateTime.Now;
            voluntary.StartDate = dtp_BeginTime_.SelectedDate;
            voluntary.EndDate = dtp_EndTime_.SelectedDate;
            voluntary.IsOnline = chk_IsOnline_.Checked;
            voluntary.Content = txt_Content_.SaveData();
            Service.DataContext.SubmitChanges();

            ShowMsg(true, ActionResult.DefaultResult.Message);
        }

        public override void ShowMsg(bool isSucess, string msg)
        {
            if (isSucess)
            {
                Response.Redirect("StudentVoluntaryServiceList.aspx");
                return;
            }
            base.ShowMsg(isSucess, msg);
        }
    }
}