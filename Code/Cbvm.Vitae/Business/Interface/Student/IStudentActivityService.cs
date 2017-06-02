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
    public interface IStudentActivityService
    {
        StudentActivityPresentation Get(StudentActivityCriteria criteria);

        EntityCollection<StudentActivityPresentation> GetAll(StudentActivityCriteria criteria);

        ActionResult Save(StudentActivityPresentation presentation);

        ActionResult ChangeVerifyStatus(int activityId, VerifyStatus status,
            string verifyReason, string evaluation, string teacherNum);

        ActionResult Delete(string studentNum, int id);

        StudentActivityPresentation GetFrontActivityById(int id, string studentNum);

        IList<StudentActivityPresentation> GetResumeActivityList(string studentNum, string keyword);

        ActionResult SetStatus(string studentNum, int id, bool isOnline);

        EntityCollection<StudentActivityPresentation> GetVerifyAll(StudentActivityCriteria criteria, string teacherNum);

        IList<ContentPresentation> GetNewestFrontActivityList(string studentNum, out int totalCount);

        IList<ContentPresentation> GetFrontActivityList(string studentNum, string keyword);
    }
}