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
    public interface IStudentDailyEssayService
    {
        StudentDailyEssayPresentation Get(StudentDailyEssayCriteria criteria);

        EntityCollection<StudentDailyEssayPresentation> GetAll(StudentDailyEssayCriteria criteria);

        ActionResult Save(StudentDailyEssayPresentation presentation);

        ActionResult Delete(string studentNum, int id);

        ActionResult SetStatus(string studentNum, int id,bool isOnline);

        IList<ContentPresentation> GetNewestFrontDailyEssayList(string studentNum, out int totalCount);

        IList<ContentPresentation> GetFrontDailyEssayList(string studentNum, string keyword);

        StudentDailyEssayPresentation GetFrontDailyEssayById(int id, string studentNum);
    }
}
