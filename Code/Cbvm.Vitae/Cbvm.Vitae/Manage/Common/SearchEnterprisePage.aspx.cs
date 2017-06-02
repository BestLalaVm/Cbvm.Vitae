using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation;
using Presentation.Cache;
using Presentation.Criteria;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Enterprise.View;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class SearchEnterprisePage : BaseCommonListPage<EnterpriseCommonPresentation, EnterpriseCommandCriteria>
    {
        private IList<EnterpriseCommonPresentation> Enterprises
        {
            get {
                var _enterprises = this.ViewState["Enterprises"] as IList<EnterpriseCommonPresentation>;
                if (_enterprises == null)
                {
                    _enterprises = new List<EnterpriseCommonPresentation>();
                }

                return _enterprises;
            }
            set
            {
                this.ViewState["Enterprises"] = value;
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {
            List<string> enterpriseCodes = new List<string>();

            for (var index = 0; index < RadGridControl.MasterTableView.Items.Count; index++)
            {
                var grdItem = RadGridControl.MasterTableView.Items[index];
                var chkEnterprise = grdItem.FindControl("chkEnterprise") as CheckBox;
                if (chkEnterprise.Checked)
                {
                    enterpriseCodes.Add(grdItem.GetDataKeyValue("Code").ToString());
                }
            }

            Session.AddEntityToSession<EnterpriseCommonPresentation>(
                Enterprises.Where(ic => enterpriseCodes.Contains(ic.Code)).ToList());
        }

        protected override void InitData()
        {
            base.InitData();

            RadGridControl.ClientSettings.Scrolling.ScrollHeight = 332;
        }

        protected override RadGrid RadGridControl
        {
            get { return grdStudent; }
        }

        protected override Presentation.UIView.EntityCollection<EnterpriseCommonPresentation> GetSearchResultList(EnterpriseCommandCriteria criteria)
        {
            var query = DataContext.Enterprise.Select(it => new 
            {
                Code=it.Code,
                Name=it.Name,
                it.ID
            });

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.Name.Contains(criteria.Name));
            }
            
            var totalCount = query.Count();

            query = query.OrderByDescending(it => it.ID);
            if (criteria.NeedPaging)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }

            var list = query.Select(it => new EnterpriseCommonPresentation
            {
                Code = it.Code,
                Name = it.Name,
            }).ToList();

            var enterpriselist = this.Translate2Presentations<EnterpriseCommonPresentation>(list);

            enterpriselist.TotalCount = totalCount;


            Enterprises = list;

            return enterpriselist;
        }
    }
}