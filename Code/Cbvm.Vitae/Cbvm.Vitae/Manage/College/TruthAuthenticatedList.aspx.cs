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

namespace Cbvm.Vitae.Manage.College
{
    public partial class TruthAuthenticatedList : BaseCollegeListPage<TruthAuthenticatedPresentation, TruthAuthenticatedCriteria>
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


        protected override void InitBindData()
        {
            //prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
            prm_DepartCode_.BindSource(BindingSourceType.DepartInfo, true, new DepartCriteria()
            {
                CollegeCode = CurrentUser.Identity
            });
            prm_MarjorCode_.BindSource(BindingSourceType.MarjorInfo, true, new MarjorCriteria
            {
                UniversityCode = CurrentUser.Identity
            });
        }

        protected override EntityCollection<TruthAuthenticatedPresentation> GetSearchResultList(TruthAuthenticatedCriteria criteria)
        {
            var query = DataContext.StudentTruthAuthenticated.Where(it => it.Student.CollegeCode == CurrentUser.Identity);

            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.RequestDate >= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                query = query.Where(it => it.RequestDate < criteria.DateTo.Value.AddDays(1));
            }

            if (!String.IsNullOrEmpty(criteria.StudentName))
            {
                query = query.Where(it => it.Student.NameEn.Contains(criteria.StudentName) || it.Student.NameZh.Contains(criteria.StudentName));
            }

            if (!String.IsNullOrEmpty(criteria.StudentNum))
            {
                query = query.Where(it => it.StudentNum.Contains(criteria.StudentNum));
            }

            if (!String.IsNullOrEmpty(criteria.MarjorCode))
            {
                query = query.Where(it => it.Student.MarjorCode == criteria.MarjorCode);
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
                ID=it.ID,
                StudentName = it.Student.NameZh + " " + it.Student.NameEn,
                StudentNum = it.StudentNum,
                MarjorName = it.Student.Marjor.Name,
                RequestDate = it.RequestDate,
                EnterpriseName = it.Enterprise.Name,
                EnterpriseCode = it.RequestEnterpriseCode,
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
                StartDate = it.RequestDate.ToCustomerShortDateString(),
                it.StudentNum,
                it.StudentName,
                it.MarjorName,
                it.EnterpriseCode,
                it.EnterpriseName,
                it.IsAuthenticated
            }).ToList();
        }
    }
}