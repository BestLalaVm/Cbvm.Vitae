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
    public class MarjorService : BaseService, IMarjorService
    {
        public MarjorPresentation Get(string key)
        {
            return dataContext.Marjor.Where(it => it.Code == key).Select(it => new MarjorPresentation
            {
                Code=it.Code,
                Description=it.Description,
                Name=it.Name,
                UniversityCode = it.UniversityCode
            }).FirstOrDefault();
        }

        public MarjorPresentation Get(MarjorCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<MarjorPresentation> GetAll(MarjorCriteria criteria)
        {
            var query = dataContext.Marjor.Where(it => it.UniversityCode == criteria.UniversityCode);
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

            var result = Translate2Presentations(query.Select(it => new MarjorPresentation()
            {
                Code = it.Code,
                Description = it.Description,
                Name = it.Name,
                UniversityCode = it.UniversityCode
            }).ToList());
            result.TotalCount = totalCount;

            return result;
        }

        public IQueryable<MarjorPresentation> GetQuery()
        {

            var query = dataContext.Marjor.Select(it => new MarjorPresentation
            {
                Code = it.Code,
                Description = it.Description,
                Name = it.Name,
                UniversityCode = it.UniversityCode
            }).AsQueryable();

            
            return query;
        }

        public ActionResult Save(MarjorPresentation presentation)
        {
            if (String.IsNullOrEmpty(presentation.Code))
            {
                presentation.Code = GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.Marjor, presentation.UniversityCode);
            }
            var data = dataContext.Marjor.Where(it => it.Code == presentation.Code).FirstOrDefault();
            if (data == null)
            {
                data = new LkDataContext.Marjor()
                {
                    Code = presentation.Code,
                    UniversityCode = presentation.UniversityCode
                };

                dataContext.Marjor.InsertOnSubmit(data);
            }
            data.Name = presentation.Name;
            data.Description = presentation.Description;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SaveAll(IList<MarjorPresentation> presentations)
        {
            presentations.ToList().ForEach(item =>
            {
                Save(item);
            });

            return ActionResult.DefaultResult;
        }

        public ActionResult DeleteByTKey(string key)
        {
            var data = dataContext.Marjor.FirstOrDefault(it => it.Code == key);

            if (data != null)
            {
                dataContext.Marjor.DeleteOnSubmit(data);

                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }
    }
}
