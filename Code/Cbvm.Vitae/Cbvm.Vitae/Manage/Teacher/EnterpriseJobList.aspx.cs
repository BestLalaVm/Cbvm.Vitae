using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation;
using Presentation.Cache;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using Telerik.Web.UI;


namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class EnterpriseJobList : BaseTeacherListPage<EnterpriseJobForTeacherPresentation, EnterpriseJobCriteria>
    {
        private IEnterpriseJobService Service
        {
            get
            {
                return new EnterpriseJobService();
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

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdEnterpriseJob; }
        }

        protected override EntityCollection<EnterpriseJobForTeacherPresentation> GetSearchResultList(EnterpriseJobCriteria criteria)
        {
            return Service.GetAllForTeachers(criteria);
        }
    }
}