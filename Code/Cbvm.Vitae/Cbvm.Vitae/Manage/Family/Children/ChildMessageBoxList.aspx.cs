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
using Presentation.UIView.MsgBox;
using Telerik.Web.UI;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildMessageBoxList : BaseFamilyListPage<MessageBoxViewPresentation, MessageBoxCriteria>
    {
        private IMessageBoxService Service
        {
            get
            {
                return new MessageBoxService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return this.pnlCondition; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdMsgList; }
        }

        protected override EntityCollection<MessageBoxViewPresentation> GetSearchResultList(MessageBoxCriteria criteria)
        {
            criteria.UserName = CurrentUser.UserName;
            criteria.SenderType = (int)Presentation.Enum.UserType.Student;
            criteria.ReceiverType = (int)CurrentUser.UserType;
            return Service.GetViewAll(criteria);
        }
    }
}