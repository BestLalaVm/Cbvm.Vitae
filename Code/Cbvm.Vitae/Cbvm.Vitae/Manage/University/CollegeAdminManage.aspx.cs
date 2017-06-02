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
using Business.Interface.College;
using Business.Service.College;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Business;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.University
{
    public partial class CollegeAdminManage : BaseUniversityListPage<CollegeAdminPresentation, CollegeAdminCriteria>
    {
        private ICollegeAdminService Service
        {
            get
            {
                return new CollegeAdminService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void radGrid_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var txtUserName = e.Item.FindControl("txtUserName") as TextBox;
            var drpCollegeName = e.Item.FindControl("drp_CollegeName_") as DropDownList;
            int id = 0;
            if (e.Item.ItemIndex >= 0){
                id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
            }

            var presentation = new CollegeAdminPresentation()
            {
                ID = id,
                UserName = txtUserName.Text,
                
                CollegeCode = drpCollegeName.SelectedValue,
                CreateTime = DateTime.Now
            };
            var chkChangePassword = e.Item.FindControl("chkChangePassword") as CheckBox;
            if (chkChangePassword.Enabled)
            {
                var txtPassword = e.Item.FindControl("txtPassword") as TextBox;
                presentation.Password = txtPassword.Text;
            }

            var result = Service.Save(presentation);

            if (!result.IsSucess)
            {
                e.Canceled = true;
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void radGrid_DeleteCommand(object source, GridCommandEventArgs e)
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
            this.RadGridControl.MasterTableView.InsertItem();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected void radGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var txtUserName = e.Item.FindControl("txtUserName") as TextBox;
                var txtPassword = e.Item.FindControl("txtPassword") as TextBox;
                var drpCollegeName = e.Item.FindControl("drp_CollegeName_") as DropDownList;

                drpCollegeName.BindSource(BindingSourceType.College, false, new Presentation.Criteria.Base.CollegeCriteria()
                {
                    UniversityCode = CurrentUser.Identity
                });

                if (!(e.Item is GridEditFormInsertItem))
                {
                    var collegeAdmin = e.Item.DataItem as CollegeAdminPresentation;
                    txtUserName.Text = collegeAdmin.UserName;
                    drpCollegeName.SelectedValue = collegeAdmin.CollegeCode;

                    txtUserName.Enabled = false;
                    drpCollegeName.Enabled = false;
                }
            }
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdTeacherGroup; }
        }

        protected override EntityCollection<CollegeAdminPresentation> GetSearchResultList(CollegeAdminCriteria criteria)
        {
            return Service.GetAll(criteria);
        }

        protected void chkChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            var chkChangePassword = sender as CheckBox;
            if (chkChangePassword != null)
            {
                var trPassword = chkChangePassword.NamingContainer.FindControl("trPassword") as System.Web.UI.HtmlControls.HtmlTableRow;
                trPassword.Visible = chkChangePassword.Checked;
            }
        }
    }
}