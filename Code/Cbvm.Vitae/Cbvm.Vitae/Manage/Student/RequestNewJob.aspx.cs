using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Cache;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class RequestNewJob : BaseStudentPage
    {
        private IEnterpriseJobService Service
        {
            get { return new EnterpriseJobService(); }
        }

        private string JobCode
        {
            get { return Request.QueryString["JobCode"]; }
        }

        protected override void InitData()
        {
            base.InitData();

            var job = Service.Get(JobCode, true);
            if (job == null)
            {
                Response.Write("职位不存在!");
                Response.End();
                return;
            }

            ltlEnterpriseName.Text = job.Enterprise.Name;
            ltlJobName.Text = job.Name;
            ltlNum.Text = job.Num.ToString();
            ltlSalaryScope.Text = job.SalaryScope;
            ltlWorkPlace.Text = job.WorkPlace;

            var teachers = DataContext.EnterpriseJobRequestQueue.Where(it => it.JobCode == JobCode && it.StudentNum == StudentNum).SelectMany(ix => ix.EnterpriseJobReferraler.Select(ii => new
            {
                teacherNum = ii.UserName,
                Note = ii.Content
            })).ToList();

            this.txt_TeacherNum_.Value = String.Join(";", teachers.Select(ix=>ix.teacherNum));
            txt_TeacherName_.Text = String.Join(";", GlobalAutoCache.TeacherPresentationList.Where(ix => teachers.Any(i => i.teacherNum==ix.TeacherNum)).Select(it => it.NameZh).ToList());

            txtNote.Text = teachers.Select(ix => ix.Note).FirstOrDefault();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var serviceJobRequester = new EnterpriseJobRequestService();
            var teacherNums = txt_TeacherNum_.Value.Split(';').Where(it => !String.IsNullOrEmpty(it)).ToList();

            var result = serviceJobRequester.AddRequestJob(CurrentUser.UserName, JobCode, teacherNums, txtNote.Text);
            //if (result.IsSucess)
            //{
            //    this.ReflashFrame();
            //}
            this.txt_TeacherName_.Text = String.Join(";", GlobalAutoCache.TeacherPresentationList.Where(it => teacherNums.Contains(it.TeacherNum)).Select(it => it.NameZh));

            ShowMsg(result.IsSucess, result.Message);
        }
    }
}