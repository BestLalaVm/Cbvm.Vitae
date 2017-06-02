using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Presentation.Enum;
using LkHelper;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildCareerPlanManage : BaseFamilyListPage<StudentCareerPlanPresentation, StudentCareerPlanCriteria>
    {
        private IStudentCareerPlanService Service
        {
            get
            {
                return new StudentCareerPlanService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return this.pnlCondition; }
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.DeleteByTKey(id);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChildCareerPlanDetail.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdActivity; }
        }

        protected override EntityCollection<StudentCareerPlanPresentation> GetSearchResultList(StudentCareerPlanCriteria criteria)
        {
            criteria.StudentNum = CurrentUser.Identity;
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentCareerPlanPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Title,
                StartDate = it.StartDate.HasValue?it.StartDate.Value.ToCustomerShortDateString():"",
                EndDate = it.EndDate.HasValue ? it.EndDate.Value.ToCustomerShortDateString() : "",
                it.IsImplemented,
                it.IsOnline,
                ID = it.ID,
                it.Index,
            }).ToList();
        }
    }
}