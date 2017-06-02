using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace WebLibrary.Helper
{
    public static class AuthorizeHelper
    {
        public static void SetCurrentUser(LoginUserPresentation user, bool customerRedirect = false)
        {
            user.LogTime = DateTime.Now;
            var accountInfoJson = AccountSecurityManage.SerializeAccountInfo(user);

            if (HttpContext.Current.Request.Url.LocalPath.ToLower().Contains("login"))
            {
                FormsAuthentication.RedirectFromLoginPage(accountInfoJson, false);
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, accountInfoJson, DateTime.Now,
                                                                                 DateTime.Now.AddMinutes(30),
                                                                                 false, String.Empty,
                                                                                 FormsAuthentication.FormsCookiePath);
                string encryptedCookie = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(cookie);

                if (!customerRedirect)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
                }
            }
        }

        internal static LoginUserPresentation GetCurrentUser(string accountInfoJson)
        {
            return AccountSecurityManage.DeserializeAccountInfo(accountInfoJson);
        }

        public static void LogOut()
        {
            //try
            //{
            //    using (var client = new System.Net.WebClient())
            //    {
            //        client.DownloadString(LkDataContext.AppConfig.JavaLogoutLink);
            //    }
            //}
            //catch(Exception ex)
            //{

            //}
            SetLogout();

            FormsAuthentication.SignOut();
        }

        public static string AuthorizateStudentInfo(StudentPresentation student, CultureInfo culture)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return student.NameZh;
            }
            if (string.IsNullOrEmpty(student.NameZh))
            {
                return "";
            }
            StringBuilder name = new StringBuilder();
            for (var index = 0; index < student.NameZh.Length; index++)
            {
                if (index == 0)
                {
                    name.Append(student.NameZh[index].ToString());
                }
                else
                {
                    name.Append("*");
                }
            }
            return name.ToString();
        }

        private static string THIRD_SESSION_LOGOUT_KEY = "THIRD_SESSION_LOGOUT_KEY";
        private static void SetLogout()
        {
            //using (var db = new LkDataContext.CVAcademicianDataContext())
            //{
            //    db.SystemLog.InsertOnSubmit(new LkDataContext.SystemLog()
            //    {
            //        Name = "SetLogout" + DateTime.Now.Ticks.ToString(),
            //        Message = HttpContext.Current.Request.Url.ToString(),
            //        IPAddress = HttpContext.Current.Request.UserHostAddress,
            //        LogTime = DateTime.Now,
            //        LogType = 1
            //    });
            //    db.SubmitChanges();
            //}


            HttpContext.Current.Session.Add(THIRD_SESSION_LOGOUT_KEY, "1");
        }

        public static bool IsNotifyThirdLogout()
        {
            var flag = HttpContext.Current.Session[THIRD_SESSION_LOGOUT_KEY] == "1";
            HttpContext.Current.Session.Remove(THIRD_SESSION_LOGOUT_KEY);

            return flag;

        }
    }
}
