using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Business.Interface.DepartAdmin;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary;
using Business.Interface.Student;
using Presentation.UIView.Base;
using Presentation.Criteria.Base;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;

namespace Business.Service.Student
{
    public class StudentCareerPlanService : BaseService, IStudentCareerPlanService
    {
        public StudentCareerPlanPresentation Get(int key)
        {
            return dataContext.StudentCareerPlan.Where(it => it.ID == key).Select(it => new StudentCareerPlanPresentation
            {
                ID = it.ID,
                Description = it.Description,
                Title = it.Title,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                LastUpdateTime=it.LastUpdateTime,
                StudentNum = it.StudentNum,
                IsOnline=it.IsOnline,
                IsImplemented=it.IsImplemented
            }).FirstOrDefault();
        }

        public StudentCareerPlanPresentation Get(StudentCareerPlanCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<StudentCareerPlanPresentation> GetAll(StudentCareerPlanCriteria criteria)
        {
            var query = dataContext.StudentCareerPlan.AsQueryable();
            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = query.Where(it => it.Title.Contains(criteria.Title));
            }

            if (criteria.StartDate.HasValue)
            {
                query = query.Where(it => it.StartDate >= criteria.StartDate.Value);
            }

            if (criteria.EndDate.HasValue)
            {
                query = query.Where(it => it.EndDate <= criteria.EndDate.Value);
            }

            if (!String.IsNullOrEmpty(criteria.StudentNum))
            {
                query = query.Where(it => it.StudentNum == criteria.StudentNum);
            }

            query = query.OrderByDescending(it => it.ID);

            var totalCount = 0;
            if (criteria.NeedPaging)
            {
                totalCount = query.Count();
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }

            var result = Translate2Presentations(query.Select(it => new StudentCareerPlanPresentation()
            {
                ID = it.ID,
                //Description = it.Description,
                Title = it.Title,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                LastUpdateTime = it.LastUpdateTime,
                StudentNum = it.StudentNum,
                IsOnline = it.IsOnline,
                IsImplemented = it.IsImplemented
            }).ToList());
            result.TotalCount = totalCount;

            return result;
        }

        public IQueryable<StudentCareerPlanPresentation> GetQuery()
        {

            var query = dataContext.StudentCareerPlan.Select(it => new StudentCareerPlanPresentation
            {
                ID = it.ID,
                Description = it.Description,
                Title = it.Title,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                LastUpdateTime = it.LastUpdateTime,
                StudentNum = it.StudentNum,
                IsOnline = it.IsOnline,
                IsImplemented = it.IsImplemented
            }).AsQueryable();


            return query;
        }

        public ActionResult Save(StudentCareerPlanPresentation presentation)
        {
            var data = dataContext.StudentCareerPlan.Where(it => it.ID == presentation.ID).FirstOrDefault();
            if (data == null)
            {
                data = new LkDataContext.StudentCareerPlan()
                {
                    CreateTime = DateTime.Now
                };

                dataContext.StudentCareerPlan.InsertOnSubmit(data);
            }
            data.Title = presentation.Title;
            data.Description = presentation.Description;
            data.LastUpdateTime = DateTime.Now;
            data.StartDate = presentation.StartDate;
            data.EndDate = presentation.EndDate;
            data.StudentNum = presentation.StudentNum;
            data.IsImplemented = presentation.IsImplemented;
            data.IsOnline = presentation.IsOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SaveAll(IList<StudentCareerPlanPresentation> presentations)
        {
            presentations.ToList().ForEach(item =>
            {
                Save(item);
            });

            return ActionResult.DefaultResult;
        }

        public ActionResult DeleteByTKey(int key)
        {
            var data = dataContext.StudentCareerPlan.FirstOrDefault(it => it.ID == key);

            if (data != null)
            {
                dataContext.StudentCareerPlan.DeleteOnSubmit(data);

                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }
    }
}
