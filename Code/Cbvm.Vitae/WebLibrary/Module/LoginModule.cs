using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace WebLibrary.Module
{
    public class LoginModule : IHttpModule
    {
        private class UrlMappingItem
        {
            public string RegexKey
            {
                get;
                set;
            }
            public string RegexValue
            {
                get;
                set;
            }
        }

        private List<UrlMappingItem> urlMappingList = new List<UrlMappingItem>();
        private List<UrlMappingItem> paramMappingList = new List<UrlMappingItem>();

        public LoginModule()
        {
            paramMappingList.Add(new UrlMappingItem
            {
                RegexKey = "^/Manage/Student/(\\S)*",
                RegexValue = "/Manage/Student/Login.aspx"
            });
            paramMappingList.Add(new UrlMappingItem
            {
                RegexKey = "^/Manage/Teacher/(\\S)*",
                RegexValue = "/Manage/Teacher/Login.aspx"
            });
            paramMappingList.Add(new UrlMappingItem
            {
                RegexKey = "^/Manage/Enterprise/(\\S)*",
                RegexValue = "/Manage/Enterprise/Login.aspx"
            });
            paramMappingList.Add(new UrlMappingItem
            {
                RegexKey = "^/Manage/DepartAdmin/(\\S)*",
                RegexValue = "/Manage/DepartAdmin/Login.aspx"
            });

            urlMappingList.Add(new UrlMappingItem
            {
                RegexKey = "(\\S)*/",
                RegexValue = ""
            });
        }

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
            context.AuthorizeRequest += new EventHandler(context_AuthorizeRequest);
        }

        void context_AuthorizeRequest(object sender, EventArgs e)
        {

        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var context = (sender as HttpApplication).Context;
            var returnUrl = HttpUtility.UrlDecode(context.Request.QueryString["ReturnUrl"]);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                if (!Igorne(context))
                {
                    urlMappingList.ForEach(it =>
                    {
                        var regex = new Regex(it.RegexKey, RegexOptions.IgnoreCase);
                        if (regex.IsMatch(returnUrl))
                        {
                            var newPath = string.Format("{0}?ReturnUrl={1}", it.RegexValue, HttpUtility.UrlEncode(returnUrl));
                            context.RewritePath(newPath);
                        }
                    });
                }
            }
        }

        private bool Igorne(HttpContext context)
        {
            var regex = new Regex("^/login.aspx$", RegexOptions.IgnoreCase);
            return !regex.IsMatch(context.Request.Path);
        }
    }
}
