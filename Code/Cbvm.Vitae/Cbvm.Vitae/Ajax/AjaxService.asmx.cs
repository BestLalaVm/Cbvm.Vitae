using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Business.Service;
using Business.Service.Enterprise;
using Presentation.UIView;

namespace Cbvm.Vitae.Ajax
{
    /// <summary>
    /// Summary description for AjaxService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AjaxService : WebService
    {
        private MessageBoxService Service
        {
            get
            {
                return new MessageBoxService();
            }
        }
        //[WebMethod]
        //public MessageBoxPresentation GetMessageBoxInfo(int msgID, string receiverKey, UserType userType)
        //{
        //    return Service.GetMessageBoxInfo(msgID, receiverKey, userType);
        //}

        //[WebMethod]
        //public ActionResult SaveMessageBoxInfo(string senderName, UserType senderType, string receiverName,
        //                                       UserType receiverType, string subject, string message,
        //                                       int refMessageId)
        //{
        //    subject = " ";
        //    return Service.SaveMessageBoxInfo(senderName, senderType, receiverName, receiverType, subject,
        //                                               message, refMessageId);
        //}

        [WebMethod]
        public ActionResult ChangeRequestJobStage(int jobRequestID, int recruitFlowID, string description)
        {
            EnterpriseJobRequestService requesterServer = new EnterpriseJobRequestService();
            var result = requesterServer.ChangeRequestJobStage(jobRequestID,recruitFlowID, description);
            return result;
        }

        [WebMethod]
        public ActionResult DenyRequest(int jobRequestId)
        {
            EnterpriseJobRequestService requesterServer = new EnterpriseJobRequestService();
            return requesterServer.DenyRequestJob(jobRequestId);
        }
    }
}
