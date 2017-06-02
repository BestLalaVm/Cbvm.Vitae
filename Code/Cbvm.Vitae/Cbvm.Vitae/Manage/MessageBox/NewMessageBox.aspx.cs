using System;
using System.Linq;
using Business.Interface;
using Business.Service;
using LkHelper;
using Presentation;
using Presentation.Cache;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.MessageBox;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.MessageBox
{
    public partial class NewMessageBox : BasePage
    {
        private IMessageBoxService Service
        {
            get
            {
                return new MessageBoxService();
            }
        }

        private MessageBoxPresentation ReplyMsgBox
        {
            get
            {
                var msg = this.ViewState["ReplyMsgBox"] as MessageBoxPresentation;
                if (msg == null)
                {
                    int msgId = 0;
                    if (int.TryParse(Request.QueryString["RefId"], out msgId))
                    {
                        msg = Service.Get(msgId);
                    }
                    if (msg != null)
                    {
                        this.ViewState["ReplyMsgBox"] = msg;
                    }
                }
                return msg;
            }
        }

        public override UserType InitUserType()
        {
            return CurrentUser.UserType;
        }

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Post;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            var receiver = "";
            UserType receiverType ;
            if (ReplyMsgBox == null)
            {
                //var receiverValue = txtReceiver.SelectedValue.Split('|');
                //receiver = receiverValue[0];
                receiver = this.txt_ReceiveUserName_.Value;
                //receiverType = (UserType) Enum.Parse(typeof (UserType), receiverValue[1]);
                receiverType = (UserType)Enum.Parse(typeof(UserType), txt_ReceiveUserType_.Value);
            }
            else
            {
                receiver = ReplyMsgBox.SenderKey;
                receiverType = ReplyMsgBox.SenderType;
            }
            var actionResult = Service.Send(CurrentUser.UserName, CurrentUser.UserType, receiver, receiverType,
                txtSubject.Text, editMsgBody.SaveData());
            if (actionResult.IsSucess)
            {
                Response.Redirect("../MessageBox/MessageBoxList.aspx");
            }
        }

        //protected void txtReceiver_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        //{
        //    var list = GlobalAutoCache.GetAllAutoList(e.Text.Trim());
        //    txtReceiver.Items.Clear();
        //    txtReceiver.DataValueField = "Value";
        //    txtReceiver.DataTextField = "Text";
        //    txtReceiver.DataSource = list.Take(50).Select(it => new
        //    {
        //        it.Code,
        //        it.Index,
        //        it.Description,
        //        it.UserType,
        //        it.UserTypeLabel,
        //        it.Name,
        //        Value = String.Format("{0}|{1}", it.Code, it.UserType),
        //        Text = GenerateText(it)
        //    });
        //    txtReceiver.DataBind();
        //}

        private string GenerateText(AutoValuePresentation autoValue)
        {
            switch (autoValue.UserType)
            {
                case UserType.Teacher:
                    return String.Format("{0} 老师({1})", autoValue.Name, autoValue.Code);
                case UserType.Enterprise:
                    return String.Format("{0}-{1}({2})", autoValue.Description, autoValue.Name, autoValue.Code);
                default:
                    if (autoValue.UserType == UserType.Student && CurrentUser.UserType == UserType.Family)
                    {
                        if (CurrentUser.UserName.StartsWith(autoValue.Code) &&
                            CurrentUser.UserName.Length - autoValue.Code.Length == 4)
                        {
                            return String.Format("子女({0}-{1})", autoValue.Name, autoValue.Code);
                        }
                    }
                    else
                    {
                        if (autoValue.UserType == UserType.Family && CurrentUser.UserType == UserType.Student)
                        {
                            if (autoValue.Code.StartsWith(CurrentUser.Identity) &&
                                autoValue.Code.Length - CurrentUser.Identity.Length == 4)
                            {
                                return String.Format("家长{0}-{1}", autoValue.Name, autoValue.Code);
                            }
                        }
                    }
                    return String.Format("{0}({1})", autoValue.Name, autoValue.Code);
            }
        }

        protected override void InitBindData()
        {
            base.InitBindData();

            if (ReplyMsgBox != null)
            {
                var sender = GlobalAutoCache.GetAutoVlaue(ReplyMsgBox.SenderKey, ReplyMsgBox.SenderType);
                txt_ReceiveUserName_.Value = ReplyMsgBox.SenderKey;
                txt_ReceiveName_.Text = sender.Name;
                txt_ReceiveUserType_.Value = sender.UserType.ToString();

                txtSubject.Text = EmailTemplateHelper.GetEmailTemplateSubject(SystemEmailType.ReplyEmail)
                    .Replace("$$Subject$$", ReplyMsgBox.Subject);

                var receiver = GlobalAutoCache.GetAutoVlaue(ReplyMsgBox.ReceiverKey, ReplyMsgBox.ReceiverType);
                string body =
                    EmailTemplateHelper.GetEmailTemplateBody(SystemEmailType.ReplyEmail)
                        .Replace("$$SenderName$$",
                            String.Format("{0}<{1}>-{2}", sender.Name, sender.Code,
                                EnumHelper.GetEnumDescription(sender.UserType)))
                        .Replace("$$SendTime$$", ReplyMsgBox.CreateTime.ToCustomerDateString())
                        .Replace("$$ReceiverName$$",
                            String.Format("{0}<{1}>-{2}", receiver.Name, receiver.Code,
                                EnumHelper.GetEnumDescription(receiver.UserType)))
                        .Replace("$$Subject$$", ReplyMsgBox.Subject)
                        .Replace("$$Content$$", ReplyMsgBox.Content);

                //txtReceiver.Enabled = false;
                btnReferral.Visible = false;

                editMsgBody.LoadData(body);
                editMsgBody.Focus();
            }
        }
    }
}