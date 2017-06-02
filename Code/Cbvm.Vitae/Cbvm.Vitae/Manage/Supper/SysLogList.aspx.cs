using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;

namespace Cbvm.Vitae.Manage.Supper
{
    public partial class SysLogList : BaseSupperListPage<SystemLogPresentation, SystemLogCriteria>
    {
        private ISystemLogService Service
        {
            get
            {
                return new SystemLogService();
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

        protected override void InitBindData()
        {
            prm_LogType_.Items.Add(new ListItem("",""));
            prm_LogType_.Items.Add(new ListItem("Normal", "0"));
            prm_LogType_.Items.Add(new ListItem("Warning", "1"));
            prm_LogType_.Items.Add(new ListItem("Error", "2"));
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdSystemLog; }
        }

        protected override EntityCollection<SystemLogPresentation> GetSearchResultList(SystemLogCriteria criteria)
        {
            return Service.GetAll(criteria);
        }
    }
}