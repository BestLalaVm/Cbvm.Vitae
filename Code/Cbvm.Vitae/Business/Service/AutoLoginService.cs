using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Interface;
using LkDataContext;
using Presentation.Enum;
using Presentation.UIView;

namespace Business.Service
{
    public class AutoLoginService : BaseService, IAutoLoginService
    {
        private IQueryable<LoginUserTmpModel> CreateQuery(string token, string ipaddress, string userName, UserType userType, bool isCheckEnabled = true)
        {
            IQueryable<LoginUserTmpModel> query = null;
            switch (userType)
            {
                case UserType.College:
                    query = from it in dataContext.CollegeAdmin
                            join ix in dataContext.AccessToken on it.UserName equals ix.UserName
                            where ix.UserType == (int)userType
                            select new LoginUserTmpModel
                            {
                                UserName = ix.UserName,
                                Token = ix.Token,
                                Name = it.College.Name,
                                UserType = (UserType)ix.UserType,
                                IPAddress = ix.IPAddress,
                                CreatedTime = ix.CreateTime,
                                IsEnabled = ix.IsEnabled,
                                LogoutTime = ix.LogoutTime
                            };
                    break;
                case UserType.Enterprise:
                    query = from it in dataContext.Enterprise
                            join ix in dataContext.AccessToken on it.UserName equals ix.UserName
                            where ix.UserType == (int)userType
                            select new LoginUserTmpModel
                            {
                                UserName = ix.UserName,
                                Token = ix.Token,
                                Name = it.Name,
                                UserType = (UserType)ix.UserType,
                                IPAddress = ix.IPAddress,
                                CreatedTime = ix.CreateTime,
                                IsEnabled = ix.IsEnabled,
                                LogoutTime = ix.LogoutTime
                            };
                    break;
                case UserType.Family:
                    query = from it in dataContext.StudentFamilyAccount
                            join ix in dataContext.AccessToken on it.UserName equals ix.UserName
                            where ix.UserType == (int)userType
                            select new LoginUserTmpModel
                            {
                                UserName = ix.UserName,
                                Token = ix.Token,
                                Name = it.NameZh + " " + it.NameEn,
                                UserType = (UserType)ix.UserType,
                                IPAddress = ix.IPAddress,
                                CreatedTime = ix.CreateTime,
                                IsEnabled = ix.IsEnabled,
                                LogoutTime = ix.LogoutTime
                            };
                    break;
                case UserType.Student:
                    query = from it in dataContext.Student
                            join ix in dataContext.AccessToken on it.StudentNum equals ix.UserName
                            where ix.UserType == (int)userType
                            select new LoginUserTmpModel
                            {
                                UserName = ix.UserName,
                                Token = ix.Token,
                                Name = it.NameZh + " " + it.NameEn,
                                UserType = (UserType)ix.UserType,
                                IPAddress = ix.IPAddress,
                                CreatedTime = ix.CreateTime,
                                IsEnabled = ix.IsEnabled,
                                LogoutTime = ix.LogoutTime
                            };
                    break;
                case UserType.Teacher:
                    query = from it in dataContext.Teacher
                            join ix in dataContext.AccessToken on it.TeacherNum equals ix.UserName
                            where ix.UserType == (int)userType
                            select new LoginUserTmpModel
                            {
                                UserName = ix.UserName,
                                Token = ix.Token,
                                Name = it.NameZh + " " + it.NameEn,
                                UserType = (UserType)ix.UserType,
                                IPAddress = ix.IPAddress,
                                CreatedTime = ix.CreateTime,
                                IsEnabled = ix.IsEnabled,
                                LogoutTime = ix.LogoutTime
                            };
                    break;
                case UserType.University:
                    query = from it in dataContext.UniversityAdmin
                            join ix in dataContext.AccessToken on it.UserName equals ix.UserName
                            where ix.UserType == (int)userType
                            select new LoginUserTmpModel
                            {
                                UserName = ix.UserName,
                                Token = ix.Token,
                                Name = it.University.Name,
                                UserType = (UserType)ix.UserType,
                                IPAddress = ix.IPAddress,
                                CreatedTime = ix.CreateTime,
                                IsEnabled = ix.IsEnabled,
                                LogoutTime = ix.LogoutTime
                            };
                    break;
            }

            if (query == null)
            {
                return null;
            }

            if (isCheckEnabled)
            {
                query = query.Where(it => it.IsEnabled);
            }

            //query = query.Where(it => it.UserType == (UserType)userType && it.UserName == userName && it.Token == token && it.IPAddress == ipaddress);
            query = query.Where(it => it.Token == token);

            return query;
        }

        string IAutoLoginService.CreateToken(string userName, Presentation.Enum.UserType userType, string ipaddress)
        {
            var token = Guid.NewGuid().ToString();

            var currentToken = dataContext.AccessToken.FirstOrDefault(it => it.IPAddress == ipaddress && it.UserName == userName && it.UserType == (int)userType && it.IsEnabled && !it.LogoutTime.HasValue);
            if (currentToken == null)
            {
                dataContext.AccessToken.InsertOnSubmit(new AccessToken()
                {
                    IPAddress = ipaddress,
                    IsEnabled = true,
                    Token = token,
                    UserName = userName,
                    UserType = (int)userType,
                    CreateTime = DateTime.Now
                });

                dataContext.SubmitChanges();
            }
            else
            {
                token = currentToken.Token;
            }

            return token;
        }

        AutoLoginPresentation IAutoLoginService.Login(string token, string ipaddress, string userName, UserType userType)
        {
            var query = CreateQuery(token, ipaddress, userName, userType);

            var expiredDate = DateTime.Now.AddMinutes(-10);
            var data = query.Where(it => it.CreatedTime >= expiredDate).Select(it => new AutoLoginPresentation()
            {
                Name = it.Name,
                Token = it.Token,
                UserName = it.UserName,
                UserType = it.UserType
            }).FirstOrDefault();

            if (data != null)
            {
                #region details
                LoginUserPresentation detail = null;
                switch (data.UserType)
                {
                    case UserType.College:
                        detail = dataContext.College.Where(it => it.CollegeAdmin.Any(ix => ix.UserName == data.UserName)).SelectMany(it => it.CollegeAdmin).Select(collegeAdmin => new LoginUserPresentation()
                        {
                            Id = collegeAdmin.ID,
                            Identity = collegeAdmin.CollegeCode,
                            LogTime = DateTime.Now,
                            UserName = collegeAdmin.UserName,
                            UserLabel = collegeAdmin.UserName,
                            UserType = UserType.College,
                            FullName = collegeAdmin.College.University.Name + "-" + collegeAdmin.College.Name
                        }).FirstOrDefault();
                        break;
                    case UserType.University:
                        detail = dataContext.University.Where(it => it.UniversityAdmin.Any(ix => ix.UserName == data.UserName)).SelectMany(it => it.UniversityAdmin).Select(universityAdmin => new LoginUserPresentation()
                        {
                            Id = universityAdmin.ID,
                            Identity = universityAdmin.UniversityCode,
                            LogTime = DateTime.Now,
                            UserName = universityAdmin.UserName,
                            UserLabel = universityAdmin.UserName,
                            UserType = UserType.University,
                            FullName = universityAdmin.University.Name
                        }).FirstOrDefault();
                        break;
                    case UserType.Student:
                        detail = dataContext.Student.Where(it => it.StudentNum == data.UserName).Select(student => new LoginUserPresentation()
                        {
                            Id = student.ID,
                            Identity = student.CollegeCode,
                            UserName = student.StudentNum,
                            UserType = UserType.Student,
                            UserLabel = student.NameZh,
                            LogTime = DateTime.Now,
                            FullName = student.NameZh + "同学",
                            Image = student.Photo
                        }).FirstOrDefault();
                        break;
                    case UserType.Teacher:
                        detail = dataContext.Teacher.Where(it => it.TeacherNum == data.UserName).Select(teacher => new LoginUserPresentation()
                        {
                            Id = teacher.ID,
                            Identity = teacher.CollegeCode,
                            UserType = UserType.Teacher,
                            UserLabel = teacher.NameZh,
                            UserName = teacher.TeacherNum,
                            LogTime = DateTime.Now,
                            FullName = teacher.NameZh + "老师"
                        }).FirstOrDefault();
                        break;
                    case UserType.Family:
                        detail = dataContext.StudentFamilyAccount.Where(it => it.UserName == data.UserName).Select(family => new LoginUserPresentation
                        {
                            Id = family.ID,
                            UserName = family.UserName,
                            UserType = UserType.Family,
                            UserLabel = family.NameZh,
                            LogTime = DateTime.Now,
                            Identity = family.StudentNum,
                            FullName = String.Format("{0}家长", family.NameZh)
                        }).FirstOrDefault();

                        detail.AddtionalUser.Add("StudentNum", detail.Identity);
                        break;
                    case UserType.Enterprise:
                        var enterprise = dataContext.Enterprise.Where(it => it.UserName == data.UserName).FirstOrDefault();
                        detail = new LoginUserPresentation()
                        {
                            Id = enterprise.ID,
                            Identity = enterprise.Code,
                            LogTime = DateTime.Now,
                            UserName = enterprise.UserName,
                            UserType = UserType.Enterprise,
                            UserLabel = enterprise.Name,
                            AddtionalUser = new Dictionary<string, string>(){
                                    {"isPermission", ((VerifyStatus)enterprise.VerifyStatus==VerifyStatus.Passed).ToString()}
                            },
                            FullName = enterprise.Name
                        };
                        detail.AddtionalUser = new Dictionary<string, string>()
                        {
                             {"isPermission", ((VerifyStatus)enterprise.VerifyStatus==VerifyStatus.Passed).ToString()}
                        };
                        break;
                }
                #endregion

                data.Detail = detail;

                //var logoutRequests = dataContext.LogoutRequest.Where(it => it.UserName == userName && it.UserType == (int)userType && it.IpAddress == ipaddress && it.IsEnabled).ToList();
                var logoutRequests = dataContext.LogoutRequest.Where(it => it.UserName == data.UserName && it.UserType == (int)data.UserType && it.IsEnabled).ToList();
                if (logoutRequests.Any())
                {
                    logoutRequests.ForEach(item =>
                    {
                        item.IsEnabled = false;
                        item.ExecuteTime = DateTime.Now;
                    });
                }

                //dataContext.AccessToken.Where(it => it.Token == token && it.UserName == userName && it.UserType == (int)userType && it.IPAddress == ipaddress && it.IsEnabled).ToList().ForEach(item =>
                dataContext.AccessToken.Where(it => it.Token == token && it.UserName == data.UserName && it.UserType == (int)data.UserType && it.IsEnabled).ToList().ForEach(item =>
                {
                    item.IsEnabled = false;
                });

                dataContext.SubmitChanges();
            }

            return data;
        }

        bool IAutoLoginService.Logout(string token, string ipaddress, string userName, UserType userType)
        {
            var query = CreateQuery(token, ipaddress, userName, userType, false);
            if (!query.Any())
            {
                return false;
            }

            var accessToken = dataContext.AccessToken.Where(it => it.UserName == userName && it.IPAddress == ipaddress && it.UserType == (int)userType && !it.LogoutTime.HasValue).OrderByDescending(ix => ix.CreateTime).FirstOrDefault();
            if (accessToken == null)
            {
                return false;
            }

            accessToken.IsEnabled = false;
            accessToken.LogoutTime = DateTime.Now;

            dataContext.LogoutRequest.InsertOnSubmit(new LogoutRequest()
            {
                CreeateTime = DateTime.Now,
                UserName = userName,
                UserType = (int)userType,
                Token = token,
                IpAddress = ipaddress,
                IsEnabled = true
            });

            dataContext.SubmitChanges();

            return true;
        }

        private class LoginUserTmpModel
        {
            public string UserName { get; set; }

            public string Name { get; set; }

            public string Token { get; set; }

            public UserType UserType { get; set; }

            public string IPAddress { get; set; }

            public DateTime CreatedTime { get; set; }

            public bool IsEnabled { get; set; }

            public DateTime? LogoutTime { get; set; }
        }


        public bool IsForce2Logout(string ipaddress, string userName, UserType userType)
        {

            var logoutRequests = dataContext.LogoutRequest.Where(it => it.IsEnabled && it.IpAddress == ipaddress && it.UserName == userName && it.UserType == (int)userType && !it.ExecuteTime.HasValue).ToList();
            if (logoutRequests.Any())
            {
                try
                {
                    logoutRequests.ForEach(item =>
                    {
                        item.IsEnabled = false;
                        item.ExecuteTime = DateTime.Now;
                    });

                    dataContext.SubmitChanges();
                }
                finally
                {

                }
                var date = DateTime.Now.AddHours(-2);
                return logoutRequests.Any(it => it.CreeateTime > date);
            }

            return false;
        }


        public void AddLoginHistory(string userName, UserType userType, string ipaddress, string originalIpAddress)
        {
            try
            {
                dataContext.LoginHistory.InsertOnSubmit(new LoginHistory()
                {
                    IPAddress = ipaddress,
                    OriginalIPAddress = originalIpAddress,
                    UserName = userName,
                    UserType = (int)userType,
                    VisitedTime = DateTime.Now
                });

                dataContext.SubmitChanges();
            }
            catch { }
        }
    }
}
