using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;

namespace Business.Interface.University
{
    public interface IUniversityMessageCategoryService 
    {
        UniversityMessageCategoryPresentation Get(string code);

        ActionResult Save(UniversityMessageCategoryPresentation presentation);

        EntityCollection<UniversityMessageCategoryPresentation> GetAll(UniversityMessageCategoryCriteria criteria);

        ActionResult Delete(string code);
    }
}
