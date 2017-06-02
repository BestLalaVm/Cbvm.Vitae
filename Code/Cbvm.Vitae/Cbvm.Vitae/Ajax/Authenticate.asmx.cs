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
using LkDataContext;
using Business.Service;
using Business.Interface;
using System.Web.Security;

using Business.Interface;
using Business.Service;
using Business.Service.Enterprise;
using Business.Service.Family;
using Business.Service.Student;
using Business.Service.Teacher;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary.Helper;
using Business.Service.College;
using Business.Service.University;

namespace Cbvm.Vitae.Ajax
{
    /// <summary>
    /// Authenticate 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [ScriptService]
    public class Authenticate : System.Web.Services.WebService
    {
        private IAutoLoginService _loginService;
        private IAutoLoginService loginService {
            get
            {
                if (_loginService == null)
                {
                    _loginService = new AutoLoginService();
                }

                return _loginService;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public JsonResultData Login(string token, string userName, UserType userType, string ipaddress)
        {
            var result = new JsonResultData();

            //var ipaddress = HttpContext.Current.Request.UserHostAddress;

            var newIpaddress = AppConfig.GetResolvedIpAddress(ipaddress);
            var data = loginService.Login(token, newIpaddress, userName, userType);
            

            if (data == null)
            {
                result.Success = false;
                result.Message = "无效数据!";
            }
            else
            {
                result.Model = new AuthenticationPresentation()
                {
                    Name = data.Name,
                    Token = data.Token,
                    UserName = data.UserName,
                    UserType = data.UserType.ToString()
                };
            }

            return result;
        }

        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResultData Logout(string token, string userName, UserType userType, string ipaddress)
        {
            var result = new JsonResultData();

            //var ipaddress = HttpContext.Current.Request.UserHostAddress;
            var newIpaddress = AppConfig.GetResolvedIpAddress(ipaddress);

            var data = loginService.Logout(token, newIpaddress, userName, userType);

            if (!data)
            {
                result.Success = false;
                result.Message = "无效数据!";
            }
            else
            {
                //WebLibrary.Helper.AuthorizeHelper.LogOut();
            }

            return result;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResultData LoginWithUserName(string userName,string password, UserType userType,string ipaddress)
        {
            var result = new JsonResultData();

            IAuthenticateService instance = null;
            switch (userType)
            {
                //case UserType.DepartAdmin:
                //    instance = new DepartAdminService();
                //    break;
                case UserType.Enterprise:
                    instance = new EnterpriseService();
                    break;
                case UserType.Teacher:
                    instance = new TeacherService();
                    break;
                case UserType.Family:
                    instance = new FamilyService();
                    break;
                case UserType.College:
                    instance = new CollegeService();
                    break;
                case UserType.University:
                    instance = new UniversityService();
                    break;
                default:
                    instance = new StudentService();
                    break;
            }
            var loginUser = instance.Login(userName, password);
            if (loginUser != null)
            {
                var newIpaddress = AppConfig.GetResolvedIpAddress(ipaddress);
                var token = loginService.CreateToken(loginUser.UserName, loginUser.UserType, newIpaddress);

                loginService.AddLoginHistory(userName, userType, newIpaddress, ipaddress);

                result.Model = new
                {
                    Authentication = new AuthenticationPresentation
                    {
                        Token = token,
                        UserName=loginUser.UserName,
                        UserType=loginUser.UserType.ToString(),
                        Name=loginUser.Name,
                    },
                    User = loginUser
                };
            }
            else
            {
                result.Success = false;
                result.Message = "无效数据!";
            }

            return result;
        }
    }

    public class AuthenticationPresentation
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string UserType { get; set; }

        public string Image { get; set; }
    }
}
