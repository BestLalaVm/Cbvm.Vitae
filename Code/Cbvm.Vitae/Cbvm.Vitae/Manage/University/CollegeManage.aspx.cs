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

namespace Cbvm.Vitae.Manage.University
{
    public partial class CollegeManage : BaseUniversityListPage<CollegePresentation, CollegeCriteria>
    {
        private ICollegeService Service
        {
            get
            {
                return new CollegeService();
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
            string code = null;
            if (e.Item.ItemIndex >= 0){
                code = (string)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"];
            }


            var result = Service.Save(new CollegePresentation()
            {
                Code = code,
                Name = txtName.Text,
                Description = txtDescription.Text,
                UniversityCode=CurrentUser.Identity
            });

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
                try
                {
                    var code = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"].ToString();
                    var result = Service.DeleteByTKey(code);
                    ShowMsg(result.IsSucess, result.Message);
                }
                catch (Exception ex)
                {
                    ShowMsg(false, "你无法删除该学院信息!");
                }
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
                var txtName = e.Item.FindControl("txtName") as TextBox;
                var txtDescription = e.Item.FindControl("txtDescription") as TextBox;

                if (!(e.Item is GridEditFormInsertItem))
                {
                    var college = e.Item.DataItem as CollegePresentation;
                    txtName.Text = college.Name;
                    txtDescription.Text = college.Description;
                }
            }
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdTeacherGroup; }
        }

        protected override EntityCollection<CollegePresentation> GetSearchResultList(CollegeCriteria criteria)
        {
            return Service.GetAll(criteria);
        }
    }
}