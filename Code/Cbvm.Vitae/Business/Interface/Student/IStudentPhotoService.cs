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
    public interface IStudentPhotoService
    {
        StudentPhotoPresentation Get(StudentPhotoCriteria criteria);

        EntityCollection<StudentPhotoPresentation> GetAll(StudentPhotoCriteria criteria);

        ActionResult SetMainPhoto(int photoId,string studentNum);

        ActionResult SaveAll(List<StudentPhotoPresentation> photos, int dictoryId);
    }
}
