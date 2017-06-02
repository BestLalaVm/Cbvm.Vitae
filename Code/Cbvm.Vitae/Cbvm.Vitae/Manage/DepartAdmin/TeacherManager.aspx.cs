using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Business.Interface.Teacher;
using Business.Service.Teacher;
using Presentation.Cache;
using Presentation.Criteria.Teacher;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Base;
using Presentation.UIView.Teacher;
using Telerik.Web.UI;
using WebLibrary.Helper;


namespace Cbvm.Vitae.Manage.DepartAdmin
{
    public partial class TeacherManager : BaseDepartAdminListPage<TeacherPresentation, TeacherCriteria>
    {
        private ITeacherService Service
        {
            get
            {
                return new TeacherService();
            }
        }

        private IList<TeacherPresentation> TeacherCollection
        {
            get
            {
                IList<TeacherPresentation> list = this.ViewState["TeacherCollection"] as IList<TeacherPresentation>;
                if (list == null)
                {
                    list = new List<TeacherPresentation>();
                }
                return list;
            }
            set
            {
                this.ViewState["TeacherCollection"] = value;
            }
        }

        protected override Panel PnlConditionControl
        {
            get
            {
                return pnlCondition;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.grdTeacher.MasterTableView.InsertItem();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.grdTeacher.Rebind();
        }

        protected override void InitData()
        {
            base.InitData();
            prm_TeacherGroupCode_.BindSource(BindingSourceType.TeacherGroupCodeInfo, true);
        }

        protected void grdTeacher_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                TextBox txtTeacherNum = e.Item.FindControl("txtTeacherNum") as TextBox;
                TextBox txtNameZh = e.Item.FindControl("txtNameZh") as TextBox;
                RadioButton rdoSexMale = e.Item.FindControl("rdoSexMale") as RadioButton;
                TextBox txtTelephone = e.Item.FindControl("txtTelephone") as TextBox;
                CheckBox chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
                TextBox txtNameEn = e.Item.FindControl("txtNameEn") as TextBox;
                TextBox txtNativePlace = e.Item.FindControl("txtNativePlace") as TextBox;
                TextBox txtGraduateSchool = e.Item.FindControl("txtGraduateSchool") as TextBox;
                DropDownList drp_EducationName_ = e.Item.FindControl("drp_EducationName_") as DropDownList;
                ListBox lstTeacherGroupCode = e.Item.FindControl("lstTeacherGroupCode") as ListBox;
                TextBox txtMarjorName = e.Item.FindControl("txtMarjorName") as TextBox;
                TextBox txtWorkingYear = e.Item.FindControl("txtWorkingYear") as TextBox;

                var teacherID = 0;
                if (e.Item.ItemIndex > -1)
                {
                    teacherID = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                }
                var teacher = TeacherCollection.FirstOrDefault(it => it.Id == teacherID);
                if (teacher == null)
                {
                    teacher = new TeacherPresentation
                        {
                            Id = teacherID,
                            RelativeGroups = new EntityCollection<TeacherGroupPresentation>()
                        };
                }
                teacher.DepartCode =this.DepartCode;
                teacher.TeacherNum = txtTeacherNum.Text;
                teacher.NameZh = txtNameZh.Text;
                teacher.Sex = (rdoSexMale.Checked ? SexType.male : SexType.female);
                teacher.Telephone = txtTelephone.Text;
                teacher.IsOnline = chkIsOnline.Checked;
                teacher.NameEn = txtNameEn.Text;
                teacher.NativePlace = txtNativePlace.Text;
                teacher.School = txtGraduateSchool.Text;
                teacher.EducationCode = drp_EducationName_.SelectedValue;
                teacher.MarjorName = txtMarjorName.Text;
                teacher.WorkingYear = txtWorkingYear.Text;

                var deletedGroupCodes = new List<string>();
                var teacherGroupCodeIndexList = lstTeacherGroupCode.GetSelectedIndices();
                teacher.RelativeGroups.ForEach(existsTeacher =>
                    {
                        if (!teacherGroupCodeIndexList.Any(
                            index => lstTeacherGroupCode.Items[index].Value == existsTeacher.Code))
                        {
                            deletedGroupCodes.Add(existsTeacher.Code);
                        }
                    });
                teacher.RelativeGroups.RemoveAll(ic => deletedGroupCodes.Contains(ic.Code));
                foreach (var index in teacherGroupCodeIndexList)
                {
                    var groupCode = lstTeacherGroupCode.Items[index].Value;
                    if (!String.IsNullOrEmpty(groupCode))
                    {
                        if (!teacher.RelativeGroups.Any(it => it.Code == groupCode))
                        {
                            teacher.RelativeGroups.Add(new TeacherGroupPresentation()
                                {
                                    Code = groupCode
                                });
                        }
                    }
                }

                teacher.RelativeCourses = null;
                var result = Service.Save(teacher, false, true);
                if (!result.IsSucess)
                {
                    e.Canceled = true;
                    ShowMsg(result.IsSucess, result.Message);
                }
            }
        }

        protected void grdTeacher_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                DropDownList drp_EducationName_ = e.Item.FindControl("drp_EducationName_") as DropDownList;
                ListBox lstTeacherGroupCode = e.Item.FindControl("lstTeacherGroupCode") as ListBox;
                drp_EducationName_.BindSource(BindingSourceType.EducationCodeInfo, false);
                lstTeacherGroupCode.BindSource(BindingSourceType.TeacherGroupCodeInfo, true);

                var emptyItem = lstTeacherGroupCode.Items.FindByValue("");
                if (emptyItem != null)
                {
                    emptyItem.Text = "无";
                }

                if (!(e.Item is GridEditFormInsertItem))
                {
                    var teacher = e.Item.DataItem as TeacherPresentationView;
                    TextBox txtTeacherNum = e.Item.FindControl("txtTeacherNum") as TextBox;
                    TextBox txtNameZh = e.Item.FindControl("txtNameZh") as TextBox;
                    RadioButton rdoSex = e.Item.FindControl("rdoSexMale") as RadioButton;
                    RadioButton rdoSexfeMale = e.Item.FindControl("rdoSexfeMale") as RadioButton;
                    TextBox txtTelephone = e.Item.FindControl("txtTelephone") as TextBox;
                    CheckBox chkIsOnline = e.Item.FindControl("chkIsOnline") as CheckBox;
                    TextBox txtNameEn = e.Item.FindControl("txtNameEn") as TextBox;
                    TextBox txtNativePlace = e.Item.FindControl("txtNativePlace") as TextBox;
                    TextBox txtGraduateSchool = e.Item.FindControl("txtGraduateSchool") as TextBox;
                    TextBox txtWorkingYear = e.Item.FindControl("txtWorkingYear") as TextBox;
                    TextBox txtMarjorName = e.Item.FindControl("txtMarjorName") as TextBox;


                    txtTeacherNum.Text = teacher.TeacherNum;
                    txtNameZh.Text = teacher.NameZh;
                    rdoSex.Checked = (teacher.Sex == SexType.male);
                    rdoSexfeMale.Checked = !rdoSex.Checked;
                    chkIsOnline.Checked = teacher.IsOnline;
                    txtTelephone.Text = teacher.Telephone;
                    txtNameEn.Text = teacher.NameEn;
                    txtNativePlace.Text = teacher.NativePlace;
                    txtGraduateSchool.Text = teacher.School;
                    drp_EducationName_.SelectedValue = teacher.EducationCode;
                    txtMarjorName.Text = teacher.MarjorName;
                    txtWorkingYear.Text = teacher.WorkingYear;

                    foreach (var group in teacher.RelativeGroups)
                    {
                        var item = lstTeacherGroupCode.Items.FindByValue(group.Code);
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }

        protected void radGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int) e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(id);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected override RadGrid RadGridControl
        {
            get { return grdTeacher; }
        }

        protected override EntityCollection<TeacherPresentation> GetSearchResultList(TeacherCriteria criteria)
        {
            criteria.IncludeGroup = true;
            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<TeacherPresentation> list)
        {
            grdTeacher.DataSource = list.Select(it => new TeacherPresentationView
            {
                Index =it.Index,
                ContactPhone =it.ContactPhone,
                DepartCode =it.DepartCode,
                Description =it.Description,
                EducationCode = it.EducationCode,
                TeacherNum = it.TeacherNum,
                Telephone = it.Telephone,
                Email = it.Email,
                MarjorName = it.MarjorName,
                NameEn = it.NameEn,
                NameZh = it.NameZh,
                School = it.School,
                Id = it.Id,
                IsMarried = it.IsMarried,
                IsOnline = it.IsOnline,
                NativePlace = it.NativePlace,
                Photo = it.Photo,
                Sex = it.Sex,
                ThumbPath = it.ThumbPath,
                WorkingYear = it.WorkingYear,
                RelativeCourses = it.RelativeCourses,
                RelativeGroups =it.RelativeGroups,
            }).ToList();
        }


        protected void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
        }

        internal class TeacherPresentationView : TeacherPresentation
        {
            public string EducationName
            {
                get
                {
                    return GlobalBaseDataCache.GetEducationName(EducationCode);
                }
            }

            public string GroupName
            {
                get
                {
                    return String.Join(";", RelativeGroups.Select(ic => ic.Name));
                }
            }
        }
    }
}