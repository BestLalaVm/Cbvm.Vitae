using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation.Criteria;
using Presentation.UIView;

namespace Cbvm.Vitae.Manage.Supper
{
    public partial class MailQueueList : BaseSupperListPage<MailQueuePresentation, MailQueueCriteria>
    {

        private IMailQueueService Service
        {
            get
            {
                return new MailQueueService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            this.RadGridControl.Rebind();
        }

        protected override void InitBindData()
        {
            prm_IsSended_.Items.Add(new ListItem("", ""));
            prm_IsSended_.Items.Add(new ListItem("已发送", "1"));
            prm_IsSended_.Items.Add(new ListItem("未发送", "0"));
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdEmailQueue; }
        }

        protected override EntityCollection<MailQueuePresentation> GetSearchResultList(MailQueueCriteria criteria)
        {
            return Service.GetAll(criteria);
        }

    }
}