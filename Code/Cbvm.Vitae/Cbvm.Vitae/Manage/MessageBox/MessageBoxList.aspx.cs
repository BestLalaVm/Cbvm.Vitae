using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.MessageBox;
using Presentation.UIView.MsgBox;
using Telerik.Web.UI;

namespace Cbvm.Vitae.Manage.MessageBox
{
    public partial class MessageBoxList : BasePageList<MessageBoxPresentation, MessageBoxCriteria>
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
            get { return pnlCondition; }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdMsgBox; }
        }

        protected override EntityCollection<MessageBoxPresentation> GetSearchResultList(MessageBoxCriteria criteria)
        {
            return Service.GetAll(criteria, CurrentUser.UserName, CurrentUser.UserType);
        }

        public override UserType InitUserType()
        {
            return CurrentUser.UserType;
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Post;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<MessageBoxPresentation> list)
        {
            radGrid.DataSource = list.Select(ix => new
            {
                ix.Index,
                ix.Id,
                ix.ReceiverKey,
                ix.ReceiverType,
                ix.Subject,
                Time = ix.CreateTime,
                MsgType =
                 ((ix.SenderKey == CurrentUser.UserName && ix.SenderType == CurrentUser.UserType)? 
                 "已发送至" + ix.ReceiverKey + "(" +EnumHelper.GetEnumDescription(ix.ReceiverType) + ")": "已接收"),
                ReadLabel = (ix.IsReaded? "已读": "未读")
            }).ToList();
        }

        protected override void InitBindData()
        {
            base.InitBindData();

            this.prm_MsgType_.Items.Add(new ListItem("全部", "0"));
            this.prm_MsgType_.Items.Add(new ListItem("已接收信件", "1"));
            this.prm_MsgType_.Items.Add(new ListItem("已发送信件", "2"));

            prm_IsReaded_.Items.Add(new ListItem("全部", ""));
            prm_IsReaded_.Items.Add(new ListItem("未读", "0"));
            prm_IsReaded_.Items.Add(new ListItem("已读", "1"));
        }
    }
}