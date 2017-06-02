using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Xml.XPath;
using Business.Interface.Enterprise;
using Business.Interface.Student;
using Business.Service.Enterprise;
using Business.Service.Student;
using Presentation;
using Presentation.Cache;
using Presentation.Enum;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Ajax
{
    /// <summary>
    /// Summary description for ExternalService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ExternalService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public ListPresentation<EnterprisePresentation> GetEnterpriseList(TopEnterpriseType topType, int pageSize = 20)
        {
            if (pageSize < 1)
            {
                pageSize = 20;
            }

            var enterpriseServer = new EnterpriseService();
            HotEnterpriseType hotType = HotEnterpriseType.ALL;
            switch (topType)
            {
                case TopEnterpriseType.TopHotJob:
                    hotType = HotEnterpriseType.TopHotJob;
                    break;
                case TopEnterpriseType.TopNewest:
                    hotType = HotEnterpriseType.TopNewest;
                    break;
                case TopEnterpriseType.TopNotified:
                    hotType = HotEnterpriseType.TopNotified;
                    break;
                case TopEnterpriseType.TopSalary:
                    hotType = HotEnterpriseType.TopSalary;
                    break;
            }


            var list = enterpriseServer.GetFrontHotEnterpriseList(hotType, 1, pageSize);

            return new ListPresentation<EnterprisePresentation>()
                {
                    MoreLink = "/Enterprise/",
                    List = list.Select(it => new EnterprisePresentation()
                        {
                            Code = it.Code,
                            Name = it.Name,
                            Link = UrlRuleHelper.GenerateUrl(it.Code, it.Name, RulePathType.Enterprise)
                        }).ToList()
                };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public ListPresentation<JobPresentation> GetEnterpriseJobList(int pageSize = 20)
        {
            IEnterpriseJobService service = new EnterpriseJobService();

            if (pageSize < 1)
            {
                pageSize = 20;
            }

            var list = service.GetEnterpriseJobRequest(pageSize);
            var result = new ListPresentation<JobPresentation>()
            {
                MoreLink = "",
                List = list.Select(it => new JobPresentation
                {
                    Code = it.Code,
                    Address = it.Address,
                    ContactName = it.ContactName,
                    Education = it.Education,
                    Name = it.Name,
                    Num = it.Num,
                    RecruitmentTargets = it.RecruitmentTargets,
                    SalaryScope = it.SalaryScope,
                    WorkPlace = it.WorkPlace,
                    Enterprise = new EnterprisePresentation()
                    {
                        Address = it.Enterprise.Address,
                        Code = it.Enterprise.Code,
                        Description = it.Enterprise.Description,
                        Name = it.Enterprise.Name,
                        Link = UrlRuleHelper.GenerateUrl(it.Code, it.Name, RulePathType.Enterprise)
                    }
                }).ToList()
            };
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public ListPresentation<StudentPresentation> GetTopStudentList(int pageSize = 20)
        {
            if (pageSize < 1)
            {
                pageSize = 20;
            }

            IStudentService service = new StudentService();
            var list = service.GetFrontStudentList(StudentSearchType.TopClick, pageSize, null);

            return
                new ListPresentation<StudentPresentation>()
                {
                    MoreLink = "/Student/",
                    List =
                        list.Select(it => new StudentPresentation()
                        {
                            Code = it.StudentNum,
                            NameZh = it.NameZh,
                            NameEn = it.NameEn,
                            MarjorName =
                                GlobalBaseDataCache.GetMarjorName(it.MarjorCode),
                            //DepartName =
                            //    GlobalBaseDataCache.GetDepartName(it.DepartCode),
                            Sex = GlobalBaseDataCache.GetSexLabel(it.Sex),
                            Link =
                                UrlRuleHelper.GenerateUrl(it.StudentNum, it.NameZh,
                                    RulePathType.StudentInfo)
                        }).ToList()
                };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public ListPresentation<StudentJobRequestPresentation> GetStudentJobRequestList(string studentCode,
                                                                                        int pageSize = 20)
        {
            if (pageSize < 1)
            {
                pageSize = 20;
            }

            IEnterpriseJobRequestService service=new EnterpriseJobRequestService();
            var list = service.GetJobRequestForStudent(studentCode, pageSize);
            return
                new ListPresentation<StudentJobRequestPresentation>()
                    {
                        MoreLink = "",
                        List = list.Select(it => new StudentJobRequestPresentation()
                            {
                                JobName = it.JobName,
                                JobNum = it.JobNum,
                                JobCode = it.JobCode,
                                Address = it.Address,
                                ContactName = it.ContactName,
                                CompanyName = it.EnterpriseName,
                                JobRequestTime = it.RequestTime,
                                WorkPlace = it.WorkPlace
                            }).ToList()
                    };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public ListPresentation<StudentProjectPresentation> GetStudentProjectList(string studentCode, int pageSize = 20)
        {
            if (pageSize < 1)
            {
                pageSize = 20;
            }
            IStudentProjectService service = new StudentProjectService();
            var list = service.GetTopProjectList(studentCode, pageSize);

            return new ListPresentation<StudentProjectPresentation>()
                {
                    MoreLink = UrlRuleHelper.GenerateStudentMoreUrl(studentCode, StudentRulePathType.Project),
                    List = list.Select(it => new StudentProjectPresentation()
                        {
                            ID = it.Id,
                            Name = it.Name,
                            Description = it.Description,
                            Link =
                                UrlRuleHelper.GenerateUrl(studentCode, it.Id.ToString(), it.Name,
                                                          StudentRulePathType.Project)
                        }).ToList()
                };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public ListPresentation<TeacherVerifyProjectPresentation> GetTeacherVerifyProjectList(
            StudentVerifyStatus status,
            string teacherNum,
            int pageSize = 20)
        {
            if (pageSize < 1)
            {
                pageSize = 20;
            }
            IStudentProjectService service=new StudentProjectService();
            VerifyStatus verifyStatus = TranslateToVerifyStatus(status) ?? VerifyStatus.UnPassed;
            var list = service.GetTeacherVerifyProjectList(teacherNum, verifyStatus, pageSize);
            var responseList = new ListPresentation<TeacherVerifyProjectPresentation>()
                {
                    List = new List<TeacherVerifyProjectPresentation>()
                };

            list.ForEach(project =>
            {
                responseList.List.Add(new TeacherVerifyProjectPresentation()
                {
                    Name = project.Name,
                    Description = project.Description,
                    ID = project.Id,
                    Status = EnumHelper.GetEnumDescription(project.VerfyStatus),
                    TeacherNum = project.TeacherNum,
                    TeacherName = GlobalBaseDataCache.GetTeacherName(project.TeacherNum)
                });
            });

            return responseList;
        }

        private VerifyStatus? TranslateToVerifyStatus(StudentVerifyStatus? status)
        {
            switch (status)
            {
                case StudentVerifyStatus.Passed:
                    return VerifyStatus.Passed;
                case StudentVerifyStatus.UnPassed:
                    return VerifyStatus.UnPassed;
                case StudentVerifyStatus.WaitAudited:
                    return VerifyStatus.WaitAudited;
                default:
                    return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResultData SubmitResume(string studentNum, string jobCode)
        {
            var response = new JsonResultData();

            var server = new EnterpriseJobRequestService();
            var result = server.AddRequestJob(studentNum, jobCode);

            if (result.IsSucess)
            {
                if (String.IsNullOrEmpty(result.Message))
                {
                    response.Message = "投递成功!";
                }
                else
                {
                    response.Message = result.Message;
                }                
            }
            else
            {
                response.Message = "投递失败!";
            }

            return response;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResultData InviteStudent(string enterpriseCode, string studentNum)
        {
            var response = new JsonResultData();

            var server = new EnterpriseService();

            var result = server.InviteStudent(studentNum, enterpriseCode);

            if (result.IsSucess)
            {
                if (String.IsNullOrEmpty(result.Message))
                {
                    response.Message = "邀请成功!";
                }
                else
                {
                    response.Message = result.Message;
                }
            }
            else
            {
                response.Message = "邀请失败!";
            }

            return response;
        }

    }

    public enum TopEnterpriseType
    {
        TopNotified = 0,
        TopSalary = 1,
        TopHotJob = 2,
        TopNewest = 3,
        ALL = 4
    }

    public enum StudentVerifyStatus
    {
        All=0,
        WaitAudited = 1,
        Passed = 2,
        UnPassed = 3,
    }

    public class EnterprisePresentation : Presentation
    {
        [System.ComponentModel.Description("编码")]
        public string Code { get; set; }

        [System.ComponentModel.Description("企业名称")]
        public string Name { get; set; }

        [System.ComponentModel.Description("描述")]
        public string Description { get; set; }

        [System.ComponentModel.Description("地址")]
        public string Address { get; set; }
    }

    public class JobPresentation : Presentation
    {
        [System.ComponentModel.Description("编码")]
        public string Code { get; set; }

        [System.ComponentModel.Description("职位名称")]
        public string Name { get; set; }

        [System.ComponentModel.Description("招聘数量")]
        public int Num { get; set; }

        [System.ComponentModel.Description("联系人")]
        public string ContactName { get; set; }

        [System.ComponentModel.Description("工作地点")]
        public string WorkPlace { get; set; }

        [System.ComponentModel.Description("学历")]
        public string Education { get; set; }

        [System.ComponentModel.Description("工作地址")]
        public string Address { get; set; }

        [System.ComponentModel.Description("薪资范围")]
        public string SalaryScope { get; set; }

        [System.ComponentModel.Description("招聘对象(工作年限要求)")]
        public string RecruitmentTargets { get; set; }

        public EnterprisePresentation Enterprise { get; set; }
    }

    public class StudentPresentation : Presentation
    {
        public string Code { get; set; }

        public string NameEn { get; set; }

        public string NameZh { get; set; }

        public string MarjorName { get; set; }

        public string DepartName { get; set; }

        public string Sex { get; set; }
    }

    public class StudentProjectPresentation : Presentation
    {
        [System.ComponentModel.Description("编号")]
        public int ID { get; set; }

        [System.ComponentModel.Description("项目名称")]
        public string Name { get; set; }

        [System.ComponentModel.Description("项目描述")]
        public string Description { get; set; }
    }

    public class StudentJobRequestPresentation : Presentation
    {
        [System.ComponentModel.Description("职位编码")]
        public string JobCode { get; set; }

        [System.ComponentModel.Description("职位名称")]
        public string JobName { get; set; }

        [System.ComponentModel.Description("招聘数量")]
        public int JobNum { get; set; }

        [System.ComponentModel.Description("公司名称")]
        public string CompanyName { get; set; }

        [System.ComponentModel.Description("投递时间")]
        public DateTime JobRequestTime { get; set; }

        [System.ComponentModel.Description("公司地址")]
        public string Address { get; set; }

        [System.ComponentModel.Description("联系人")]
        public string ContactName { get; set; }

        [System.ComponentModel.Description("工作地点")]
        public string WorkPlace { get; set; }

    }

    public class TeacherVerifyProjectPresentation : StudentProjectPresentation
    {
        [System.ComponentModel.Description("教师工号")]
        public string TeacherNum { get; set; }

        [System.ComponentModel.Description("教师姓名")]
        public string TeacherName { get; set; }

        [System.ComponentModel.Description("审核状态")]
        public string Status { get; set; }
    }

    public class Presentation
    {
        public string Link { get; set; }
    }

    public class ListPresentation<T> where T : Presentation
    {
        public List<T> List { get; set; }

        public string MoreLink { get; set; }
    }
}
