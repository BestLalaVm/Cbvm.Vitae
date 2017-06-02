using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;
using Presentation.UIView;

namespace Business.Interface
{
    public interface IAutoLoginService
    {
        string CreateToken(string userName, UserType userType, string ipaddress);

        AutoLoginPresentation Login(string token, string ipaddress, string userName, UserType userType);

        bool Logout(string token, string ipaddress, string userName, UserType userType);

        bool IsForce2Logout(string ipaddress, string userName, UserType userType);


        void AddLoginHistory(string userName, UserType userType, string ipaddress, string originalIpAddress);
    }
}
