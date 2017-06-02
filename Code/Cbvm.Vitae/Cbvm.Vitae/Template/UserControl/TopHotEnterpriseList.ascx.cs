using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using LkHelper;
using Presentation.Enum;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Template.UserControl
{
    public partial class TopHotEnterpriseList : BaseFrontUserControl
    {
        public IEnterpriseService EnterpriseService
        {
            get
            {
                return new EnterpriseService();
            }
        }

        protected string Link2MoreEnterprise
        {
            get
            {
                return UrlRuleHelper.GenerateMoreUrlLink(RulePathType.Enterprise);
            }
        }

        public string Caption
        {
            get
            {
                if (this.ViewState["Caption"] == null) return null;
                return (string)this.ViewState["Caption"];
            }
            set
            {
                this.ViewState["Caption"] = value;
            }
        }

        public HotEnterpriseType EnterpriseHotType
        {
            get
            {
                if (this.ViewState["EnterpriseHotType"] == null) return HotEnterpriseType.TopHotJob;
                return (HotEnterpriseType)this.ViewState["EnterpriseHotType"];
            }
            set
            {
                this.ViewState["EnterpriseHotType"] = value;
            }
        }

        protected override void InitData()
        {
            rptEnterprise.DataSource = EnterpriseService.GetFrontHotEnterpriseList(EnterpriseHotType, 1, 20).Select(it => new
            {
                Url = UrlRuleHelper.GenerateUrl(it.Code, it.Name.Cut(100, ""), RulePathType.Enterprise),
                Name = it.Name.Cut(28, " "),
                Tooltip = it.Name,
            }).ToList();
            rptEnterprise.DataBind();
            base.InitData();
        }
    }
}