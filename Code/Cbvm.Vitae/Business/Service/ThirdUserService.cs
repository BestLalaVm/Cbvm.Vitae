using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Presentation;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary;

namespace Business.Service
{
    public class ThirdUserService : BaseService, IAuthenticateService
    {
        public LoginUserPresentation Login(string userName, string password)
        {
            var thirdUser =
                dataContext.ThirdUser.FirstOrDefault(
                    it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(password));
            if (thirdUser != null)
            {
                return new LoginUserPresentation()
                {
                    Identity = thirdUser.UserName,
                    UserName = thirdUser.UserName,
                    UserType = UserType.Guest,
                    LogTime = DateTime.Now,
                    Id = thirdUser.ID,
                    UserLabel = EnumHelper.GetEnumDescription(UserType.Guest)
                };
            }
            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public ActionResult ResetPassword(string userName, Presentation.Enum.UserType userType, string checkCode)
        {
            throw new NotImplementedException();
        }

        public static LoginUserPresentation LoginViaGuest()
        {
            string userName = "Guest";
            string password = "1234";
            IAuthenticateService instance = new ThirdUserService();
            return instance.Login(userName, password);
        }
    }
}
