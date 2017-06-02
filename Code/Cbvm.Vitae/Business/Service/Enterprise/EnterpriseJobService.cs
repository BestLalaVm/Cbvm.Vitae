using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Enterprise;
using LkDataContext;
using Presentation;
using Presentation.Cache;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;

namespace Business.Service.Enterprise
{
    public class EnterpriseJobService:BaseService, IEnterpriseJobService
    {
        public EntityCollection<EnterpriseJobPresentation> GetAll(EnterpriseJobCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseJob
                where !it.Enterprise.IsDeleted && !it.IsDelete
                      && it.EnterpriseCode == criteria.EnterpriseCode
                select it;

            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query where it.Enterprise.Name.Contains(criteria.EnterpriseName.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.WorkPlace))
            {
                query = from it in query where it.WorkPlace.Contains(criteria.WorkPlace.Trim()) select it;

            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime), criteria, out totalCount);

            var list = query.Select(it => new EnterpriseJobPresentation
            {
                Code = it.Code,
                Name = it.Name,
                Num = it.Num,
                Education = it.Education,
                EndTime = it.EndTime,
                StartTime = it.StartTime,
                WorkPlace = it.WorkPlace,
                SalaryScope = it.SalaryScope,
                //Address = it.Address,
                AgeScope = it.AgeScope,
                ContactName = it.ContactName,
                CreateTime = it.CreateTime,
                DepartName = it.DepartName,
                Description = it.Description,
                DisplayOrder = it.DisplayOrder,
                IsOnline = it.IsOnline,
                Nature = it.Nature,
                Professional = it.Professional,
                RecruitmentTargets = it.RecruitmentTargets,
                Sex =  it.Sex,
                Tax = it.Tax,
                Telephone = it.Telephone
            }).ToList();

            EntityCollection<EnterpriseJobPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public EnterpriseJobPresentation Get(string code,bool includeRelativeData)
        {
            return 
                dataContext.EnterpriseJob.Where(it => it.Code == code && !it.IsDelete).Select(it => new EnterpriseJobPresentation()
                {
                    Code = it.Code,
                    Name = it.Name,
                    Num = it.Num,
                    Education = it.Education,
                    EndTime = it.EndTime,
                    StartTime = it.StartTime,
                    WorkPlace = it.WorkPlace,
                    SalaryScope = it.SalaryScope,
                    //Address=it.Address,
                    AgeScope=it.AgeScope,
                    ContactName=it.ContactName,
                    Description=it.Description,
                    IsOnline=it.IsOnline,
                    Telephone=it.Telephone,
                    Enterprise = (!includeRelativeData ? null : new EnterprisePresentation()
                    {
                        //Address = it.Enterprise.Address,
                        Code = it.Enterprise.Code,
                        ContactName = it.Enterprise.ContactName,
                        ContactPhone = it.Enterprise.ContactPhone,
                        Corporation = it.Enterprise.Corporation,
                        CreateTime = it.Enterprise.CreateTime,
                        Description = it.Enterprise.Description,
                        Email = it.Enterprise.Email,
                        EnterpriseTypeCode = it.Enterprise.EnterpriseTypeCode,
                        Id = it.Enterprise.ID,
                        IndustryCode = it.Enterprise.IndustryCode,
                        LicenseNoImage = it.Enterprise.LicenseNoImage,
                        OrganizationCodeImage = it.Enterprise.OrganizationCodeImage,
                        Name = it.Enterprise.Name,
                        RegionCode = it.Enterprise.RegionCode,
                        ScopeCode = it.Enterprise.ScopeCode,
                        UserName = it.Enterprise.UserName,
                        VerifyStatus = (VerifyStatus)it.Enterprise.VerifyStatus,
                        WebSite = it.Enterprise.WebSite,
                    })
                }).FirstOrDefault();
        }

        public ActionResult Save(EnterpriseJobPresentation presentation)
        {
            try
            {
                var enterpriseJob = dataContext.EnterpriseJob.FirstOrDefault(it => it.Code == presentation.Code);
                if (enterpriseJob == null)
                {
                    enterpriseJob = new EnterpriseJob()
                    {
                        Code =
                            GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.EnterpriseJob,
                                presentation.EnterpriseCode),
                        CreateTime = DateTime.Now,
                        EnterpriseCode = presentation.EnterpriseCode
                    };
                    dataContext.EnterpriseJob.InsertOnSubmit(enterpriseJob);
                }

                enterpriseJob.Address = presentation.Address;
                enterpriseJob.AgeScope = presentation.AgeScope;
                enterpriseJob.ContactName = presentation.ContactName;
                enterpriseJob.DepartName = presentation.DepartName;
                enterpriseJob.Description = presentation.Description;
                enterpriseJob.DisplayOrder = presentation.DisplayOrder;
                enterpriseJob.Education = presentation.Education;
                enterpriseJob.EndTime = presentation.EndTime;
                enterpriseJob.IsOnline = presentation.IsOnline;
                enterpriseJob.Name = presentation.Name;
                enterpriseJob.Nature = presentation.Nature;
                enterpriseJob.Num = presentation.Num;
                enterpriseJob.Professional = presentation.Professional;
                enterpriseJob.SalaryScope = presentation.SalaryScope;
                enterpriseJob.Sex = (int) presentation.Sex;
                enterpriseJob.StartTime = presentation.StartTime;
                enterpriseJob.Tax = presentation.Tax = presentation.Tax;
                enterpriseJob.Telephone = presentation.Telephone;
                enterpriseJob.WorkPlace = presentation.WorkPlace;
                enterpriseJob.RecruitmentTargets = presentation.RecruitmentTargets;

                enterpriseJob.ProvinceCode = String.IsNullOrEmpty(presentation.ProvinceCode) ? null : presentation.ProvinceCode;
                enterpriseJob.ProvinceName = presentation.ProvinceName;
                enterpriseJob.CityCode =String.IsNullOrEmpty(presentation.CityCode)?null: presentation.CityCode;
                enterpriseJob.CityName = presentation.CityName;
                enterpriseJob.DistrictCode = String.IsNullOrEmpty(presentation.DistrictCode) ? null : presentation.DistrictCode;
                enterpriseJob.DistrictName = presentation.DistrictName;

                enterpriseJob.StreetCode = String.IsNullOrEmpty(presentation.StreetCode) ? null : presentation.StreetCode;
                enterpriseJob.StreetName = presentation.StreetName;

                dataContext.SubmitChanges();

                return ActionResult.DefaultResult;
            }
            catch (Exception ex)
            {
                return ActionResult.CreateErrorActionResult(ex.ToString());
            }
        }

        public ActionResult Delete(EnterpriseJobCriteria criteria)
        {
            var enterpriseJob =
                dataContext.EnterpriseJob.FirstOrDefault(
                    it => it.Code == criteria.Code && it.EnterpriseCode == criteria.EnterpriseCode);
            if (enterpriseJob == null)
            {
                return ActionResult.NotFoundResult;
            }
            enterpriseJob.IsDelete = true;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<EnterpriseJobForStudentPresentation> GetAllForStudents(EnterpriseJobCriteria criteria,
            string studentNum)
        {
            var query = GetBaseForExternalerQuery(criteria);
            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime), criteria, out totalCount);

            var list = query.Select(ic => new EnterpriseJobForStudentPresentation()
            {
                Code = ic.Code,
                EnterpriseName = ic.Enterprise.Name,
                Education = ic.Education,
                EndTime = ic.EndTime,
                Name = ic.Name,
                Num = ic.Num,
                SalaryScope = ic.SalaryScope,
                WorkPlace = ic.WorkPlace,
                StartTime = ic.StartTime,
                SexRequired = GlobalBaseDataCache.GetSexLabel(ic.Sex),
                JobRequestDescription = ic.EnterpriseJobRequestQueue.Any(ix => ix.StudentNum == studentNum)
                    ? "申请推荐过"
                    : (ic.EnterpriseJobRequester.Any(ix => ix.StudentNum == studentNum) ? "申请过" : "")
            }).ToList();

            EntityCollection<EnterpriseJobForStudentPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;

            return null;
        }

        public List<EnterpriseJobPresentation> GetEnterpriseJobRequest(int pageSize = 20)
        {
            var query = GetBaseForExternalerQuery(new EnterpriseJobCriteria()
            {
                PageSize = pageSize,
                NeedPaging = false
            });
            var list = query.Take(pageSize).Select(ix => new EnterpriseJobPresentation()
            {
                Code = ix.Code,
                Name = ix.Name,
                Num = ix.Num,
                Nature = ix.Nature,
                //Address = ix.Address,
                AgeScope = ix.AgeScope,
                ContactName = ix.ContactName,
                DepartName = ix.DepartName,
                Description = ix.Description,
                DisplayOrder = ix.DisplayOrder,
                Education = ix.Education,
                EndTime = ix.EndTime,
                EnterpriseCode = ix.EnterpriseCode,
                IsOnline = ix.IsOnline,
                Professional = ix.Professional,
                RecruitmentTargets = ix.RecruitmentTargets,
                SalaryScope = ix.SalaryScope,
                Sex =  ix.Sex,
                StartTime = ix.StartTime,
                Tax = ix.Tax,
                Telephone = ix.Telephone,
                WorkPlace = ix.WorkPlace,
                CreateTime = ix.CreateTime,
                Enterprise = new EnterprisePresentation()
                {
                    Code = ix.Enterprise.Code,
                    //Address = ix.Enterprise.Address,
                    ContactName = ix.Enterprise.ContactName,
                    ContactPhone = ix.Enterprise.ContactPhone,
                    Corporation = ix.Enterprise.Corporation,
                    CreateTime = ix.Enterprise.CreateTime,
                    Description = ix.Enterprise.Description,
                    Email = ix.Enterprise.Email,
                    EnterpriseTypeCode = ix.Enterprise.EnterpriseTypeCode,
                    IndustryCode = ix.Enterprise.IndustryCode,
                    IsOnline = ix.Enterprise.IsOnline,
                    LicenseNoImage = ix.Enterprise.LicenseNoImage,
                    OrganizationCodeImage = ix.Enterprise.OrganizationCodeImage,
                    Name = ix.Enterprise.Name,
                    RegionCode = ix.Enterprise.RegionCode,
                    ScopeCode = ix.Enterprise.ScopeCode,
                    UserName = ix.Enterprise.UserName,
                    VerifyStatus = (VerifyStatus) ix.Enterprise.VerifyStatus,
                    WebSite = ix.Enterprise.WebSite
                }
            }).ToList();

            return list;
        }

        internal static string GetJobRequestStatus(EnterpriseJobRequester jobRequest, string studentNum)
        {
            if (jobRequest != null)
            {
                var jobQueue = jobRequest.EnterpriseJobRequestQueue;
                JobRequestRecruitStage stage = null;
                if (jobQueue == null ||
                    jobRequest.RequestTime > jobQueue.CreateTime)
                {
                    stage = jobRequest.JobRequestRecruitStage.OrderByDescending(ic => ic.ID).FirstOrDefault();
                }
                else
                {
                    if (!jobQueue.EnterpriseJobRequester.Any())
                    {
                        return "请求推荐中";
                    }
                    stage = jobQueue.EnterpriseJobRequester.FirstOrDefault()
                        .JobRequestRecruitStage.OrderByDescending(ic => ic.ID)
                        .FirstOrDefault();
                }

                if (stage == null)
                {
                    return EnumHelper.GetEnumDescription(RecruitType.Request);
                }
                else
                {
                    return EnumHelper.GetEnumDescription((RecruitType) stage.RecruitFlowSetted.RecruitType);
                }
            }
            return "请求推荐中";
        }

        public EntityCollection<EnterpriseJobForTeacherPresentation> GetAllForTeachers(EnterpriseJobCriteria criteria)
        {
            var query = GetBaseForExternalerQuery(criteria);
            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime), criteria, out totalCount);

            var list = query.Select(ic => new EnterpriseJobForTeacherPresentation()
            {
                Code = ic.Code,
                EnterpriseName = ic.Enterprise.Name,
                Education = ic.Education,
                EndTime = ic.EndTime,
                Name = ic.Name,
                Num = ic.Num,
                SalaryScope = ic.SalaryScope,
                WorkPlace = ic.WorkPlace,
                StartTime = ic.StartTime,
                SexRequired = GlobalBaseDataCache.GetSexLabel(ic.Sex)
            }).ToList();

            EntityCollection<EnterpriseJobForTeacherPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        private IQueryable<EnterpriseJob> GetBaseForExternalerQuery(EnterpriseJobCriteria criteria)
        {
            var query = from it in dataContext.EnterpriseJob
                        where !it.Enterprise.IsDeleted && it.Enterprise.IsOnline && !it.IsDelete
                        select it;
            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = from it in query where it.Enterprise.Name.Contains(criteria.EnterpriseName.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query where it.Name.Contains(criteria.Name.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.WorkPlace))
            {
                query = from it in query where it.WorkPlace.Contains(criteria.WorkPlace.Trim()) select it;

            }
            return query;
        }
    }
}
