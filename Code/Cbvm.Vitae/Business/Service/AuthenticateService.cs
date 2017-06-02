using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Presentation.Enum;
using Presentation.UIView;

namespace Business.Service
{
    public class AuthenticateService : BaseService, IAuthenticateService
    {
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public ActionResult ResetPassword(string userName, UserType userType, string checkCode)
        {
            throw new NotImplementedException();
        }

        public LoginUserPresentation Login(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
