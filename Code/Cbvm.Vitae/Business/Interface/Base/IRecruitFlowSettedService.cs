using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Base;
using Presentation.UIView;
using Presentation.UIView.Base;

namespace Business.Interface.Base
{
    public interface IRecruitFlowSettedService
    {
        IList<RecruitFlowSettedPresentation> GetAllRecruitFlow();

        RecruitFlowSettedPresentation Get(int id);

        ActionResult Save(RecruitFlowSettedPresentation presentation);

        EntityCollection<RecruitFlowSettedPresentation> GetAll(RecruitFlowSettedCriteria criteria);

        ActionResult Delete(int id);

        ActionResult SetRecruitFlowOrder(int recruitID, int nextRecruitID);
    }
}
