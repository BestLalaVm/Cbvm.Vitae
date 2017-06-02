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
    public class StudentProjectService : BaseService, IStudentProjectService
    {
        public StudentProjectPresentation Get(StudentProjectCriteria criteria)
        {
            var project = dataContext.StudentProject.FirstOrDefault(it => it.ID == criteria.Id);
            if (project == null)
            {
                return null;
            }

            var presentation = Translate2Presentation(project, criteria.IncludeRelativeData);

            return presentation;
        }

        public EntityCollection<StudentProjectPresentation> GetAll(StudentProjectCriteria criteria)
        {
            var query = from it in dataContext.StudentProject where it.StudentNum == criteria.StudentNum select it;

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
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

            var list = query.Select(project => Translate2Presentation(project, criteria.IncludeRelativeData)).ToList();
            EntityCollection<StudentProjectPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;


            return entityCollection;
        }

        public ActionResult Save(StudentProjectPresentation presentation)
        {
            var studentProject =
                dataContext.StudentProject.FirstOrDefault(
                    it => it.ID == presentation.Id && it.StudentNum == presentation.StudentNum);
            if (studentProject == null)
            {
                studentProject = new StudentProject()
                {
                    CreateTime = DateTime.Now,
                    VerfyStatus = (int) VerifyStatus.WaitAudited,
                    StudentNum = presentation.StudentNum
                };
                dataContext.StudentProject.InsertOnSubmit(studentProject);
            }
            if (studentProject.VerfyStatus == (int) VerifyStatus.Passed)
            {
                return ActionResult.DefaultResult;
            }

            studentProject.Name = presentation.Name;
            studentProject.Description = presentation.Description;
            studentProject.TeacherNum = presentation.TeacherNum;
            studentProject.StudentNum = presentation.StudentNum;
            studentProject.BeginTime = presentation.BeginTime;
            studentProject.EndTime = presentation.EndTime;
            studentProject.IsOnline = presentation.IsOnline;
            studentProject.Evaluate = presentation.Evaluate;
            studentProject.Position = presentation.Position;
            studentProject.PositionDescrition = presentation.PositionDescrition;
            studentProject.TeamDescription = presentation.TeamDescription;

            foreach (var attachment in studentProject.StudentProjectAttachment)
            {
                if (!presentation.AttachmentPresentations.Any(ic => ic.ID == attachment.ID))
                {
                    dataContext.StudentProjectAttachment.DeleteOnSubmit(attachment);
                }
            }

            foreach (var attachment in presentation.AttachmentPresentations.Where(ic=>ic.IsNew))
            {
                studentProject.StudentProjectAttachment.Add(new StudentProjectAttachment()
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

        public ActionResult ChangeVerifyStatus(int projectId, VerifyStatus status,
            string verifyReason, string evaluation, int? skillLevel,
            int? dreativeLevel, int? usableLevel, string teacherNum)
        {
            var studentProject =
                dataContext.StudentProject.FirstOrDefault(
                    it => it.ID == projectId && it.TeacherNum == teacherNum);

            if (studentProject == null)
            {
                return ActionResult.CreateErrorActionResult("找不到对应的数据!");
            }

            studentProject.EvaluateFromTeacher = evaluation;
            studentProject.VerfyStatus = (int) status;
            studentProject.VerifyStatusReason = verifyReason;
            if (skillLevel.HasValue)
            {
                studentProject.SkillLevel = skillLevel.Value;
            }
            if (dreativeLevel.HasValue)
            {
                studentProject.DreativeLevel = dreativeLevel.Value;
            }
            if (usableLevel.HasValue)
            {
                studentProject.UsableLevel = usableLevel.Value;
            }

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult Delete(string studentNum, int id)
        {
            var project = dataContext.StudentProject.FirstOrDefault(it => it.ID == id && it.StudentNum == studentNum);
            if (project == null)
                return ActionResult.CreateErrorActionResult("找不到对应数据!");

            project.StudentProjectAttachment.Clear();
            project.StudentProjectComment.Clear();

            dataContext.StudentProject.DeleteOnSubmit(project);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public IList<ContentPresentation> GetNewestFrontProjectList(string studentNum, out int totalCount)
        {
            var query = from it in GetBaseFrontQuery() where it.StudentNum == studentNum select it;
            totalCount = query.Count();
            return query.Take(9).Select(it => Translate2ContentPresentation(it)).ToList();
        }

        public StudentProjectPresentation GetFrontProjctById(int id, string studentNum)
        {
            var project = GetBaseFrontQuery().FirstOrDefault(it => it.StudentNum == studentNum && it.ID == id);
            return Translate2Presentation(project, true);
        }

        public List<ContentPresentation> GetFrontProjectList(string studentNum, string keyword)
        {
            var query = GetBaseFrontQuery().Where(it => it.StudentNum == studentNum);
            if (!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(it => it.Name.Contains(keyword));
            }
            return query.Select(it => Translate2ContentPresentation(it)).ToList();
        }

        public List<StudentProjectPresentation> GetResumeProjectList(string studentNum, string keyword)
        {
            var query = from it in GetBaseFrontQuery() where it.StudentNum==studentNum select it;

            if (!String.IsNullOrEmpty(keyword))
            {
                query = from it in query where it.Name.Contains(keyword) select it;
            }
            return query.Select(it => Translate2Presentation(it, false)).ToList();
        }

        private IQueryable<StudentProject> GetBaseFrontQuery()
        {
            return dataContext.StudentProject.Where(it => it.IsOnline && it.VerfyStatus == (int) VerifyStatus.Passed);
        }

        private StudentProjectPresentation Translate2Presentation(StudentProject project,bool includeRelativeData)
        {
            if (project == null) return null;
            var presentation = new StudentProjectPresentation()
            {
                Name = project.Name,
                Description = project.Description,
                TeacherNum = project.TeacherNum,
                StudentNum = project.StudentNum,
                BeginTime = project.BeginTime,
                EndTime = project.EndTime,
                Id = project.ID,
                IsOnline = project.IsOnline,
                Evaluate = project.Evaluate,
                EvaluateFromTeacher = project.EvaluateFromTeacher,
                Position = project.Position,
                PositionDescrition = project.PositionDescrition,
                SkillLevel = project.SkillLevel,
                DreativeLevel = project.DreativeLevel,
                UsableLevel = project.UsableLevel,
                VerfyStatus = (VerifyStatus) project.VerfyStatus,
                VerifyStatusReason = project.VerifyStatusReason,
                StudentNameEn = project.Student.NameEn,
                StudentNameZh = project.Student.NameZh,
                TeamDescription = project.TeamDescription,
                TeacherName = project.Teacher.NameZh
            };
            if (includeRelativeData)
            {
                presentation.AttachmentPresentations =
                    project.StudentProjectAttachment.Select(it => new AttachmentPresentation()
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
                    project.StudentProjectComment.Select(it => new CommentPresentation()
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
            var project = dataContext.StudentProject.FirstOrDefault(it => it.StudentNum == studentNum && it.ID == id);
            if (project == null)
            {
                return ActionResult.NotFoundResult;
            }
            project.IsOnline = isOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<StudentProjectPresentation> GetVerifyAll(StudentProjectCriteria criteria,
            string teacherNum)
        {
            var query = from it in dataContext.StudentProject where it.TeacherNum == teacherNum select it;
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.VerfyStatus))
            {
                int verfyStatus = 0;
                if (int.TryParse(criteria.VerfyStatus, out verfyStatus))
                {
                    query = from it in query where it.VerfyStatus == verfyStatus select it;
                }
            }
            if (criteria.DateFrom.HasValue)
            {
                query = from it in query
                    where it.BeginTime.Date >= criteria.DateFrom && it.EndTime.Date >= criteria.DateFrom
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

            var list = query.Select(project => Translate2Presentation(project, criteria.IncludeRelativeData)).ToList();
            EntityCollection<StudentProjectPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public List<StudentProjectPresentation> GetTopProjectList(string studentNum, int pageSize = 20)
        {
            var query = from it in dataContext.StudentProject
                where it.IsOnline && it.VerfyStatus == (int) VerifyStatus.Passed && it.StudentNum == studentNum
                select it;
            return
                query.OrderByDescending(ix => ix.ID)
                    .Take(pageSize)
                    .Select(ix => Translate2Presentation(ix, false))
                    .ToList();
        }

        public EntityCollection<StudentProjectPresentation> GetTeacherVerifyProjectList(string teacherNum,
            VerifyStatus status, int pageSize = 20)
        {
            var query = from it in dataContext.StudentProject
                where it.IsOnline && it.VerfyStatus == (int) status
                select it;
            var totalCount = query.Count();
            var list = query.Take(pageSize).Select(ix => Translate2Presentation(ix, false)).ToList();

            EntityCollection<StudentProjectPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        private ContentPresentation Translate2ContentPresentation(StudentProject project)
        {
            return new ContentPresentation()
            {
                Identity = project.ID.ToString(),
                ReferenceCode = project.StudentNum,
                Name = project.Name,
                Time = project.CreateTime
            };
        }
    }
}
