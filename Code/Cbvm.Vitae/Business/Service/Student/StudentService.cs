using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Business.Interface.Student;
using LkDataContext;
using Microsoft.SqlServer.Server;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Student;
using Presentation.UIView.Student.View;
using WebLibrary;
using WebLibrary.Helper;

namespace Business.Service.Student
{
    public class StudentService : BaseService, IStudentService, IAuthenticateService
    {
        #region IAuthenticateService

        public LoginUserPresentation Login(string userName, string password)
        {
            var student =
                dataContext.Student.FirstOrDefault(
                    it => !it.IsDelete && it.StudentNum == userName &&
                          it.Password == AccountSecurityManage.MD5Password(password));
            if (student != null)
            {
                return new LoginUserPresentation()
                {
                    Id = student.ID,
                    Identity = student.CollegeCode,
                    UserName = student.StudentNum,
                    UserType = UserType.Student,
                    UserLabel = student.NameZh,
                    LogTime = DateTime.Now,
                    FullName=String.Format("{0}同学",student.NameZh)
                };
            }

            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                var student =
                    dataContext.Student.FirstOrDefault(
                        it => !it.IsDelete && it.Password == AccountSecurityManage.MD5Password(oldPassword) &&
                              it.StudentNum == userName);
                if (student == null)
                {
                    return ActionResult.NotFoundResult;
                }
                student.Password = AccountSecurityManage.MD5Password(newPassword);
                dataContext.SubmitChanges();

                return ActionResult.DefaultResult;
            }
            catch (Exception ex)
            {
                WriteLog(ex);

                return ActionResult.CreateErrorActionResult(ex.ToString());
            }
        }

        public ActionResult ResetPassword(string userName, UserType userType, string checkCode)
        {
            if (CheckCodeHelper.ValidateCheckCode(userType.ToString(), checkCode))
            {
                var student = dataContext.Student.FirstOrDefault(it => !it.IsDelete && it.StudentNum == userName);
                if (student != null)
                {
                    if (!String.IsNullOrEmpty(student.Email))
                    {
                        var newPassword = AccountSecurityManage.GenerateRadomPassword();
                        student.Password = AccountSecurityManage.MD5Password(newPassword);

                        dataContext.MailQueue.InsertOnSubmit(new MailQueue()
                        {
                            CreateTime = DateTime.Now,
                            Cc = null,
                            IsSended = false,
                            Name =
                                EmailTemplateHelper.FormatterResetPasswordContent(student.NameZh, userName, newPassword,
                                    EmailTemplateHelper.GetEmailTemplateSubject(SystemEmailType.ResetPassword)),
                            Sender = EmailTemplateHelper.MailSender,
                            Receiver = student.Email,
                            ReceiverName = student.Email,
                            Message =
                                EmailTemplateHelper.FormatterResetPasswordContent(student.NameZh, userName, newPassword,
                                    EmailTemplateHelper.GetEmailTemplateBody(SystemEmailType.ResetPassword))
                        });

                        dataContext.SubmitChanges();
                        return new ActionResult()
                        {
                            IsSucess = true,
                            Message = string.Format("密码重置成功! 已经发送至您的邮箱:{0}", student.Email)
                        };
                    }
                    return new ActionResult()
                    {
                        IsSucess = false,
                        Message = "用户Email信息为空,无法进行密码重置, 请联系管理员!"
                    };
                }
                return ActionResult.CreateErrorActionResult("用户名无效!");
            }

            return ActionResult.CreateErrorActionResult("验证码错误");
        }
        #endregion

        public StudentPresentation Get(string studentNum)
        {
            return
                dataContext.Student.Where(it => it.StudentNum == studentNum)
                    .Select(it => Translate2Presentation(it, true))
                    .FirstOrDefault();
        }

        public ActionResult Save(StudentPresentation studentPresentation)
        {
            var student = GetStudent(studentPresentation.StudentNum);
            student.Address = studentPresentation.Address;
            student.Birthday = studentPresentation.Birthday;
            student.Class = studentPresentation.Class;
            student.MarjorCode = studentPresentation.MarjorCode;
            student.Description = studentPresentation.Description;
            student.Email = studentPresentation.Email;
            student.IDentityNum = studentPresentation.IDentityNum;
            student.IsMarried = studentPresentation.IsMarried;
            student.IsOnline = studentPresentation.IsOnline;

            student.NameZh = studentPresentation.NameZh;
            student.NameEn = studentPresentation.NameEn;
            student.NativePlace = studentPresentation.NativePlace;
            student.Period = studentPresentation.Period;
            if (!String.IsNullOrEmpty(studentPresentation.Photo))
            {
                student.Photo = studentPresentation.Photo;
                student.ThumbPath = studentPresentation.ThumbPath;
            }
            student.Politics = (int)studentPresentation.Politics;
            student.Sex = (int)studentPresentation.Sex;
            student.Tall = studentPresentation.Tall;
            student.Telephone = studentPresentation.Telephone;
            student.WebSite = studentPresentation.WebSite;
            student.Weight = studentPresentation.Weight;

            if (student.StudentJobExpect == null)
            {
                student.StudentJobExpect = new StudentJobExpect();
            }
            student.StudentJobExpect.JobAddress = studentPresentation.JobExpect.JobAddress;
            student.StudentJobExpect.JobContent = studentPresentation.JobExpect.JobContent;
            student.StudentJobExpect.JobRequired = studentPresentation.JobExpect.JobRequired;
            student.StudentJobExpect.JobSalary = studentPresentation.JobExpect.JobSalary;
            student.StudentJobExpect.OpenType = studentPresentation.JobExpect.OpenType;
            student.StudentJobExpect.ProvinceCode = studentPresentation.JobExpect.ProvinceCode;
            student.StudentJobExpect.ProvinceName = studentPresentation.JobExpect.ProvinceName;
            student.StudentJobExpect.CityCode = studentPresentation.JobExpect.CityCode;
            student.StudentJobExpect.CityName = studentPresentation.JobExpect.CityName;
            student.StudentJobExpect.StreetCode = studentPresentation.JobExpect.StreetCode;
            student.StudentJobExpect.StreetName = studentPresentation.JobExpect.StreetName;
            student.StudentJobExpect.DistrictCode = studentPresentation.JobExpect.DistrictCode;
            student.StudentJobExpect.DistrictName = studentPresentation.JobExpect.DistrictName;
            student.StudentJobExpect.JobAddress = studentPresentation.JobExpect.JobAddress;

            student.StudentJobExpect.LastUpdateTime = DateTime.Now;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public StudentIntroducePresentation GetIntroduce(string studentNum)
        {
            return
                dataContext.Student.Where(it => it.StudentNum == studentNum)
                    .Select(it => new StudentIntroducePresentation()
                    {
                        AboutMe = it.AboutMe,
                        Activity = it.Activity,
                        Book = it.Book,
                        Interested = it.Interested,
                        Movie = it.Movie,
                        Music = it.Music,
                        Program = it.Program,
                        StudentNum = it.StudentNum
                    }).FirstOrDefault();
        }

        public ActionResult SaveIntroduce(StudentIntroducePresentation studentIntroduce)
        {
            var student = GetStudent(studentIntroduce.StudentNum);
            if (student == null)
            {
                return ActionResult.NotFoundResult;
            }
            student.AboutMe = studentIntroduce.AboutMe;
            student.Program = studentIntroduce.Program;
            student.Music = studentIntroduce.Music;
            student.Movie = studentIntroduce.Movie;
            student.Book = studentIntroduce.Book;
            student.Interested = studentIntroduce.Interested;
            student.Activity = studentIntroduce.Activity;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        private LkDataContext.Student GetStudent(string studentNum)
        {
            var student = dataContext.Student.FirstOrDefault(it => it.StudentNum == studentNum);
            if (student == null)
            {
                student = new LkDataContext.Student()
                {
                    StudentNum = studentNum
                };
                dataContext.Student.InsertOnSubmit(student);
            }
            return student;
        }

        public EntityCollection<StudentPresentation> GetAll(StudentCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<StudentFrontPresentation> GetFrontStudentList(StudentSearchType type, int pageSize, UserType? userType)
        {
            List<StudentFrontPresentation> list = new List<StudentFrontPresentation>();
            if (type == StudentSearchType.TopClick)
            {
                list = GetTopClickStudentFront(pageSize);
            }
            else
            {
                list = GetTopEvaluateStudentFront(pageSize);
            }

            EntityCollection<StudentFrontPresentation> entityCollection = Translate2Presentations(list);
            return entityCollection;
        }

        public static void AddVisitedRecord(string studentNum, string IPAddress, string userName, UserType userType)
        {
            using (CVAcademicianDataContext dataContext = new CVAcademicianDataContext())
            {
                var studentVisited =
                    dataContext.StudentVisited.Where(
                        ix => ix.StudentNum == studentNum && ix.UserName == userName && ix.UserType == (int) userType)
                        .OrderByDescending(ix => ix.ID)
                        .FirstOrDefault();
                if (studentVisited == null ||
                    studentVisited.VisitTime <= DateTime.Now.AddHours(-1))
                {
                    dataContext.StudentVisited.InsertOnSubmit(new StudentVisited()
                    {
                        StudentNum = studentNum,
                        IPAddress = IPAddress,
                        VisitTime = DateTime.Now,
                        UserType = (int) userType,
                        UserName = userName
                    });

                    dataContext.SubmitChanges();
                }
            }
        }

        public EntityCollection<StudentVisitedPresentation> GetVisitStudentList(string studentNum,
            int pageSize = 10)
        {
            var query = from it in dataContext.Student
                where
                    dataContext.StudentVisited.Any(
                        ic =>
                            ic.UserName == it.StudentNum && ic.UserType == (int) UserType.Student &&
                            ic.StudentNum == studentNum)
                select it;

            int totalCount = 0;
            query = PageingQueryable(query, new StudentCriteria()
            {
                PageIndex = 0,
                PageSize = pageSize
            }, out totalCount);

            var list = query.Select(it => new StudentVisitedPresentation()
            {
                MarjorCode = it.MarjorCode,
                NameZh = it.NameZh,
                StudentNum = it.StudentNum,
                ThumbPath = it.ThumbPath,
                Sex = (SexType) it.Sex,
                VisitTime = (dataContext.StudentVisited.Where(ic => ic.UserName == it.StudentNum).Any()
                    ? dataContext.StudentVisited.Where(ic => ic.UserName == it.StudentNum)
                        .OrderByDescending(ic => ic.ID)
                        .FirstOrDefault()
                        .VisitTime
                    : (DateTime?) null)
            }).ToList();

            EntityCollection<StudentVisitedPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public EntityCollection<StudentEmployedFrontPresentation> GetTopEmployedStudentList(int pageSize)
        {
            var query = from t1 in dataContext.Student
                join
                    t2 in dataContext.EnterpriseJobRequester on t1.StudentNum equals t2.StudentNum
                join t3 in dataContext.EnterpriseJob on t2.JobCode equals t3.Code
                join t4 in dataContext.Enterprise on t3.EnterpriseCode equals t4.Code
                where t2.JobRequestRecruitStage.Any(ic => ic.RecruitFlowSetted.RecruitType == (int) RecruitType.Passed)
                orderby t2.ID descending
                select new
                {
                    t1.NameEn,
                    t1.NameZh,
                    t1.StudentNum,
                    t1.MarjorCode,
                    t2.RequestTime,
                    JobName = t3.Name,
                    EnterpriseName = t4.Name,
                    JobRequestId = t2.ID,
                    ReferralsQueueId = t2.RequestQueueID,
                    RecruitFlowChangedTime = t2.JobRequestRecruitStage.Max(ic => ic.CreateTime),
                    t1.Photo,
                    EmployedTime = t2.JobRequestRecruitStage.Max(ic => ic.CreateTime),
                    t2.EnterpriseJobRequestQueue
                };
            var list = query.Take(pageSize).ToList().Select(ix => new StudentEmployedFrontPresentation()
            {
                NameEn = ix.NameEn,
                NameZh = ix.NameZh,
                StudentNum = ix.StudentNum,
                MarjorCode = ix.MarjorCode,
                RequestTime = ix.RequestTime,
                JobName = ix.JobName,
                EnterpriseName = ix.EnterpriseName,
                JobRequestId = ix.JobRequestId,
                ReferralsQueueId = ix.ReferralsQueueId,
                RecruitFlowChangedTime = ix.RecruitFlowChangedTime,
                Photo = ix.Photo,
                EmployedTime = ix.EmployedTime,
                JobReferralers =
                    ix.EnterpriseJobRequestQueue == null
                        ? new List<EnterpriseJobReferralerPresentation>()
                        : ix.EnterpriseJobRequestQueue.EnterpriseJobReferraler.Select(
                            ic => new EnterpriseJobReferralerPresentation()
                            {
                                Content = ic.Content,
                                ReferralState = (ReferralState) ic.ReferralState,
                                UserName = ic.UserName,
                                UserType = (UserType) ic.UserType,
                                NameZh = GlobalAutoCache.GetAutoVlaue(ic.UserName, (UserType) ic.UserType).Name
                            }).ToList()
            }).ToList();

            return Translate2Presentations(list);
        }

        public EntityCollection<StudentCommonPresentation> GetCommonAll(StudentCriteria criteria)
        {
            var query = from it in dataContext.Student where !it.IsDelete select it;
            if (!String.IsNullOrEmpty(criteria.MarjorCode))
            {
                query = from it in query where it.MarjorCode == criteria.MarjorCode select it;
            }

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query
                    where it.NameZh.Contains(criteria.Name.Trim()) || it.NameEn.Contains(criteria.Name.Trim())
                    select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new StudentCommonPresentation()
            {
                Class = it.Class,
                MarjorName = GlobalBaseDataCache.GetMarjorName(it.MarjorCode),
                NameZh = it.NameZh,
                Period = it.Period,
                SexLabel = GlobalBaseDataCache.GetSexLabel(it.Sex),
                StudentNum = it.StudentNum
            }).ToList();
            EntityCollection<StudentCommonPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        private StudentPresentation Translate2Presentation(LkDataContext.Student student, bool includeRelativeData)
        {
            var presentation = new StudentPresentation()
            {
                Address = student.Address,
                Birthday = student.Birthday,
                Class = student.Class,
                Description = student.Description,
                Email = student.Email,
                IDentityNum = student.IDentityNum,
                IsMarried = student.IsMarried,
                IsOnline = student.IsOnline,
                MarjorCode = student.MarjorCode,
                NameEn = student.NameEn,
                NameZh = student.NameZh,
                NativePlace = student.NativePlace,
                Period = student.Period,
                Photo = student.Photo,
                Politics = (PoliticsType)student.Politics,
                Sex = (SexType) student.Sex,
                StudentNum = student.StudentNum,
                Tall = student.Tall,
                Telephone = student.Telephone,
                ThumbPath = student.ThumbPath,
                WebSite = student.WebSite,
                Weight = student.Weight
            };
            if (includeRelativeData)
            {
                presentation.JobExpect = (student.StudentJobExpect != null
                    ? new StudentJobExpectPresentation()
                    {
                        JobAddress = student.StudentJobExpect.JobAddress,
                        JobContent = student.StudentJobExpect.JobContent,
                        JobRequired = student.StudentJobExpect.JobRequired,
                        JobSalary = student.StudentJobExpect.JobSalary,
                        OpenType = student.StudentJobExpect.OpenType,
                        ProvinceCode = student.StudentJobExpect.ProvinceCode,
                        CityCode = student.StudentJobExpect.CityCode,
                        DistrictCode = student.StudentJobExpect.DistrictCode,
                        StreetCode = student.StudentJobExpect.StreetCode,

                        ProvinceName = student.StudentJobExpect.ProvinceName,
                        DistrictName = student.StudentJobExpect.DistrictName,
                        CityName = student.StudentJobExpect.CityName,
                        StreetName = student.StudentJobExpect.StreetName
                    }
                    : null);
            }
            return presentation;
        }

        private List<StudentFrontPresentation> GetTopClickStudentFront(int pageSize)
        {
            var query = from it in dataContext.Student
                where !it.IsDelete
                select new
                {
                    it.AboutMe,
                    it.Activity,
                    it.Address,
                    it.Birthday,
                    it.Book,
                    it.Class,
                    it.Description,
                    it.Email,
                    it.ID,
                    it.IDentityNum,
                    it.Interested,
                    it.IsDelete,
                    it.IsMarried,
                    it.IsOnline,
                    it.MarjorCode,
                    it.Movie,
                    it.Music,
                    it.NameEn,
                    it.NameZh,
                    it.NativePlace,
                    it.Period,
                    it.Photo,
                    it.Politics,
                    it.Program,
                    it.Sex,
                    it.StudentNum,
                    it.Tall,
                    it.Telephone,
                    it.ThumbPath,
                    it.WebSite,
                    it.Weight,
                    VisitedCount = (it.StudentVisited.Where(ix=>(ix.UserName!=it.StudentNum && ix.UserType==(int)UserType.Student) || ix.UserType!=(int)UserType.Student).Count())
                };
            query = from it in query orderby it.VisitedCount descending select it;

            var list = query.Take(pageSize).Select(it => new StudentFrontPresentation()
            {
                AboutMe = it.AboutMe,
                Activity = it.Activity,
                Address = it.Address,
                Birthday = it.Birthday,
                Book = it.Book,
                Class = it.Class,
                Description = it.Description,
                Email = it.Email,
                Id = it.ID,
                IDentityNum = it.IDentityNum,
                Interested = it.Interested,
                IsMarried = it.IsMarried,
                IsOnline = it.IsOnline,
                MarjorCode = it.MarjorCode,
                Movie = it.Movie,
                Music = it.Music,
                NameEn = it.NameEn,
                NameZh = it.NameZh,
                NativePlace = it.NativePlace,
                Period = it.Period,
                Photo = it.Photo,
                Politics = (PoliticsType) it.Politics,
                Program = it.Program,
                Sex = (SexType) it.Sex,
                StudentNum = it.StudentNum,
                Tall = it.Tall,
                Telephone = it.Telephone,
                ThumbPath = it.ThumbPath,
                WebSite = it.WebSite,
                Weight = it.Weight,
                VisitedCount = it.VisitedCount
            }).ToList();

            return list;
        }

        private List<StudentFrontPresentation> GetTopEvaluateStudentFront(int pageSize)
        {
            var query = from it in dataContext.Student
                        where !it.IsDelete
                        select new
                        {
                            it.AboutMe,
                            it.Activity,
                            it.Address,
                            it.Birthday,
                            it.Book,
                            it.Class,
                            it.Description,
                            it.Email,
                            it.ID,
                            it.IDentityNum,
                            it.Interested,
                            it.IsDelete,
                            it.IsMarried,
                            it.IsOnline,
                            it.MarjorCode,
                            it.Movie,
                            it.Music,
                            it.NameEn,
                            it.NameZh,
                            it.NativePlace,
                            it.Period,
                            it.Photo,
                            it.Politics,
                            it.Program,
                            it.Sex,
                            it.StudentNum,
                            it.Tall,
                            it.Telephone,
                            it.ThumbPath,
                            it.WebSite,
                            it.Weight,
                            Total = (it.StudentProject.Sum(ic => ic.SkillLevel + ic.UsableLevel + ic.UsableLevel))
                        };
            query = from it in query orderby it.Total descending select it;

            var list = query.Take(pageSize).Select(it => new StudentFrontPresentation()
            {
                AboutMe = it.AboutMe,
                Activity = it.Activity,
                Address = it.Address,
                Birthday = it.Birthday,
                Book = it.Book,
                Class = it.Class,
                Description = it.Description,
                Email = it.Email,
                Id = it.ID,
                IDentityNum = it.IDentityNum,
                Interested = it.Interested,
                IsMarried = it.IsMarried,
                IsOnline = it.IsOnline,
                MarjorCode = it.MarjorCode,
                Movie = it.Movie,
                Music = it.Music,
                NameEn = it.NameEn,
                NameZh = it.NameZh,
                NativePlace = it.NativePlace,
                Period = it.Period,
                Photo = it.Photo,
                Politics = (PoliticsType)it.Politics,
                Program = it.Program,
                Sex = (SexType)it.Sex,
                StudentNum = it.StudentNum,
                Tall = it.Tall,
                Telephone = it.Telephone,
                ThumbPath = it.ThumbPath,
                WebSite = it.WebSite,
                Weight = it.Weight,
            }).ToList();

            return list;
        }

        public StudentFrontPresentation GetFront(string studentNum)
        {
            return
                dataContext.Student.Where(it => it.StudentNum == studentNum)
                    .Select(it => Translate2FrontPresentation(it, true))
                    .FirstOrDefault();
        }

        public EntityCollection<StudentFrontPresentation> GetSearchFrontStudentList(StudentFontAdvanceCriteria criteria)
        {
            var query = from it in dataContext.Student where !it.IsDelete select it;
            if (!String.IsNullOrEmpty(criteria.KeyWord))
            {
                query = from it in query
                        where
                            it.StudentNum.Contains(criteria.KeyWord.Trim()) || it.NameEn.Contains(criteria.KeyWord.Trim()) ||
                            it.NameZh.Contains(criteria.KeyWord.Trim()) || it.StudentJobExpect.JobContent.Contains(criteria.KeyWord.Trim()) ||
                            it.StudentJobExpect.JobRequired.Contains(criteria.KeyWord.Trim())
                        select it;
            }

            if (!String.IsNullOrEmpty(criteria.MarjorCode))
            {
                query = from it in query where it.MarjorCode == criteria.MarjorCode select it;
            }
            if (!String.IsNullOrEmpty(criteria.Marjor))
            {
                query = from it in query where it.Marjor.Code.Contains(criteria.Marjor) || it.Marjor.Name.Contains(criteria.Marjor) select it;
            }
            //if (!String.IsNullOrEmpty(criteria.Depart))
            //{
            //    query = from it in query where it.Depart.Code.Contains(criteria.Depart) || it.Depart.Name.Contains(criteria.Depart) select it;
            //}
            if (criteria.SexType.HasValue)
            {
                query = from it in query where it.Sex == (int)criteria.SexType select it;
            }

            var totalCount = query.Count();

            var list =
                query.OrderByDescending(it => it.StudentVisited.Count()).Skip((criteria.PageIndex - 1) * criteria.PageSize)
                    .Take(criteria.PageSize)
                    .Select(it => Translate2FrontPresentation(it, false))
                    .ToList();

            EntityCollection<StudentFrontPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        private StudentFrontPresentation Translate2FrontPresentation(LkDataContext.Student student, bool includeRelativeData)
        {
            var presentation = new StudentFrontPresentation()
            {
                Address = student.Address,
                Birthday = student.Birthday,
                Class = student.Class,
                Description = student.Description,
                Email = student.Email,
                IDentityNum = student.IDentityNum,
                IsMarried = student.IsMarried,
                IsOnline = student.IsOnline,
                MarjorCode = student.MarjorCode,
                NameEn = student.NameEn,
                NameZh = student.NameZh,
                NativePlace = student.NativePlace,
                Period = student.Period,
                Photo = student.Photo,
                Politics = (PoliticsType) student.Politics,
                Sex = (SexType) student.Sex,
                StudentNum = student.StudentNum,
                Tall = student.Tall,
                Telephone = student.Telephone,
                ThumbPath = student.ThumbPath,
                WebSite = student.WebSite,
                Weight = student.Weight,
                AboutMe = student.AboutMe,
                Interested = student.Interested,
                Activity = student.Activity,
                Movie = student.Movie,
                Music = student.Music,
                Program = student.Program,
                VisitedCount = student.StudentVisited.Count(),
                CollegeName=student.College.Name
            };
            if (includeRelativeData)
            {
                presentation.JobExpect = (student.StudentJobExpect != null
                    ? new StudentJobExpectPresentation()
                    {
                        //JobAddress = student.StudentJobExpect.JobAddress,
                        JobContent = student.StudentJobExpect.JobContent,
                        JobRequired = student.StudentJobExpect.JobRequired,
                        JobSalary = student.StudentJobExpect.JobSalary,
                        OpenType = student.StudentJobExpect.OpenType
                    }
                    : null);
            }
            return presentation;
        }

        public EntityCollection<StudentCommonPresentation> GetReferralAll(string jobCode, int pageSize, int pageIndex)
        {
            var query = from it in dataContext.Student
                where !it.IsDelete &&
                      it.EnterpriseJobRequestQueue.Any(ix => ix.JobCode == jobCode)
                select it;

            var list = query.Skip(pageSize*pageIndex).Take(pageSize).Select(it => new StudentCommonPresentation()
            {
                Class = it.Class,
                MarjorName = GlobalBaseDataCache.GetMarjorName(it.MarjorCode),
                NameZh = it.NameZh,
                Period = it.Period,
                SexLabel = GlobalBaseDataCache.GetSexLabel(it.Sex),
                StudentNum = it.StudentNum,
                CreayeTime =
                    it.EnterpriseJobRequestQueue.Where(ix => ix.JobCode == jobCode)
                        .OrderByDescending(ix => ix.ID)
                        .FirstOrDefault()
                        .CreateTime
            }).ToList();

            EntityCollection<StudentCommonPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = query.Count();
            return entityCollection;
        }
    }
}
