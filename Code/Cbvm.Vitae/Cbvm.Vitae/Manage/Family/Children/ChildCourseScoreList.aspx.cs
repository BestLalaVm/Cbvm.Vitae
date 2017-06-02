using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation;
using Presentation.Criteria.Student;


namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildCourseScoreList : BaseFamilyPage
    {
        private string CourseCode
        {
            get
            {
                return Request.QueryString["CourseCode"];
            }
        }

        private IStudentCourseScoreService Service
        {
            get { return new StudentCourseScoreService(); }
        }

        protected void grdCourseScore_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdCourseScore.DataSource = Service.GetAll(new StudentCourseScoreCriteria()
            {
                CourseCode = CourseCode,
                StudentNum = StudentNum
            }).Select(it => new
            {
                it.CourseName,
                it.CourseCode,
                ExamineTime = it.ExamineTime.ToString("yyyy-MM-dd hh:mm"),
                it.StudentCourseCode,
                it.Index,
                CourseScoreType = EnumHelper.GetEnumDescription(it.CourseScoreType)
            });
        }
    }
}