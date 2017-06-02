using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;

namespace Business.Interface.Enterprise
{
    public interface IEnterpriseRecruitBatchService
    {
        EnterpriseRecruitBatchPresentation Get(int id);

        ActionResult Save(EnterpriseRecruitBatchPresentation presentation);

        ActionResult Delete(int id);

        EntityCollection<EnterpriseRecruitBatchStatisticsPresentation> GetAllStatistics(
            EnterpriseRecruitBatchCriteria criteria);
    }
}
