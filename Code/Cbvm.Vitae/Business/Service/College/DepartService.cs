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

namespace Business.Service.College
{
    public class DepartService : BaseService, IDepartService
    {
        public DepartPresentation Get(string key)
        {
            return dataContext.Depart.Where(it => it.Code == key).Select(it => new DepartPresentation
            {
                Code = it.Code,
                Description = it.Description,
                Name = it.Name
            }).FirstOrDefault();
        }

        public DepartPresentation Get(DepartCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<DepartPresentation> GetAll(DepartCriteria criteria)
        {
            var query = dataContext.Depart.Where(it => it.CollegeCode == criteria.CollegeCode);
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.Name.Contains(criteria.Name));
            }
            query = query.OrderByDescending(it => it.ID);

            var totalCount = 0;
            if (criteria.NeedPaging)
            {
                totalCount = query.Count();
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }

            var result = Translate2Presentations(query.Select(it => new DepartPresentation()
            {
                Code = it.Code,
                Description = it.Description,
                Name = it.Name,
                CollegeCode = it.CollegeCode
            }).ToList());
            result.TotalCount = totalCount;

            return result;
        }

        public IQueryable<DepartPresentation> GetQuery()
        {

            var query = dataContext.Depart.Select(it => new DepartPresentation
            {
                Code = it.Code,
                Description = it.Description,
                Name = it.Name,
                CollegeCode = it.CollegeCode
            }).AsQueryable();


            return query;
        }

        public ActionResult Save(DepartPresentation presentation)
        {
            if (String.IsNullOrEmpty(presentation.Code))
            {
                presentation.Code = GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.Depart, null);
            }
            var data = dataContext.Depart.Where(it => it.Code == presentation.Code).FirstOrDefault();
            if (data == null)
            {
                data = new LkDataContext.Depart()
                {
                    Code = presentation.Code,
                    CollegeCode = presentation.CollegeCode
                };

                dataContext.Depart.InsertOnSubmit(data);
            }
            data.Name = presentation.Name;
            data.Description = presentation.Description;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SaveAll(IList<DepartPresentation> presentations)
        {
            presentations.ToList().ForEach(item =>
            {
                Save(item);
            });

            return ActionResult.DefaultResult;
        }

        public ActionResult DeleteByTKey(string key)
        {
            var data = dataContext.Depart.FirstOrDefault(it => it.Code == key);

            if (data != null)
            {
                dataContext.Depart.DeleteOnSubmit(data);

                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }
    }
}
