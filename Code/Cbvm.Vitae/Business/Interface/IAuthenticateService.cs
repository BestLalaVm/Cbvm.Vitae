using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;
using Presentation.UIView;

namespace Business.Interface
{
    public interface IAuthenticateService
    {
        LoginUserPresentation Login(string userName, string password);

        ActionResult ChangePassword(string userName, string oldPassword, string newPassword);

        ActionResult ResetPassword(string userName, UserType userType, string checkCode);


    }
}
