using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;

namespace Business.Interface.Enterprise
{
    public interface IEnterpriseJobRequestService
    {
        EntityCollection<EnterpriseJobRequestPresentation> GetAll(EnterpriseJobRequestCriteria criteria);

        ActionResult AddRequestJob(string studentNum, string jobCode, List<string> teacherNums, string note = null);

        ActionResult AddRequestJob(string studentNum, string jobCode);

        ActionResult ChangeRequestJobStage(int jobRequestID, int recruitFlowID, string notes = null,
            bool isSubmitted = true);

        ActionResult ChangeRequestJobStage(int stageId, string description);

        ActionResult InviteToAudition(int requestID);

        JobRequestRecruitStagePresentation LoadNewestJobRequestRecruitStage(int jobRequestID);

        ActionResult DenyRequestJob(int jobRequestID);

        ActionResult InitToViewedRequestJob(int jobRequestID);

        ActionResult ChangeRequestJobStage(IDictionary<int, IList<int>> recruitRequestList, string notes = null);

        EntityCollection<EnterpriseJobRequestPresentation> GetAllWithoutBatch(string enterpriseCode);

        EntityCollection<EnterpriseJobRequestForStudentPresentation> GetAllForStudent(
            EnterpriseJobRequestCriteria criteria, string studentNum);

        EntityCollection<EnterpriseJobRequestFromTeacherPresentation> GetJobRequestAll(
            EnterpriseJobRequestCriteria criteria);

        EntityCollection<EnterpriseJobRequestPresentation> GetAllByBatchId(int batchId, string enterpriseCode);

        List<EnterpriseJobRequestForStudentPresentation> GetJobRequestForStudent(string studentNum, int pageSize = 20);
    }
}
