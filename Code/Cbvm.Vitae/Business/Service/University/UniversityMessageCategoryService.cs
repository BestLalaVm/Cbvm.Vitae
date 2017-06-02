using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;
using Business.Interface.University;
using LkDataContext;

namespace Business.Service.University
{
    public class UniversityMessageCategoryService : BaseService, IUniversityMessageCategoryService
    {
        public UniversityMessageCategoryPresentation Get(string code)
        {
            return dataContext.UniversityMessageCategory.Select(it => new UniversityMessageCategoryPresentation
            {
                Code = it.Code,
                Name = it.Name,
                Description = it.Description,
                CreateTime=it.CreateTime
            }).FirstOrDefault();
        }

        public ActionResult Save(UniversityMessageCategoryPresentation presentation)
        {
            var category = dataContext.UniversityMessageCategory.FirstOrDefault(it => it.Code==presentation.Code);
            if (category == null)
            {
                category = new UniversityMessageCategory()
                {
                    CreateTime = DateTime.Now,
                    UniversityCode = presentation.UniversityCode,
                    Code = GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.UniversityMessageCategory, presentation.UniversityCode)
                };
                dataContext.UniversityMessageCategory.InsertOnSubmit(category);
            }
            category.Name = presentation.Name;
            category.Description = presentation.Description;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<UniversityMessageCategoryPresentation> GetAll(UniversityMessageCategoryCriteria criteria)
        {
            var query = from it in dataContext.UniversityMessageCategory select it;
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.UniversityCode))
            {
                query = from it in query where it.UniversityCode == criteria.UniversityCode select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime), criteria, out totalCount);

            var list = query.Select(departMessage => new UniversityMessageCategoryPresentation()
            {
                Code=departMessage.Code,
                Name = departMessage.Name,
                Description = departMessage.Description,
                UniversityCode = departMessage.UniversityCode,
                CreateTime=departMessage.CreateTime
            }).ToList();

            EntityCollection<UniversityMessageCategoryPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Delete(string code)
        {
            var departMessage = dataContext.UniversityMessageCategory.FirstOrDefault(it => it.Code == code);
            if (departMessage == null)
            {
                return ActionResult.NotFoundResult;
            }

            dataContext.UniversityMessageCategory.DeleteOnSubmit(departMessage);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
