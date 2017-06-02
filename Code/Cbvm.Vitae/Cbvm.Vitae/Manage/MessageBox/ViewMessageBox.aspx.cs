using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface;
using Business.Service;
using Presentation.Cache;
using Presentation.Enum;

namespace Cbvm.Vitae.Manage.MessageBox
{
    public partial class ViewMessageBox : BasePage
    {
        protected string MsgId
        {
            get
            {
                return Request.QueryString["ID"];
            }
        }

        private IMessageBoxService Service
        {
            get
            {
                return new MessageBoxService();
            }
        }

        public override UserType InitUserType()
        {
            return CurrentUser.UserType;
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.BaseInfo;
        }

        protected override void InitData()
        {
            base.InitData();
            int id;
            if(int.TryParse(MsgId,out  id))
            {
                var msg = Service.Get(id);
                if (msg != null)
                {
                    ltlContent.Text = msg.Content;

                    string profix = "";
                    if (msg.SenderType == UserType.Student && msg.ReceiverType == UserType.Family &&
                        msg.ReceiverKey.StartsWith(msg.SenderKey))
                    {
                        profix = "子女";
                    }
                    else
                    {
                        if (msg.SenderType == UserType.Family &&
                            msg.ReceiverType == UserType.Student &&
                            msg.SenderKey.StartsWith(msg.ReceiverKey))
                        {
                            profix = "家长";
                        }
                    }
                    if (String.IsNullOrEmpty(profix))
                    {
                        ltlSender.Text = GetMsgUserText(msg.SenderKey, msg.SenderType);
                    }
                    else
                    {
                        ltlSender.Text = String.Format("{0}({1})", profix, GetMsgUserText(msg.SenderKey, msg.SenderType));
                    }

                    ltlReceiver.Text = GetMsgUserText(msg.ReceiverKey, msg.ReceiverType);
                    ltlSubject.Text = msg.Subject;

                    if (msg.SenderKey == CurrentUser.UserName && msg.SenderType == CurrentUser.UserType)
                    {
                        btnReply.Visible = false;
                    }
                    if (msg.ReceiverType == CurrentUser.UserType && msg.ReceiverKey == CurrentUser.UserName)
                    {
                        Service.SetReaded(id);
                    }
                }
            }
        }

        private string GetMsgUserText(string key, UserType userType)
        {
            var autoValue = GlobalAutoCache.GetAutoVlaue(key, userType);
            if (autoValue != null)
            {
                return String.Format("{0}<{1}>", autoValue.Name, autoValue.Code);
            }
            else
            {
                return key;
            }
        }
    }
}