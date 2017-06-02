using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LkDataContext;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.MessageBox;
using Presentation.UIView.MsgBox;

namespace Business.Interface
{
    public interface IMessageBoxService
    {
        //EntityCollection<MessageBoxItemViewPresentation> GetAll(MessageBoxCriteria criteria);

        //MessageBoxItemPresentation Get(int id);

        //EntityCollection<MessageBoxViewPresentation> GetViewAll(MessageBoxCriteria criteria);

        EntityCollection<MessageBoxPresentation> GetAll(MessageBoxCriteria criteria, string receiverKey,
            UserType receiverType);

        MessageBoxPresentation Get(int id);

        ActionResult Send(string senderUserName, UserType senderType, string receiverUserName, UserType receiverType,
            string subject, string content);

        int GetNewestMessageBoxCount(string receiverUserName, UserType receiverType);

        ActionResult SetReaded(int id);

    }
}
