using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Business.Interface.Student;
using LkDataContext;
using Presentation.Criteria;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Service.Student
{
    public class StudentDailyEssayService : BaseService, IStudentDailyEssayService
    {
        public StudentDailyEssayPresentation Get(StudentDailyEssayCriteria criteria)
        {
            var dailyEssay = dataContext.StudentDailyEssay.FirstOrDefault(it => it.ID == criteria.Id);
            if (dailyEssay == null)
            {
                return null;
            }

            var presentation = new StudentDailyEssayPresentation()
            {
                Id = dailyEssay.ID,
                Title = dailyEssay.Title,
                Content = dailyEssay.Content,
                CreateTime = dailyEssay.CreateTime,
                IsOnline = dailyEssay.IsOnline,
                StudentNum = dailyEssay.StudentNum
            };
            if (criteria.IncludeRelativeData)
            {
                presentation.CommentPresentations =
                    dailyEssay.StudentDailyEssayComment.Select(it => new CommentPresentation()
                    {
                        Comment = it.Comment,
                        CommentTime = it.CreateTime,
                        ParentID = it.ParentID,
                        UserName = it.UserName,
                        UserType = (UserType) it.UserType
                    }).ToList();
            }

            return presentation;
        }

        public EntityCollection<StudentDailyEssayPresentation> GetAll(StudentDailyEssayCriteria criteria)
        {
            var query = from it in dataContext.StudentDailyEssay where it.StudentNum == criteria.StudentNum select it;
            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = from it in query where it.Title.Contains(criteria.Title.Trim()) select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query where it.CreateTime >= criteria.DateFrom select it;
            }

            if (criteria.DateTo.HasValue)
            {
                var dateTo = criteria.DateTo.Value.AddDays(1).Date;
                query = from it in query where it.CreateTime < dateTo select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(ix=>ix.CreateTime), criteria, out totalCount);

            var list = query.Select(dailyEssay => Translate2Presentation(dailyEssay, false, false)).ToList();
            EntityCollection<StudentDailyEssayPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(StudentDailyEssayPresentation presentation)
        {
            var dailyEssay =
                dataContext.StudentDailyEssay.FirstOrDefault(
                    it => it.ID == presentation.Id && it.StudentNum == presentation.StudentNum);
            if (dailyEssay == null)
            {
                dailyEssay = new StudentDailyEssay()
                {
                    StudentNum = presentation.StudentNum,
                    CreateTime = DateTime.Now
                };
                dataContext.StudentDailyEssay.InsertOnSubmit(dailyEssay);
            }
            dailyEssay.Content = presentation.Content;
            dailyEssay.Title = presentation.Title;
            dailyEssay.IsOnline = presentation.IsOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult Delete(string studentNum, int id)
        {
            var daily = dataContext.StudentDailyEssay.FirstOrDefault(it => it.ID == id && it.StudentNum == studentNum);
            if (daily == null)
            {
                return ActionResult.CreateErrorActionResult("找不到数据!");
            }

            dataContext.StudentDailyEssay.DeleteOnSubmit(daily);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public StudentDailyEssayPresentation GetFrontDailyEssayById(int id, string studentNum)
        {
            var dailyEssay =
                dataContext.StudentDailyEssay.FirstOrDefault(
                    it => it.IsOnline && it.StudentNum == studentNum && it.ID == id);
            if (dailyEssay == null)
            {
                return null;
            }
            return Translate2Presentation(dailyEssay,false, true);
        }

        public ActionResult SetStatus(string studentNum, int id, bool isOnline)
        {
            var dailyEssay =
                dataContext.StudentDailyEssay.FirstOrDefault(it => it.StudentNum == studentNum && it.ID == id);
            if (dailyEssay == null)
            {
                return ActionResult.NotFoundResult;
            }
            dailyEssay.IsOnline = isOnline;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public IList<ContentPresentation> GetFrontDailyEssayList(string studentNum, string keyword)
        {
            var query = from it in GetBaseFontQuery()
                        where it.StudentNum == studentNum
                        select it;
            if (!String.IsNullOrEmpty(keyword))
            {
                query = from it in query where it.Title.Contains(keyword) && it.Content.Contains(keyword) select it;
            }

            var list = query.Select(dailyEssay => Translate2ContentPresentation(dailyEssay)).ToList();

            return list;
        }

        public IList<ContentPresentation> GetNewestFrontDailyEssayList(string studentNum, out int totalCount)
        {
            var query = GetBaseFontQuery().Where(it => it.StudentNum == studentNum);
            totalCount = query.Count();

            return query.OrderByDescending(it=>it.CreateTime).Take(9).Select(it => Translate2ContentPresentation(it)).ToList();
        }

        private IQueryable<StudentDailyEssay> GetBaseFontQuery()
        {
            var query = from it in dataContext.StudentDailyEssay where it.IsOnline select it;
            return query;
        }

        private StudentDailyEssayPresentation Translate2Presentation(StudentDailyEssay dailyEssay,bool includeRelativeData,bool includeContent =true)
        {
            var presentation= new StudentDailyEssayPresentation()
            {
                Id = dailyEssay.ID,
                Title = dailyEssay.Title,
                CreateTime = dailyEssay.CreateTime,
                IsOnline = dailyEssay.IsOnline,
                StudentNum = dailyEssay.StudentNum
            };
            if (includeContent)
            {
                presentation.Content = dailyEssay.Content;
            }
            return presentation;
        }

        private ContentPresentation Translate2ContentPresentation(StudentDailyEssay dailyEssay)
        {
            var presentation = new ContentPresentation()
            {
                Identity = dailyEssay.ID.ToString(),
                Name = dailyEssay.Title,
                ReferenceCode = dailyEssay.StudentNum,
                Time = dailyEssay.CreateTime
            };

            return presentation;
        }
    }
}
 