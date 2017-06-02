using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.College;
using Business.Service.College;
using Presentation.Criteria.College;
using Presentation.UIView;
using Presentation.UIView.College;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Presentation.Enum;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class FellowshipManage : BaseStudentListPage<AssessmentPresentation, AssessmentCriteria>
    {

        private IAssessmentService Service
        {
            get
            {
                return new AssessmentService();
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
            Response.Redirect("FellowshipDetail.aspx");
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

        protected override EntityCollection<AssessmentPresentation> GetSearchResultList(AssessmentCriteria criteria)
        {
            criteria.CollegeCode = CurrentUser.Identity;
            criteria.StudentNum = CurrentUser.UserName;
            criteria.AssessType = AssessType.Fellowship;
            return Service.GetAll(criteria);
        }
    }
}