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

namespace Business.Service.DepartAdmin
{
    public class DepartAdminService : BaseService, IDepartAdminService, IAuthenticateService
    {

        #region IAuthenticateService

        public LoginUserPresentation Login(string userName, string password)
        {
            var departAdmin =
                dataContext.DepartAdmin.FirstOrDefault(
                    it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(password));

            if (departAdmin != null)
            {
                return new LoginUserPresentation()
                {
                    Id = departAdmin.ID,
                    Identity = departAdmin.DepartCode,
                    LogTime = DateTime.Now,
                    UserName = departAdmin.UserName,
                    UserLabel = departAdmin.UserName,
                    UserType = UserType.DepartAdmin
                };
            }

            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                var departAdmin =
                    dataContext.DepartAdmin.FirstOrDefault(
                        it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(oldPassword));
                if (departAdmin == null)
                {
                    return ActionResult.NotFoundResult;
                }
                departAdmin.Password = AccountSecurityManage.MD5Password(newPassword);
                dataContext.SubmitChanges();
                return ActionResult.DefaultResult;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return new ActionResult
                {
                    IsSucess = false,
                    Message = ex.ToString()
                    //Message = string.Format(MessageResource.FailtureForChangePasswordLog, ex.ToString())
                };
            }
        }

        public ActionResult ResetPassword(string userName, UserType userType, string checkCode)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
