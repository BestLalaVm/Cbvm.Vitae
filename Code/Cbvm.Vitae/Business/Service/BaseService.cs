using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LkDataContext;
using Presentation.Criteria;
using Presentation.UIView;
using Business.Interface;

namespace Business.Service
{
    public class BaseService : IOrigianlBaseService
    {
        private CVAcademicianDataContext _dataContext;
        protected CVAcademicianDataContext dataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new CVAcademicianDataContext();
                }
                return _dataContext;
            }
        }

        public void WriteLog(string message)
        {
            
        }

        public void WriteLog(Exception exception)
        {
            
        }

        public IQueryable<T> PageingQueryable<T>(IQueryable<T> query, BaseCriteria criteria, out int totalCount)
        {
            totalCount = query.Count();

            if (criteria.NeedPaging)
            {
                query =
                    query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize)
                        .Select(it => it);
            }

            return query;
        }

        public EntityCollection<T> Translate2Presentations<T>(List<T> list) where T : BasePresentation, new()
        {
            int index = 0;
            EntityCollection<T> entityCollection =
                new EntityCollection<T>();
            if (list == null)
            {
                return entityCollection;
            }
            list.ForEach(it =>
            {
                it.Index = ++index;
                entityCollection.Add(it);
            });

            return entityCollection;
        }

        public IQueryable<TOR> GetOrigianlQuery<TOR>() where TOR : class
        {
            return dataContext.GetTable<TOR>().AsQueryable();
        }


        public CVAcademicianDataContext DataContext
        {
            get { return dataContext; }
        }
    }
}
