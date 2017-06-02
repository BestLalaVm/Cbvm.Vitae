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
using Presentation.UIView.College.View;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class SearchCollegePage : BaseCommonListPage<CollegeCommonPresentation, CollegeCommandCriteria>
    {
        private IList<CollegeCommonPresentation> Colleges
        {
            get
            {
                var _colleges = this.ViewState["Colleges"] as IList<CollegeCommonPresentation>;
                if (_colleges == null)
                {
                    _colleges = new List<CollegeCommonPresentation>();
                }

                return _colleges;
            }
            set
            {
                this.ViewState["Colleges"] = value;
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
            List<string> collegeCodes = new List<string>();

            for (var index = 0; index < RadGridControl.MasterTableView.Items.Count; index++)
            {
                var grdItem = RadGridControl.MasterTableView.Items[index];
                var chkCollege = grdItem.FindControl("chkCollege") as CheckBox;
                if (chkCollege.Checked)
                {
                    collegeCodes.Add(grdItem.GetDataKeyValue("Code").ToString());
                }
            }

            Session.AddEntityToSession<CollegeCommonPresentation>(
                Colleges.Where(ic => collegeCodes.Contains(ic.Code)).ToList());
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

        protected override Presentation.UIView.EntityCollection<CollegeCommonPresentation> GetSearchResultList(CollegeCommandCriteria criteria)
        {
            var query = DataContext.College.Select(it => new
            {
                Code = it.Code,
                Name = it.Name,
                it.CreateTime
            });

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.Name.Contains(criteria.Name));
            }

            var totalCount = query.Count();

            query = query.OrderByDescending(it => it.CreateTime);
            if (criteria.NeedPaging)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }

            var list = query.Select(it => new CollegeCommonPresentation
            {
                Code = it.Code,
                Name = it.Name,
            }).ToList();

            var collegelist = this.Translate2Presentations<CollegeCommonPresentation>(list);

            collegelist.TotalCount = totalCount;


            Colleges = list;

            return collegelist;
        }
    }
}