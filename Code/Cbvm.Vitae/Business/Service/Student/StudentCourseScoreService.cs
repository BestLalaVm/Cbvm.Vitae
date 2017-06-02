using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Student;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Service.Student
{
    public class StudentCourseScoreService : BaseService, IStudentCourseScoreService
    {
        public StudentCourseScorePresentation Get(StudentCourseScoreCriteria criteria)
        {
            var courseScore =
                dataContext.StudentCourseScore.FirstOrDefault(it => it.StudentCourse.StudentNum == criteria.StudentNum
                && it.ID==criteria.Id);
            if (courseScore == null)
            {
                return null;
            }
            return new StudentCourseScorePresentation()
            {
                CourseCode = courseScore.CourseCode,
                CourseScoreType = (CourseScoreType) courseScore.CourseScoreType,
                ExamineTime = courseScore.ExamineTime,
                Score = courseScore.Score,
                StudentCourseCode = courseScore.StudentCourseCode,
                CourseName = courseScore.Course.Name
            };
        }

        public ActionResult Save(StudentCourseScorePresentation presentation)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<StudentCourseScorePresentation> GetAll(StudentCourseScoreCriteria criteria)
        {
            var query = from it in dataContext.StudentCourseScore
                where it.StudentCourse.StudentNum == criteria.StudentNum
                select it;

            if (!String.IsNullOrEmpty(criteria.CourseName))
            {
                query = from it in query where it.Course.Name.Contains(criteria.CourseName.Trim()) select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(courseScore => new StudentCourseScorePresentation()
            {
                CourseCode = courseScore.CourseCode,
                CourseScoreType = (CourseScoreType) courseScore.CourseScoreType,
                ExamineTime = courseScore.ExamineTime,
                Score = courseScore.Score,
                StudentCourseCode = courseScore.StudentCourseCode,
                CourseName = courseScore.Course.Name
            }).ToList();
            EntityCollection<StudentCourseScorePresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }
    }
}
