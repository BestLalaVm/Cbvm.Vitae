using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria;
using Presentation.UIView;

namespace Business.Interface
{
    public interface ICommentService
    {
        EntityCollection<CommentPresentation> GetAll(CommentCriteria criteria);

        ActionResult Save(CommentPresentation presentation);
    }
}
