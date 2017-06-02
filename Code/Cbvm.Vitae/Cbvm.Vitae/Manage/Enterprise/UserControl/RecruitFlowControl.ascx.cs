using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Business.Interface.Base;
using Business.Interface.Enterprise;
using Business.Service.Base;
using Business.Service.Enterprise;
using Presentation;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView.Base;
using Presentation.UIView.Enterprise;
using Telerik.Web.UI;
using Telerik.Web.UI.Dock;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Enterprise.UserControl
{
    public partial class RecruitFlowControl : BaseUserControl
    {
        private const string JobRequestMsgFormat = "<div class='recruit-cell'>" +
                                                   "   <div class='recruit-requester'>" +
                                                   "      <div class='recruit-info'>" +
                                                   "      <div><span>{0}{1}</span></div>" +
                                                   "        <div>{4}</div>" +
                                                   "        <div>{10}</div>" +
                                                   "        <div><a href='{3}' alt='简历' target='_blank'>查看简历</a></div>" +
                                                   "      </div>" +
                                                   "      <div class='recruit-addition'>" +
                                                   "        <div class='recruit-image'>" +
                                                   "          <img src='{5}' alt=''>" +
                                                   "         </div>" +
                                                   "        <div>" +
                                                   "           <a href='#' onclick='AddNote({7},{8});'><span>备注</span></a>" +
                                                   "         </div>" +
                                                   "      </div>" +
                                                   "      <br class='clear'/>" +
                                                   "   </div>" +
                                                   "   <div class='content'>" +
                                                   "     <div>岗位:<span>{6}</span><div>" +
                                                   "     <div>电话:<span>{9}</span><div>" +
                                                   "     <div>投递时间:<span>{2}<span></div><div style='color:red;'>{11}</div>" +
                                                   "   </div>" +
                                                   "</div>";

        private IEnterpriseJobRequestService Service
        {
            get
            {
                return new EnterpriseJobRequestService();
            }
        }

        public string EnterpriseCode
        {
            get
            {
                return (this.Page as BaseEnterprisePage).EnterpriseCode;
            }
        }

        private int RecruitBatchID
        {
            get
            {
                var recruitBatchID = this.ViewState["RecruitBatchID"];
                if (recruitBatchID == null)
                {
                    int batchId = 0;
                    int.TryParse(Request.QueryString["RecruitBatchID"], out batchId);
                    recruitBatchID = batchId;
                    this.ViewState["RecruitBatchID"] = recruitBatchID;
                }
                return (int)recruitBatchID;
            }
        }

        private IList<EnterpriseJobRequestPresentation> EnterpriseJobRequestViewList
        {
            get
            {
                var jobRequestViewList =
                    this.ViewState["EnterpriseJobRequestViewList"] as IList<EnterpriseJobRequestPresentation>;
                return jobRequestViewList;
            }
            set { this.ViewState["EnterpriseJobRequestViewList"] = value; }
        }

        private IList<RecruitFlowSettedPresentation> RecruitFlowList
        {
            get
            {
                var recruitFlowSettedList = this.ViewState["RecruitFlowSettedList"] as IList<RecruitFlowSettedPresentation>;
                return recruitFlowSettedList;
            }
            set { this.ViewState["RecruitFlowSettedList"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            IRecruitFlowSettedService recruitFlowService = new RecruitFlowSettedService();
            RecruitFlowList = recruitFlowService.GetAllRecruitFlow();
            CreateDockZoneBlock(RecruitFlowList);
            Search();
        }

        private void Search()
        {
            EnterpriseJobRequestViewList = Service.GetAll(new EnterpriseJobRequestCriteria()
            {
                EnterpriseCode = EnterpriseCode,
                BatchId = RecruitBatchID,
                RequestTimeFrom = prm_DateFrom_.SelectedDate.HasValue ? prm_DateFrom_.SelectedDate.Value : (DateTime?) null,
                RequestTimeTo = prm_DateTo_.SelectedDate.HasValue ? prm_DateTo_.SelectedDate.Value : (DateTime?) null,
                JobName = prm_JobTitle_.Text,
                StudentName = prm_StudentName_.Text
            });
            CreateDockBlock(EnterpriseJobRequestViewList);
        }

        private void LoadDockZone(RadDockZone radDockZone, int recruitID, IList<EnterpriseJobRequestPresentation> jobRequestedList)
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
                                          jobView.RequestTime.ToString("yyyy-MM-dd hh:MM"),
                                          String.Format("../../../../Template/StudentResume.aspx?StudentNum={0}",
                                                        jobView.StudentNum),
                                                        jobView.StudentName,
                                         FileHelper.GetPersonAbsoluatePath(jobView.Sex, jobView.ThumbPath, true),
                                          jobView.JobName,
                                          jobView.Id,
                                          recruitID,
                                          jobView.Telephone,
                                          EnumHelper.GetEnumDescription(jobView.Sex),
                                          GenerateReferralers(jobView)),
                        //Title = jobView.JobCode,
                        Title = jobView.JobName,
                        OnClientDragEnd = "DragEnd",
                        EnableDrag = true,
                        //Collapsed = true
                    };
                if (index > 5)
                {
                    radDock.Collapsed = true;
                }

                if (jobView.RecruitType == RecruitType.Passed || jobView.RecruitType == RecruitType.NoPassed)
                {
                    radDock.Collapsed = true;
                }
                radDockZone.Controls.Add(radDock);
            }
        }

        private void CreateDockZoneBlock(IList<RecruitFlowSettedPresentation> recruitFlowList)
        {
            foreach (var recruitItem in recruitFlowList)
            {
                var panel = new Panel()
                {
                    CssClass = "flow-dockzone"
                };
                Literal ltlContent = new Literal()
                {
                    Text = String.Format("<div class='flow-caption'><strong>{0}</strong></div>", recruitItem.Name)
                };
                panel.Controls.Add(ltlContent);

                var rdzDockItem = new RadDockZone()
                    {
                        Orientation = Orientation.Vertical,
                        UniqueName = String.Format("DockZone_Recruit_Flow_{0}", recruitItem.Id),
                        EnableViewState = false
                    };
                panel.Controls.Add(rdzDockItem);
                RadDockLayout1.Controls.Add(panel);
            }

        }

        private void CreateDockBlock(IList<EnterpriseJobRequestPresentation> jobRequestViews)
        {
            for (var index = 0; index < RadDockLayout1.Controls.Count; index++)
            {
                var panel = RadDockLayout1.Controls[index] as Panel;
                if (panel != null)
                {
                    RadDockZone radDockZone = panel.Controls[1] as RadDockZone;
                    var recruitID =
                        int.Parse(radDockZone.UniqueName.Substring(radDockZone.UniqueName.LastIndexOf("_") + 1,
                                                                   radDockZone.UniqueName.Length -
                                                                   radDockZone.UniqueName.LastIndexOf("_") - 1));

                    radDockZone.Controls.Clear();

                    var recruitFlow = RecruitFlowList.FirstOrDefault(it => it.Id == recruitID);
                    IList<EnterpriseJobRequestPresentation> jobViewList = null;
                    if (recruitFlow.RecruitType == RecruitType.Request)
                    {
                        jobViewList = jobRequestViews.Where(it => it.JobRequestRecruitStagePresentation == null ||
                                                                               it.JobRequestRecruitStagePresentation.RecruitFlowId ==
                                                                               recruitFlow.Id).ToList();
                    }
                    else
                    {
                        jobViewList = jobRequestViews.Where(it => it.JobRequestRecruitStagePresentation != null &&
                                                                               it.JobRequestRecruitStagePresentation.RecruitFlowId ==
                                                                               recruitFlow.Id).ToList();
                    }

                    LoadDockZone(radDockZone,recruitID, jobViewList);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            prm_DateFrom_.SelectedDate = null;
            prm_DateTo_.SelectedDate = null;

            prm_JobTitle_.Text = "";
            prm_StudentName_.Text = "";

            //Search();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var recruitDictionary = new Dictionary<int, IList<int>>();
            foreach (var dockZone in RadDockLayout1.RegisteredZones)
            {
                var jobRequestIdList = GenerateJobRequestIdList(dockZone);
                if (jobRequestIdList.Any())
                {
                    var dockZoneUniqueName = dockZone.UniqueName;
                    var recruitFlowId = 0;
                    if (int.TryParse(dockZoneUniqueName.Replace("DockZone_Recruit_Flow_", ""), out recruitFlowId))
                    {
                        recruitDictionary.Add(recruitFlowId, jobRequestIdList);
                    }
                }
            }

            var actionResult = Service.ChangeRequestJobStage(recruitDictionary);
            ShowSelfMsg(actionResult.IsSucess, actionResult.Message);
        }

        private IList<int> GenerateJobRequestIdList(RadDockZone dockZone)
        {
            IList<int> jobRequestIdList = new List<int>();
            foreach (var dock in dockZone.Docks)
            {
                int jobRequestId = 0;
                if (int.TryParse(dock.UniqueName, out jobRequestId))
                {
                    jobRequestIdList.Add(jobRequestId);
                }
            }
            return jobRequestIdList;
        }

        private string GenerateReferralers(EnterpriseJobRequestPresentation jobRequestView)
        {
            if (jobRequestView.JobReferralers == null) return "";
            return String.Format("推荐人:{0}",
                                 String.Join(",", jobRequestView.JobReferralers.Select(it => it.NameZh)));
        }
    }
}