using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation;
using Presentation.Cache;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class AskedReferralsToEnterpriseList : BaseTeacherListPage<AskedReferralsJobPresentation, EnterpriseJobRequestQueueCriteria>
    {
        private IEnterpriseJobRequestQueueService Service
        {
            get
            {
                return new EnterpriseJobRequestQueueService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdEnterpriseJob; }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        private string RendHTML(AskedReferralsJobPresentation referralsQueue)
        {
            if (referralsQueue.ReferralState == ReferralState.Pending)
            {
                return "<a href='#' title='进入推荐' onclick=\"EditReferralsToEnterprisePage('" +
                       referralsQueue.Id +
                       "','" + referralsQueue.StudentName + "','" + referralsQueue.StudentNum +
                       "')\" class=\"grid-edit\"><span>进入推荐</span></a>";
            }
            if (referralsQueue.RequestQueueType == RequestQueueType.AskForReferral)
            {
                return EnumHelper.GetEnumDescription(referralsQueue.ReferralState);
            }
            else
            {
                return EnumHelper.GetEnumDescription(referralsQueue.RequestQueueType);
            }
        }

        protected override void InitBindData()
        {
            base.InitBindData();
            prm_MarjorCode_.BindSource(BindingSourceType.MarjorInfo, true);
            prm_ReferralStateFromReferraler_.BindSource(BindingSourceType.ReferralStateInfo, true);
        }

        protected override Presentation.UIView.EntityCollection<AskedReferralsJobPresentation>
            GetSearchResultList(EnterpriseJobRequestQueueCriteria criteria)
        {
            criteria.TeacherNum = TeacherNum;
            return Service.GetAllAskedReferrals(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<AskedReferralsJobPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Id,
                it.Index,
                it.EnterpriseName,
                it.JobName,
                it.SexRequired,
                it.Education,
                it.WorkPlace,
                it.SalaryScope,
                it.StartTime,
                it.EndTime,
                it.JobCode,
                it.StudentMarjorName,
                it.StudentClass,
                it.StudentName,
                ReferralState = GlobalBaseDataCache.GetReferralStateName(it.ReferralState),
                RenderHtml = RendHTML(it),
                it.StudentNum,
                it.AskedReferralNote,
                it.RequestTime
            }).ToList();
        }
    }
}