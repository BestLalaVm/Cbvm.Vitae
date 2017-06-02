using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using Presentation.UIView.Student.View;

namespace Business.Interface.Student
{
    public interface IStudentService : IOrigianlBaseService
    {
        StudentPresentation Get(string studentNum);

        ActionResult Save(StudentPresentation student);

        StudentIntroducePresentation GetIntroduce(string studentNum);

        ActionResult SaveIntroduce(StudentIntroducePresentation studentIntroduce);

        EntityCollection<StudentPresentation> GetAll(StudentCriteria criteria);

        StudentFrontPresentation GetFront(string studentNum);

        EntityCollection<StudentFrontPresentation> GetSearchFrontStudentList(StudentFontAdvanceCriteria criteria);

        EntityCollection<StudentCommonPresentation> GetCommonAll(StudentCriteria criteria);

        EntityCollection<StudentCommonPresentation> GetReferralAll(string jobCode,int pageSize,int pageIndex);

        EntityCollection<StudentFrontPresentation> GetFrontStudentList(StudentSearchType type, int pageSize,
            UserType? userType);
    }
}
