using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Business.Interface.DepartAdmin;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary;
using Business.Interface.University;

namespace Business.Service.University
{
    public class UniversityService : BaseService, IAuthenticateService, IUniversityService
    {
        public LoginUserPresentation Login(string userName, string password)
        {
            var universityAdmin = dataContext.UniversityAdmin
                .Where(it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(password)).Select(it => new
                {
                    it.ID,
                    it.UniversityCode,
                    it.UserName,
                    UniversityName = it.University.Name
                }).FirstOrDefault();

            if (universityAdmin != null)
            {
                return new LoginUserPresentation()
                {
                    Id = universityAdmin.ID,
                    Identity = universityAdmin.UniversityCode,
                    LogTime = DateTime.Now,
                    UserName = universityAdmin.UserName,
                    UserLabel = universityAdmin.UserName,
                    UserType = UserType.University,
                    FullName=universityAdmin.UniversityName
                };
            }

            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                var universityAdmin =
                    dataContext.UniversityAdmin.FirstOrDefault(
                        it => it.Password == AccountSecurityManage.MD5Password(oldPassword) &&
                              it.UserName == userName);
                if (universityAdmin == null)
                {
                    return ActionResult.NotFoundResult;
                }
                universityAdmin.Password = AccountSecurityManage.MD5Password(newPassword);
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
            throw new NotImplementedException();
        }
    }
}
