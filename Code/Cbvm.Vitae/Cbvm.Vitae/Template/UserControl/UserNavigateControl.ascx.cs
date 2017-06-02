using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLibrary.Helper;
using Cbvm.Vitae.App_Code;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class UserNavigateControl : BaseUserControl
    {
        public void LoadData(PageContentType pageType, string code)
        {
            string url = "";
            string caption = "";
            string content = "";
            switch (pageType)
            {
                case PageContentType.Enterprise:
                    caption = "企业列表";
                    url = "/Enterprise";
                    if (!String.IsNullOrEmpty(code))
                    {
                        content = String.Format(" > <a href='{0}'><span>企业明细</span></a>",
                                                UrlRuleHelper.GenerateUrl(code, "", RulePathType.Enterprise));
                    }
                    break;
                case PageContentType.DepartMessage:
                    caption = "新闻列表";
                    url = "/Message";
                    if (!String.IsNullOrEmpty(code))
                    {
                        content = String.Format(" > <a href='{0}'><span>新闻正文</span></a>",
                                                UrlRuleHelper.GenerateUrl(code, "", RulePathType.DepartMessage));
                    }
                    break;
                case PageContentType.Student:
                    caption = "学生列表";
                    url = "/Student";
                    break;
            }

            ltlNavigate.Text =
                String.Format("<a href='/template/default.aspx'><span>家校企首页</span></a> > <a href='{0}'><span>{1}</span></a>{2}", url, caption,content);
        }
    }
}