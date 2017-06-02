using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using LkDataContext;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;

namespace Business.Service.Student
{
    public class StudentActivityCommentService:BaseService,ICommentService
    {
        public EntityCollection<CommentPresentation> GetAll(CommentCriteria criteria)
        {
            var query =
                dataContext.StudentActivityComment.Where(
                    it => it.StudentActivity.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation &&
                          it.ActivityID == criteria.ReferenceId);
            int totalCount;
            query = PageingQueryable(query.OrderByDescending(ix => ix.ID), criteria, out totalCount);

            var list = query.Select(Comment => new CommentPresentation()
            {
                Comment = Comment.Comment,
                CommentTime = Comment.CreateTime,
                ParentID = Comment.ParentID,
                ReferenceID = Comment.ActivityID,
                UserName = Comment.UserName,
                UserType = (UserType)Comment.UserType
            }).ToList();

            EntityCollection<CommentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;

        }

        public ActionResult Save(CommentPresentation presentation)
        {
            var exercitation =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation &&
                          it.ID == presentation.ReferenceID);
            if (exercitation == null)
            {
                return ActionResult.NotFoundResult;
            }
            exercitation.StudentActivityComment.Add(new StudentActivityComment()
            {
                Comment = presentation.Comment,
                CreateTime = DateTime.Now,
                UserName = presentation.UserName,
                UserType = (int)presentation.UserType
            });
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
