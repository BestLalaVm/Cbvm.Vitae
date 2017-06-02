using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Presentation.Criteria;
using Presentation.UIView;

namespace Business.Service.Student
{
    public class StudentProfessionalCommentService:BaseService,ICommentService
    {

        public EntityCollection<CommentPresentation> GetAll(CommentCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ActionResult Save(CommentPresentation presentation)
        {
            throw new NotImplementedException();
        }
    }
}
