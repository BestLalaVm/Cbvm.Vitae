using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentDictoryManage : BaseStudentPage
    {
        private IStudentDictoryService Service
        {
            get
            {
                return new StudentDictoryService();
            }
        }

        private IList<StudentDictoryPresentation> StudentDictoryList
        {
            get
            {
                var list = this.ViewState["StudentDictoryList"] as IList<StudentDictoryPresentation>;
                if (list == null)
                {
                    list = Service.GetAll(new StudentDictoryCriteria()
                    {
                        StudentNum = CurrentUser.UserName
                    });
                    this.ViewState["StudentDictoryList"] = list;
                }
                return list;
            }
        }

        protected override void InitData()
        {
            rptDictory.DataSource = StudentDictoryList.Select(it => new
            {
                Title = it.Name.Cut(15, "..."),
                Description = !String.IsNullOrEmpty(it.Description) ? it.Description : it.Name,
                it.Id,
                it.CreateTime,
                TotalPhotos = (String.Format("{0} 张", it.PhotoCount)),
                Photo = FileHelper.GetPhotoAbsoluatePath(it.ThumbPath)
            }).ToList();
            rptDictory.DataBind();

            base.InitData();
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {

        }
    }
}