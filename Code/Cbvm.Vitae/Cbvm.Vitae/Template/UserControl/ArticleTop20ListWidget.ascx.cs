using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.University;
using Business.Service.University;
using LkHelper;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class ArticleTop20ListWidget : BaseFrontUserControl
    {
        private IUniversityMessageService Service
        {
            get
            {
                return new UniversityMessageService();
            }
        }

        protected override void InitData()
        {
            rptMessage.DataSource = Service.GetTop20FrontMessageList().Select(it => new
            {
                Title = string.Format("<a href='{0}' title={1}>{2}</a>", UrlRuleHelper.GenerateUrl(it.Id.ToString(), it.Title.Cut(40, ""), RulePathType.DepartMessage), it.Title, it.Title.Cut(30, " ")),
                LastUpdateTime = it.LastUpdateTime.ToCustomerShortDateString()
            });
            rptMessage.DataBind();
            base.InitData();
        }
    }
}