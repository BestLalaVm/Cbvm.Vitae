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
    public interface IStudentProjectService
    {
        StudentProjectPresentation Get(StudentProjectCriteria criteria);

        EntityCollection<StudentProjectPresentation> GetAll(StudentProjectCriteria criteria);

        ActionResult Save(StudentProjectPresentation presentation);

        ActionResult ChangeVerifyStatus(int projectId, VerifyStatus status,
            string verifyReason, string evaluation, int? skillLevel,
            int? dreativeLevel, int? usableLevel, string teacherNum);

        ActionResult Delete(string studentNum, int id);

        ActionResult SetStatus(string studentNum, int id, bool isOnline);

        StudentProjectPresentation GetFrontProjctById(int id, string studentNum);

        List<StudentProjectPresentation> GetResumeProjectList(string studentNum, string keyword);

        EntityCollection<StudentProjectPresentation> GetVerifyAll(StudentProjectCriteria criteria, string teacherNum);

        List<StudentProjectPresentation> GetTopProjectList(string studentNum, int pageSize = 20);

        EntityCollection<StudentProjectPresentation> GetTeacherVerifyProjectList(string teacherNum,
            VerifyStatus status, int pageSize = 20);

        IList<ContentPresentation> GetNewestFrontProjectList(string studentNum, out int totalCount);

        List<ContentPresentation> GetFrontProjectList(string studentNum, string keyword);
    }
}
