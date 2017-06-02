using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.College;
using Presentation.UIView;
using Presentation.UIView.College;
using Telerik.Web.UI;
using Business.Interface;
using Presentation.Enum;
using WebLibrary.Helper;
using LkHelper;
using Presentation.Cache;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class StudentTruthAuthenticatedList : BaseEnterpriseListPage<TruthAuthenticatedPresentation, TruthAuthenticatedCriteria>
    {
        private IStudentTruthAuthenticatedService _Service;
        private IStudentTruthAuthenticatedService Service
        {
            get
            {
                if (_Service == null)
                {
                    _Service = new StudentTruthAuthenticatedService();
                }

                return _Service;
            }
        }

        public string StudentNum
        {
            get
            {
                return Request.QueryString["StudentNum"];
            }
        }

        private LkDataContext.CVAcademicianDataContext DataContext
        {
            get
            {
                return Service.DataContext;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdProject; }
        }


        protected override EntityCollection<TruthAuthenticatedPresentation> GetSearchResultList(TruthAuthenticatedCriteria criteria)
        {
            var query = DataContext.StudentTruthAuthenticated.Where(it => it.StudentNum == this.StudentNum);

            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.RequestDate >= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                query = query.Where(it => it.RequestDate < criteria.DateTo.Value.AddDays(1));
            }

            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = query.Where(it => it.Enterprise.Name.Contains(criteria.EnterpriseName));
            }

            var totalCount = query.Count();

            query = query.OrderByDescending(it => it.ID);
            if (criteria.NeedPaging)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }
            var entityList = Service.Translate2Presentations<TruthAuthenticatedPresentation>(query.Select(it => new TruthAuthenticatedPresentation
            {
                ID = it.ID,
                StudentName = it.Student.NameZh + " " + it.Student.NameEn,
                StudentNum = it.StudentNum,
                MarjorName = it.Student.Marjor.Name,
                RequestDate = it.RequestDate,
                EnterpriseName = it.Enterprise.Name,
                EnterpriseCode = it.RequestEnterpriseCode,
                AuthenticatedComment=it.AuthenticatedComment,
                IsAuthenticated=it.IsAuthenticated
            }).ToList());

            entityList.TotalCount = totalCount;

            return entityList;
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<TruthAuthenticatedPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.ID,
                it.Index,
                RequestDate = it.RequestDate.ToCustomerDateString(),
                it.StudentNum,
                it.StudentName,
                it.MarjorName,
                it.EnterpriseCode,
                it.EnterpriseName,
                it.AuthenticatedComment,
                it.IsAuthenticated
            }).ToList();
        }
    }
}