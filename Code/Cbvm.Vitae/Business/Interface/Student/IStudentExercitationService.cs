using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Interface.Student
{
    public interface IStudentExercitationService
    {
        StudentExercitationPresentation Get(StudentExercitationCriteria criteria);

        EntityCollection<StudentExercitationPresentation> GetAll(StudentExercitationCriteria criteria);

        ActionResult Save(StudentExercitationPresentation presentation);

        ActionResult ChangeVerifyStatus(int activityId, VerifyStatus status,
            string verifyReason, string evaluation, string teacherNum);

        ActionResult Delete(string studentNum, int id);

        ActionResult SetStatus(string studentNum, int id, bool isOnline);

        EntityCollection<StudentExercitationPresentation> GetVerifyAll(StudentExercitationCriteria criteria, string teacherNum);

        StudentExercitationPresentation GetFrontById(string studentNum, int exercitationId);

        IList<ContentPresentation> GetFrontExercitationList(string studentNum, string keyword);

        IList<ContentPresentation> GetNewestFrontExercitationList(string studentNum, out int totalCount);
    }
}
