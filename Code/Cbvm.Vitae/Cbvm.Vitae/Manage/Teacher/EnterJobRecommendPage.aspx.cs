using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation;
using Presentation.Cache;
using Presentation.Enum;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Student;
using Cbvm.Vitae;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class EnterJobRecommendPage : BaseTeacherPage
    {
        private IEnterpriseJobService Service
        {
            get
            {
                return new EnterpriseJobService();
            }
        }

        protected EnterpriseJobPresentation EnterpriseJob
        {
            get
            {
                var job = this.ViewState["EnterpriseJob"] as EnterpriseJobPresentation;
                if (job == null)
                {
                    var jobCode = Request.QueryString["JobCode"];
                    job = Service.Get(jobCode,true);
                    this.ViewState["EnterpriseJob"] = job;

                }
                return job;
            }
        }

        protected override void InitData()
        {
            base.InitData();

            ltlEnterpriseName.Text = EnterpriseJob.Enterprise.Name;
            ltlJobName.Text = EnterpriseJob.Name;
            ltlNum.Text = EnterpriseJob.Num.ToString();
            ltlWorkPlace.Text = EnterpriseJob.WorkPlace;


            this.grdStudent.AllowCustomPaging = false;
            this.grdStudent.AllowPaging = false;
            this.grdStudent.ClientSettings.Scrolling.AllowScroll = false;
            this.grdStudent.MasterTableView.AllowPaging = false;
            this.grdStudent.MasterTableView.AllowCustomPaging = false;
        }

        private List<StudentPresentation> CurrentStudentPrsentationList
        {
            get
            {
                return Session.GetRecommendStudentFromSession();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> studentNumList = new List<string>();

            for (var index = 0; index < grdStudent.MasterTableView.Items.Count; index++)
            {
                var grdItem = grdStudent.MasterTableView.Items[index];
                studentNumList.Add(grdItem.GetDataKeyValue("StudentNum").ToString());
            }

            var referralService = new EnterpriseJobRequestQueueService();
            var result = referralService.RecommendStudentToJobFromTeacher(EnterpriseJob.Code, studentNumList, txtNote.Text,TeacherNum);
            ShowMsg(result.IsSucess, result.Message);

            Session.ClearRecommendCache();
        }

        protected void grdStudent_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var studentNum = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["StudentNum"];
            var list = CurrentStudentPrsentationList;
            var item = list.FirstOrDefault(ic => ic.StudentNum.Equals(studentNum));
            list.Remove(item);

            
            Session.AddRecommendStudentToSession(list);
            grdStudent.Rebind();
        }

        protected void grdStudent_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int index = 0;
            grdStudent.DataSource = CurrentStudentPrsentationList.Select(it => new
            {
                Index = (++index),
                it.StudentNum,
                MarjorName = GlobalBaseDataCache.GetMarjorName(it.MarjorCode),
                SexLabel = EnumHelper.GetEnumDescription((SexType) it.Sex),
                it.Class,
                Name = String.Format("{0} {1}", it.NameZh, it.NameEn)
            });
        }
    }
}