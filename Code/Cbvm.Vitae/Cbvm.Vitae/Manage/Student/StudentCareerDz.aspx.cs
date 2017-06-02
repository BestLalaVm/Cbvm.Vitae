using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.UIView.Student;
using System.Text;
using Presentation.Criteria;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentCareerDz : BaseStudentDetailPage
    {
        private IList<StudentCareerDzWorkPlacePresentation> WorkPlaces
        {
            get
            {
                var _workPlace = this.ViewState["WorkPlaces"] as IList<StudentCareerDzWorkPlacePresentation>;
                if (_workPlace == null)
                {
                    _workPlace = new List<StudentCareerDzWorkPlacePresentation>();
                }

                return _workPlace;
            }
            set
            {
                this.ViewState["WorkPlaces"] = value;
            }
        }

        protected override void InitData()
        {
            lstNarure.BindSource(BindingSourceType.PositionNatureInfo, false);

            var data = DataContext.StudentCareerDz.Where(it => it.StudentNum == StudentNum).Select(it => new
            {
                it.Keyword,
                it.Frequence,
                it.WorkNature,
                WorkPlaces = it.StudentCareerDzWorkPlace.Select(ix => new StudentCareerDzWorkPlacePresentation
                {
                    ID = it.ID,
                    ProvinceCode = ix.ProvinceCode,
                    ProvinceName = ix.ProvinceName,
                    CityCode = ix.CityCode,
                    CityName = ix.CityName
                }).ToList()
            }).FirstOrDefault();

            if (data != null)
            {
                txt_Keyword_.Text = data.Keyword;
                drp_Frquence_.SelectedValue = data.Frequence;

                if (!String.IsNullOrEmpty(data.WorkNature))
                {
                    foreach (var nature in data.WorkNature.Split(';'))
                    {
                        var item = lstNarure.Items.FindByValue(nature);
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                }

                WorkPlaces = data.WorkPlaces;
            }

            rptWorkPlace.DataSource = WorkPlaces;
            rptWorkPlace.DataBind();

            base.InitData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var careerDz = DataContext.StudentCareerDz.Where(it => it.StudentNum == this.StudentNum).FirstOrDefault();
            if (careerDz == null)
            {
                careerDz = new LkDataContext.StudentCareerDz()
                {
                    StudentNum = this.StudentNum
                };
                DataContext.StudentCareerDz.InsertOnSubmit(careerDz);
            }
            careerDz.Keyword = txt_Keyword_.Text;
            careerDz.CountLeft = 10;
            careerDz.Frequence = drp_Frquence_.SelectedValue;

            var nature = new StringBuilder();
            foreach (var itemIndex in this.lstNarure.GetSelectedIndices())
            {
                if (nature.Length > 0)
                {
                    nature.Append(";");
                }
                nature.Append(lstNarure.Items[itemIndex].Value);
            }
            careerDz.WorkNature = nature.ToString();
            careerDz.UpdateTime = DateTime.Now;

            var removedWorkPlaces = careerDz.StudentCareerDzWorkPlace.Where(it => !this.WorkPlaces.Any(ix => ix.ID == it.ID)).ToList();
            for (var index = 0; index < removedWorkPlaces.Count(); index++)
            {
                var item = removedWorkPlaces[index];
                var remvedWorkPlace = careerDz.StudentCareerDzWorkPlace.FirstOrDefault(it => it.ID == item.ID);
                DataContext.StudentCareerDzWorkPlace.DeleteOnSubmit(remvedWorkPlace);
            }

            foreach (StudentCareerDzWorkPlacePresentation workPlaceData in this.WorkPlaces)
            {
                var workPlace = careerDz.StudentCareerDzWorkPlace.Where(it => it.ID == workPlaceData.ID).FirstOrDefault();
                if (workPlace == null)
                {
                    workPlace = new LkDataContext.StudentCareerDzWorkPlace
                    {
                        CreateTime = DateTime.Now
                    };
                    careerDz.StudentCareerDzWorkPlace.Add(workPlace);
                }
                workPlace.ProvinceCode = workPlaceData.ProvinceCode;
                workPlace.ProvinceName = workPlaceData.ProvinceName;

                workPlace.CityCode = workPlaceData.CityCode;
                workPlace.CityName = workPlaceData.CityName;
            }

            DataContext.SubmitChanges();

            this.ShowMsg(true, "保存成功!");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var item = btn.NamingContainer as RepeaterItem;
            if (item != null)
            {
                var datas = this.WorkPlaces;
                var hdfValue = item.FindControl("hdfValue") as HiddenField;
                var workPlace = datas.Where(it => it.ID == int.Parse(hdfValue.Value)).FirstOrDefault();

                datas.Remove(workPlace);
                WorkPlaces = datas;

                rptWorkPlace.DataSource = WorkPlaces;
                rptWorkPlace.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var workPlaces = this.WorkPlaces;
            workPlaces.Add(new StudentCareerDzWorkPlacePresentation()
            {
                ID = int.MaxValue - WorkPlaces.Count()
            });

            this.WorkPlaces = workPlaces;
            rptWorkPlace.DataSource = workPlaces;
            rptWorkPlace.DataBind();
        }

        protected void rptWorkPlace_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var drp_Province_ = e.Item.FindControl("drp_Province_") as DropDownList;
            var drp_City_ = e.Item.FindControl("drp_City_") as DropDownList;
            var hdfValue = e.Item.FindControl("hdfValue") as HiddenField;

            drp_Province_.BindSource(BindingSourceType.ProvinceInfo, true);

            var workplacePresentation = e.Item.DataItem as StudentCareerDzWorkPlacePresentation;
            if (workplacePresentation != null)
            {
                drp_Province_.SelectedValue = workplacePresentation.ProvinceCode;
                if (!String.IsNullOrEmpty(workplacePresentation.ProvinceCode))
                {
                    drp_City_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
                    {
                        Code = workplacePresentation.ProvinceCode
                    });

                    drp_City_.SelectedValue = workplacePresentation.CityCode;
                }

                hdfValue.Value = workplacePresentation.ID.ToString();
            }
        }

        protected void drp_Province__SelectedIndexChanged(object sender, EventArgs e)
        {
            var drp_Province_ = sender as DropDownList;
            RepeaterItem item = drp_Province_.NamingContainer as RepeaterItem;
            var drp_City_ = item.FindControl("drp_City_") as DropDownList;
            drp_City_.BindSource(BindingSourceType.CityInfo, true, new AddressCriteria()
            {
                Code = drp_Province_.SelectedValue
            });

            var hdfValue = drp_City_.NamingContainer.FindControl("hdfValue") as HiddenField;
             var datas = this.WorkPlaces;
            var workPlace = datas.Where(it => it.ID == int.Parse(hdfValue.Value)).FirstOrDefault();
            workPlace.ProvinceCode = drp_Province_.SelectedValue;

            this.WorkPlaces = datas;
        }

        protected void drp_City__SelectedIndexChanged(object sender, EventArgs e)
        {
            var drp_City_ =  sender as DropDownList;
            if (drp_City_ != null)
            {
                var hdfValue = drp_City_.NamingContainer.FindControl("hdfValue") as HiddenField;
                var datas = this.WorkPlaces;
                var workPlace = datas.Where(it => it.ID == int.Parse(hdfValue.Value)).FirstOrDefault();
                if (workPlace != null)
                {
                    workPlace.CityCode = drp_City_.SelectedValue;

                    var drp_Province_ = drp_City_.NamingContainer.FindControl("drp_Province_") as DropDownList;

                    workPlace.ProvinceCode = drp_Province_.SelectedValue;

                    this.WorkPlaces = datas;
                }
            }
        }
    }
}