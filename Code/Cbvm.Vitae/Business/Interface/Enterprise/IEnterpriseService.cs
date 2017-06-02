using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Front;

namespace Business.Interface.Enterprise
{
    public interface IEnterpriseService
    {
        EnterprisePresentation Get(string code);

        ActionResult Save(EnterprisePresentation presentation);

        EntityCollection<EnterprisePresentation> GetAll(EnterpriseCriteria criteria);

        ActionResult Delete(int id);

        ActionResult SetVerifyStatus(int Id, VerifyStatus status, bool IsFamous);

        EntityCollection<EnterpriseTopPresentation> GetFrontHotEnterpriseList(HotEnterpriseType type, int pageIndex,
            int pageSize);

        EntityCollection<EnterprisePresentation> GetFrontEnterpriseList(string keyword, int pageIndex, int pageSize);

        EnterprisePresentation GetFrontEnterpriseByCode(string enterpriseCode);

        ActionResult Register(EnterprisePresentation presentation, out int enterpriseId);

        EnterpriseViewPresentation GetFrontDetail(string code);

        ActionResult InviteStudent(string studentNum, string enterpriseCode);

    }
}
