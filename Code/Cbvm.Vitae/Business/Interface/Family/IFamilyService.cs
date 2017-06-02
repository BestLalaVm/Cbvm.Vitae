using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Family;
using Presentation.UIView;
using Presentation.UIView.Family;
using Presentation.UIView.Family.View;
using Presentation.UIView.Student;

namespace Business.Interface.Family
{
    public interface IFamilyService
    {
        FamilyPresentation Get(string userName);

        ActionResult Save(FamilyPresentation familyPresentation);

        EntityCollection<FamilyForAdminPresentation> GetForAdminAll(FamilyCriteria criteria);

        FamilyPresentation GetByStudentNum(string studentNum);
    }
}
