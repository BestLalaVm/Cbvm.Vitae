using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.Enum;

namespace Cbvm.Vitae.Manage
{
    public partial class MessageBoxDialog : BasePage
    {
        protected string ReceiverId
        {
            get { return Request.QueryString["ReceiverId"]; }
        }

        protected string ReceiverType
        {
            get { return Request.QueryString["ReceiverType"]; }
        }

        protected string ReceiverName
        {
            get { return Request.QueryString["ReceiverName"]; }
        }

        public override UserType InitUserType()
        {
            return UserType.Guest;
        }

        public override AttachmentType InitAttachmentType()
        {
            throw new NotImplementedException();
        }

        protected override void InitData()
        {
            txtReceiver.Text = ReceiverName;
            base.InitData();
        }
    }
}