using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Presentation.Enum;

namespace Business.Interface.Student
{
    public interface IStudentProfessionalService
    {
        StudentProfessionalPresentation Get(StudentProfessionalCriteria criteria);

        EntityCollection<StudentProfessionalPresentation> GetAll(StudentProfessionalCriteria criteria);

        ActionResult Save(StudentProfessionalPresentation presentation);

        ActionResult Delete(string studentNum, int id);

        IList<ContentPresentation> GetNewestFrontProfessionalList(string studentNum, out int totalCount);

        ActionResult SetStatus(string studentNum, int id, bool isOnline);

        StudentProfessionalPresentation GetFrontProfessionalById(int id, string studentNum);

        IList<ContentPresentation> GetFrontProfessionalList(string studentNum, string keyword);

        ActionResult ChangeVerifyStatus(int professionId, VerifyStatus status, ProfessionalType sourceType, string verifyReason, string evaluation, string teacherNum);
    }
}
