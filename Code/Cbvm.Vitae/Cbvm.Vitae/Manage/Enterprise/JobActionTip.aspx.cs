using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class JobActionTip : BaseEnterprisePage
    {
        protected string JobRequestId
        {
            get { return Request.QueryString["JobRequestId"]; }
        }

        protected string RecruitFlowId
        {
            get { return Request.QueryString["RecruitFlowId"]; }
        }

        private IEnterpriseJobRequestService Service
        {
            get { return new EnterpriseJobRequestService(); }
        }

        protected override void InitData()
        {
            base.InitData();
            int jobRequestId = 0;
            if (int.TryParse(Request.QueryString["JobRequestID"], out jobRequestId))
            {
                var jobStage = Service.LoadNewestJobRequestRecruitStage(jobRequestId);
                if (jobStage != null)
                {
                    this.txtDescription.Text = jobStage.Description;
                }
            }
        }
    }
}