using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Base;
using Business.Service.Base;
using Presentation.Criteria.Base;
using Presentation.Criteria.College;
using Presentation.UIView;
using Presentation.UIView.Base;
using Business.Interface.University;
using Business.Service.University;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Business;
using Presentation.UIView.University;
using Presentation.Criteria.Universoty;
using WebLibrary.Helper;
using Business.Interface;

namespace Cbvm.Vitae.Manage.University
{
    public partial class JobFairManage : BaseUniversityListPage<JobFareViewPresentation, JobFareViewCriteria>
    {
        private IJobFairManageService _Service;
        private IJobFairManageService Service
        {
            get
            {
                if (_Service == null)
                {
                    _Service = new JobFairManageService();
                }

                return _Service;
            }
        }

        private LkDataContext.CVAcademicianDataContext DataContext
        {
            get
            {
                return ((IOrigianlBaseService)Service).DataContext;
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void radGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var jobFare = DataContext.JobFairManage.Where(it => it.ID == id).FirstOrDefault();
                if (jobFare != null)
                {
                    DataContext.JobFairManageCollege.DeleteAllOnSubmit(jobFare.JobFairManageCollege);
                    DataContext.JobFairManageEnterprise.DeleteAllOnSubmit(jobFare.JobFairManageEnterprise);
                    DataContext.JobFairManage.DeleteOnSubmit(jobFare);
                    DataContext.SubmitChanges();

                    ShowMsg(true, "删除成功!");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("JobFairDetail.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdTeacherGroup; }
        }

        protected override EntityCollection<JobFareViewPresentation> GetSearchResultList(JobFareViewCriteria criteria)
        {
            criteria.UniversityCode = this.UniversityCode;

            var query = DataContext.JobFairManage.Where(it => it.UniversityCode==criteria.UniversityCode);

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.Name.Contains(criteria.Name));
            }

            if (criteria.DateFrom.HasValue)
            {
                query = query.Where(it => it.BeginTime <= criteria.DateFrom.Value);
            }

            if (criteria.DateTo.HasValue)
            {
                query = query.Where(it => it.EndTime >= criteria.DateTo.Value);
            }

            if (!String.IsNullOrEmpty(criteria.EnterpriseName))
            {
                query = query.Where(it => it.JobFairManageEnterprise.Any(ix => ix.Enterprise.Name.Contains(criteria.EnterpriseName)));
            }

            var totalCount = query.Count();

            query = query.OrderByDescending(it => it.ID);
            if (criteria.NeedPaging)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }
            var jobFareList = Service.Translate2Presentations<JobFareViewPresentation>(query.Select(it => new JobFareViewPresentation
            {
                ID = it.ID,
                Name = it.Name,
                BeginTime = it.BeginTime,
                EndTime = it.EndTime,
                IsOnline=it.IsOnline,
                Enterprises = it.JobFairManageEnterprise.Select(ix => new JobFareEnterprisePresentation
                {
                    Code = ix.Enterprise.Code,
                    Name = ix.Enterprise.Name
                }).ToList(),
                Colleges = it.JobFairManageCollege.Select(ix => new JobFareCollegePresentation
                {
                    Code = ix.College.Code,
                    Name = ix.College.Name
                }).ToList()
            }).ToList());

            jobFareList.TotalCount = totalCount;

            return jobFareList;
        }
    }
}