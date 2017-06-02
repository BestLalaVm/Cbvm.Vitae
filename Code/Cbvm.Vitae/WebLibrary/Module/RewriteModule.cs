using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using LkDataContext;

namespace WebLibrary.Module
{
    public class RewriteModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var context = (sender as HttpApplication).Context;
            var url = HttpUtility.UrlDecode(context.Request.FilePath);
            if (url.ToLower().Contains("login.aspx"))
            {
                url = HttpUtility.UrlDecode(context.Request.RawUrl);
            }
            if (!IgnoreReWrite(url))
            {
                foreach (var rule in RewriteRuleCache.RewriteRuleList)
                {
                    var regex = new Regex(rule.LookFor, RegexOptions.IgnoreCase);
                    if (regex.IsMatch(url))
                    {
                        string targetUrl = regex.Replace(url, rule.SendTo);

                        if (context.Request.Url.PathAndQuery.Contains("?"))
                        {
                            var queryDic = new Dictionary<string, string>();

                            if (targetUrl.Contains("?"))
                            {
                                var queryString = targetUrl.Substring(targetUrl.IndexOf("?") + 1).Split('&');
                                foreach (var item in queryString)
                                {
                                    if (item.Contains("="))
                                    {
                                        if (!queryDic.ContainsKey(item.Split('=')[0]))
                                        {
                                            queryDic.Add(item.Split('=')[0], item.Split('=')[1]);
                                        }
                                    }
                                }

                                targetUrl = targetUrl.Substring(0, targetUrl.IndexOf('?'));
                            }
                            foreach (var key in context.Request.QueryString.AllKeys)
                            {
                                if (!queryDic.ContainsKey(key))
                                {
                                    queryDic.Add(key, context.Request.QueryString[key]);
                                }
                            }
                            StringBuilder urlPath = new StringBuilder();
                            foreach (var key in queryDic.Keys)
                            {
                                if (urlPath.Length > 0)
                                {
                                    urlPath.Append("&");
                                }
                                urlPath.AppendFormat("{0}={1}", key, HttpUtility.UrlEncode(queryDic[key]));
                            }
                            targetUrl = String.Format("{0}?{1}", targetUrl, urlPath.ToString());
                        }
                        if (url.ToLower().Contains("login.aspx"))
                        {
                            regex = new Regex(@"^/(\w+)/login.aspx\?(ReturnUrl=/(\S+))", RegexOptions.IgnoreCase);
                            if (regex.IsMatch(targetUrl))
                            {
                                targetUrl = regex.Replace(targetUrl, "/template/login.aspx?$2");
                            }
                        }
                        context.RewritePath(targetUrl, false);
                        break;
                    }
                }
            }
        }

        private bool IgnoreReWrite(string p_strUrl)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(p_strUrl,
                @"(\.png|\.jpg|\.gif|\.swf|\.css|\.js|\.axd|\.asmx|\.wma|\.wmv|\.avi|\.wav|\.mpeg|\.mpg|\.mpe|\.mp3|\.m3u|\.mid|\.midi|\.snd|\.mkv|\.doc|\.txt|\.docx|\.xls|\.xlsx|\.pdf|\.ico|\.eot|\.svg|\.ttf|\.woff)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
                | System.Text.RegularExpressions.RegexOptions.Compiled);
        }
    }

    internal static class RewriteRuleCache
    {
        private static List<RewriteRule> _RewriteRuleList;

        internal static List<RewriteRule> RewriteRuleList
        {
            get
            {
                if (_RewriteRuleList == null || !_RewriteRuleList.Any())
                {
                    using (var dataContext = new CVAcademicianDataContext())
                    {
                        _RewriteRuleList =
                            dataContext.RewriteRule.Where(it => it.IsOnline).OrderBy(it => it.Priority).ToList();
                    }
                }
                return _RewriteRuleList;
            }
        }
    }
}
