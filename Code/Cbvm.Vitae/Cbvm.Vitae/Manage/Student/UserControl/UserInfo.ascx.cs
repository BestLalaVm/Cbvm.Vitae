using System;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation;
using Presentation.Cache;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using WebLibrary.Helper;
using Presentation.Criteria;

namespace Cbvm.Vitae.Manage.Student.UserControl
{
    public partial class UserInfo : BaseUserControl
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
            }
        }

        protected UploadFileItemPresentation Filedata
        {
            get { return this.ViewState["Filedata"] as UploadFileItemPresentation; }
            set { this.ViewState["Filedata"] = value; }
        }

        protected override void BindData()
        {
            drp_MarjorCode_.BindSource(BindingSourceType.MarjorInfo, false);
            drp_Period_.BindSource(BindingSourceType.PeriodInfo, false);
            rdo_Sex_.BindSource(BindingSourceType.SexInfo, false);
            rdo_Married_.BindSource(BindingSourceType.MarriedInfo, false);
            drp_Politics_.BindSource(BindingSourceType.PoliticsInfo, false);
            drp_JobSalary_.BindSource(BindingSourceType.SalaryInfo, false);
            chk_OpenType_.BindSource(BindingSourceType.StudentOpenTypeInfo, false);
            //drp_ProvinceName_.BindSource(BindingSourceType.ProvinceInfo, true);
        }

        protected override void InitData()
        {
            var current = Service.Get(CurrentUser.UserName);
            txt_Address_.Text = current.Address;
            rad_Birthday_.SelectedDate = current.Birthday;
            txt_Class_.Text = current.Class;
            txt_Description_.Text = current.Description;
            txt_Email_.Text = current.Email;
            txt_IDentityNum_.Text = current.IDentityNum;
            chkIsOnline.Checked = current.IsMarried;
            chkIsOnline.Checked = current.IsOnline;
            drp_MarjorCode_.SelectedValue = current.MarjorCode;
            txt_NameEn_.Text = current.NameEn;
            txt_NameZh_.Text = current.NameZh;
            txt_NativePlace_.Text = current.NativePlace;
            drp_Period_.SelectedValue = current.Period;
            imgSource.ImageUrl = FileHelper.GetPersonAbsoluatePath((SexType)current.Sex, current.Photo, false);
            drp_Politics_.SelectedValue = ((int) current.Politics).ToString();
            rdo_Sex_.SelectedValue = ((int) current.Sex).ToString();
            txt_StudentNum_.Text = current.StudentNum;
            txt_Tall_.Text = current.Tall;
            txt_Telephone_.Text = current.Telephone;
            txt_WebSite_.Text = current.WebSite;
            rdo_Married_.SelectedValue = GlobalBaseDataCache.GetMarriedValue(current.IsMarried);


            if (current.JobExpect != null)
            {
                for (var index = 0; index < this.chk_OpenType_.Items.Count; index++)
                {
                    int value = 0;
                    if (int.TryParse(chk_OpenType_.Items[index].Value, out value))
                    {
                        if (((int)current.JobExpect.OpenType).Contain(value))
                        {
                            chk_OpenType_.Items[index].Selected = true;
                        }
                    }
                }

                //if (!String.IsNullOrEmpty(current.JobExpect.ProvinceCode))
                //{
                //    drp_CityName_.Enabled = true;
                //    drp_CityName_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
                //    {
                //        Code = current.JobExpect.ProvinceCode
                //    });
                //}

                //if (!String.IsNullOrEmpty(current.JobExpect.CityCode))
                //{
                //    drp_DistrictName_.Enabled = true;
                //    drp_DistrictName_.BindSource(BindingSourceType.DistrictInfo, true, new AddressCriteria()
                //    {
                //        Code = current.JobExpect.CityCode
                //    });
                //}

                //drp_ProvinceName_.SelectedValue = current.JobExpect.ProvinceCode;
                //drp_CityName_.SelectedValue = current.JobExpect.CityCode;
                //drp_DistrictName_.SelectedValue = current.JobExpect.DistrictCode;

                txt_JobAddress_.Text = current.JobExpect.JobAddress;
                txt_JobContent_.Text = current.JobExpect.JobContent;
                txt_JobRequired_.Text = current.JobExpect.JobRequired;
                drp_JobSalary_.SelectedValue = current.JobExpect.JobSalary;
            }

            Filedata = new UploadFileItemPresentation()
                {
                    Path = current.Photo,
                    ThumbPath = current.ThumbPath
                };
        }

        protected void upLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgSource.ImageUrl = fileItem.FilePath;
            var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Student,
                                                                     AttachmentType.BaseInfo, fileItem.FileName);
            FileHelper.DrawingUploadFile(fileItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), 60, 50);
            Filedata = new UploadFileItemPresentation
                {
                    Path = fileItem.FilePath,
                    ThumbPath = thumbPath
                };
        }

        protected override void OnInit(EventArgs e)
        {
            upLoadControl.FinishUploadingImageEventHandler +=
                new BaseUploadControl.FinishUploadingImageEvent(upLoadControl_FinishUploadingImageEventHandler);
            base.OnInit(e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var studentPresentation = new StudentPresentation()
            {
                StudentNum = txt_StudentNum_.Text,
                NameEn = txt_NameEn_.Text,
                NameZh = txt_NameZh_.Text,
                Address = txt_Address_.Text,
                Birthday = rad_Birthday_.SelectedDate,
                Class = txt_Class_.Text,
                Description = txt_Description_.Text,
                Email = txt_Email_.Text,
                IDentityNum = txt_IDentityNum_.Text,
                IsMarried = (rdo_Married_.SelectedValue == "1" ? true : false),
                IsOnline = chkIsOnline.Checked,
                MarjorCode=drp_MarjorCode_.SelectedValue,
                NativePlace = txt_NativePlace_.Text,
                Period = drp_Period_.SelectedValue,
                Sex = (SexType)int.Parse(rdo_Sex_.SelectedValue),
                Tall=txt_Tall_.Text,
                Telephone=txt_Telephone_.Text,
                WebSite=txt_WebSite_.Text,
                JobExpect = new StudentJobExpectPresentation()
                {
                    OpenType = (int)GetStudentOpenType(),
                    JobSalary = drp_JobSalary_.Text,
                    JobContent = txt_JobContent_.Text,
                    JobRequired = txt_JobRequired_.Text,
                    JobAddress = txt_JobAddress_.Text
                }
            };
            //if (drp_ProvinceName_.SelectedItem != null)
            //{
            //    studentPresentation.JobExpect.ProvinceCode = drp_ProvinceName_.SelectedValue;
            //    studentPresentation.JobExpect.ProvinceName = drp_ProvinceName_.SelectedItem.Text;
            //}

            //if (drp_CityName_.SelectedItem != null)
            //{
            //    studentPresentation.JobExpect.CityCode = drp_CityName_.SelectedValue;
            //    studentPresentation.JobExpect.CityName = drp_CityName_.SelectedItem.Text;
            //}

            //if (drp_DistrictName_.SelectedItem != null)
            //{
            //    studentPresentation.JobExpect.DistrictCode = drp_DistrictName_.SelectedValue;
            //    studentPresentation.JobExpect.DistrictName = drp_DistrictName_.SelectedItem.Text;
            //}

            if (Filedata != null)
            {
                studentPresentation.Photo = Filedata.Path;
                studentPresentation.ThumbPath = Filedata.ThumbPath;
            }
            if (!string.IsNullOrEmpty(drp_Politics_.SelectedValue))
            {
                studentPresentation.Politics = (PoliticsType)int.Parse(drp_Politics_.SelectedValue);
            }
            var result = Service.Save(studentPresentation);

            ShowMsg(result.IsSucess, result.Message);
        }

        private StudentOpenType GetStudentOpenType()
        {
            var openType = StudentOpenType.None;
            for (var index = 0; index < this.chk_OpenType_.Items.Count; index++)
            {
                if (this.chk_OpenType_.Items[index].Selected)
                {
                    int value = 0;
                    if (int.TryParse(chk_OpenType_.Items[index].Value, out value))
                    {
                        openType = openType | (StudentOpenType) value;
                    }
                }
            }
            return openType;
        }

        //protected void drp_ProvinceName__SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    drp_CityName_.Enabled = true;
        //    drp_DistrictName_.Enabled = false;
        //    drp_CityName_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
        //    {
        //        Code = drp_ProvinceName_.SelectedValue
        //    });

        //    drp_DistrictName_.SelectedValue = "";
        //}

        //protected void drp_CityName__SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    drp_DistrictName_.Enabled = true;
        //    drp_DistrictName_.BindSource(BindingSourceType.DistrictInfo, true, new AddressCriteria()
        //    {
        //        Code = drp_CityName_.SelectedValue
        //    });
        //}
    }

}
