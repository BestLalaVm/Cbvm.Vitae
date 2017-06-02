using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Cache;
using WebLibrary.Helper;
using Cbvm.Vitae.App_Code;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class EnterpriseDetailWidget : BaseFrontUserControl
    {
        private IEnterpriseService Service
        {
            get
            {
                return new EnterpriseService();
            }
        }

        protected override void InitData()
        {
            var enterprise = Service.GetFrontEnterpriseByCode(Request.QueryString["KeyCode"]);
            if (enterprise == null)
            {
                this.RedirectToDefaultPage(); return;
            }

            if (phContainer.PresentationEmptyCheck(enterprise))
            {
                ltlContactName.Text = enterprise.ContactName;
                ltlTelephone.Text = enterprise.ContactPhone;
                ltlAddress.Text = enterprise.Address;
                ltlEmail.Text = enterprise.Email;
                ltlDescription.Text = enterprise.Description;

                var enterpriseType =
                    GlobalBaseDataCache.EnterpriseTypeList.FirstOrDefault(it => it.Code == enterprise.EnterpriseTypeCode);
                if (enterpriseType != null)
                {
                    ltlEnterpriseTypeName.Text = enterpriseType.Name;
                }
                var industryItem =
                    GlobalBaseDataCache.CdIndustryList.FirstOrDefault(it => it.Code == enterprise.IndustryCode);
                if (industryItem != null)
                {
                    ltlIndustryName.Text = industryItem.Name;
                }
                
                ltlName.Text = enterprise.Name;
                var scopeItem =
                    GlobalBaseDataCache.EnterpriseScopeList.FirstOrDefault(it => it.Code == enterprise.ScopeCode);
                if (scopeItem != null)
                {
                    ltlScopeID.Text = scopeItem.Name;
                }
                rptPostList.DataSource = enterprise.JobPresentations.Select(it => new
                {
                    it.Address,
                    it.AgeScope,
                    it.ContactName,
                    it.CreateTime,
                    it.DepartName,
                    it.Description,
                    Education = GlobalBaseDataCache.GetEducationName(it.Education),
                    DateTimeScope = getDateTimeScope(it.StartTime, it.EndTime),
                    it.Name,
                    it.Nature,
                    it.Num,
                    it.Professional,
                    it.RecruitmentTargets,
                    it.SalaryScope,
                    Sex = GlobalBaseDataCache.GetSexLabel((int) it.Sex),
                    it.Tax,
                    it.Telephone,
                    it.WorkPlace,
                    it.Code,
                    IsExpried = (it.EndTime < DateTime.Now)
                }).ToList();

                
                rptPostList.DataBind();
            }

            ucNaviageControl.LoadData(PageContentType.Enterprise, enterprise.Code);
            base.InitData();
        }

        private string getDateTimeScope(DateTime? startTime, DateTime? endTime)
        {
            var sb = new StringBuilder();
            if (startTime.HasValue)
            {
                sb.AppendFormat("从{0}", startTime.Value.ToShortDateString());
            }
            if (endTime.HasValue)
            {
                if (startTime.HasValue)
                {
                    sb.Append("至");
                }
                else
                {
                    sb.Append("有效期至:");
                }
                sb.Append(endTime.Value.ToShortDateString());
            }
            return sb.ToString();
        }

        protected void rptPostList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                var phRequestJob = e.Item.FindControl("phRequestJob") as PlaceHolder;
                if (CurrentUser.UserType != Presentation.Enum.UserType.Student)
                {
                    phRequestJob.Visible = false;
                }
            }
        }

        protected void btnRequestJob_Click(object sender, EventArgs e)
        {
            var btnRequestJob = sender as LinkButton;

            var server = new EnterpriseJobRequestService();
            var result = server.AddRequestJob(CurrentUser.UserName, btnRequestJob.CommandArgument);
            ShowMsg(result.IsSucess, result.Message);
        }
    }
}