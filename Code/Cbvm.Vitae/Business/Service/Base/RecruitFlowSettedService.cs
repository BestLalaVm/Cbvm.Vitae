using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Base;
using Presentation.Criteria.Base;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Base;

namespace Business.Service.Base
{
    public class RecruitFlowSettedService : BaseService, IRecruitFlowSettedService
    {
        public IList<RecruitFlowSettedPresentation> GetAllRecruitFlow()
        {
            var list = dataContext.RecruitFlowSetted.OrderBy(ix => ix.DisplayOrder)
                .Select(it => new RecruitFlowSettedPresentation()
                {
                    Name = it.Name,
                    Description = it.Description,
                    DisplayOrder = it.DisplayOrder ?? 0,
                    RecruitType = (RecruitType) it.RecruitType,
                    Id = it.ID
                }).ToList();
            EntityCollection<RecruitFlowSettedPresentation> entityCollection = Translate2Presentations(list);

            return entityCollection;
        }

        public RecruitFlowSettedPresentation Get(int id)
        {
            var recruitFlowSetted = dataContext.RecruitFlowSetted.FirstOrDefault(it => it.ID == id);
            if (recruitFlowSetted == null)
            {
                return null;
            }

            return new RecruitFlowSettedPresentation()
            {
                Name=recruitFlowSetted.Name,
                Description = recruitFlowSetted.Description,
                DisplayOrder = recruitFlowSetted.DisplayOrder??0,
                Id = recruitFlowSetted.ID,
                RecruitType = (RecruitType)recruitFlowSetted.RecruitType
            };
        }

        public ActionResult Save(RecruitFlowSettedPresentation presentation)
        {
            var recruitFlow = dataContext.RecruitFlowSetted.FirstOrDefault(ic => ic.ID == presentation.Id);
            if (recruitFlow == null) return ActionResult.NotFoundResult;
            recruitFlow.Name = presentation.Name;
            recruitFlow.Description = presentation.Description;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<RecruitFlowSettedPresentation> GetAll(RecruitFlowSettedCriteria criteria)
        {
            var query = from it in dataContext.RecruitFlowSetted select it;
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
            }
            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.DisplayOrder), criteria, out totalCount);

            var list = query.Select(it => new RecruitFlowSettedPresentation()
            {
                Name = it.Name,
                Description = it.Description,
                DisplayOrder = it.DisplayOrder ?? 0,
                RecruitType = (RecruitType) it.RecruitType,
                Id = it.ID
            }).ToList();


            EntityCollection<RecruitFlowSettedPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.ForEach(item =>
            {
                item.DisplayOrder = item.Index;
            });
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Delete(int id)
        {
            var recruitFlowSetted = dataContext.RecruitFlowSetted.FirstOrDefault(it => it.ID == id);
            if (recruitFlowSetted == null)
            {
                return ActionResult.NotFoundResult;
            }

            dataContext.RecruitFlowSetted.DeleteOnSubmit(recruitFlowSetted);

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SetRecruitFlowOrder(int recruitId, int nextRecruitId)
        {
            var recruit = dataContext.RecruitFlowSetted.FirstOrDefault(ic => ic.ID == recruitId);
            var nextRecruit = dataContext.RecruitFlowSetted.FirstOrDefault(ic => ic.ID == nextRecruitId);
            if (recruit != null && nextRecruit != null)
            {
                var display = recruit.DisplayOrder;
                recruit.DisplayOrder = nextRecruit.DisplayOrder;
                nextRecruit.DisplayOrder = display;

                dataContext.SubmitChanges();

                return ActionResult.DefaultResult;
            }
            return ActionResult.NotFoundResult;
        }
    }
}
