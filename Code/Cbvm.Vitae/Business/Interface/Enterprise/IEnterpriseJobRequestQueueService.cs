using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Enterprise;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using Presentation.UIView.Student;

namespace Business.Interface.Enterprise
{
    public interface IEnterpriseJobRequestQueueService
    {
        EnterpriseJobRequestQueuePresentation Get(EnterpriseJobRequestQueueCriteria criteria);

        EntityCollection<AskedReferralsJobPresentation> GetAllAskedReferrals(EnterpriseJobRequestQueueCriteria criteria);

        ActionResult ChangeReferralState(string teacherNum, int referralsQueueID, ReferralState state,
            string description);

        ActionResult RecommendStudentToJobFromTeacher(string jobCode, List<string> studentCodes, string note,
            string teacherNum);

        EntityCollection<EnterpriseJobForStudentPresentation> GetAllForStudents(
            EnterpriseJobRequestQueueCriteria criteria,
            string studentNum);

        EntityCollection<TeacherReferralJobForStudentPresentation> GetAllTeacherReferralForStudents(
            EnterpriseJobRequestQueueCriteria criteria,
            string studentNum);

        ActionResult ChangeReferralJobQueueState(string jobCode, int referralId);
    }
}
