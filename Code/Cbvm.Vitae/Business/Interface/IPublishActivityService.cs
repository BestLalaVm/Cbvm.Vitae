using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria;
using Presentation.UIView;

namespace Business.Interface
{
    public interface IPublishActivityService
    {
        PublishActivityPresentation Get(PublishActivityCriteria criteria);

        EntityCollection<PublishActivityPresentation> GetAll(PublishActivityCriteria criteria);

        ActionResult Save(PublishActivityPresentation presentation);

        ActionResult Delete(int id);

        ActionResult ApplyPublishActivity2StudentNum(int id, string studentNum);

        ActionResult SetStatus(string teacherNum, int id, bool isOnline);

        EntityCollection<PublishActivityPresentation> GetAllForStudent(PublishActivityCriteria criteria,
            string studentNum);
    }
}
