using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Interface.Student;
using Business.Service.Enterprise;
using Business.Service.Student;
using Presentation.Criteria.Enterprise;
using Telerik.Web.UI;

namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class AlreadyReferralStudentList : BaseTeacherDetailPage
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
            }
        }

        private IEnterpriseJobService JobService
        {
            get
            {
                return new EnterpriseJobService();
            }
        }


        private string JobCode
        {
            get
            {
                return Request.QueryString["JobCode"];
            }
        }

        protected override void InitBindData()
        {
            base.InitBindData();
            grdStudent.ClientSettings.Scrolling.ScrollHeight = 387;
        }

        protected override void InitData()
        {
            var queue = JobService.Get(JobCode, true);
            ltlEnterpriseName.Text = queue.Enterprise.Name;
            ltlJobName.Text = queue.Name;
        }

        protected void radGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            grdStudent.CurrentPageIndex = e.NewPageIndex;
            grdStudent.Rebind();
        }

        protected void radGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var list = Service.GetReferralAll(JobCode, grdStudent.PageSize, grdStudent.CurrentPageIndex);
            grdStudent.DataSource = list;
            grdStudent.VirtualItemCount = list.TotalCount;
        }
    }
}