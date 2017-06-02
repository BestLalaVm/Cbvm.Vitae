using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Enterprise;
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
    public class EnterpriseJobRequestQueueService : BaseService, IEnterpriseJobRequestQueueService
    {
        public EnterpriseJobRequestQueuePresentation Get(EnterpriseJobRequestQueueCriteria criteria)
        {
            var jobReferral =
                dataContext.EnterpriseJobRequestQueue.FirstOrDefault(
                    it =>
                        it.ID == criteria.Id && it.EnterpriseJobReferraler.Any(ix=>ix.UserName
                         == criteria.TeacherNum &&
                        ix.UserType == (int)UserType.Teacher));
            if (jobReferral == null)
            {
                return null;
            }

            var presentation = new EnterpriseJobRequestQueuePresentation()
            {
                Education = jobReferral.EnterpriseJob.Education,
                EnterpriseName = jobReferral.EnterpriseJob.Enterprise.Name,
                JobCode = jobReferral.JobCode,
                JobName = jobReferral.EnterpriseJob.Name,
                ReferralState = (ReferralState)jobReferral.EnterpriseJobReferraler.OrderByDescending(ic=>ic.ID).FirstOrDefault().ReferralState,
                SalaryScope = jobReferral.EnterpriseJob.SalaryScope,
                Sex = (SexType)jobReferral.EnterpriseJob.Sex,
                StartTime = jobReferral.EnterpriseJob.StartTime,
                EndTime = jobReferral.EnterpriseJob.EndTime,
                WorkPlace = jobReferral.EnterpriseJob.WorkPlace,
                Id = jobReferral.ID,
                Class = jobReferral.Student.Class,
                CreateTime = jobReferral.CreateTime,
                StudentNum = jobReferral.StudentNum
            };

            if (criteria.IncludeRelativeData)
            {
                presentation.JobReferralers =
                    jobReferral.EnterpriseJobReferraler.Select(
                        it => new EnterpriseJobReferralerPresentation()
                        {
                            Content = it.Content,
                            ReferralState = (ReferralState)it.ReferralState,
                            UserName = it.UserName,
                            UserType = (UserType)it.UserType,
                            NameZh = (it.UserType != (int)UserType.Teacher)
                                ? ""
                                : (dataContext.Teacher.FirstOrDefault(ic => ic.TeacherNum == it.UserName)
                                    .NameZh)
                        }).ToList();
            }

            return presentation;
        }

        public EntityCollection<AskedReferralsJobPresentation> GetAllAskedReferrals(
            EnterpriseJobRequestQueueCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseJobRequestQueue
                where
                    !it.EnterpriseJob.Enterprise.IsDeleted &&
                    it.EnterpriseJobReferraler.Any(
                        ix => ix.UserName == criteria.TeacherNum && ix.UserType == (int) UserType.Teacher)
                    && it.RequestQueueType == (int) RequestQueueType.AskForReferral
                select it;
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

            if (!String.IsNullOrEmpty(criteria.WorkPlace))
            {
                query = from it in query where it.EnterpriseJob.WorkPlace.Contains(criteria.WorkPlace) select it;
            }

            if (!String.IsNullOrEmpty(criteria.MarjorCode))
            {
                query = from it in query where it.Student.MarjorCode==criteria.MarjorCode select it;
            }

            if (!String.IsNullOrEmpty(criteria.StudentName))
            {
                query = from it in query where it.Student.NameZh.Contains(criteria.StudentName) select it;
            }

            if (!String.IsNullOrEmpty(criteria.StudentNum))
            {
                query = from it in query where it.Student.StudentNum.Contains(criteria.StudentNum) select it;
            }

            if (!String.IsNullOrEmpty(criteria.ReferralStateFromReferraler))
            {
                int referralValue = int.Parse(criteria.ReferralStateFromReferraler);
                query = from it in query where it.ReferralState == referralValue select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(jobReferral => new AskedReferralsJobPresentation()
            {
                JobCode = jobReferral.JobCode,
                JobName = jobReferral.EnterpriseJob.Name,
                ReferralState = (ReferralState)jobReferral.ReferralState,
                RequestTime = jobReferral.CreateTime,
                SalaryScope = jobReferral.EnterpriseJob.SalaryScope,
                StartTime = jobReferral.EnterpriseJob.StartTime,
                EndTime = jobReferral.EnterpriseJob.EndTime,
                WorkPlace = jobReferral.EnterpriseJob.WorkPlace,
                AskedReferralNote = jobReferral.Note,
                EnterpriseName = jobReferral.EnterpriseJob.Enterprise.Name,
                StudentNum = jobReferral.Student.StudentNum,
                StudentName = jobReferral.Student.NameZh,
                SexRequired = GlobalBaseDataCache.GetSexLabel(jobReferral.EnterpriseJob.Sex),
                Education = jobReferral.EnterpriseJob.Education,
                Id = jobReferral.ID,
                StudentClass = jobReferral.Student.Class,
                RequestQueueType = (RequestQueueType)jobReferral.RequestQueueType,
                StudentMarjorName = GlobalBaseDataCache.GetMarjorName(jobReferral.Student.MarjorCode)
            }).ToList();

            EntityCollection<AskedReferralsJobPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult ChangeReferralState(string teacherNum, int referralsQueueID, ReferralState state,
            string description)
        {
            var jobReferralsQueue =
                dataContext.EnterpriseJobRequestQueue.FirstOrDefault(it => it.ID == referralsQueueID);
            if (jobReferralsQueue == null)
            {
                return ActionResult.NotFoundResult;
            }
            var jobReferraler =
                jobReferralsQueue.EnterpriseJobReferraler.FirstOrDefault(
                    it => it.UserName == teacherNum && it.UserType == (int) UserType.Teacher);

            //只要老师点击推荐或者拒绝的话,则学生的投递信息将直接到达企业那边
            if (jobReferraler.ReferralState != (int) state)
            {
                jobReferraler.ReferralState = (int) state;
                if (!jobReferralsQueue.EnterpriseJobRequester.Any() &&
                    jobReferraler.ReferralState == (int) ReferralState.Passed)
                {
                    var jobRequest = new EnterpriseJobRequester()
                    {
                        JobCode = jobReferralsQueue.JobCode,
                        StudentNum = jobReferralsQueue.StudentNum,
                        RequestTime = DateTime.Now,
                    };
                    jobReferralsQueue.EnterpriseJobRequester.Add(jobRequest);
                    jobReferralsQueue.IsProcessed = true;

                    EmailTemplateHelper.NotifyEnterpriseJobStatusEmail(jobRequest, EnterpriseEmailType.AgreeToReferral,
                        dataContext, teacherNum);
                }
            }
            if (jobReferralsQueue.ReferralState != (int) state)
            {
                jobReferralsQueue.ReferralState = (int) state;
                jobReferralsQueue.Content = description;
            }
            jobReferraler.ReferralState = (int) state;
            jobReferraler.Content = description;
            jobReferraler.EnterpriseJobReferralerRemark.Add(new EnterpriseJobReferralerRemark()
            {
                Content = description,
                CreateTime = DateTime.Now,
                ReferralState = (int) state
            });
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult RecommendStudentToJobFromTeacher(string jobCode, List<string> studentCodes, string note,
            string teacherNum)
        {
            var enterpriseJob = dataContext.EnterpriseJob.FirstOrDefault(ix => ix.Code == jobCode);
            if (enterpriseJob == null) return ActionResult.NotFoundResult;
            studentCodes.ForEach(studentNum =>
            {
                var jobRequest =
                    dataContext.EnterpriseJobRequester.FirstOrDefault(
                        it =>
                            it.JobCode == jobCode && it.RequestQueueID != null &&
                            it.EnterpriseJobRequestQueue.StudentNum == studentNum &&
                            it.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Any(ix => ix.UserName == teacherNum &&
                                                                                            ix.UserType ==
                                                                                            (int) UserType.Teacher));
                if (jobRequest == null)
                {
                    jobRequest = new EnterpriseJobRequester()
                    {
                        JobCode = jobCode,
                        StudentNum = studentNum,
                        RequestTime = DateTime.Now
                    };

                    jobRequest.EnterpriseJobRequestQueue = new EnterpriseJobRequestQueue()
                    {
                        JobCode = jobCode,
                        StudentNum = studentNum,
                        IsProcessed = true,
                        //this will be true only when it is autocally sent to enterprise
                        IsSendToEnterprise = false,
                        CreateTime = DateTime.Now,
                        ReferralState = (int) ReferralState.Passed,
                        RequestQueueType = (int) RequestQueueType.Referraled,
                        Content = note
                    };
                    var jobReferraler = new EnterpriseJobReferraler()
                    {
                        CreateTime = DateTime.Now,
                        Content = note,
                        ReferralState = (int) ReferralState.Passed,
                        UserName = teacherNum,
                        UserType = (int)UserType.Teacher,
                    };
                    jobReferraler.EnterpriseJobReferralerRemark.Add(new EnterpriseJobReferralerRemark()
                    {
                        Content = note,
                        CreateTime = DateTime.Now,
                        ReferralState = (int)ReferralState.Passed
                    });
                    jobRequest.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Add(jobReferraler);

                    enterpriseJob.EnterpriseJobRequester.Add(jobRequest);

                    EmailTemplateHelper.NotifyEnterpriseJobStatusEmail(jobRequest,
                        EnterpriseEmailType.ReferraledToEnterprise, dataContext, teacherNum);
                }
            });

            dataContext.SubmitChanges();

            return new ActionResult()
            {
                IsSucess = true,
                Message = "推荐成功!"
            };
        }


        public EntityCollection<EnterpriseJobForStudentPresentation> GetAllForStudents(
            EnterpriseJobRequestQueueCriteria criteria, string studentNum)
        {
            var query = from it in dataContext.EnterpriseJobRequestQueue
                where it.StudentNum == studentNum
                select it;

            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query where it.EnterpriseJob.Enterprise.Name.Contains(criteria.EnterpriseName.Trim()) select it;
            }
            if (!String.IsNullOrEmpty(criteria.JobName))
            {
                query = from it in query where it.EnterpriseJob.Name.Contains(criteria.JobName.Trim()) select it;
            }
            if (!String.IsNullOrEmpty(criteria.WorkPlace))
            {
                query = from it in query where it.EnterpriseJob.WorkPlace.Contains(criteria.WorkPlace) select it;
            }
            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(ic => new EnterpriseJobForStudentPresentation()
            {
                Code = ic.JobCode,
                Education = ic.EnterpriseJob.Education,
                StartTime = ic.EnterpriseJob.StartTime,
                EndTime = ic.EnterpriseJob.EndTime,
                EnterpriseName = ic.EnterpriseJob.Enterprise.Name,
                Name = ic.EnterpriseJob.Name,
                Num = ic.EnterpriseJob.Num,
                SalaryScope = ic.EnterpriseJob.SalaryScope,
                SexRequired = GlobalBaseDataCache.GetSexLabel(ic.EnterpriseJob.Sex),
                WorkPlace = ic.EnterpriseJob.WorkPlace
            }).ToList();

            EntityCollection<EnterpriseJobForStudentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }


        public EntityCollection<TeacherReferralJobForStudentPresentation> GetAllTeacherReferralForStudents(EnterpriseJobRequestQueueCriteria criteria, string studentNum)
        {
            var query = from it in dataContext.EnterpriseJobRequester
                where it.StudentNum == studentNum && it.EnterpriseJobRequestQueue != null
                select it;

            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query where it.EnterpriseJob.Enterprise.Name.Contains(criteria.EnterpriseName.Trim()) select it;
            }
            if (!String.IsNullOrEmpty(criteria.JobName))
            {
                query = from it in query where it.EnterpriseJob.Name.Contains(criteria.JobName.Trim()) select it;
            }
            if (!String.IsNullOrEmpty(criteria.WorkPlace))
            {
                query = from it in query where it.EnterpriseJob.WorkPlace.Contains(criteria.WorkPlace) select it;
            }
            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new TeacherReferralJobForStudentPresentation()
            {
                Code = it.JobCode,
                Name = it.EnterpriseJob.Name,
                Num = it.EnterpriseJob.Num,
                WorkPlace = it.EnterpriseJob.WorkPlace,
                StartTime = it.EnterpriseJob.StartTime,
                RecruitTypeStatus=GlobalBaseDataCache.GetReferralStateName((ReferralState)it.EnterpriseJobRequestQueue.ReferralState),
                SexRequired = GlobalBaseDataCache.GetSexLabel(it.EnterpriseJob.Sex),
                SalaryScope = it.EnterpriseJob.SalaryScope,
                ReferralTime = it.EnterpriseJobRequestQueue.CreateTime,
                EndTime = it.EnterpriseJob.EndTime,
                EnterpriseName = it.EnterpriseJob.Enterprise.Name,
                Education = it.EnterpriseJob.Education,
                Referralers =
                    String.Join(",",
                        it.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Select(
                            ix => GlobalBaseDataCache.GetTeacherName(ix.UserName)).ToList()),
                ActualReferralers = String.Join(",",
                    it.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Where(
                        ix => ix.ReferralState == (int) ReferralState.Passed).Select(
                            ix => GlobalBaseDataCache.GetTeacherName(ix.UserName)).ToList())
            }).ToList();

            EntityCollection<TeacherReferralJobForStudentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        private object lockObject = new object();
        private object lockObject2 = new object();
        public ActionResult ChangeReferralJobQueueState(string jobCode, int referralId)
        {
            lock (lockObject)
            {
                var jobReferral =
                    dataContext.EnterpriseJobRequestQueue.FirstOrDefault(
                        it => it.ID == referralId && !it.IsProcessed && !it.IsSendToEnterprise
                              && it.ReferralState == (int) ReferralState.Pending);
                if (jobReferral != null)
                {
                    if (!jobReferral.IsProcessed && !jobReferral.IsSendToEnterprise)
                    {
                        lock (lockObject2)
                        {
                            var jobRequest =
                                dataContext.EnterpriseJobRequester.FirstOrDefault(ic => ic.ID == referralId);
                            if (jobRequest != null)
                            {
                                jobRequest = new EnterpriseJobRequester()
                                {
                                    RequestQueueID = jobReferral.ID,
                                    JobCode = jobReferral.JobCode,
                                    StudentNum = jobReferral.StudentNum,
                                    RequestTime = DateTime.Now
                                };
                                dataContext.EnterpriseJobRequester.InsertOnSubmit(jobRequest);
                            }

                            jobReferral.IsProcessed = true;
                            jobReferral.IsSendToEnterprise = true;
                            jobReferral.ReferralState = (int) ReferralState.TimeOut;
                            dataContext.SubmitChanges();
                        }
                    }
                }
            }

            return new ActionResult()
            {
                IsSucess = true
            };
        }
    }
}
