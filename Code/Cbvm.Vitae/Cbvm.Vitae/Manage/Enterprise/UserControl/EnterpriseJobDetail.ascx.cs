using System;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Enum;
using Presentation.UIView.Enterprise;
using WebLibrary.Helper;
using Presentation.UIView;
using Presentation.Criteria;
using System.Web.UI.WebControls;


namespace Cbvm.Vitae.Manage.Enterprise.UserControl
{
    public partial class EnterpriseJobDetail : BaseUserControl
    {
        public void LoadData(EnterpriseJobPresentation job)
        {
            drpSex.BindSource(BindingSourceType.SexInfo, false);
            drpNature.BindSource(BindingSourceType.PositionNatureInfo, false);
            drpRecruitmentTarget.BindSource(BindingSourceType.RequiredTargeterInfo, false);
            drpEducation.BindSource(BindingSourceType.EducationCodeInfo, false);
            drp_ProvinceName_.BindSource(BindingSourceType.ProvinceInfo, true);
           

            if (job != null)
            {
                txtAgeScope.Text = job.AgeScope;
                //txtAddress.Text = job.Address;
                txtContactName.Text = job.ContactName;
                radEditor.LoadData(job.Description);
                //drpEducation.Text = job.Education;
                for (var index = 0; index < drpEducation.Items.Count; index++)
                {
                    if (drpEducation.Items[index].Text == job.Education)
                    {
                        drpEducation.Items[index].Selected = true;
                    }
                }
                radStartTime.SelectedDate = job.StartTime;
                radEndTime.SelectedDate = job.EndTime;

                txtName.Text = job.Name;

                txtSalaryScope.Text = job.SalaryScope;
                txtTelephone.Text = job.Telephone;
                txtWorkPlace.Text = job.WorkPlace;
                radNum.Value = job.Num;
                chkIsOnline.Checked = job.IsOnline;

                drpRecruitmentTarget.SelectedValue = job.RecruitmentTargets;
                drpNature.SelectedValue = job.Nature;

                drpSex.SelectedValue = ((int) job.Sex).ToString();


                if (!String.IsNullOrEmpty(job.ProvinceCode))
                {
                    drp_CityName_.Enabled = true;
                    drp_CityName_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
                    {
                        Code = job.ProvinceCode
                    });
                }

                if (!String.IsNullOrEmpty(job.CityCode))
                {
                    drp_DistrictName_.Enabled = true;
                    drp_DistrictName_.BindSource(BindingSourceType.DistrictInfo, true, new AddressCriteria()
                    {
                        Code = job.CityCode
                    });
                }

                drp_ProvinceName_.SelectedValue = job.ProvinceCode;
                drp_CityName_.SelectedValue = job.CityCode;
                drp_DistrictName_.SelectedValue = job.DistrictCode;

            }
        }

        public EnterpriseJobPresentation SaveData(string code)
        {
            var job = new EnterpriseJobPresentation()
            {
                Code = code,
                AgeScope = txtAgeScope.Text,
                //Address = txtAddress.Text,
                ContactName = txtContactName.Text,
                Description = radEditor.SaveData(),
                EndTime = radEndTime.SelectedDate ?? DateTime.Now,
                StartTime = radStartTime.SelectedDate ?? DateTime.Now,
                Name = txtName.Text,
                Nature = drpNature.SelectedItem.Text,
                Num = (int) (radNum.Value ?? 0),
                SalaryScope = txtSalaryScope.Text,
                RecruitmentTargets = drpRecruitmentTarget.SelectedValue,
                Education = drpEducation.Text,
                Sex = int.Parse(drpSex.SelectedValue),
                Telephone = txtTelephone.Text,
                WorkPlace = txtWorkPlace.Text,
                IsOnline = chkIsOnline.Checked
            };

            if (drpEducation.SelectedItem != null)
            {
                job.Education = drpEducation.SelectedItem.Text;
            }

            if (drp_ProvinceName_.SelectedItem != null)
            {
                job.ProvinceCode = drp_ProvinceName_.SelectedValue;
                job.ProvinceName = drp_ProvinceName_.SelectedItem.Text;
            }

            if (drp_CityName_.SelectedItem != null)
            {
                job.CityCode = drp_CityName_.SelectedValue;
                job.CityName = drp_CityName_.SelectedItem.Text;
            }

            if (drp_DistrictName_.SelectedItem != null)
            {
                job.DistrictCode = drp_DistrictName_.SelectedValue;
                job.DistrictName = drp_DistrictName_.SelectedItem.Text;
            }

            return job;
        }

        protected void drp_ProvinceName__SelectedIndexChanged(object sender, EventArgs e)
        {
            drp_CityName_.Enabled = true;
            drp_DistrictName_.Enabled = false;
            drp_CityName_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
            {
                Code = drp_ProvinceName_.SelectedValue
            });

            drp_DistrictName_.SelectedValue = "";
        }

        protected void drp_CityName__SelectedIndexChanged(object sender, EventArgs e)
        {
            drp_DistrictName_.Enabled = true;
            drp_DistrictName_.BindSource(BindingSourceType.DistrictInfo, true, new AddressCriteria()
            {
                Code = drp_CityName_.SelectedValue
            });
        }
    }
}