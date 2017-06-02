using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Cbvm.Vitae.Manage.UserControl;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class EmailTemplateList : BaseEnterprisePage
    {
        private EntityCollection<EnterpriseEmailTemplatePresentation> EnterpriseEmailTemplateList
        {
            get
            {
                var emailTemplateList = this.ViewState["EmailTemplateList"] as EntityCollection<EnterpriseEmailTemplatePresentation>;
                return emailTemplateList;
            }
            set { this.ViewState["EmailTemplateList"] = value; }
        }

        private IEnterpriseEmailTemplateService Service
        {
            get { return new EnterpriseEmailTemplateService(); }
        }

        protected override void InitData()
        {
            base.InitData();
            EnterpriseEmailTemplateList =
                Service.GetAll(new EnterpriseEmailTemplateCriteria()
                    {
                        EnterpriseCode = EnterpriseCode,
                        PageSize = this.grdEmailTemplate.PageSize,
                        PageIndex = this.grdEmailTemplate.CurrentPageIndex,
                        NeedPaging = true
                    });
        }

        protected void grdEmailTemplate_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var txtSubject = e.Item.FindControl("txtSubject") as TextBox;
                var edtControl = e.Item.FindControl("edtControl") as EditorControl;
                var txtCc = e.Item.FindControl("txtCc") as TextBox;
                var ltlHelpTip = e.Item.FindControl("ltlHelpTip") as Literal;

                var dataItem = e.Item.DataItem as EnterpriseEmailTemplatePresentation;
                txtSubject.Text = dataItem.Subject;
                ltlHelpTip.Text = dataItem.HelpTip;
                edtControl.LoadData(dataItem.Body);
                txtCc.Text = dataItem.Cc;
            }
        }

        protected void grdEmailTemplate_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.grdEmailTemplate.DataSource = EnterpriseEmailTemplateList;
            this.grdEmailTemplate.VirtualItemCount = EnterpriseEmailTemplateList.TotalCount;
        }

        protected void grdEmailTemplate_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                var emailTemplateID = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];

                var txtSubject = e.Item.FindControl("txtSubject") as TextBox;
                var edtControl = e.Item.FindControl("edtControl") as EditorControl;
                var txtCc = e.Item.FindControl("txtCc") as TextBox;
                var result = Service.Save(new EnterpriseEmailTemplatePresentation()
                {
                    Id = emailTemplateID,
                    Subject = txtSubject.Text,
                    Body = edtControl.SaveData(),
                    Cc = txtCc.Text,
                    EnterpriseCode = CurrentUser.UserName
                });

                ShowMsg(result.IsSucess, result.Message);
                InitData();
            }
        }
    }
}