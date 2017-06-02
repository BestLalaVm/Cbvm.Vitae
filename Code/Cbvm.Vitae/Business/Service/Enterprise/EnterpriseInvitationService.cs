using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.UIView;
using Presentation.UIView.Student;
using Presentation.Criteria.Student;
using Business.Interface.Enterprise;
using Presentation.UIView.Enterprise;

namespace Business.Service.Enterprise
{
    public class EnterpriseInvitationService : BaseService, IEnterpriseInvitationService
    {
        public EntityCollection<StudentInvitationPresentation> GetInvivations(StudentInvitationCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseInvitation where it.StudentNum==criteria.StudentNum
                        select it;

            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query
                        where it.Enterprise.Name.Contains(criteria.EnterpriseName.Trim())
                        select it;
            }
            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.CreatedTime >= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                var dateTo = criteria.DateTo.Value.AddDays(1).Date;
                query = query.Where(it => it.CreatedTime < dateTo);
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new StudentInvitationPresentation()
            {
                EnterpriseCode=it.EnterpriseCode,
                EnterpriseName = it.Enterprise.Name,
                InvitedDate = it.CreatedTime,
                IsViewed = it.IsViewed,
                ID = it.ID,
                JobNameList=it.EnterpriseJobRequester.Select(ix=>ix.EnterpriseJob.Name).ToList(),
                IsSendJobRequest = it.Enterprise.EnterpriseJob.Any(ix => ix.EnterpriseJobRequester.Any(tt => tt.InvitationId == it.ID))
            }).ToList();

            EntityCollection<StudentInvitationPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }


        public EntityCollection<EnterpriseInvitationPresentation> GetInvivations(Presentation.Criteria.Enterprise.EnterpriseInvitationCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseInvitation where it.EnterpriseCode==criteria.EnterpriseCode
                        select it;

            if (!String.IsNullOrEmpty(criteria.StudentName))
            {
                query = from it in query
                        where it.Student.NameZh.Contains(criteria.StudentName.Trim())
                        select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.CreatedTime >= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                var dateTo = criteria.DateTo.Value.AddDays(1).Date;
                query = query.Where(it => it.CreatedTime < dateTo);
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new EnterpriseInvitationPresentation()
            {
                CollegeName=it.Student.College.Name,
                MarjorName=it.Student.Marjor.Name,
                StudentName = it.Student.NameZh,
                StudentNum = it.StudentNum,
                InvitedDate=it.CreatedTime,
                IsViewed = it.IsViewed,
                ID = it.ID,
                HasRequested = it.Enterprise.EnterpriseJob.Any(ix => ix.EnterpriseJobRequester.Any(tt => tt.InvitationId==it.ID && tt.StudentNum==it.StudentNum))
            }).ToList();

            EntityCollection<EnterpriseInvitationPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }
    }
}
