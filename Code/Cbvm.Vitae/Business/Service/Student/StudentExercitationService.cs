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
    public class StudentExercitationService : BaseService, IStudentExercitationService
    {
        public StudentExercitationPresentation Get(StudentExercitationCriteria criteria)
        {
            var activity =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ID == criteria.Id && it.ActivityType1.ActivityDataType ==
                          (int)ActivityDataType.Exercitation);
            if (activity == null)
                return null;
            return Translate2Presentation(activity, true);
        }

        public ActionResult Save(StudentExercitationPresentation presentation)
        {
            var studentActivity =
                dataContext.StudentActivity.FirstOrDefault(
                    it => it.ID == presentation.Id && it.ActivityType1.ActivityDataType ==
                          (int)ActivityDataType.Exercitation);
            if (studentActivity == null)
            {
                var activityType = dataContext.ActivityType.FirstOrDefault(
                    ic => ic.ActivityDataType == (int) ActivityDataType.Exercitation);
                studentActivity = new StudentActivity()
                {
                    CreateTime = DateTime.Now,
                    VerfyStatus = (int) VerifyStatus.WaitAudited,
                    StudentNum = presentation.StudentNum,
                    ActivityType = activityType.Code
                };
                dataContext.StudentActivity.InsertOnSubmit(studentActivity);
            }
            if (studentActivity.VerfyStatus == (int)VerifyStatus.Passed)
            {
                return ActionResult.DefaultResult;
            }
            studentActivity.Title = presentation.Title;
            studentActivity.Content = presentation.Content;
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
                          it.ActivityType1.ActivityDataType == (int)ActivityDataType.Exercitation);

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

        public EntityCollection<StudentExercitationPresentation> GetAll(StudentExercitationCriteria criteria)
        {
            var query = from it in dataContext.StudentActivity
                        where it.StudentNum == criteria.StudentNum &&
                              it.ActivityType1.ActivityDataType == (int)ActivityDataType.Exercitation
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
            query = PageingQueryable(query.OrderByDescending(ix => ix.CreateTime), criteria, out totalCount);

            var list = query.Select(activity => Translate2Presentation(activity, false, false)).ToList();
            EntityCollection<StudentExercitationPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;


            return entityCollection;
        }

        public ActionResult Delete(string studentNum, int id)
        {
            var activity = dataContext.StudentActivity.FirstOrDefault(it => it.ID == id && it.StudentNum == studentNum
                && it.ActivityType1.ActivityDataType == (int)ActivityDataType.Exercitation);
            if (activity == null)
                return ActionResult.CreateErrorActionResult("找不到对应数据!");

            activity.StudentActivityAttachment.Clear();
            activity.StudentActivityComment.Clear();

            dataContext.StudentActivity.DeleteOnSubmit(activity);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SetStatus(string studentNum, int id, bool isOnline)
        {
            var exercitation =
                dataContext.StudentActivity.FirstOrDefault(it => it.ID == id && it.StudentNum == studentNum &&
                                                                   it.ActivityType1.ActivityDataType ==
                                                                   (int) ActivityDataType.Exercitation);
            if (exercitation == null)
            {
                return ActionResult.NotFoundResult;
            }
            exercitation.IsOnline = isOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public StudentExercitationPresentation GetFrontById(string studentNum, int exercitationId)
        {
            var query = from it in GetBaseFrontQuery()
                where it.StudentNum == studentNum && it.ID == exercitationId
                select it;
            var exercitation = query.FirstOrDefault();
            if (exercitation == null)
            {
                return null;
            }
            return Translate2Presentation(exercitation, false);
        }

        public IList<ContentPresentation> GetFrontExercitationList(string studentNum, string keyword)
        {
            var query = from it in GetBaseFrontQuery()
                        where it.StudentNum == studentNum
                        select it;
            if (!String.IsNullOrEmpty(keyword))
            {
                query = from it in query where it.Title.Contains(keyword) select it;
            }

            return query.Select(activity => Translate2ContentPresentation(activity)).ToList();
        }

        public IList<ContentPresentation> GetNewestFrontExercitationList(string studentNum, out int totalCount)
        {
            var query = from it in GetBaseFrontQuery()
                        where it.StudentNum == studentNum
                        select it;
            totalCount = query.Count();

            return query.Take(9).Select(activity => Translate2ContentPresentation(activity)).ToList();
        }

        public IList<StudentExercitationPresentation> GetFrontResumeExercitationList(string studentNum, string keyword)
        {
            var query = from it in GetBaseFrontQuery()
                        where it.StudentNum == studentNum
                        select it;
            if (!String.IsNullOrEmpty(keyword))
            {
                query = from it in query where it.Title.Contains(keyword) select it;
            }

            return query.Select(activity => Translate2Presentation(activity,false,false)).ToList();
        }
 
        public EntityCollection<StudentExercitationPresentation> GetVerifyAll(StudentExercitationCriteria criteria, string teacherNum)
        {
            var query = from it in dataContext.StudentActivity
                        where it.TeacherNum == teacherNum
                              && it.ActivityType1.ActivityDataType == (int)ActivityDataType.Exercitation
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

            var list = query.Select(activity => Translate2Presentation(activity, false, false)).ToList();
            EntityCollection<StudentExercitationPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        private IQueryable<StudentActivity> GetBaseFrontQuery()
        {
            return
                dataContext.StudentActivity.Where(it => it.IsOnline && it.VerfyStatus == (int) VerifyStatus.Passed &&
                                                          it.ActivityType1.ActivityDataType ==
                                                          (int) ActivityDataType.Exercitation);
        }

        private StudentExercitationPresentation Translate2Presentation(StudentActivity activity, bool includeRelativeData,bool includeContent =true)
        {
            var presentation= new StudentExercitationPresentation()
            {
                ActivityType = activity.ActivityType,
                Address = activity.Address,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime,
                EvaluateFromTeacher = activity.EvaluateFromTeacher,
                Content = activity.Content,
                Title = activity.Title,
                IsOnline = activity.IsOnline,
                ReferenceID = activity.ReferenceID,
                Id = activity.ID,
                StudentNum = activity.StudentNum,
                StudentNameZh = activity.Student.NameZh,
                StudentNameEn = activity.Student.NameEn,
                VerfyStatus = (VerifyStatus) activity.VerfyStatus,
                VerifyStatusReason = activity.VerifyStatusReason,
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
                        DocumentType = (DocumentType)it.DocumentType,
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

        private ContentPresentation Translate2ContentPresentation(StudentActivity activity)
        {
            var presentation = new ContentPresentation()
            {
                Identity = activity.ID.ToString(),
                Name = activity.Title,
                ReferenceCode = activity.StudentNum,
                Time = activity.CreateTime
            };

            return presentation;
        }
    }
}
