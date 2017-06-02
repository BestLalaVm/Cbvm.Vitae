using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;
using LkDataContext;

namespace Business.Interface.University
{
    public interface IUniversityMessageService
    {
        UniversityMessagePresentation Get(int id);

        ActionResult Save(UniversityMessagePresentation presentation);

        EntityCollection<UniversityMessagePresentation> GetAll(UniversityMessageCriteria criteria);

        ActionResult Delete(int id);

        EntityCollection<UniversityMessagePresentation> GetTop20FrontMessageList();

        EntityCollection<MessageUiPresentation> GetFrontMessageList(int pageIndex, int pageSize);

        ActionResult SetStatus(int id, bool isOnline);

        ActionResult SetIsTop(int id, bool isTop);

        IQueryable<UniversityMessage> GetQuery();
    }
}
