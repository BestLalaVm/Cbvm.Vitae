using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;

namespace Business.Interface.Student
{
    public interface IStudentVoluntaryServiceService : IBaseService<int, StudentVoluntaryServicePresentation, StudentVoluntaryServiceCriteria>
    {

    }
}
