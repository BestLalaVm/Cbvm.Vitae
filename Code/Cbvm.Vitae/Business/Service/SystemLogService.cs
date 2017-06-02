using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;

namespace Business.Service
{
    public class SystemLogService:BaseService,ISystemLogService
    {
        public EntityCollection<SystemLogPresentation> GetAll(SystemLogCriteria criteria)
        {
            var query = from it in dataContext.SystemLog select it;
            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new SystemLogPresentation()
            {
                Name = it.Name,
                Message = it.Message,
                LogType = (LogType) it.LogType,
                LogTime = it.LogTime,
                IPAddress = it.IPAddress
            }).ToList();
            EntityCollection<SystemLogPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }
    }
}
