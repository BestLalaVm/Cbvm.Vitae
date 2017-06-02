using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebLibrary.Helper
{
    public static class HtmlContentHelper
    {
        public static string GetEmptyContent()
        {
            return "没任何数据!";
        }

        public static string HtmlEncode(this string content)
        {
            if (String.IsNullOrEmpty(content)) return "";
            //return HttpUtility.HtmlEncode(content);

            return content;
        }
    }
}
