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
    public interface IEnterpriseJobService
    {
        EntityCollection<EnterpriseJobPresentation> GetAll(EnterpriseJobCriteria criteria);

        EnterpriseJobPresentation Get(string code, bool includeRelativeData);

        ActionResult Save(EnterpriseJobPresentation presentation);

        ActionResult Delete(EnterpriseJobCriteria criteria);

        EntityCollection<EnterpriseJobForStudentPresentation> GetAllForStudents(EnterpriseJobCriteria criteria,
            string studentNum);

        EntityCollection<EnterpriseJobForTeacherPresentation> GetAllForTeachers(EnterpriseJobCriteria criteria);

        List<EnterpriseJobPresentation> GetEnterpriseJobRequest(int pageSize = 20);
    }
}
