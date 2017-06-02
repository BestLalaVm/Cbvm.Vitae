using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class RecruitFlowDetailByBatch : BaseEnterpriseDetailPage
    {
        private IEnterpriseJobRequestService Service
        {
            get
            {
                return new EnterpriseJobRequestService();
            }
        }

        protected int BatchId
        {
            get
            {
                int batchId = 0;
                int.TryParse(Request.QueryString["BatchID"], out batchId);
                return batchId;
            }
        }

        protected override void InitData()
        {
            base.InitData();
            drp_RecruitFlow_.BindSource(BindingSourceType.RecruitFlowSettedInfo, false);
        }

        protected EntityCollection<EnterpriseJobRequestPresentation> OriginalEnterpriseJobRequestViewList
        {
            get
            {
                var jobRequestList =
                    this.ViewState["OriginalEnterpriseJobRequestViewList"] as EntityCollection<EnterpriseJobRequestPresentation>;
                if (jobRequestList == null)
                {
                    jobRequestList = Service.GetAll(new EnterpriseJobRequestCriteria()
                    {
                        BatchId = BatchId,
                        EnterpriseCode =this.EnterpriseCode
                    });
                    this.ViewState["OriginalEnterpriseJobRequestViewList"] = jobRequestList;
                }
                return jobRequestList;
            }
        }

        protected IList<EnterpriseJobRequestPresentation> TargetEnterpriseJobRequestViewList
        {
            get
            {
                var jobRequestList =
                    this.ViewState["TargetEnterpriseJobRequestViewList"] as EntityCollection<EnterpriseJobRequestPresentation>;
                if (jobRequestList == null)
                {
                    jobRequestList = new EntityCollection<EnterpriseJobRequestPresentation>();
                    this.ViewState["TargetEnterpriseJobRequestViewList"] = jobRequestList;
                }
                return jobRequestList;
            }
        }

        protected void radGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var list =
                OriginalEnterpriseJobRequestViewList.Where(
                    it => !TargetEnterpriseJobRequestViewList.Any(ic => ic.Id == it.Id)).ToList();
            for (int index = 0; index < list.Count; index++)
            {
                list[index].Index = index + 1;
            }
            grdRecruitFlowDetail.DataSource = list;

            this.grdRecruitFlowDetail.AllowCustomPaging = false;
            this.grdRecruitFlowDetail.AllowPaging = false;
            this.grdRecruitFlowDetail.MasterTableView.AllowCustomPaging = false;
            this.grdRecruitFlowDetail.MasterTableView.AllowPaging = false;
        }

        protected void grdTargetJobRequestStage_NeedDataSource(object source,
                                                               Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            for (int index = 0; index < TargetEnterpriseJobRequestViewList.Count; index++)
            {
                TargetEnterpriseJobRequestViewList[index].Index = index + 1;
            }
            grdTargetJobRequestStage.DataSource = TargetEnterpriseJobRequestViewList;

            this.grdTargetJobRequestStage.AllowCustomPaging = false;
            this.grdTargetJobRequestStage.AllowPaging = false;
            this.grdTargetJobRequestStage.MasterTableView.AllowCustomPaging = false;
            this.grdTargetJobRequestStage.MasterTableView.AllowPaging = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var recruitId = 0;
            if (int.TryParse(drp_RecruitFlow_.SelectedValue, out recruitId))
            {
                var recruitDictionary = new Dictionary<int, IList<int>>();
                recruitDictionary.Add(recruitId, TargetEnterpriseJobRequestViewList.Select(it => it.Id).ToList());
                var actionResult = Service.ChangeRequestJobStage(recruitDictionary, txtNotes.Text);
                if (!actionResult.IsSucess)
                {
                    ShowMsg(actionResult.IsSucess, actionResult.Message);
                    return;
                }
                Response.Redirect("RecruitFlowList.aspx");
            }
        }

        protected void btnMoveLeft_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < this.grdTargetJobRequestStage.MasterTableView.Items.Count; index++)
            {
                var gridItem = this.grdTargetJobRequestStage.MasterTableView.Items[index];
                var chkTargetSelected = gridItem.FindControl("chkTargetSelected") as CheckBox;
                if (chkTargetSelected.Checked)
                {
                    var jobRequest =
                        TargetEnterpriseJobRequestViewList.FirstOrDefault(
                            it => it.Id == (int)gridItem.OwnerTableView.DataKeyValues[index]["ID"]);
                    TargetEnterpriseJobRequestViewList.Remove(jobRequest);
                }
            }
            this.grdTargetJobRequestStage.Rebind();
            this.grdRecruitFlowDetail.Rebind();
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            var chkbox = sender as CheckBox;
            GridTableView radGridTableView = null;
            radGridTableView = (chkbox.NamingContainer as GridItem).OwnerTableView;
            string chkBoxId = "chkTargetSelected";
            if (chkbox.ID == "chkOriginalHeader")
            {
                chkBoxId = "chkOriginalSelected";
            }
            for (int index = 0; index < radGridTableView.Items.Count; index++)
            {
                var gridItem = radGridTableView.Items[index];
                var chkSelected = gridItem.FindControl(chkBoxId) as CheckBox;
                chkSelected.Checked = chkbox.Checked;
            }
        }

        protected void btnMoveRight_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < this.grdRecruitFlowDetail.MasterTableView.Items.Count; index++)
            {
                var gridItem = this.grdRecruitFlowDetail.MasterTableView.Items[index];
                var chkOriginalSelected = gridItem.FindControl("chkOriginalSelected") as CheckBox;
                if (chkOriginalSelected.Checked)
                {
                    var jobRequest =
                        OriginalEnterpriseJobRequestViewList.FirstOrDefault(
                            it => it.Id == (int)gridItem.OwnerTableView.DataKeyValues[index]["ID"]);
                    TargetEnterpriseJobRequestViewList.Add(jobRequest);
                }
            }
            this.grdTargetJobRequestStage.Rebind();
            this.grdRecruitFlowDetail.Rebind();
        }
    }
}