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

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentPhotoManage : BaseStudentPage
    {
        private IStudentPhotoService Service
        {
            get { return new StudentPhotoService(); }
        }

        private IStudentDictoryService DictoryService
        {
            get { return new StudentDictoryService(); }
        }

        protected int CurrentDictoryId
        {
            get
            {
                var dictoryId = 0;
                int.TryParse(Request.QueryString["dctId"], out dictoryId);
                return dictoryId;
            }
        }

        protected StudentDictoryPresentation CurrentDictory
        {
            get
            {
                var dictory = DictoryService.Get(new StudentDictoryCriteria()
                {
                    Id = CurrentDictoryId,
                    StudentNum = CurrentUser.UserName
                });
                if (dictory == null)
                {
                    Response.Redirect("StudentDictoryManage.aspx");
                    Response.End();
                }
                return dictory;
            }
        }

        protected List<StudentPhotoPresentation> CurrentDictoryPhotoList
        {
            get
            {
                var list = this.ViewState["CurrentDictoryPhotoList"] as List<StudentPhotoPresentation>;
                if (list == null)
                {
                    list = Service.GetAll(new StudentPhotoCriteria()
                    {
                        DictoryId = CurrentDictoryId,
                        StudentNum = CurrentUser.UserName
                    });
                    this.ViewState["CurrentDictoryPhotoList"] = list;
                }
                return list;
            }
        }

        protected override void InitData()
        {
            rptPhoto.DataSource = CurrentDictoryPhotoList.Select(it => new
            {
                Photo = it.SmallPath,
                Title = it.Name.Cut(15, "..."),
                it.Name,
                it.CreateTime,
                it.IsDictoryPhoto,
                it.Id,
                it.DictoryId
            }).ToList();
            rptPhoto.DataBind();
            base.InitData();
        }
    }
}