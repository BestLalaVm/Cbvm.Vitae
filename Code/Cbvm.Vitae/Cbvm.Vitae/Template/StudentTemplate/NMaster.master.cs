using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LkHelper;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Template.StudentTemplate
{
    public partial class NMaster : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //ltlNaviagte
            var studentPage = base.Page as BaseFrontStudentPage;


            var studentUrl = UrlRuleHelper.GenerateUrl(studentPage.StudentNum,
                                                       studentPage.StudentNum,
                                                       RulePathType.StudentInfo);

            if (string.IsNullOrEmpty(studentPage.KeyWord))
            {
                ltlNaviagte.Text = String.Format("<a href='{0}'><span>个人主页</span></a> > {1}", studentUrl,
                                                 studentPage.PageName);
            }
            else
            {
                ltlNaviagte.Text = String.Format("<a href='{0}'><span>个人主页</span></a> > {1} > {2}", studentUrl,
                                                 studentPage.PageName, studentPage.KeyWord.Cut(20, "..."));
            }
        }
    }
}