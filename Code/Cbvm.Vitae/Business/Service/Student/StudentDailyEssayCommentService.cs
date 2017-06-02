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
    public class StudentDailyEssayCommentService:BaseService,ICommentService
    {
        public EntityCollection<CommentPresentation> GetAll(CommentCriteria criteria)
        {
            var query = dataContext.StudentDailyEssayComment.Where(it => it.DailyEssayID == criteria.ReferenceId);
            if (criteria.IsFrontRequest)
            {
                query =
                    query.Where(it => it.StudentDailyEssay.IsOnline);
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(ix => ix.ID), criteria, out totalCount);

            var list = query.Select(daily => new CommentPresentation()
            {
                Comment = daily.Comment,
                CommentTime = daily.CreateTime,
                ParentID = daily.ParentID,
                ReferenceID = daily.DailyEssayID,
                UserName = daily.UserName,
                UserType = (UserType)daily.UserType
            }).ToList();

            EntityCollection<CommentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(CommentPresentation presentation)
        {
            var project = dataContext.StudentDailyEssay.FirstOrDefault(it => it.ID == presentation.ReferenceID);
            if (project == null)
            {
                return ActionResult.NotFoundResult;
            }

            project.StudentDailyEssayComment.Add(new StudentDailyEssayComment()
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
