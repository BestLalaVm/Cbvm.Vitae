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
    public class StudentProfessionalService : BaseService, IStudentProfessionalService
    {
        public StudentProfessionalPresentation Get(StudentProfessionalCriteria criteria)
        {
            var profession =
                dataContext.StudentProfessional.FirstOrDefault(it => it.ID == criteria.Id && it.Type == (int)criteria.Type);
            if (profession == null)
            {
                return null;
            }
            var presentation = Translate2Presentation(profession, criteria.IncludeRelativeData);

            return presentation;
        }

        public EntityCollection<StudentProfessionalPresentation> GetAll(StudentProfessionalCriteria criteria)
        {
            var query = from it in dataContext.StudentProfessional where it.Type==(int)criteria.Type select it;

            if (!String.IsNullOrEmpty(criteria.StudentNum))
            {
                query = from it in query where it.StudentNum == criteria.StudentNum select it;
            }

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query where it.ObtainTime.Date >= criteria.DateFrom select it;
            }

            if (criteria.DateTo.HasValue)
            {
                var dateTo = criteria.DateTo.Value.AddDays(1).Date;
                query = from it in query where it.ObtainTime < dateTo select it;
            }

            int verfyStatus = 0;
            if (int.TryParse(criteria.VerfyStatus, out verfyStatus))
            {
                query = from it in query where it.VerfyStatus == verfyStatus select it;
            }

            if (!String.IsNullOrEmpty(criteria.TeacherNum))
            {
                query = from it in query where it.TeacherNum == criteria.TeacherNum select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(ix => ix.CreateTime), criteria, out totalCount);

            var list = query.Select(profession => Translate2Presentation(profession, criteria.IncludeRelativeData)).ToList();

            EntityCollection<StudentProfessionalPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(StudentProfessionalPresentation presentation)
        {
            var professional =
                dataContext.StudentProfessional.FirstOrDefault(
                    it => it.ID == presentation.Id && it.StudentNum == presentation.StudentNum && it.Type==(int)presentation.Type);
            if (professional == null)
            {
                professional = new StudentProfessional()
                {
                    StudentNum = presentation.StudentNum,
                    CreateTime = DateTime.Now,
                    Type=(int)presentation.Type,
                    VerfyStatus = (int)VerifyStatus.WaitAudited
                };
                dataContext.StudentProfessional.InsertOnSubmit(professional);
            }
            professional.Name = presentation.Name;
            professional.ObtainTime = presentation.ObtainTime;
            professional.Description = presentation.Description;
            professional.IsOnline = presentation.IsOnline;
            professional.Type = (int)presentation.Type;
            professional.LastUpdateTime = DateTime.Now;

            if (presentation.VerfyStatus == VerifyStatus.WaitAudited)
            {
                professional.TeacherNum = presentation.TeacherNum;
            }            

            foreach (var attachment in professional.StudentProfessionalAttachment)
            {
                if (presentation.AttachmentPresentations.Any(ic => ic.ID == attachment.ID))
                {
                    dataContext.StudentProfessionalAttachment.DeleteOnSubmit(attachment);
                }
            }

            foreach (var attachment in  presentation.AttachmentPresentations.Where(it => it.IsNew))
            {
                professional.StudentProfessionalAttachment.Add(new StudentProfessionalAttachment()
                {
                    CreateTime = DateTime.Now,
                    Description = attachment.FileLabel,
                    DisplayOrder = attachment.DisplayOrder,
                    DocumentType = (int) attachment.DocumentType,
                    IsMain = attachment.IsMain,
                    Path = attachment.Path,
                    ThumbPath = attachment.ThumbPath
                });
            }

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult Delete(string studentNum, int id)
        {
            var professional =
                dataContext.StudentProfessional.FirstOrDefault(it => it.StudentNum == studentNum && it.ID == id);
            if (professional == null)
            {
                return ActionResult.CreateErrorActionResult("找不到数据!");
            }
            dataContext.StudentProfessionalAttachment.DeleteAllOnSubmit(professional.StudentProfessionalAttachment);
            dataContext.StudentProfessional.DeleteOnSubmit(professional);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SetStatus(string studentNum, int id, bool isOnline)
        {
            var professional =
                dataContext.StudentProfessional.FirstOrDefault(ic => ic.StudentNum == studentNum && ic.ID == id);
            if (professional == null)
            {
                return ActionResult.NotFoundResult;
            }
            professional.IsOnline = isOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public IList<ContentPresentation> GetNewestFrontProfessionalList(string studentNum, out int totalCount)
        {
            var query = from it in GetBaseFrontQuery() where it.StudentNum == studentNum select it;
            totalCount = query.Count();
            return query.Take(9).Select(it => Translate2ContentPresentation(it)).ToList();
        }

        public StudentProfessionalPresentation GetFrontProfessionalById(int id, string studentNum)
        {
            var professional =
                GetBaseFrontQuery().Where(it => it.ID == id && it.StudentNum == studentNum).FirstOrDefault();
            if (professional == null)
            {
                return null;
            }
            return Translate2Presentation(professional, true);
        }

        public IList<ContentPresentation> GetFrontProfessionalList(string studentNum, string keyword)
        {
            var query = GetBaseFrontQuery().Where(it => it.StudentNum == studentNum);
            if (!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(it => it.Name.Contains(keyword));
            }
            return query.Select(it => Translate2ContentPresentation(it)).ToList();
        }

        public IList<StudentProfessionalPresentation> GetFrontResumeProfessionalList(string studentNum, string keyword)
        {
            var query = GetBaseFrontQuery().Where(it => it.StudentNum == studentNum);
            if (!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(it => it.Name.Contains(keyword));
            }
            return query.Select(it => Translate2Presentation(it, false)).ToList();
        }

        private IQueryable<StudentProfessional> GetBaseFrontQuery()
        {
            return dataContext.StudentProfessional.Where(it => it.IsOnline);
        }

        private StudentProfessionalPresentation Translate2Presentation(StudentProfessional profession,bool includeRelativeData)
        {
            var presentation= new StudentProfessionalPresentation()
            {
                Id = profession.ID,
                Name = profession.Name,
                Description = profession.Description,
                LastUpdateTime = profession.LastUpdateTime,
                ObtainTime = profession.ObtainTime,
                IsOnline = profession.IsOnline,
                StudentNum = profession.StudentNum,
                VerfyStatus = (VerifyStatus)profession.VerfyStatus,
                TeacherNum=profession.TeacherNum,
                VerifyStatusReason=profession.VerifyStatusReason,
                EvaluateFromTeacher=profession.EvaluateFromTeacher,
                Type=(ProfessionalType)profession.Type,
                TeacherName = profession.Teacher.NameZh
            };
            if (includeRelativeData)
            {
                presentation.AttachmentPresentations =
                    profession.StudentProfessionalAttachment.Select(it => new AttachmentPresentation()
                    {
                        ID = it.ID,
                        DisplayOrder = it.DisplayOrder,
                        DocumentType = (DocumentType)it.DocumentType,
                        Path = it.Path,
                        ThumbPath = it.ThumbPath,
                        FileLabel = it.Description,
                        IsMain = it.IsMain
                    }).ToList();
            }

            return presentation;
        }

        private ContentPresentation Translate2ContentPresentation(StudentProfessional profession)
        {
            return new ContentPresentation()
            {
                Identity = profession.ID.ToString(),
                Name = profession.Name,
                ReferenceCode = profession.StudentNum,
                Time = profession.CreateTime
            };
        }

        public ActionResult ChangeVerifyStatus(int professionId, VerifyStatus status,ProfessionalType sourceType, string verifyReason, string evaluation, string teacherNum)
        {
            var profession =
                dataContext.StudentProfessional.FirstOrDefault(
                    it => it.ID == professionId && it.TeacherNum == teacherNum);

            if (profession == null)
            {
                return ActionResult.CreateErrorActionResult("找不到对应的数据!");
            }

            profession.EvaluateFromTeacher = evaluation;
            profession.VerfyStatus = (int)status;
            profession.VerifyStatusReason = verifyReason;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
