using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.UIView;
using Presentation.UIView.Student;
using Presentation.Criteria.Student;
using Presentation.UIView.Enterprise;
using Presentation.Criteria.Enterprise;

namespace Business.Interface.Enterprise
{
    public interface IEnterpriseInvitationService
    {
        EntityCollection<StudentInvitationPresentation> GetInvivations(StudentInvitationCriteria criteria);

        EntityCollection<EnterpriseInvitationPresentation> GetInvivations(EnterpriseInvitationCriteria criteria);
    }
}
