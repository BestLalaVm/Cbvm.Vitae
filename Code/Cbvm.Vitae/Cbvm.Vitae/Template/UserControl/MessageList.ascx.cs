using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.University;
using Business.Service.University;
using LkHelper;
using WebLibrary.Helper;
using Cbvm.Vitae.App_Code;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class MessageList : BaseFrontUserControl
    {
        private IUniversityMessageService Service
        {
            get
            {
                return new UniversityMessageService();
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

        protected override void InitData()
        {
            Bind();

            ucNaviageControl.LoadData(PageContentType.DepartMessage, "");
            base.InitData();
        }

        private int Index = 1;
        private int getIndex()
        {
            return Index++;
        }

        private void Bind()
        {
            var list = Service.GetFrontMessageList(PageIndex - 1, CustomPager.PageSize);
            rptDepartMsg.DataSource = list.Select(it => new
                {
                    Index = getIndex(),
                    Url = UrlRuleHelper.GenerateUrl(it.Id.ToString(), it.Title.Cut(60, ""), RulePathType.DepartMessage),
                    Title = it.Title.Cut(40, "..."),
                    Tooltip = it.Title,
                    LastUpdateTime = it.LastUpdateTime.ToCustomerShortDateString()
                }).ToList();
            rptDepartMsg.DataBind();

            if (!list.Any())
            {
                divMsg.Visible = true;
                CustomPager.Visible = false;
            }
            else
            {
                this.CustomPager.TotalReords = list.TotalCount;
            }
        }

        protected override void InitPostData()
        {
            CustomPager.PageIndex = this.PageIndex;
            CustomPager.PageSize = 40;

            if (String.IsNullOrEmpty(Request.QueryString["keyword"]))
            {
                CustomPager.PageItemTemplate = "/Message/{0}";
            }
            else
            {
                CustomPager.PageItemTemplate = "/Message/{0}?keyword=" + Request.QueryString["keyword"];
            }
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
                item.Url = String.Format("/Student/{0}?keyword={1}", item.Index, item.Keyword);
            }
        }

        public string GetEmptyReponse()
        {
            return "没任何消息数据";
        }
    }
}