using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Student;
using LkDataContext;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Service.Student
{
    public class StudentPhotoCommentService : BaseService, IStudentPhotoCommentService
    {
        public EntityCollection<StudentPhotoCommentPresentation> GetAll(StudentPhotoCommentCriteria criteria)
        {
            var query = from it in dataContext.StudentPhotoComment
                where it.StudentDictory.StudentNum == criteria.StudentNum
                      && it.StudentPhotoId == criteria.PhotoId
                select it;
            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new StudentPhotoCommentPresentation()
            {
                Comment = it.Comment,
                CommentTime = it.CreateTime,
                UserName = it.UserName,
                UserType = (UserType) it.UserType
            }).ToList();

            EntityCollection<StudentPhotoCommentPresentation> entityCollection = Translate2Presentations(list);

            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(StudentPhotoCommentPresentation presentation)
        {
            dataContext.StudentPhotoComment.InsertOnSubmit(new StudentPhotoComment()
            {
                Comment = presentation.Comment,
                CreateTime = DateTime.Now,
                StudentDictoryId = presentation.DictoryId,
                StudentPhotoId = presentation.PhotoId,
                UserType = (int) presentation.UserType,
                UserName = presentation.UserName
            });
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
