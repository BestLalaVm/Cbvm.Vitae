using System;
using System.Collections.Generic;
using System.Linq;
using Presentation.Cache;
using Presentation.UIView.Student.View;
using System.Text;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class StudentTop20Widget : BaseFrontUserControl
    {
        #region DataSource
        /// <summary>
        /// DataSource
        /// </summary>
        public IList<StudentFrontPresentation> DataSource
        {
            get;
            set;
        }
        #endregion

        #region Caption
        /// <summary>
        /// Caption
        /// </summary>
        public string Caption
        {
            get
            {
                if (this.ViewState["Caption"] == null)
                {
                    return "点击率排行榜";
                }
                return this.ViewState["Caption"].ToString();
            }
            set
            {
                this.ViewState["Caption"] = value;
            }
        }
        #endregion

        public override void DataBind()
        {
            var studentList = DataSource;
            this.rptStudent.DataSource = studentList.Select(it => new
            {
                Url = UrlRuleHelper.GenerateUrl(it.StudentNum.ToString(), it.NameZh, RulePathType.StudentInfo),
                Photo = FileHelper.GetPersonAbsoluatePath(it.Sex, it.Photo,true),
                NameZh = AuthorizeHelper.AuthorizateStudentInfo(it, this.GetCultureInfo()),
                it.StudentNum,
                MarjorName = GetMarjorName(it.MarjorCode),
                Title = GetTitle(it),
                it.VisitedCount
            }).ToList();
            rptStudent.DataBind();

            base.DataBind();
        }

        private string GetMarjorName(string marjorCode)
        {
            var marjorName = GlobalBaseDataCache.GetMarjorName(marjorCode);
            StringBuilder sb = new StringBuilder();
            while (marjorName.Length > 8)
            {
                sb.AppendFormat("{0}<br>", marjorName.Substring(0, 8));
                marjorName = marjorName.Substring(8, marjorName.Length - 8);
            }
            sb.Append(marjorName);
            return sb.ToString();
        }

        private string GetTitle(StudentFrontPresentation student)
        {
            //return GlobalBaseDataCache.GetDepartName(student.DepartCode);

            return null;
        }
    }
}