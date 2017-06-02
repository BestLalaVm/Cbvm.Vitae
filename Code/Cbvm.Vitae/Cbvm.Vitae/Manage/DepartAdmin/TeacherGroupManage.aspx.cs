using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Base;
using Business.Service.Base;
using Presentation.Criteria.Base;
using Presentation.UIView;
using Presentation.UIView.Base;


namespace Cbvm.Vitae.Manage.DepartAdmin
{
    public partial class TeacherGroupManage : BaseDepartAdminListPage<TeacherGroupPresentation, TeacherGroupCriteria>
    {
        private ITeacherGroupService Service
        {
            get
            {
                return new TeacherGroupService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void radGrid_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var txtName = e.Item.FindControl("txtName") as TextBox;
            var txtDescription = e.Item.FindControl("txtDescription") as TextBox;
            string groupCode = (string) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];

            var result = Service.Save(new TeacherGroupPresentation()
            {
                Code = groupCode,
                Name = txtName.Text,
                Description = txtDescription.Text
            });

            if (!result.IsSucess)
            {
                e.Canceled = true;
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected  void radGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var txtName = e.Item.FindControl("txtName") as TextBox;
                var txtDescription = e.Item.FindControl("txtDescription") as TextBox;

                var teacherGroup = e.Item.DataItem as TeacherGroupPresentation;
                txtName.Text = teacherGroup.Name;
                txtDescription.Text = teacherGroup.Description;
            }
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdTeacherGroup; }
        }

        protected override EntityCollection<TeacherGroupPresentation> GetSearchResultList(TeacherGroupCriteria criteria)
        {
            return Service.GetAll(criteria);
        }
    }
}