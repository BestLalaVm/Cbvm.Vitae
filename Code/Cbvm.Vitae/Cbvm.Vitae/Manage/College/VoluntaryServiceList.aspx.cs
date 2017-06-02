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
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using Business.Interface;
using Presentation.Enum;
using WebLibrary.Helper;
using LkHelper;
using Presentation.Cache;
using Presentation.Criteria.College;

namespace Cbvm.Vitae.Manage.College
{
    public partial class VoluntaryServiceList : BaseCollegeListPage<StudentVoluntaryServicePresentation,StudentVoluntaryServiceCriteria>
    {
        private IOrigianlBaseService _Service;
        private IOrigianlBaseService Service
        {
            get
            {
                if (_Service == null)
                {
                    _Service = new StudentVoluntaryServiceService();
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentVoluntaryServiceDetail.aspx");
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
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
            prm_DepartCode_.BindSource(BindingSourceType.DepartInfo, true, new DepartCriteria()
            {
                CollegeCode = CurrentUser.Identity
            });
            prm_MarjorCode_.BindSource(BindingSourceType.MarjorInfo, true, new MarjorCriteria
            {
                UniversityCode = CurrentUser.Identity
            });
        }

        protected override EntityCollection<StudentVoluntaryServicePresentation> GetSearchResultList(StudentVoluntaryServiceCriteria criteria)
        {
            var query = DataContext.StudentVoluntaryService.Where(it => it.Student.CollegeCode == CurrentUser.Identity);

            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = query.Where(it => it.Title.Contains(criteria.Title));
            }

            VerifyStatus verfyStatus;
            if (Enum.TryParse(criteria.VerfyStatus, out verfyStatus))
            {
                query = query.Where(it => it.VerifyStatus == (int)verfyStatus);
            }

            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.StartDate <= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                query = query.Where(it => it.EndDate >= criteria.DateTo.Value);
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

            var totalCount = query.Count();

            query = query.OrderByDescending(it => it.ID);
            if (criteria.NeedPaging)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }
            var entityList = Service.Translate2Presentations<StudentVoluntaryServicePresentation>(query.Select(it => new StudentVoluntaryServicePresentation
            {
                ID = it.ID,
                IsOnline = it.IsOnline,
                Title = it.Title,
                Status = (VerifyStatus)it.VerifyStatus,
                StartDate = it.StartDate,
                EndDate = it.EndDate,
                StudentName=it.Student.NameZh+" "+it.Student.NameEn,
                StudentNum = it.StudentNum,
                MarjorName=it.Student.Marjor.Name,
            }).ToList());

            entityList.TotalCount = totalCount;

            return entityList;
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentVoluntaryServicePresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.ID,
                it.Index,
                it.Title,
                StartDate = it.StartDate.HasValue ? it.StartDate.Value.ToCustomerShortDateString() : "",
                EndDate = it.EndDate.HasValue ? it.EndDate.Value.ToCustomerShortDateString() : "",
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(it.Status),
                it.IsOnline,
                it.StudentNum,
                it.StudentName,
                it.MarjorName
            }).ToList();
        }
    }
}