using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria;
using Presentation.UIView;

namespace Business.Interface
{
    public interface IMailQueueService
    {
        MailQueuePresentation Get(MailQueueCriteria criteria);

        EntityCollection<MailQueuePresentation> GetAll(MailQueueCriteria criteria);
    }
}
