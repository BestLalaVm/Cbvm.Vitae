using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Enterprise;
using CustomControl;
using LkDataContext;
using Presentation.Cache;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using WebLibrary.Helper;

namespace Business.Service.Enterprise
{
    public partial class EnterpriseJobRequestService : BaseService, IEnterpriseJobRequestService
    {
        public EntityCollection<EnterpriseJobRequestPresentation> GetAll(EnterpriseJobRequestCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseJobRequester select it;
            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query
                        where it.EnterpriseJob.Enterprise.Name.Contains(criteria.EnterpriseName.Trim())
                        select it;
            }

            if (!string.IsNullOrEmpty(criteria.JobName))
            {
                query = from it in query where it.EnterpriseJob.Name.Contains(criteria.JobName.Trim()) select it;
            }

            if (criteria.RequestTimeFrom.HasValue)
            {
                query = from it in query where it.RequestTime >= criteria.RequestTimeFrom select it;
            }

            if (criteria.RequestTimeTo.HasValue)
            {
                query = from it in query where it.RequestTime <= criteria.RequestTimeTo select it;
            }
            if (criteria.BatchId.HasValue)
            {
                query = from it in query
                        where it.EnterpriseBatchRelative.Any(ic => ic.RecruitBatchID == criteria.BatchId.Value)
                        select it;
            }
            if (criteria.IsReferralsQueueIDNotNull)
            {
                query = from it in query where it.EnterpriseJobRequestQueue != null select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.ID), criteria, out totalCount);

            var list = query.ToList().Select(it => new EnterpriseJobRequestPresentation()
            {
                Id = it.ID,
                Company = it.EnterpriseJob.Enterprise.Name,
                ContactName = it.EnterpriseJob.ContactName,
                JobCode = it.JobCode,
                JobDescription = it.EnterpriseJob.Description,
                JobStage = GetJobStage(it),
                JobName = it.EnterpriseJob.Name,
                RequestTime = it.RequestTime,
                JobNum = it.EnterpriseJob.Num,
                StudentName = it.Student.NameZh,
                Telephone = it.Student.Telephone,
                StageDescription = GetStageDescription(it),
                RequestStatus = GetRequestStatus(it),
                Sex = (SexType)it.Student.Sex,
                ThumbPath = it.Student.ThumbPath,
                JobRequestRecruitStagePresentation=!it.JobRequestRecruitStage.Any()?null: new JobRequestRecruitStagePresentation()
                {
                    Description=it.JobRequestRecruitStage.OrderByDescending(ix=>ix.ID).FirstOrDefault().Description,
                    IsNewest = it.JobRequestRecruitStage.OrderByDescending(ix => ix.ID).FirstOrDefault().IsNewest,
                    JobRequestId = it.JobRequestRecruitStage.OrderByDescending(ix => ix.ID).FirstOrDefault().JobRequestID,
                    RecruitFlowId = it.JobRequestRecruitStage.OrderByDescending(ix => ix.ID).FirstOrDefault().RecruitFlowID
                },
                JobReferralers = (
                    it.EnterpriseJobRequestQueue == null
                        ? new List<EnterpriseJobReferralerPresentation>()
                        : it.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Where(ix=>ix.ReferralState==(int)ReferralState.Passed).Select(
                            ic => new EnterpriseJobReferralerPresentation()
                            {
                                UserName = ic.UserName,
                                UserType = (UserType)ic.UserType,
                                NameZh = GlobalBaseDataCache.GetTeacherName(ic.UserName),
                                Content = ic.Content,
                                ReferralState = (ReferralState)ic.ReferralState
                            }).ToList()
                    ),
                StudentNum = it.StudentNum
            }).ToList();

            EntityCollection<EnterpriseJobRequestPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
            return null;
        }

        public EntityCollection<EnterpriseJobRequestPresentation> GetAllWithoutBatch(string enterpriseCode)
        {
            var query =
                dataContext.EnterpriseJobRequester.Where(
                    it => it.EnterpriseJob.EnterpriseCode == enterpriseCode && !it.EnterpriseBatchRelative.Any() &&
                          it.JobRequestRecruitStage.Any(
                              ic =>
                                  ic.RecruitFlowSetted.RecruitType ==
                                  (int)RecruitType.View) &&
                          !it.JobRequestRecruitStage.Any(
                              ic =>
                                  ic.RecruitFlowSetted.RecruitType ==
                                  (int)RecruitType.NoPassed));

            int totalCount = 0;

            query = PageingQueryable(query.OrderByDescending(it => it.ID), new EnterpriseJobRequestCriteria()
            {
                EnterpriseCode = enterpriseCode
            }, out totalCount);

            var list = query.ToList().Select(it => new EnterpriseJobRequestPresentation()
            {
                Id = it.ID,
                Company = it.EnterpriseJob.Enterprise.Name,
                ContactName = it.EnterpriseJob.ContactName,
                JobCode = it.JobCode,
                JobDescription = it.EnterpriseJob.Description,
                JobStage = GetJobStage(it),
                JobName = it.EnterpriseJob.Name,
                RequestTime = it.RequestTime,
                JobNum = it.EnterpriseJob.Num,
                StudentName = it.Student.NameZh,
                Telephone = it.Student.Telephone,
                StageDescription = GetStageDescription(it),
                RequestStatus = GetRequestStatus(it),
                JobReferralers = (
                    it.EnterpriseJobRequestQueue == null
                        ? new List<EnterpriseJobReferralerPresentation>()
                        : it.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Select(
                            ic => new EnterpriseJobReferralerPresentation()
                            {
                                UserName = ic.UserName,
                                UserType = (UserType) ic.UserType,
                                Content = ic.Content,
                                ReferralState = (ReferralState) ic.ReferralState
                            }).ToList()
                    ),
                StudentNum = it.StudentNum,
                ThumbPath = it.Student.ThumbPath,
                Sex = (SexType)it.Student.Sex
            }).ToList();

            EntityCollection<EnterpriseJobRequestPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public EntityCollection<EnterpriseJobRequestForStudentPresentation> GetAllForStudent(EnterpriseJobRequestCriteria criteria, string studentNum)
        {
            var query = from it in dataContext.EnterpriseJobRequester where it.StudentNum == studentNum select it;
            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query
                        where it.EnterpriseJob.Enterprise.Name.Contains(criteria.EnterpriseName.Trim())
                        select it;
            }
            if (!String.IsNullOrEmpty(criteria.JobName))
            {
                query = from it in query where it.EnterpriseJob.Name.Contains(criteria.JobName) select it;
            }

            if (criteria.RequestTimeFrom.HasValue)
            {
                query = from it in query where it.RequestTime >= criteria.RequestTimeFrom.Value select it;
            }

            if (criteria.RequestTimeTo.HasValue)
            {
                query = from it in query where it.RequestTime <= criteria.RequestTimeTo.Value select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.ID), criteria, out totalCount);

            var list = query.Select(ic => new EnterpriseJobRequestForStudentPresentation()
            {
                ContactName = ic.EnterpriseJob.ContactName,
                EnterpriseName = ic.EnterpriseJob.Enterprise.Name,
                JobCode = ic.JobCode,
                JobName = ic.EnterpriseJob.Name,
                RequestTime = ic.RequestTime,
                JobStage = GetJobStage(ic),
                StageDescription = GetStageDescription(ic),
                WorkPlace = ic.EnterpriseJob.WorkPlace,
                JobNum = ic.EnterpriseJob.Num,
                Address = ic.EnterpriseJob.Address
            }).ToList();

            EntityCollection<EnterpriseJobRequestForStudentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public EntityCollection<EnterpriseJobRequestFromTeacherPresentation> GetJobRequestAll(
            EnterpriseJobRequestCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseJob
                        join ic in dataContext.EnterpriseJobRequester
                            on it.Code equals ic.JobCode
                        where it.EnterpriseCode == criteria.EnterpriseCode
                        select new
                        {
                            EnterprseJob = it,
                            JobRequest = ic
                        };
            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query
                        where it.EnterprseJob.Enterprise.Name.Contains(criteria.EnterpriseName.Trim())
                        select it;
            }

            if (!string.IsNullOrEmpty(criteria.JobName))
            {
                query = from it in query where it.EnterprseJob.Name.Contains(criteria.JobName.Trim()) select it;
            }

            if (!string.IsNullOrEmpty(criteria.StudentName))
            {
                query = from it in query where (it.JobRequest.Student.NameZh.Contains(criteria.StudentName.Trim())) select it;
            }

            if (criteria.RequestTimeFrom.HasValue)
            {
                query = from it in query where it.JobRequest.RequestTime >= criteria.RequestTimeFrom select it;
            }

            if (criteria.RequestTimeTo.HasValue)
            {
                query = from it in query where it.JobRequest.RequestTime <= criteria.RequestTimeTo select it;
            }
            if (criteria.BatchId.HasValue)
            {
                query = from it in query
                        where it.JobRequest.EnterpriseBatchRelative.Any(ic => ic.RecruitBatchID == criteria.BatchId.Value)
                        select it;
            }
            if (criteria.IsReferralsQueueIDNotNull)
            {
                query = from it in query where it.JobRequest.EnterpriseJobRequestQueue != null && it.JobRequest.EnterpriseJobRequestQueue.IsProcessed select it;
            }
            else
            {
                //Changed by Vincent. 请求列表应该是包括被推荐的信息. 20150315
                //query = from it in query where it.JobRequest.EnterpriseJobRequestQueue == null || it.JobRequest.EnterpriseJobRequestQueue.IsSendToEnterprise select it;
            }

            if (!String.IsNullOrEmpty(criteria.RecruitFlowID))
            {
                int recruitFlowId = 0;
                if (int.TryParse(criteria.RecruitFlowID, out recruitFlowId))
                {
                    query = from it in query
                        where
                            ((it.JobRequest.JobRequestRecruitStage.Any() &&
                              it.JobRequest.JobRequestRecruitStage.OrderByDescending(ic => ic.ID)
                                  .FirstOrDefault()
                                  .RecruitFlowID == recruitFlowId) ||
                             (!it.JobRequest.JobRequestRecruitStage.Any() &&
                              dataContext.RecruitFlowSetted.Any(
                                  ix => ix.ID == recruitFlowId && ix.RecruitType == (int) RecruitType.Request)))
                        select it;
                }
            }

            if (!String.IsNullOrEmpty(criteria.Referraler))
            {
                query = from it in query
                        where
                            it.JobRequest.EnterpriseJobRequestQueue != null &&
                            it.JobRequest.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Any(
                                ic => ic.UserType == (int)UserType.Teacher &&
                                      dataContext.Teacher.Any(
                                          ix => ix.TeacherNum == ic.UserName && ix.NameZh.Contains(criteria.Referraler)))
                        select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.JobRequest.ID), criteria, out totalCount);

            var list = query.ToList().Select(it => new EnterpriseJobRequestFromTeacherPresentation()
            {
                Id = it.JobRequest.ID,
                JobCode = it.EnterprseJob.Code,
                JobDescription = it.EnterprseJob.Description,
                JobName = it.EnterprseJob.Name,
                JobNum = it.EnterprseJob.Num,
                JobStage = GetJobStage(it.JobRequest),
                RequestStatus = GetRequestStatus(it.JobRequest),
                RequestTime = it.JobRequest.RequestTime,
                StudentName = it.JobRequest.Student.NameZh,
                StudentNum = it.JobRequest.StudentNum,
                StudentSex = (SexType)it.JobRequest.Student.Sex,
                StudentTelephone = it.JobRequest.Student.Telephone,
                IsInviated=it.JobRequest.IsInvited,
                InviatedDate=it.JobRequest.InvitedDate,
                Referralers = it.JobRequest.EnterpriseJobRequestQueue == null
                    ? new List<string>()
                    : it.JobRequest.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Select(
                        ic =>
                            GlobalBaseDataCache.GetTeacherName(ic.UserName)
                        ).ToList(),
                AuthenticatedRequestTime = it.JobRequest.Student.StudentTruthAuthenticated.OrderByDescending(ix => ix.RequestDate).ThenByDescending(ix=>ix.IsAuthenticated).Select(ix => ix.RequestDate).FirstOrDefault(),
                IsAuthenticated = it.JobRequest.Student.StudentTruthAuthenticated.OrderByDescending(ix => ix.RequestDate).ThenByDescending(ix => ix.IsAuthenticated).Select(ix => ix.IsAuthenticated).FirstOrDefault(),
            }).ToList();


            EntityCollection<EnterpriseJobRequestFromTeacherPresentation> entityCollection = Translate2Presentations<EnterpriseJobRequestFromTeacherPresentation>(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;

            return null;
        }

        public EntityCollection<EnterpriseJobRequestPresentation> GetAllByBatchId(int batchId, string enterpriseCode)
        {
            var jobRequests =
                dataContext.EnterpriseJobRequester.Where(
                    ic =>
                        ic.EnterpriseBatchRelative.Any(ix => ix.RecruitBatchID == batchId) &&
                        ic.EnterpriseJob.EnterpriseCode == enterpriseCode);
            var jobRequestPresentations = jobRequests.Select(it => new EnterpriseJobRequestPresentation()
            {
                Id = it.ID,
                Company = it.EnterpriseJob.Enterprise.Name,
                ContactName = it.EnterpriseJob.ContactName,
                JobCode = it.JobCode,
                JobDescription = it.EnterpriseJob.Description,
                JobStage = GetJobStage(it),
                JobName = it.EnterpriseJob.Name,
                RequestTime = it.RequestTime,
                StageDescription = GetStageDescription(it),
                StudentNum = it.StudentNum,
                ThumbPath = it.Student.ThumbPath,
                JobNum = it.EnterpriseJob.Num,
                Telephone = it.Student.Telephone,
                StudentName = it.Student.NameZh,
                Sex = (SexType) it.Student.Sex
            }).ToList();
            EntityCollection<EnterpriseJobRequestPresentation> entityCollection = Translate2Presentations(jobRequestPresentations);

            return entityCollection;
        }

        public ActionResult AddRequestJob(string studentNum, string jobCode, List<string> teacherNums,
            string note = null)
        {
            var result = ValidateJobRequest(studentNum, jobCode);
            if (!result.IsSucess)
            {
                return result;
            }

            var requestDate = DateTime.Now.AddDays(-1);
            if (!dataContext.EnterpriseJobRequester.Any(it => it.StudentNum == studentNum && it.JobCode == jobCode && it.RequestTime >= requestDate))
            {
                if (teacherNums == null || !teacherNums.Any())
                {
                    var inviteLog = dataContext.EnterpriseInvitation
                                                    .Where(it => it.StudentNum == studentNum &&
                                                                 it.Enterprise.EnterpriseJob.Any(ix => ix.Code == jobCode))
                                                    .OrderByDescending(it => it.CreatedTime).FirstOrDefault();

                    var job = new EnterpriseJobRequester()
                    {
                        JobCode = jobCode,
                        StudentNum = studentNum,
                        RequestTime = DateTime.Now
                    };

                    if (inviteLog != null)
                    {
                        job.IsInvited = true;
                        job.EnterpriseInvitation = inviteLog;
                        job.InvitedDate = inviteLog.CreatedTime;
                        job.Note = String.Format("于{0}邀请过来学生前台面试", inviteLog.CreatedTime);
                    }

                    dataContext.EnterpriseJobRequester.InsertOnSubmit(job);
                }
                else
                {
                    var jobRequestQueue = new EnterpriseJobRequestQueue()
                    {
                        JobCode = jobCode,
                        CreateTime = DateTime.Now,
                        IsProcessed = false,
                        IsSendToEnterprise = false,
                        StudentNum = studentNum
                    };
                    dataContext.Teacher.Where(it => teacherNums.Contains(it.TeacherNum))
                        .Select(ic => ic.TeacherNum)
                        .ToList()
                        .ForEach(
                            teacherNum =>
                            {
                                jobRequestQueue.EnterpriseJobReferraler.Add(new EnterpriseJobReferraler()
                                {
                                    CreateTime = DateTime.Now,
                                    Content = note,
                                    ReferralState = (int)ReferralState.Pending,
                                    UserName = teacherNum,
                                    UserType = (int)UserType.Teacher
                                });
                            });

                    dataContext.EnterpriseJobRequestQueue.InsertOnSubmit(jobRequestQueue);
                }

                dataContext.SubmitChanges();
            }
            else
            {
                result.Message = "每天最多只能给一份职位投递一次简历";
            }

            return result;
        }

        public ActionResult AddRequestJob(string studentNum, string jobCode)
        {
            var result = AddRequestJob(studentNum, jobCode, null, null);

            return result;

            //var result = ValidateJobRequest(studentNum, jobCode);
            //if (!result.IsSucess)
            //{
            //    return result;
            //}

            //var requestDate = DateTime.Now.AddDays(-1);
            //if (!dataContext.EnterpriseJobRequester.Any(it => it.StudentNum == studentNum && it.JobCode == jobCode && it.RequestTime >= requestDate))
            //{
            //    var inviteLog = dataContext.EnterpriseInvitation
            //                    .Where(it => it.StudentNum == studentNum && 
            //                                 it.Enterprise.EnterpriseJob.Any(ix => ix.Code == jobCode))
            //                    .OrderByDescending(it=>it.CreatedTime).FirstOrDefault();

            //    var job = new EnterpriseJobRequester()
            //    {
            //        JobCode = jobCode,
            //        StudentNum = studentNum,
            //        RequestTime = DateTime.Now
            //    };

            //    if (inviteLog != null)
            //    {
            //        job.IsInvited = true;
            //        job.EnterpriseInvitation = inviteLog;
            //        job.InvitedDate = inviteLog.CreatedTime;
            //        job.Note = String.Format("于{0}邀请过来学生前台面试", inviteLog.CreatedTime);
            //    }

            //    dataContext.EnterpriseJobRequester.InsertOnSubmit(job);
            //    dataContext.SubmitChanges();
            //}
            //else
            //{
            //    result.Message = "每天最多只能给一份职位投递一次简历";
            //}

            //return result;
        }

        public ActionResult ChangeRequestJobStage(int jobRequestID, int recruitFlowID, string notes = null,
            bool isSubmitted = true)
        {
            if (!dataContext.RecruitFlowSetted.Any(ic => ic.ID == recruitFlowID))
            {
                return ActionResult.CreateErrorActionResult("操作无效!");
            }

            var jobRequest = dataContext.EnterpriseJobRequester.FirstOrDefault(ix => ix.ID == jobRequestID);
            if (jobRequest == null) return ActionResult.NotFoundResult;

            jobRequest.JobRequestRecruitStage.ToList().ForEach(
                jobStages =>
                {
                    jobStages.IsNewest = false;
                });

            jobRequest.JobRequestRecruitStage.Add(new JobRequestRecruitStage()
            {
                JobRequestID = jobRequestID,
                CreateTime = DateTime.Now,
                RecruitFlowID = recruitFlowID,
                IsNewest = true,
                Description = notes
            });
            var emailTyep = EmailTemplateHelper.TranslateToEnterpriseEmailType(recruitFlowID);
            EmailTemplateHelper.NotifyEnterpriseJobStatusEmail(jobRequest,emailTyep.Value, dataContext);

            if (isSubmitted)
            {
                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }

        public ActionResult ChangeRequestJobStage(int stageId, string description)
        {
            var jobRecruitStage = dataContext.JobRequestRecruitStage.FirstOrDefault(it => it.ID == stageId);
            if (jobRecruitStage == null)
            {
                return ActionResult.CreateErrorActionResult("RecruitStages不存在!");
            }
            jobRecruitStage.Description = description;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult InviteToAudition(int requestID)
        {
            var jobRequest = dataContext.EnterpriseJobRequester.FirstOrDefault(it => it.ID == requestID);
            if (jobRequest != null)
            {
                var emailTemplate =
                    EmailTemplateHelper.GetEnterpriseEmailTemplate(jobRequest.EnterpriseJob.EnterpriseCode,
                        EnterpriseEmailType.Invited);

                var dic = new Dictionary<string, string>();
                var properties = jobRequest.EnterpriseJob.GetType().GetProperties();
                foreach (var property in properties)
                {
                    object value = property.GetValue(jobRequest.EnterpriseJob, null);
                    dic.Add(string.Format("Job{0}", property.Name), (value == null ? "" : value.ToString()));
                }
                properties = jobRequest.EnterpriseJob.Enterprise.GetType().GetProperties();
                foreach (var property in properties)
                {
                    object value = property.GetValue(jobRequest.EnterpriseJob.Enterprise, null);
                    dic.Add(property.Name, (value == null ? "" : value.ToString()));
                }
                var reader = new TemplateRender();
                var body = reader.Render(emailTemplate.Body, dic);
                var subject = reader.Render(emailTemplate.Subject, dic);

                dataContext.MessageBox.InsertOnSubmit(new MessageBox()
                {
                    Subject = subject,
                    Content = body,
                    CreateTime = DateTime.Now,
                    SenderKey = jobRequest.EnterpriseJob.Enterprise.UserName,
                    SenderType = (int) UserType.Enterprise,
                    ReceiverKey = jobRequest.StudentNum,
                    ReceiverType = (int) UserType.Student
                });

                dataContext.MailQueue.InsertOnSubmit(new MailQueue()
                {
                    Sender = emailTemplate.Sender,
                    Name = subject,
                    Cc = emailTemplate.Cc,
                    Message = body,
                    CreateTime = DateTime.Now,
                    IsSended = false,
                    Receiver = jobRequest.Student.Email,
                    ReceiverName = jobRequest.Student.Email
                });

                dataContext.SubmitChanges();

                return new ActionResult
                {
                    IsSucess = true,
                    Message = "邀请成功!"
                };
            }

            return ActionResult.DefaultResult;
        }

        public JobRequestRecruitStagePresentation LoadNewestJobRequestRecruitStage(int jobRequestID)
        {
            var jobRequestStage = dataContext.JobRequestRecruitStage.Where(it => it.JobRequestID == jobRequestID)
                .OrderByDescending(it => it.ID).Select(it => new JobRequestRecruitStagePresentation()
                {
                    JobRequestId = it.JobRequestID,
                    RecruitFlowId = it.RecruitFlowID,
                    Description = it.Description,
                    IsNewest = it.IsNewest
                })
                .FirstOrDefault();

            return jobRequestStage;
        }

        public ActionResult DenyRequestJob(int jobRequestID)
        {
            var recruitStage = LoadNewestJobRequestRecruitStage(jobRequestID);

            var recruitFlow =
                dataContext.RecruitFlowSetted.FirstOrDefault(it => it.RecruitType == (int) RecruitType.NoPassed);
            if (recruitStage == null || recruitStage.RecruitFlowId != recruitFlow.ID)
            {
                dataContext.JobRequestRecruitStage.InsertOnSubmit(new JobRequestRecruitStage()
                {
                    JobRequestID = jobRequestID,
                    CreateTime = DateTime.Now,
                    Description = "",
                    RecruitFlowID = recruitFlow.ID,
                    IsNewest = true
                });
                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }

        public ActionResult InitToViewedRequestJob(int jobRequestID)
        {
            var recruitFlow =
                dataContext.RecruitFlowSetted.FirstOrDefault(it => it.RecruitType == (int) RecruitType.View);

            if (
                !dataContext.JobRequestRecruitStage.Any(
                    ic => ic.JobRequestID == jobRequestID && ic.RecruitFlowSetted.RecruitType == (int) RecruitType.View))
            {
                dataContext.JobRequestRecruitStage.InsertOnSubmit(new JobRequestRecruitStage()
                {
                    JobRequestID = jobRequestID,
                    RecruitFlowID = recruitFlow.ID,
                    CreateTime = DateTime.Now,
                    Description = "第一次查阅",
                    IsNewest = true
                });
                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }

        public ActionResult ChangeRequestJobStage(IDictionary<int, IList<int>> recruitRequestList, string notes = null)
        {
            int count = 0;
            foreach (int recruitId in recruitRequestList.Keys)
            {
                var requestIdList = recruitRequestList[recruitId];
                count += requestIdList.Count;
            }

            int index = 0;
            foreach (int recruitId in recruitRequestList.Keys)
            {
                var requestIdList = recruitRequestList[recruitId];
                foreach (var requestId in requestIdList)
                {
                    index++;
                    var result = ChangeRequestJobStage(requestId, recruitId, notes, index == count);
                    if (!result.IsSucess)
                    {
                        return result;
                    }
                }
            }

            return new ActionResult()
            {
                IsSucess = true
            };
        }

        public List<EnterpriseJobRequestForStudentPresentation> GetJobRequestForStudent(string studentNum,
            int pageSize = 20)
        {
            var query = dataContext.EnterpriseJobRequester.Where(ix => ix.StudentNum == studentNum);
            var list = query.Take(pageSize).Select(ix => new EnterpriseJobRequestForStudentPresentation()
            {
                ContactName=ix.EnterpriseJob.ContactName,
                EnterpriseName=ix.EnterpriseJob.Enterprise.Name,
                JobCode=ix.JobCode,
                JobName=ix.EnterpriseJob.Name,
                RequestTime= ix.RequestTime,
                WorkPlace = ix.EnterpriseJob.WorkPlace,
                JobNum = ix.EnterpriseJob.Num,
                Address = ix.EnterpriseJob.Address
            }).ToList();

            return list;
        }

    }

    partial class EnterpriseJobRequestService
    {
        private string GetJobStage(EnterpriseJobRequester jobRequester)
        {
            return (jobRequester.JobRequestRecruitStage.Any()
                ? jobRequester.JobRequestRecruitStage.OrderByDescending(ix => ix.ID)
                    .FirstOrDefault()
                    .RecruitFlowSetted.Name
                : dataContext.RecruitFlowSetted.FirstOrDefault(
                    ic => ic.RecruitType == (int)RecruitType.Request)
                    .Name);
        }

        private string GetStageDescription(EnterpriseJobRequester jobRequester)
        {
            return (jobRequester.JobRequestRecruitStage.Any()
                ? jobRequester.JobRequestRecruitStage.OrderByDescending(ix => ix.ID)
                    .FirstOrDefault()
                    .RecruitFlowSetted.Description
                : dataContext.RecruitFlowSetted.FirstOrDefault(
                    ic => ic.RecruitType == (int)RecruitType.Request)
                    .Description);
        }

        private string GetRequestStatus(EnterpriseJobRequester jobRequester)
        {
            return (!jobRequester.JobRequestRecruitStage.Any()
                ? dataContext.RecruitFlowSetted.FirstOrDefault(
                    ic => ic.RecruitType == (int)RecruitType.Request).Name
                : jobRequester.JobRequestRecruitStage.OrderByDescending(ic => ic.ID)
                    .FirstOrDefault()
                    .RecruitFlowSetted.Name);
        }

        private ActionResult ValidateJobRequest(string studentNum, string jobCode)
        {
            var student = dataContext.Student.FirstOrDefault(it => it.StudentNum == studentNum);
            if (student == null)
            {
                return ActionResult.CreateErrorActionResult("学号不存在!");
            }
            return ActionResult.DefaultResult;
        }
    }
}
