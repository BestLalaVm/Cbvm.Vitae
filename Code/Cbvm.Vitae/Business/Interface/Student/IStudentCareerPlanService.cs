using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.UIView;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;

namespace Business.Interface.Student
{
    public interface IStudentCareerPlanService : IBaseService<int, StudentCareerPlanPresentation, StudentCareerPlanCriteria>
    {

    }
}
