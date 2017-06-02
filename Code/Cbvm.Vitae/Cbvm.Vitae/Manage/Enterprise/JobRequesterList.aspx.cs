using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using Telerik.Web.UI;
using System.Text;
using WebLibrary.Helper;
using LkHelper;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class JobRequesterList : BaseEnterpriseListPage<EnterpriseJobRequestFromTeacherPresentation, EnterpriseJobRequestCriteria>
    {
        private IEnterpriseJobRequestService _JoinRequestService;
        private IEnterpriseJobRequestService JoinRequestService
        {
            get
            {
                if (_JoinRequestService == null)
                {
                    _JoinRequestService = new EnterpriseJobRequestService();
                }
                return _JoinRequestService;
            }
        }

        private IStudentTruthAuthenticatedService _TruthAuthenticatedService;
        private IStudentTruthAuthenticatedService TruthAuthenticatedService
        {
            get
            {
                if (_TruthAuthenticatedService == null)
                {
                    _TruthAuthenticatedService = new StudentTruthAuthenticatedService();
                }
                return _TruthAuthenticatedService;
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void linkInvite_Click(object sender, EventArgs e)
        {
            var linkInvite = sender as LinkButton;
            var item = linkInvite.NamingContainer as GridItem;
            var jobRequestID = (int)item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"];
            var result = JoinRequestService.InviteToAudition(jobRequestID);
            ShowMsg(result.IsSucess, result.Message);
        }

        protected override void InitData()
        {
            prm_RecruitFlowID_.BindSource(BindingSourceType.RecruitFlowSettedInfo, true);

            var recruitItem =
                GlobalBaseDataCache.RecruitFlowSettedList.FirstOrDefault(it => it.RecruitType == Presentation.Enum.RecruitType.Request);

            if (recruitItem != null)
            {
                prm_RecruitFlowID_.SelectedValue = prm_RecruitFlowID_.Items.FindByValue(recruitItem.Id.ToString()).Value;
            }
            base.InitData();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdEnterpriseJob; }
        }

        protected override EntityCollection<EnterpriseJobRequestFromTeacherPresentation> GetSearchResultList(
            EnterpriseJobRequestCriteria criteria)
        {
            criteria.EnterpriseCode = EnterpriseCode;
            bool referralsQueueIdNotNull = false;
            bool.TryParse(Request.QueryString["IsReferralsQueueIDNotNull"], out referralsQueueIdNotNull);
            criteria.IsReferralsQueueIDNotNull = referralsQueueIdNotNull;
            return JoinRequestService.GetJobRequestAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<EnterpriseJobRequestFromTeacherPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Index,
                it.JobName,
                it.JobNum,
                it.JobCode,
                it.StudentName,
                it.RequestTime,
                it.RequestStatus,
                it.StudentTelephone,
                Sex = GlobalBaseDataCache.GetSexLabel(it.StudentSex),
                it.StudentNum,
                it.Id,
                it.IsInviated,
                InviatedDateValue = it.InviatedDateValue,
                Referralers = String.Join(",", it.Referralers),
                AuthenticatedRequestTime = it.AuthenticatedRequestTime.HasValue?it.AuthenticatedRequestTime.Value.ToCustomerDateString():"",
                IsEnabledToAuthenticate = !it.AuthenticatedRequestTime.HasValue || it.AuthenticatedRequestTime.Value.AddDays(1) <= DateTime.Now
            });
        }

        protected void btnSendAuthenticate_Click(object sender, EventArgs e)
        {
            var btnSendAuthenticate = sender as Button;
            var gridItem = btnSendAuthenticate.NamingContainer as GridItem;
            if (gridItem != null)
            {
                var studentNum = (string)gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["StudentNum"];

                TruthAuthenticatedService.SendAuthenticated(studentNum, CurrentUser.Identity);

                this.RadGridControl.Rebind();
            }
        }

        protected void grdEnterpriseJob_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var btnSendAuthenticate = e.Item.FindControl("btnSendAuthenticate") as Button;
                if (btnSendAuthenticate != null)
                {
                    var data = e.Item.DataItem;
                    var isEnabledToAuthenticateProperty = data.GetType().GetProperty("IsEnabledToAuthenticate");
                    var isEnabledToAuthenticated = (bool)isEnabledToAuthenticateProperty.GetValue(data, null);

                    btnSendAuthenticate.Enabled = isEnabledToAuthenticated;
                    btnSendAuthenticate.Text = isEnabledToAuthenticated ? "真实性验证" : "已发送验证";
                }
            }
        }
    }
}
