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
using Business.Interface.College;
using Presentation.UIView.Base;
using Presentation.Criteria.Base;
using Presentation.Criteria.College;
using Presentation.UIView.College;

namespace Business.Service.College
{
    public class AssessmentService : BaseService, IAssessmentService
    {
        public AssessmentPresentation Get(int key)
        {
            return dataContext.Assessment.Where(it => it.ID == key).Select(it => new AssessmentPresentation
            {
                ID = it.ID,
                Description = it.Description,
                Title = it.Title,
                LastUpdator = it.LastUpdator,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                CollegeCode=it.CollegeCode,
                StudentNum = it.StudentNum,
                StudentName = it.Student.NameZh
            }).FirstOrDefault();
        }

        public AssessmentPresentation Get(AssessmentCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<AssessmentPresentation> GetAll(AssessmentCriteria criteria)
        {
            var query = dataContext.Assessment.AsQueryable();
            if (!String.IsNullOrEmpty(criteria.CollegeCode))
            {
                query = query.Where(it => it.CollegeCode == criteria.CollegeCode);
            }

            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = query.Where(it => it.Title.Contains(criteria.Title));
            }

            if (criteria.AssessType.HasValue)
            {
                query = query.Where(it => it.AssessType == (int)criteria.AssessType.Value);
            }

            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.StartDate >= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                query = query.Where(it => it.EndDate <= criteria.DateTo.Value);
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

            var result = Translate2Presentations(query.Select(it => new AssessmentPresentation()
            {
                ID = it.ID,
                Description = it.Description,
                Title = it.Title,
                LastUpdator = it.LastUpdator,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                CollegeCode = it.CollegeCode,
                StudentNum = it.StudentNum,
                StudentName=it.Student.NameZh
            }).ToList());
            result.TotalCount = totalCount;

            return result;
        }

        public IQueryable<AssessmentPresentation> GetQuery()
        {

            var query = dataContext.Assessment.Select(it => new AssessmentPresentation
            {
                ID = it.ID,
                Description = it.Description,
                Title = it.Title,
                LastUpdator = it.LastUpdator,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                CollegeCode = it.CollegeCode,
                StudentNum = it.StudentNum,
                StudentName = it.Student.NameZh
            }).AsQueryable();


            return query;
        }

        public ActionResult Save(AssessmentPresentation presentation)
        {
            var data = dataContext.Assessment.Where(it => it.ID == presentation.ID).FirstOrDefault();
            if (data == null)
            {
                data = new LkDataContext.Assessment()
                {
                    CreateTime = DateTime.Now,
                    Creator=presentation.LastUpdator,
                    CollegeCode = presentation.CollegeCode,
                    AssessType=(int)presentation.AssessType
                };

                dataContext.Assessment.InsertOnSubmit(data);
            }
            data.Title = presentation.Title;
            data.Description = presentation.Description;
            data.LastUpdator = presentation.LastUpdator;
            data.LastUpdateTime = DateTime.Now;
            data.StartDate = presentation.StartDate;
            data.EndDate = presentation.EndDate;
            data.StudentNum = presentation.StudentNum;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SaveAll(IList<AssessmentPresentation> presentations)
        {
            presentations.ToList().ForEach(item =>
            {
                Save(item);
            });

            return ActionResult.DefaultResult;
        }

        public ActionResult DeleteByTKey(int key)
        {
            var data = dataContext.Assessment.FirstOrDefault(it => it.ID == key);

            if (data != null)
            {
                dataContext.Assessment.DeleteOnSubmit(data);

                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }
    }
}
