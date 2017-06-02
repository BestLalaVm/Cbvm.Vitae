using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class ViewRequesterResume : BaseEnterprisePage
    {
        protected int JobRequestID
        {
            get
            {
                var requestID = 0;
                int.TryParse(Request.QueryString["JobRequestID"], out requestID);
                return requestID;
            }
        }

        protected string StudentNum
        {
            get { return Request.QueryString["StudentNum"]; }
        }

        private IEnterpriseJobRequestService Service
        {
            get { return new EnterpriseJobRequestService(); }
        }

        protected override void InitData()
        {
            Service.InitToViewedRequestJob(JobRequestID);
            base.InitData();
        }
    }
}