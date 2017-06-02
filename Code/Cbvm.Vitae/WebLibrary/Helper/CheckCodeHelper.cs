using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebLibrary.Helper
{
    public static class CheckCodeHelper
    {
        private const string CHECK_CODE_KEY = "CHECK_CODE_KEY";

        public static void SetCheckCode(string userType, string checkCode)
        {
            HttpCookie cookie = new HttpCookie(GenerateCookieName(userType));
            cookie.Value = checkCode;
            cookie.Expires = DateTime.Now.AddHours(6);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static bool ValidateCheckCode(string userType, string code)
        {
            var cookie = HttpContext.Current.Request.Cookies[GenerateCookieName(userType)];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value) || string.IsNullOrEmpty(code))
            {
                return false;
            }
            if (cookie.Value.ToLower().Trim() == code.ToLower().Trim())
            {
                return true;
            }
            return false;
        }

        public static void ClearCheckCode(string userType)
        {
            var cookie = HttpContext.Current.Request.Cookies[GenerateCookieName(userType)];
            cookie.Expires = DateTime.Now.AddDays(-30);
            HttpContext.Current.Request.Cookies.Add(cookie);
        }

        private static string GenerateCookieName(string userType)
        {
            return CHECK_CODE_KEY + "_" + userType.ToString();
        }
    }
}
