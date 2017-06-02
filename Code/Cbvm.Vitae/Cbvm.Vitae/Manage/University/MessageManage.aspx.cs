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
using LkDataContext;
using Cbvm.Vitae.Manage.UserControl;

namespace Cbvm.Vitae.Manage.University
{
    public partial class MessageManage : BaseUniversityListPage<UniversityMessagePresentation, UniversityMessageCriteria>
    {
        private IUniversityMessageService Service
        {
            get
            {
                return new UniversityMessageService();
            }
        }

        public string CategoryCode
        {
            get
            {
                return Request.QueryString["categoryCode"];
            }
        }
        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void grdMessage_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var drp_Category_ = e.Item.FindControl("drp_Category_") as DropDownList;
                BindCategory(drp_Category_);
                drp_Category_.SelectedValue = CategoryCode;

                if (!(e.Item is GridEditFormInsertItem))
                {
                    UniversityMessagePresentation message = e.Item.DataItem as UniversityMessagePresentation;
                    TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
                    EditorControl edtControl = e.Item.FindControl("edtControl") as EditorControl;
                    CheckBox chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
                    HiddenField hdfKey = e.Item.FindControl("hdfKey") as HiddenField;
                    CheckBox chkIsImportant = e.Item.FindControl("chkIsImportant") as CheckBox;
                    CheckBox chkIsTop = e.Item.FindControl("chkIsTop") as CheckBox;

                    txtTitle.Text = message.Title;
                    edtControl.LoadData(message.Content);
                    chkIsOnline.Checked = message.IsOnline;
                    hdfKey.Value = message.Id.ToString();
                    drp_Category_.SelectedValue = message.CategoryCode;
                    chkIsImportant.Checked = message.IsImportant;
                    chkIsTop.Checked = message.IsTop;
                }
            }
        }

        protected void grdMessage_UpdateCommand(object source, GridCommandEventArgs e)
        {
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            EditorControl edtControl = e.Item.FindControl("edtControl") as EditorControl;
            CheckBox chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
            CheckBox chkIsImportant = e.Item.FindControl("chkIsImportant") as CheckBox;
            HiddenField hdfKey = e.Item.FindControl("hdfKey") as HiddenField;
            var drp_Category_ = e.Item.FindControl("drp_Category_") as DropDownList;
            CheckBox chkIsTop = e.Item.FindControl("chkIsTop") as CheckBox;

            int msgId = 0;
            if (e.Item.ItemIndex >= 0)
            {
               msgId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
            }
            var result = Service.Save(new UniversityMessagePresentation()
            {
                Id = msgId,
                Title = txtTitle.Text,
                IsOnline = chkIsOnline.Checked,
                UniversityCode = CurrentUser.Identity,
                IsImportant=chkIsImportant.Checked,
                Content = edtControl.SaveData(),
                CategoryCode=drp_Category_.SelectedValue,
                IsTop = chkIsTop.Checked
            });
            ShowMsg(result.IsSucess, result.Message);

            if (result.IsSucess)
            {
                this.grdMessage.Rebind();
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
            criteria.UniversityCode = CurrentUser.Identity;
            criteria.CategoryCode = CategoryCode;

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

        protected void chkIsTop_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsTop = sender as CheckBox;
            var dataItem = chkIsTop.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            Service.SetIsTop(id, chkIsTop.Checked);
            RadGridControl.Rebind();
        }

        private void BindCategory(DropDownList drp_Category_)
        {
            var list = (new UniversityMessageCategoryService()).GetOrigianlQuery<UniversityMessageCategory>().Select(it => new DataItemPresentation
            {
                Value = it.Code,
                Text = it.Name
            }).ToList();

            drp_Category_.DataSource = list;
            drp_Category_.DataTextField = "Text";
            drp_Category_.DataValueField = "Value";

            drp_Category_.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var messageIds = GetSelectedMessageIds();
            if (!messageIds.Any())
            {
                ShowMsg(false, "请选择要删除的消息!"); return;
            }

            var messages = DataContext.UniversityMessage.Where(ix => messageIds.Contains(ix.ID)).ToList();
            DataContext.UniversityMessage.DeleteAllOnSubmit(messages);
            DataContext.SubmitChanges();

            ShowMsg(true, "删除成功!");

            this.grdMessage.Rebind();
        }

        protected void btnTop_Click(object sender, EventArgs e)
        {
            var messageIds = GetSelectedMessageIds();
            if (!messageIds.Any())
            {
                ShowMsg(false, "请选择要置顶的消息!"); return;
            }
            DataContext.UniversityMessage.Where(ix => messageIds.Contains(ix.ID)).ToList().ForEach(item =>
            {
                item.IsTop = true;
                item.IsTopTime = DateTime.Now;
            });

            DataContext.SubmitChanges();

            ShowMsg(true, "置顶成功!");

            this.grdMessage.Rebind();
        }

        private List<int> GetSelectedMessageIds()
        {
            var messageIds = new List<int>();
            for (var index = 0; index < RadGridControl.MasterTableView.Items.Count; index++)
            {
                var grdItem = RadGridControl.MasterTableView.Items[index];
                var chkSelected = grdItem.FindControl("chkSelected") as CheckBox;
                if (chkSelected.Checked)
                {
                    messageIds.Add((int)grdItem.GetDataKeyValue("ID"));
                }
            }

            return messageIds;
        }
    }
}