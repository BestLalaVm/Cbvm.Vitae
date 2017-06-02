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

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentVoluntaryServiceList : BaseStudentListPage<StudentVoluntaryServicePresentation, StudentVoluntaryServiceCriteria>
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

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];

                var voluntaryService = DataContext.StudentVoluntaryService.Where(it => it.ID == id && it.StudentNum == CurrentUser.UserName).FirstOrDefault();
                if (voluntaryService != null)
                {
                    DataContext.StudentVoluntaryService.DeleteOnSubmit(voluntaryService);
                    DataContext.SubmitChanges();
                    ShowMsg(true, ActionResult.DefaultResult.Message);

                }
                else
                {
                    ShowMsg(false, ActionResult.NotFoundResult.Message);
                }
                //var result = Service.Delete(CurrentUser.UserName, id);
                //ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsOnline = sender as CheckBox;
            var dataItem = chkIsOnline.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            var voluntaryService = DataContext.StudentVoluntaryService.Where(it => it.ID == id && it.StudentNum == CurrentUser.UserName).FirstOrDefault();
            if (voluntaryService != null)
            {
                voluntaryService.IsOnline = chkIsOnline.Checked;
                DataContext.SubmitChanges();
            }

            RadGridControl.Rebind();
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdProject; }
        }


        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
        }

        protected override EntityCollection<StudentVoluntaryServicePresentation> GetSearchResultList(StudentVoluntaryServiceCriteria criteria)
        {
            var query = DataContext.StudentVoluntaryService.Where(it => it.StudentNum == CurrentUser.UserName);

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

            var totalCount = query.Count();

            query = query.OrderByDescending(it => it.ID);
            if (criteria.NeedPaging)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }
            var entityList = Service.Translate2Presentations<StudentVoluntaryServicePresentation>(query.Select(it => new StudentVoluntaryServicePresentation
            {
                ID=it.ID,
                IsOnline=it.IsOnline,
                Title=it.Title,
                Status=(VerifyStatus)it.VerifyStatus,
                StartDate=it.StartDate,
                EndDate=it.EndDate
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
                it.IsOnline
            }).ToList();
        }
    }
}