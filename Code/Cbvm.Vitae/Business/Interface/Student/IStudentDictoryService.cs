using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Interface.Student
{
    public interface IStudentDictoryService
    {
        StudentDictoryPresentation Get(StudentDictoryCriteria criteria);

        ActionResult Save(StudentDictoryPresentation presentation);

        EntityCollection<StudentDictoryPresentation> GetAll(StudentDictoryCriteria criteria);
    }
}
