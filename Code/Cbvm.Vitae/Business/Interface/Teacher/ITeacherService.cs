using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Teacher;
using Presentation.UIView;
using Presentation.UIView.Teacher;

namespace Business.Interface.Teacher
{
    public interface ITeacherService
    {
        TeacherPresentation Get(TeacherCriteria criteria);

        EntityCollection<TeacherPresentation> GetAll(TeacherCriteria criteria);

        ActionResult Save(TeacherPresentation presentation,bool includeCourse,bool includeGroup);

        ActionResult Delete(int id);

        IQueryable<LkDataContext.Teacher> GetQuery();
    }
}
