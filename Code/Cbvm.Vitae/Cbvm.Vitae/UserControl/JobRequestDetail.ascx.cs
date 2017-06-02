using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using LkHelper;
using Presentation.Cache;
using Presentation.UIView.Enterprise;

namespace Cbvm.Vitae.UserControl
{
    public partial class JobRequestDetail : BaseUserControl
    {
        private IEnterpriseJobService Service
        {
            get
            {
                return new EnterpriseJobService();
            }
        }

        private EnterpriseJobPresentation Job
        {
            get
            {
                var job = this.ViewState["EnterpriseJob"] as EnterpriseJobPresentation;
                if (job == null)
                {
                    job = Service.Get(Request.QueryString["JobCode"],true);
                    this.ViewState["EnterpriseJob"] = job;
                }
                return job;
            }
        }

        protected override void InitData()
        {
            base.InitData();

            if (Job != null)
            {
                ltlEnterpriseTypeName.Text = Job.Enterprise.Address;
                ltlIndustryName.Text = Job.Enterprise.Address;
                ltlScopeID.Text = String.IsNullOrEmpty(Job.Enterprise.ScopeCode)
                                      ? ""
                                      : GlobalBaseDataCache.EnterpriseScopeList.FirstOrDefault(
                                          it => it.Code == Job.Enterprise.ScopeCode).Name;
                ltlDescription.Text = Job.Enterprise.Description;
                ltlContactName.Text = Job.Enterprise.ContactName;
                ltlTelephone.Text = Job.Enterprise.ContactPhone;
                ltlAddress.Text = Job.Enterprise.Address;
                ltlEmail.Text = Job.Enterprise.Email;


                ltlJobName.Text = Job.Name;
                ltlJobNum.Text = Job.Num.ToString();
                if (Job.CreateTime != DateTime.MinValue)
                {
                    ltlJobCreateTime.Text = Job.CreateTime.ToCustomerShortDateString();
                }

                ltlJobDateTimeScope.Text = getDateTimeScope(Job.StartTime, Job.EndTime);
                ltlJobDepartName.Text = Job.DepartName;
                ltlJobContactName.Text = Job.ContactName;
                ltlJobTelephone.Text = Job.Telephone;
                ltlJobTax.Text = Job.Tax;
                ltlJobAddress.Text = Job.Address;
                ltlJobEducation.Text = Job.Education;
                ltlJobSex.Text = getSexLabel((int) Job.Sex);
                ltlJobNature.Text = Job.Nature;
                ltlJobRecruitmentTargets.Text = Job.RecruitmentTargets;
                ltlJobAgeScope.Text = Job.AgeScope;
                ltlJobWorkPlace.Text = Job.WorkPlace;
                ltlJobDescription.Text = Job.Description;
            }
        }

        private string getSexLabel(int sexValue)
        {
            var sex = GlobalBaseDataCache.SexInfoList.FirstOrDefault(it => it.Value == sexValue.ToString());
            if (sex != null) return sex.Text;
            return "";
        }


        private string getDateTimeScope(DateTime? startTime, DateTime? endTime)
        {
            var sb = new StringBuilder();
            if (startTime.HasValue)
            {
                sb.AppendFormat("从{0}", startTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            }
            if (endTime.HasValue)
            {
                if (startTime.HasValue)
                {
                    sb.Append("至");
                }
                else
                {
                    sb.Append("有效期至:");
                }
                sb.Append(endTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            }
            return sb.ToString();
        }
    }
}