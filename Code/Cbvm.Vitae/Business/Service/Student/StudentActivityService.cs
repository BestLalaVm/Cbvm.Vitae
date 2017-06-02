using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class StudentActivityService : BaseService, IStudentActivityService
    {
        public StudentActivityPresentation Get(StudentActivityCriteria criteria)
        {
            var activity =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ID == criteria.Id && it.ActivityType1.ActivityDataType !=
                          (int) ActivityDataType.Exercitation);
            if (activity == null)
                return null;

            return Translate2ActivityPresentation(activity, criteria.IncludeRelativeData);
        }

        public ActionResult Save(StudentActivityPresentation presentation)
        {
            var studentActivity =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ID == presentation.Id && it.ActivityType1.ActivityDataType !=
                          (int)ActivityDataType.Exercitation);
            if (studentActivity == null)
            {
                studentActivity = new StudentActivity()
                {
                    CreateTime = DateTime.Now,
                    VerfyStatus = (int)VerifyStatus.WaitAudited,
                    StudentNum = presentation.StudentNum
                };
                dataContext.StudentActivity.InsertOnSubmit(studentActivity);
            }
            if (studentActivity.VerfyStatus == (int) VerifyStatus.Passed)
            {
                return ActionResult.DefaultResult;
            }
            studentActivity.Title = presentation.Title;
            studentActivity.Content = presentation.Content;
            studentActivity.ActivityType = presentation.ActivityType;
            studentActivity.Address = presentation.Address;
            studentActivity.BeginTime = presentation.BeginTime;
            studentActivity.EndTime = presentation.EndTime;
            studentActivity.IsOnline = presentation.IsOnline;
            studentActivity.TeacherNum = presentation.TeacherNum;

            foreach (var attachment in studentActivity.StudentActivityAttachment)
            {
                if (!presentation.AttachmentPresentations.Any(ic => ic.ID == attachment.ID))
                {
                    dataContext.StudentActivityAttachment.DeleteOnSubmit(attachment);
                }
            }

            foreach (var attachment in presentation.AttachmentPresentations.Where(ic => ic.IsNew))
            {
                studentActivity.StudentActivityAttachment.Add(new StudentActivityAttachment()
                {
                    CreateTime = DateTime.Now,
                    Description = attachment.FileLabel,
                    DisplayOrder = attachment.DisplayOrder,
                    Path = attachment.Path,
                    ThumbPath = attachment.ThumbPath,
                    IsMain = attachment.IsMain,
                    DocumentType = (int) attachment.DocumentType
                });
            }

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult ChangeVerifyStatus(int activityId, VerifyStatus status,
            string verifyReason, string evaluation, string teacherNum)
        {
            var studentActivity =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ID == activityId && it.TeacherNum == teacherNum &&
                          it.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation);

            if (studentActivity == null)
            {
                return ActionResult.CreateErrorActionResult("找不到对应的数据!");
            }

            studentActivity.EvaluateFromTeacher = evaluation;
            studentActivity.VerfyStatus = (int)status;
            studentActivity.VerifyStatusReason = verifyReason;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<StudentActivityPresentation> GetAll(StudentActivityCriteria criteria)
        {
            var query = from it in dataContext.StudentActivity
                        where it.StudentNum == criteria.StudentNum &&
                              it.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation
                        select it;

            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = from it in query where it.Title.Contains(criteria.Title.Trim()) select it;
            }

            int verfyStatus = 0;
            if (int.TryParse(criteria.VerfyStatus, out verfyStatus))
            {
                query = from it in query where it.VerfyStatus == verfyStatus select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query
                        where it.BeginTime.Date >= criteria.DateFrom
                              && it.EndTime.Date >= criteria.DateFrom
                        select it;
            }

            if (criteria.DateTo.HasValue)
            {
                var dateTo = criteria.DateTo.Value.AddDays(1).Date;
                query = from it in query
                        where it.EndTime.Date < dateTo && dateTo > it.BeginTime.Date
                        select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(ix=>ix.CreateTime), criteria, out totalCount);

            var list = query.Select(activity => Translate2ActivityPresentation(activity,false,false)).ToList();
            EntityCollection<StudentActivityPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Delete(string studentNum, int id)
        {
            var activity = dataContext.StudentActivity.FirstOrDefault(it => it.ID == id && it.StudentNum == studentNum
                && it.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation);
            if (activity == null)
                return ActionResult.CreateErrorActionResult("找不到对应数据!");

            activity.StudentActivityAttachment.Clear();
            activity.StudentActivityComment.Clear();

            dataContext.StudentActivity.DeleteOnSubmit(activity);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public StudentActivityPresentation GetFrontActivityById(int id, string studentNum)
        {
            var activity = GetBaseFrontQuery().FirstOrDefault(it => it.StudentNum == studentNum && it.ID == id);
            if (activity == null)
            {
                return null;
            }
            return Translate2ActivityPresentation(activity, true);
        }

        public IList<StudentActivityPresentation> GetResumeActivityList(string studentNum, string keyword)
        {
            var query = from it in GetBaseFrontQuery() where it.StudentNum == studentNum select it;
            if (!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(it => it.Title.Contains(keyword.Trim())).Select(it => it);
            }

            return query.Select(it => Translate2ActivityPresentation(it, false,false)).ToList();
        } 

        private IQueryable<StudentActivity> GetBaseFrontQuery()
        {
            var query = from it in dataContext.StudentActivity
                where
                    it.IsOnline && it.VerfyStatus == (int) VerifyStatus.Passed &&
                    it.ActivityType1.ActivityDataType != (int) ActivityDataType.Exercitation
                select it;

            return query;
        }

        private StudentActivityPresentation Translate2ActivityPresentation(StudentActivity activity,
            bool includeRelativeData,bool includeContent=true)
        {
            var presentation = new StudentActivityPresentation()
            {
                ActivityType = activity.ActivityType,
                Address = activity.Address,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime,
                EvaluateFromTeacher = activity.EvaluateFromTeacher,
                Title = activity.Title,
                IsOnline = activity.IsOnline,
                ReferenceID = activity.ReferenceID,
                Id = activity.ID,
                StudentNameEn = activity.Student.NameEn,
                StudentNameZh = activity.Student.NameZh,
                StudentNum = activity.StudentNum,
                VerfyStatus = (VerifyStatus) activity.VerfyStatus,
                VerifyStatusReason = activity.VerifyStatusReason,
                TeacherNum = activity.TeacherNum,
                TeacherName = activity.Teacher.NameZh
            };

            if (includeContent)
            {
                presentation.Content = activity.Content;
            }

            if (includeRelativeData)
            {
                presentation.AttachmentPresentations =
                    activity.StudentActivityAttachment.Select(it => new AttachmentPresentation()
                    {
                        ID = it.ID,
                        DisplayOrder = it.DisplayOrder,
                        DocumentType = (DocumentType) it.DocumentType,
                        Path = it.Path,
                        ThumbPath = it.ThumbPath,
                        FileLabel = it.Description,
                        IsMain = it.IsMain
                    }).ToList();
                presentation.CommentPresentations =
                    activity.StudentActivityComment.Select(it => new CommentPresentation()
                    {
                        Comment = it.Comment,
                        CommentTime = it.CreateTime,
                        ParentID = it.ParentID,
                        UserName = it.UserName,
                        UserType = (UserType)it.UserType
                    }).ToList();
            }

            return presentation;
        }

        public ActionResult SetStatus(string studentNum, int id, bool isOnline)
        {
            var activity =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ID == id && it.ActivityType1.ActivityDataType !=
                          (int) ActivityDataType.Exercitation && it.StudentNum == studentNum);
            if (activity == null)
            {
                return ActionResult.NotFoundResult;
            }
            activity.IsOnline = isOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<StudentActivityPresentation> GetVerifyAll(StudentActivityCriteria criteria, string teacherNum)
        {
            var query = from it in dataContext.StudentActivity
                where it.TeacherNum == teacherNum
                      && it.ActivityType1.ActivityDataType != (int) ActivityDataType.Exercitation
                select it;

            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = from it in query where it.Title.Contains(criteria.Title.Trim()) select it;
            }

            int verfyStatus = 0;
            if (int.TryParse(criteria.VerfyStatus, out verfyStatus))
            {
                query = from it in query where it.VerfyStatus == verfyStatus select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query
                        where it.BeginTime.Date >= criteria.DateFrom
                              && it.EndTime.Date >= criteria.DateFrom
                        select it;
            }

            if (criteria.DateTo.HasValue)
            {
                var dateTo = criteria.DateTo.Value.AddDays(1).Date;
                query = from it in query
                        where it.EndTime.Date < dateTo && dateTo > it.BeginTime.Date
                        select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(activity => Translate2ActivityPresentation(activity, false,false)).ToList();
            EntityCollection<StudentActivityPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public IList<ContentPresentation> GetFrontActivityList(string studentNum, string keyword)
        {
            var query = GetBaseFrontQuery().Where(it => it.StudentNum == studentNum);
            if (!String.IsNullOrEmpty(keyword))
            {
                query = from it in query where it.Title.Contains(keyword) select it;
            }
            return query.Select(it => Translate2ContentPresentation(it)).ToList();
        }

        public IList<ContentPresentation> GetNewestFrontActivityList(string studentNum, out int totalCount)
        {
            var query = GetBaseFrontQuery().Where(it => it.StudentNum == studentNum);
            totalCount = query.Count();
            return query.Take(9).Select(it => Translate2ContentPresentation(it)).ToList();
        }

        private ContentPresentation Translate2ContentPresentation(StudentActivity activity)
        {
            return new ContentPresentation()
            {
                Identity = activity.ID.ToString(),
                ReferenceCode = activity.StudentNum,
                Name = activity.Title,
                Time = activity.CreateTime
            };
        }
    }
}
