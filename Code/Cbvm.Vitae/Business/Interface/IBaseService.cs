using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria;
using Presentation.UIView;
using LkDataContext;

namespace Business.Interface
{
    public interface IBaseService<TKey, TPresentation, TCriteria> : IOrigianlBaseService
        where TPresentation : BasePresentation, new()
        where TCriteria : BaseCriteria, new()
    {
        TPresentation Get(TKey key);

        TPresentation Get(TCriteria criteria);

        EntityCollection<TPresentation> GetAll(TCriteria criteria);

        IQueryable<TPresentation> GetQuery();

        ActionResult Save(TPresentation presentation);

        ActionResult SaveAll(IList<TPresentation> presentations);

        ActionResult DeleteByTKey(TKey key);
    }

    public interface IOrigianlBaseService
    {
        IQueryable<TOR> GetOrigianlQuery<TOR>() where TOR : class;

        CVAcademicianDataContext DataContext { get; }

        EntityCollection<T> Translate2Presentations<T>(List<T> list) where T : BasePresentation, new();

        IQueryable<T> PageingQueryable<T>(IQueryable<T> query, BaseCriteria criteria, out int totalCount);
    }
}
