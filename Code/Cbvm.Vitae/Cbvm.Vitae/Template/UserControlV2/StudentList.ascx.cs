using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Enum;
using WebLibrary.Helper;
using Cbvm.Vitae.App_Code;


namespace Cbvm.Vitae.Template.UserControlV2
{
    public partial class StudentList : BaseFrontUserControl
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
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


        protected int TotalCount {
            get
            {
                return this.CustomPager.TotalReords;
            }
        }

        protected override void InitPostData()
        {
            CustomPager.PageIndex = this.PageIndex;
            if (!String.IsNullOrEmpty(Request.QueryString["keyword"]))
            {
                CustomPager.PageItemTemplate = "/Student/{0}?keyword=" + Request.QueryString["keyword"];
            }
            else
            {
                CustomPager.PageItemTemplate = "/Student/{0}";
            }
        }

        protected override void InitData()
        {
            int sexType = -1;
            if (!int.TryParse(Request.QueryString["sex"], out sexType))
            {
                sexType = -1;
            }
            var list = Service.GetSearchFrontStudentList(new StudentFontAdvanceCriteria()
            {
                KeyWord = Request.QueryString["keyword"],
                Marjor = Request.QueryString["marjor"],
                //Depart = Request.QueryString["depart"],
                //SexType = sexType == -1 ? (SexType?)null : (SexType)sexType,
                PageSize = 10,
                PageIndex = PageIndex
            });
            rptStudent.DataSource = list.Select(it => new
            {
                Photo = FileHelper.GetPersonAbsoluatePath(it.Sex, it.Photo, false),
                it.NameZh,
                it.StudentNum,
                Sex = GlobalBaseDataCache.GetSexLabel(it.Sex),
                MarjorName = GlobalBaseDataCache.GetMarjorName(it.MarjorCode),
                Url = UrlRuleHelper.GenerateUrl(it.StudentNum.ToString(), it.NameZh, RulePathType.StudentInfo),
                it.WebSite,
                it.VisitedCount
            }).ToList();
            rptStudent.DataBind();
            if (!list.Any())
            {
                divMsg.Visible = true;
                CustomPager.Visible = false;
            }
            else
            {
                this.CustomPager.TotalReords = list.TotalCount;
            }
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
                item.Url = "Student/" + item.Index;
                var hasParam = false;
                if (!String.IsNullOrEmpty(item.Keyword))
                {
                    item.Url = String.Format("{0}?keyword={1}", item.Url, item.Keyword);

                    hasParam = true;
                }

                if (!String.IsNullOrEmpty(Request.QueryString["depart"]))
                {
                    item.Url = String.Format("{0}{1}depart={2}", item.Url, hasParam ? "&" : "?", Request.QueryString["depart"]);
                    hasParam = true;
                }

                if (!String.IsNullOrEmpty(Request.QueryString["marjor"]))
                {
                    item.Url = String.Format("{0}{1}marjor={2}", item.Url, hasParam ? "&" : "?", Request.QueryString["marjor"]);
                }
            }
        }
    }
}