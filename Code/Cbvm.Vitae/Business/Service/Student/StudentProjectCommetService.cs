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
    public class StudentProjectCommetService:BaseService,ICommentService
    {
        public EntityCollection<CommentPresentation> GetAll(CommentCriteria criteria)
        {
            var query = dataContext.StudentProjectComment.Where(it => it.ProjectID == criteria.ReferenceId);
            if (criteria.IsFrontRequest)
            {
                query =
                    query.Where(
                        it => it.StudentProject.IsOnline && it.StudentProject.VerfyStatus == (int) VerifyStatus.Passed);
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(ix => ix.ID), criteria, out totalCount);

            var list = query.Select(project => new CommentPresentation()
            {
                Comment = project.Comment,
                CommentTime = project.CreateTime,
                ParentID = project.ParentID,
                ReferenceID = project.ProjectID,
                UserName = project.UserName,
                UserType = (UserType)project.UserType
            }).ToList();

            EntityCollection<CommentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(CommentPresentation presentation)
        {
            var project = dataContext.StudentProject.FirstOrDefault(it => it.ID == presentation.ReferenceID);
            if (project == null)
            {
                return ActionResult.NotFoundResult;
            }

            project.StudentProjectComment.Add(new StudentProjectComment()
            {
                Comment = presentation.Comment,
                CreateTime = DateTime.Now,
                UserName = presentation.UserName,
                UserType = (int) presentation.UserType
            });

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
