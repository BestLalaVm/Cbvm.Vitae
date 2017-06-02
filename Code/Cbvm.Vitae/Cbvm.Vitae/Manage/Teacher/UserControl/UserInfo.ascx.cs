using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Teacher;
using Business.Service.Teacher;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.Criteria.Teacher;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Teacher;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;
using WebLibrary;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Manage.Teacher.UserControl
{
    public partial class UserInfo : BaseUserControl
    {
        private ITeacherService Service
        {
            get
            {
                return new TeacherService();
            }
        }

        protected UploadFileItemPresentation Filedata
        {
            get
            {
                return this.ViewState["Filedata"] as UploadFileItemPresentation;
            }
            set
            {
                this.ViewState["Filedata"] = value;
            }
        }

        private int MaxIndex
        {
            get
            {
                if (this.ViewState["MaxIndex"] == null) return 1;
                return (int) this.ViewState["MaxIndex"];
            }
            set{
                this.ViewState["MaxIndex"] = value;
            }
        }

        #region properties
        private IList<int> DeleteRelativeCourseIDList
        {
            get
            {
                var relativeCourseList = this.ViewState["DeleteRelativeCourseIDList"] as IList<int>;
                if (relativeCourseList == null)
                {
                    relativeCourseList = new List<int>();
                    this.ViewState["DeleteRelativeCourseIDList"] = relativeCourseList;
                }
                return relativeCourseList;
            }
        }


        private TeacherPresentation CurrentTeacher
        {
            get
            {
                var teacher = this.ViewState["CurrentTeacher"] as TeacherPresentation;
                if (teacher == null)
                {
                    teacher = Service.Get(new TeacherCriteria()
                    {
                        TeacherNum = CurrentUser.UserName,
                        IncludeCourse = true
                    });
                    this.ViewState["CurrentTeacher"] = teacher;
                }
                return teacher;
            }
        }

        #endregion       

        //protected void DrpCourse__SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    drp_CourseID_.BindSource(BindingSourceType.CourseInfo, false, new StudentCourseCriteria()
        //    {
        //        CourseTypeCode = drp_CourseType_.SelectedValue,
        //        MarjorCode = drp_MarjorID_.SelectedValue
        //    });
        //}

        protected void grdSource_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var drp_MarjorID_ = e.Item.FindControl("drp_MarjorID_") as DropDownList;
                var drp_CourseTypeID_ = e.Item.FindControl("drp_CourseTypeID_") as DropDownList;
                var drp_CourseID_ = e.Item.FindControl("drp_CourseID_") as DropDownList;
                var dtpBeginTime = e.Item.FindControl("dtpBeginTime") as RadDatePicker;
                var dtpEndTime = e.Item.FindControl("dtpEndTime") as RadDatePicker;
                var chk_IsAdviserTeacher_ = e.Item.FindControl("chk_IsAdviserTeacher_") as CheckBox;

                drp_MarjorID_.BindSource(BindingSourceType.MarjorInfo, false);
                drp_CourseTypeID_.BindSource(BindingSourceType.CourseTypeInfo, false);
                DropDownControl_SelectedIndexChanged(drp_CourseTypeID_, null);

                var data = e.Item.DataItem as TeacherRelativeCourseViewPresentation;

                drp_MarjorID_.SelectedValue = data.MarjorCode;
                drp_CourseID_.SelectedValue = data.CourseCode;
                var course = GlobalBaseDataCache.CourseList.FirstOrDefault(it => it.Code == data.CourseCode);
                if (course != null)
                {
                    drp_CourseTypeID_.SelectedValue = course.TypeCode;
                }
                dtpBeginTime.SelectedDate = data.BeginTime;
                dtpEndTime.SelectedDate = data.EndTime;
                chk_IsAdviserTeacher_.Checked = data.IsAdviserTeacher;
            }
        }

        protected void DropDownControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpCourseTypeID = sender as DropDownList;
            var gridItem = drpCourseTypeID.NamingContainer as GridItem;
            var drp_CourseID_= gridItem.FindControl("drp_CourseID_") as DropDownList;
            var drp_MarjorCode_ = gridItem.FindControl("drp_MarjorID_") as DropDownList;

            drp_CourseID_.BindSource(BindingSourceType.CourseInfo, false, new StudentCourseCriteria()
            {
                CourseTypeCode = drpCourseTypeID.SelectedValue,
                MarjorCode = drp_MarjorCode_.SelectedValue
            });
        }

        //protected void grdSource_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "delete")
        //    {
        //        var courseId = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
        //        DeleteRelativeCourseIDList.Add(courseId);
        //        this.grdSource.Rebind();
        //    }
        //    else if (e.CommandName == "Update")
        //    {
        //        var drp_MarjorID_ = e.Item.FindControl("drp_MarjorID_") as DropDownList;
        //        var drp_CourseTypeID_ = e.Item.FindControl("drp_CourseTypeID_") as DropDownList;
        //        var drp_CourseID_ = e.Item.FindControl("drp_CourseID_") as DropDownList;
        //        var dtpBeginTime = e.Item.FindControl("dtpBeginTime") as RadDatePicker;
        //        var dtpEndTime = e.Item.FindControl("dtpEndTime") as RadDatePicker;
        //        var chk_IsAdviserTeacher_ = e.Item.FindControl("chk_IsAdviserTeacher_") as CheckBox;

        //        var data = CurrentTeacher.RelativeCourses[e.Item.ItemIndex];

        //        data.MarjorCode = drp_MarjorID_.SelectedValue;
        //        data.CourseCode = drp_CourseID_.SelectedValue;
        //        data.BeginTime = dtpBeginTime.SelectedDate.Value;
        //        data.EndTime = dtpEndTime.SelectedDate.Value;
        //        data.IsAdviserTeacher = chk_IsAdviserTeacher_.Checked;

        //        grdSource.Rebind();
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        //protected void btnNew_Click(object sender, EventArgs e)
        //{
        //    MaxIndex++;
        //    var courseId = 0;
        //    if (CurrentTeacher.RelativeCourses.Any())
        //    {
        //        courseId = CurrentTeacher.RelativeCourses.Max(ic => ic.Id);
        //    }
        //    var item = new TeacherRelativeCoursePresentation()
        //    {
        //        CourseCode = drp_CourseID_.SelectedValue,
        //        MarjorCode = drp_MarjorID_.SelectedValue,
        //        IsAdviserTeacher = chk_IsAdviserTeacher_.Checked,
        //        Index = MaxIndex,
        //        BeginTime = dtpBeginTime.SelectedDate.Value,
        //        EndTime = dtpEndTime.SelectedDate.Value,
        //        TeacherNum = CurrentTeacher.TeacherNum,
        //        Id = ++courseId
        //    };
        //    CurrentTeacher.RelativeCourses.Add(item);
        //    grdSource.Rebind();
        //}

        //protected void grdSource_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    grdSource.DataSource =
        //        CurrentTeacher.RelativeCourses.Where(it => !DeleteRelativeCourseIDList.Contains(it.Id))
        //                      .Select(it => new TeacherRelativeCourseViewPresentation
        //                          {
        //                              Index = it.Index,
        //                              Id = it.Id,
        //                              MarjorName = GlobalBaseDataCache.GetMarjorName(it.MarjorCode),
        //                              CourseTypeName = GlobalBaseDataCache.GetCourseTypeNameByCourseCode(it.CourseCode),
        //                              CourseName = GlobalBaseDataCache.GetCourseName(it.CourseCode),
        //                              IsAdviserTeacher = it.IsAdviserTeacher,
        //                              BeginTimeLabel = it.BeginTime.ToString("yyyy-MM-dd"),
        //                              EndTimeLabel = it.EndTime.ToString("yyyy-MM-dd"),
        //                              BeginTime = it.BeginTime,
        //                              EndTime = it.EndTime,
        //                              IsBroken = it.IsBroken,
        //                              MarjorCode = it.MarjorCode,
        //                              CourseCode = it.CourseCode
        //                          }).ToList();

        //    grdSource.VirtualItemCount =
        //        CurrentTeacher.RelativeCourses.Where(it => !DeleteRelativeCourseIDList.Contains(it.Id))
        //                      .Count();
        //}

        void upLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgSource.ImageUrl = fileItem.FilePath;
            var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Teacher, AttachmentType.BaseInfo, fileItem.FileName);
            FileHelper.DrawingUploadFile(fileItem.FileContent,FileHelper.GeneratePhysicalPath(thumbPath),60,50);
            Filedata = new UploadFileItemPresentation()
            {
                Path = fileItem.FilePath,
                ThumbPath = thumbPath
            };
        }

        protected override void OnInit(EventArgs e)
        {
            upLoadControl.FinishUploadingImageEventHandler += new BaseUploadControl.FinishUploadingImageEvent(upLoadControl_FinishUploadingImageEventHandler);
            base.OnInit(e);
        }

        protected override void InitData()
        {
            txt_ContactPhone_.Text = CurrentTeacher.ContactPhone;
            txt_Description_.Text = CurrentTeacher.Description;
            txt_Email_.Text = CurrentTeacher.Email;
            txt_MarjorName_.Text = CurrentTeacher.MarjorName;
            txt_NameEn_.Text = CurrentTeacher.NameEn;
            txt_NameZh_.Text = CurrentTeacher.NameZh;
            txt_NativePlace_.Text = CurrentTeacher.NativePlace;
            txt_School_.Text = CurrentTeacher.School;
            txt_TeacherNum_.Text = CurrentTeacher.TeacherNum;
            txt_WorkingYear_.Text = CurrentTeacher.WorkingYear;
            chkIsOnline.Checked = CurrentTeacher.IsOnline;
            rdo_Married_.SelectedValue = GlobalBaseDataCache.GetMarriedValue(CurrentTeacher.IsMarried);
            drp_EducationCode_.SelectedValue = CurrentTeacher.EducationCode;

            txt_Telephone_.Text = CurrentTeacher.Telephone;
            rdo_Sex_.SelectedValue = ((int)CurrentTeacher.Sex).ToString();

            imgSource.ImageUrl = FileHelper.GetPersonAbsoluatePath(CurrentTeacher.Sex, CurrentTeacher.Photo, false);          
        }

        protected override void BindData()
        {
            //drp_CourseType_.BindSource(BindingSourceType.CourseTypeInfo, false);
            //DrpCourse__SelectedIndexChanged(drp_CourseType_, null);
            rdo_Sex_.BindSource(BindingSourceType.SexInfo, false);
            rdo_Married_.BindSource(BindingSourceType.MarriedInfo, false);
            //drp_MarjorID_.BindSource(BindingSourceType.MarjorInfo, false);
            drp_EducationCode_.BindSource(BindingSourceType.EducationCodeInfo, false);
        }

        private void SaveData()
        {
            CurrentTeacher.TeacherNum = txt_TeacherNum_.Text;
            CurrentTeacher.Telephone = txt_Telephone_.Text;
            CurrentTeacher.WorkingYear = txt_WorkingYear_.Text;
            CurrentTeacher.Sex = (SexType)int.Parse(rdo_Sex_.SelectedValue);
            CurrentTeacher.School = txt_School_.Text;
            CurrentTeacher.NativePlace = txt_NativePlace_.Text;
            CurrentTeacher.NameZh = txt_NameZh_.Text;
            CurrentTeacher.NameEn = txt_NameEn_.Text;
            CurrentTeacher.MarjorName = txt_MarjorName_.Text;
            CurrentTeacher.IsOnline = chkIsOnline.Checked;
            CurrentTeacher.IsMarried = (rdo_Married_.SelectedValue == "1");
            CurrentTeacher.Email = txt_Email_.Text;
            CurrentTeacher.Description = txt_Description_.Text;
            CurrentTeacher.ContactPhone = txt_ContactPhone_.Text;
            CurrentTeacher.EducationCode = drp_EducationCode_.SelectedValue;
            CurrentTeacher.Photo = imgSource.ImageUrl;

            CurrentTeacher.RelativeCourses = SaveTeacherRelativeCourses();

            foreach (var deletedCourseId in DeleteRelativeCourseIDList)
            {
                CurrentTeacher.RelativeCourses.RemoveAll(it => it.Id == deletedCourseId);
            }

            var result = Service.Save(CurrentTeacher, true, false);
            ShowMsg(result.IsSucess, result.Message);
        }

        private List<TeacherRelativeCoursePresentation> SaveTeacherRelativeCourses()
        {
            //for (int i = 0; i < grdSource.Items.Count; i++)
            //{
            //    var chk_IsAdviserTeacher_ = grdSource.Items[i].FindControl("chk_IsAdviserTeacher_") as CheckBox;
            //    var courseId = (int) grdSource.MasterTableView.DataKeyValues[i]["ID"];
            //    var course = CurrentTeacher.RelativeCourses.FirstOrDefault(it => it.Id == courseId);
            //    course.IsAdviserTeacher = chk_IsAdviserTeacher_.Checked;
            //}
            return CurrentTeacher.RelativeCourses;
        }

        //protected override void InitLoadedData()
        //{
        //    base.InitLoadedData();
        //    this.grdSource.ClientSettings.Scrolling.ScrollHeight = 260;
        //}

        protected class TeacherRelativeCourseViewPresentation : TeacherRelativeCoursePresentation
        {
            public string CourseTypeName
            {
                get;
                set;
            }

            public string CourseName
            {
                get;
                set;
            }

            public string BeginTimeLabel
            {
                get;
                set;
            }

            public string EndTimeLabel
            {
                get;
                set;
            }
        }
    }
}