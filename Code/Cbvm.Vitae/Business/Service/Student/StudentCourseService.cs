using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Student;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Service.Student
{
    public class StudentCourseService : BaseService, IStudentCourseService
    {
        public StudentCoursePresentation Get(string code)
        {
            var course = dataContext.StudentCourse.FirstOrDefault(it => it.Code == code);
            if (course == null)
            {
                return null;
            }
            return new StudentCoursePresentation()
            {
                Code=course.Code,
                CourseCode = course.CourseCode,
                CourseName = course.Course.Name,
                CourseTypeName = course.Course.CourseType.Name
            };
        }

        public EntityCollection<StudentCoursePresentation> GetAll(StudentCourseCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
