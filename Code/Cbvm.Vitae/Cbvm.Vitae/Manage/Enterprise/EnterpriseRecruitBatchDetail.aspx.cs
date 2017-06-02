using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.UIView.Enterprise;
using Telerik.Web.UI;
using Telerik.Web.UI.Dock;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class EnterpriseRecruitBatchDetail : BaseEnterpriseDetailPage
    {
        private const string JobRequestMsgFormat = "<div class='recruit-cell'>" +
                                                   "   <div class='recruit-requester'>" +
                                                   "      <div class='recruit-info'>" +
                                                   "        <div><a href='{0}' target='_blank'>{1}</a></div>" +
                                                   "        <div>{2}</div>" +
                                                   "        <div><a href='{3}' alt='简历' target='_blank'>简历</a>({4})</div>" +
                                                   "      </div>" +
                                                   "      <div class='recruit-image'>" +
                                                   "         <img src='{5}' alt=''>" +
                                                   "      </div>" +
                                                   "      <br class='clear'/>" +
                                                   "   </div>" +
                                                   "   <div class='content'>" +
                                                   "     <span>{6}</span>" +
                                                   "   </div>" +
                                                   "   <div class='content'  style='color:red;'>" +
                                                   "     <span>{7}</span>" +
                                                   "   </div>" +
                                                   "</div>";

        protected int BatchID
        {
            get
            {
                object batchID = this.ViewState["BatchID"];
                if (batchID == null)
                {
                    int Id = 0;
                    if (int.TryParse(Request.QueryString["ID"], out Id))
                    {
                        this.ViewState["BatchID"] = Id;
                    }
                    batchID = Id;
                }
                return (int) batchID;
            }
            set { this.ViewState["BatchID"] = value; }
        }

        private IEnterpriseRecruitBatchService Service
        {
            get
            {
                return new EnterpriseRecruitBatchService();
            }
        }

        protected IList<EnterpriseJobRequestPresentation> EnterpriseJobRequestViewWithoutBatchList
        {
            get
            {
                var jobRequestViewList = this.ViewState["EnterpriseJobRequestViewWithoutBatchList"] as IList<EnterpriseJobRequestPresentation>;
                if (jobRequestViewList == null)
                {
                    IEnterpriseJobRequestService jobRequestService = new EnterpriseJobRequestService();
                    jobRequestViewList = jobRequestService.GetAllWithoutBatch(EnterpriseCode);
                    this.ViewState["EnterpriseJobRequestViewWithoutBatchList"] = jobRequestViewList;
                }
                return jobRequestViewList;
            }
        }

        private EnterpriseRecruitBatchPresentation RecruitBatchPresentation
        {
            get
            {
                var batch = this.ViewState["RecruitBatchPresentation"] as EnterpriseRecruitBatchPresentation;
                if (batch == null)
                {
                    batch = Service.Get(this.BatchID);
                    this.ViewState["RecruitBatchPresentation"] = batch;
                }
                return batch;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            LoadDockZone(radOriginalRequestDockZone, EnterpriseJobRequestViewWithoutBatchList);
            if (RecruitBatchPresentation != null)
            {
                LoadDockZone(radBatchRequestDockZone, RecruitBatchPresentation.JobRequestPresentations);
                txtTitle.Text = RecruitBatchPresentation.Title;
                txtDescription.Text = RecruitBatchPresentation.Description;
            }
        }

        private void LoadDockZone(RadDockZone radDockZone, IList<EnterpriseJobRequestPresentation> jobRequestedList)
        {
            //foreach (var jobView in jobRequestedList)
            for (int index = 0; index < jobRequestedList.Count; index++)
            {
                var jobView = jobRequestedList[index];
                RadDock radDock = new RadDock()
                {
                    UniqueName = jobView.Id.ToString(),
                    DockMode = DockMode.Default,
                    DefaultCommands = DefaultCommands.ExpandCollapse,
                    Text =
                        String.Format(JobRequestMsgFormat, "",
                            "RQ:" + jobView.Id,
                            jobView.RequestTime.ToString("yyyy-MM-dd"),
                            String.Format("../../../../Template/StudentResume.aspx?StudentNum={0}",
                                jobView.StudentNum), jobView.StudentName,
                            FileHelper.GetPersonAbsoluatePath(jobView.Sex, jobView.ThumbPath, true),
                            jobView.JobName,
                            GenerateReferralers(jobView)),
                    //Title = jobView.JobCode,
                    Title = String.Format("{0}({1})", jobView.JobName, jobView.JobNum),
                    OnClientDragEnd = "DragEnd",
                    EnableDrag = true
                };
                radDockZone.Controls.Add(radDock);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<int> jobRequestIDList = new List<int>();
            foreach (var dockControl in this.radBatchRequestDockZone.Docks)
            {
                int jobRequestID;
                if (int.TryParse(dockControl.UniqueName, out jobRequestID))
                {
                    jobRequestIDList.Add(jobRequestID);
                }
            }

            var recruitBatch = new EnterpriseRecruitBatchPresentation()
            {
                Id = BatchID,
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                EnterpriseCode = EnterpriseCode,
                JobRequestPresentations = jobRequestIDList.Select(it => new EnterpriseJobRequestPresentation()
                {
                    Id = it
                }).ToList()
            };
            var result = Service.Save(recruitBatch);
            if (!result.IsSucess)
            {
                ShowSelfMsg(result.IsSucess, result.Message);
            }
            else
            {
                Response.Redirect("EnterpriseRecruitBatchList.aspx");
            }

            //IList<int> jobRequestIDList = new List<int>();
            //foreach (var dockControl in this.radBatchRequestDockZone.Docks)
            //{
            //    int jobRequestID;
            //    if (int.TryParse(dockControl.UniqueName, out jobRequestID))
            //    {
            //        jobRequestIDList.Add(jobRequestID);
            //    }
            //}
            //int batchID = this.BatchID;

            //var actionRsult = Service.SaveRecruitBatch(CurrentAccount.UserName, txtTitle.Text, txtDescription.Text,
            //                                                      jobRequestIDList, ref batchID);
            //this.BatchID = batchID;
            //if (!actionRsult.IsSucess)
            //{
            //    ShowSelfMsg(actionRsult.IsSucess, actionRsult.Message);
            //}
            //else
            //{
            //    Response.Redirect("EnterpriseRecruitBatchList.aspx");
            //}
        }

        private string GenerateReferralers(EnterpriseJobRequestPresentation jobRequestView)
        {
            if (jobRequestView.JobReferralers == null) return "";
            return String.Format("推荐人:{0}",
                                 String.Join(",", jobRequestView.JobReferralers.Select(it => it.NameZh)));
        }
    }
}