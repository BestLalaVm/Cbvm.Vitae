using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Base;
using Presentation.UIView;
using Presentation.UIView.Base;

namespace Business.Interface.Base
{
    public interface ITeacherGroupService
    {
        TeacherGroupPresentation Get(string code);

        ActionResult Save(TeacherGroupPresentation presentation);

        EntityCollection<TeacherGroupPresentation> GetAll(TeacherGroupCriteria criteria);
    }
}
