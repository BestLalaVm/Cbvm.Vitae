using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;

namespace Business.Service.Enterprise
{
    public class EnterpriseEmailTemplateService : BaseService, IEnterpriseEmailTemplateService
    {
        public EnterpriseEmailTemplatePresentation Get(EnterpriseEmailTemplateCriteria criteria)
        {
            var emailTemplate =
                dataContext.EnterpriseEmailTemplate.FirstOrDefault(
                    it => it.EnterpriseCode == criteria.EnterpriseCode && it.ID == criteria.Id);
            if (emailTemplate == null)
            {
                return null;
            }
            return new EnterpriseEmailTemplatePresentation()
            {
                Body = emailTemplate.Body,
                Cc = emailTemplate.Cc,
                EmailType = (EnterpriseEmailType) emailTemplate.EmailType,
                EnterpriseCode = emailTemplate.EnterpriseCode,
                HelpTip = emailTemplate.HelpTip,
                Sender = emailTemplate.Sender,
                SenderName = emailTemplate.SenderName,
                Subject = emailTemplate.Subject
            };
        }

        public EntityCollection<EnterpriseEmailTemplatePresentation> GetAll(EnterpriseEmailTemplateCriteria criteria)
        {
            var query =
                from it in
                    dataContext.EnterpriseEmailTemplate.Where(it => it.EnterpriseCode == criteria.EnterpriseCode)
                select it;
            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.ID), criteria, out totalCount);

            var list = query.Select(emailTemplate => new EnterpriseEmailTemplatePresentation()
            {
                Body = emailTemplate.Body,
                Cc = emailTemplate.Cc,
                EmailType = (EnterpriseEmailType) emailTemplate.EmailType,
                EnterpriseCode = emailTemplate.EnterpriseCode,
                HelpTip = emailTemplate.HelpTip,
                Sender = emailTemplate.Sender,
                SenderName = emailTemplate.SenderName,
                Subject = emailTemplate.Subject
            }).ToList();

            EntityCollection<EnterpriseEmailTemplatePresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(EnterpriseEmailTemplatePresentation presentation)
        {
            return ActionResult.DefaultResult;
        }
    }
}
