using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class EditReferralsToEnterprisePage : BaseTeacherPage
    {
        private IEnterpriseJobRequestQueueService Service
        {
            get
            {
                return new EnterpriseJobRequestQueueService();
            }
        }

        private int ReferralsQueueID
        {
            get
            {
                int referralsQueueID = 0;
                int.TryParse(Request.QueryString["ReferralsQueueID"], out referralsQueueID);
                return referralsQueueID;

            }
        }

        public string StudentNum
        {
            get { return Request.QueryString["StudentNum"]; }
        }

        public string StudentName
        {
            get { return Request.QueryString["StudentName"]; }
        }


        protected override void InitData()
        {
            base.InitData();

            if (ReferralsQueueID > 0)
            {
                var jobReferralsQueue = Service.Get(new EnterpriseJobRequestQueueCriteria()
                {
                    Id = ReferralsQueueID,
                    TeacherNum = TeacherNum
                });

                ltlEnterpriseName.Text = jobReferralsQueue.EnterpriseName;
                ltlJobName.Text = jobReferralsQueue.JobName;
                ltlReferralState.Text = EnumHelper.GetEnumDescription(jobReferralsQueue.ReferralState);
                ltlWorkPlace.Text = jobReferralsQueue.WorkPlace;

                txtDescription.Text = jobReferralsQueue.Content;
                ltlNote.Text = jobReferralsQueue.Note;

                ltlStudentNum.Text = StudentNum;
                ltlStudentName.Text = StudentName;

                this.btnDeny.Visible =
                    this.btnPassed.Visible = (jobReferralsQueue.ReferralState == ReferralState.Pending);
            }
        }

        protected void btnPassed_Click(object sender, EventArgs e)
        {
            if (ReferralsQueueID > 0)
            {
                var actionReult = Service.ChangeReferralState(CurrentUser.UserName, ReferralsQueueID,
                                                              ReferralState.Passed, txtDescription.Text);
                if (actionReult.IsSucess)
                {
                    this.ReflashFrame();
                    ShowMsg(actionReult.IsSucess, "已向企业推荐!");
                }
            }
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            if (ReferralsQueueID > 0)
            {
                var actionReult = Service.ChangeReferralState(CurrentUser.UserName, ReferralsQueueID,
                                                              ReferralState.Deny,
                                                              txtDescription.Text);
                if (actionReult.IsSucess)
                {
                    this.ReflashFrame();
                    ShowMsg(actionReult.IsSucess, "已拒绝推荐!");
                }
            }
        }
    }
}