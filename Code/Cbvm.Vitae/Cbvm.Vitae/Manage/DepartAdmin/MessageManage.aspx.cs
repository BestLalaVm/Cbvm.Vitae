using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.University;
using Business.Service.University;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;
using Telerik.Web.UI;
using Cbvm.Vitae.Manage.UserControl;

namespace Cbvm.Vitae.Manage.DepartAdmin
{
    public partial class MessageManage : BaseDepartAdminListPage<UniversityMessagePresentation, UniversityMessageCriteria>
    {
        private IUniversityMessageService Service
        {
            get
            {
                return new UniversityMessageService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void grdMessage_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode && !(e.Item is GridEditFormInsertItem))
            {
                UniversityMessagePresentation message = e.Item.DataItem as UniversityMessagePresentation;
                TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
                EditorControl edtControl = e.Item.FindControl("edtControl") as EditorControl;
                CheckBox chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
                HiddenField hdfKey = e.Item.FindControl("hdfKey") as HiddenField;

                txtTitle.Text = message.Title;
                edtControl.LoadData(message.Content);
                chkIsOnline.Checked = message.IsOnline;
                hdfKey.Value = message.Id.ToString();
            }
        }

        protected void grdMessage_UpdateCommand(object source, GridCommandEventArgs e)
        {
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            EditorControl edtControl = e.Item.FindControl("edtControl") as EditorControl;
            CheckBox chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
            HiddenField hdfKey = e.Item.FindControl("hdfKey") as HiddenField;
            int msgId = 0;
            if (int.TryParse(hdfKey.Value, out msgId))
            {
                var result = Service.Save(new UniversityMessagePresentation()
                {
                    Id = msgId,
                    Title = txtTitle.Text,
                    IsOnline = chkIsOnline.Checked,
                    //DepartAdminID = MemberEntity.ID,
                    //DepartCode = MemberEntity.DepartCode,
                    Content = edtControl.SaveData()
                });
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var departMesssageId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(departMesssageId);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.grdMessage.MasterTableView.InsertItem();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdMessage; }
        }

        protected override EntityCollection<UniversityMessagePresentation> GetSearchResultList(UniversityMessageCriteria criteria)
        {
            return Service.GetAll(criteria);
        }


        protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsOnline = sender as CheckBox;
            var dataItem = chkIsOnline.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            Service.SetStatus(id, chkIsOnline.Checked);
            RadGridControl.Rebind();
        }
    }
}