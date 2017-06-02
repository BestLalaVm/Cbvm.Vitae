using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;

namespace Business.Interface.Enterprise
{
    public interface IEnterpriseEmailTemplateService
    {
        EnterpriseEmailTemplatePresentation Get(EnterpriseEmailTemplateCriteria criteria);

        EntityCollection<EnterpriseEmailTemplatePresentation> GetAll(EnterpriseEmailTemplateCriteria criteria);

        ActionResult Save(EnterpriseEmailTemplatePresentation presentation);
    }
}
