using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Presentation.Criteria;
using Presentation.UIView;

namespace Business.Service
{
    public class MailQueueService:BaseService,IMailQueueService
    {
        public MailQueuePresentation Get(MailQueueCriteria criteria)
        {
            throw new NotImplementedException(); 
        }

        public EntityCollection<MailQueuePresentation> GetAll(MailQueueCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
