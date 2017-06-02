using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using WebLibrary.Helper;
using Cbvm.Vitae.App_Code;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class EnterpriseListWidget : BaseFrontUserControl
    {
        private IEnterpriseService Service
        {
            get
            {
                return new EnterpriseService();
            }
        }

        private int PageIndex
        {
            get
            {
                if (String.IsNullOrEmpty(Request.QueryString["pageIndex"]) || Request.QueryString["pageIndex"] == "0")
                    return 1;
                int index;
                int.TryParse(Request.QueryString["pageIndex"], out index);
                return index;
            }
        }

        protected override void InitPostData()
        {
            CustomPager.PageIndex = this.PageIndex;
            CustomPager.PageItemTemplate = "/Enterprise/{0}?keyword=" + Request.QueryString["keyword"];
        }

        protected override void InitData()
        {
            var list = Service.GetFrontEnterpriseList(Request.QueryString["keyword"], PageIndex, 1000);
            rptEnterprise.DataSource = list.Select(it => new
            {
                Url = UrlRuleHelper.GenerateUrl(it.Code, it.Name, RulePathType.Enterprise),
                it.Name,
                Tooltip = it.Name
            }).ToList();

            rptEnterprise.DataBind();
            if (!list.Any())
            {
                divMsg.Visible = true;
                CustomPager.Visible = false;
            }
            else
            {
                this.CustomPager.TotalReords = list.TotalCount;
            }

            ucNaviageControl.LoadData(PageContentType.Enterprise, "");
            base.InitData();
        }

        protected void CustomPager_RepeaterDataItemPropertying(CustomControl.RepeaterDataItem item)
        {
            item.Tooltip = item.Text;
            item.Url = "";
            item.Keyword = Request.QueryString["keyword"];

            if (item.Index == PageIndex)
            {
                item.CssClass = "current";
                item.Url = "#";
            }
            else
            {
                item.Url = "/Enterprise/" + item.Index + "?keyword=" + item.Keyword;
            }
        }
    }
}