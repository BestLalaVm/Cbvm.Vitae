using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Cache;
using Presentation.UIView.Student.View;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class VisitStudentWidget : BaseFrontUserControl
    {
        #region DataSource
        /// <summary>
        /// DataSource
        /// </summary>
        public IList<StudentVisitedPresentation> DataSource
        {
            get;
            set;
        }
        #endregion

        public override void DataBind()
        {
            var studentList = DataSource;
            this.rptStudent.DataSource = studentList.Select(it => new
                {
                    Url = UrlRuleHelper.GenerateUrl(it.StudentNum.ToString(), it.NameZh, RulePathType.StudentInfo),
                    Photo = FileHelper.GetPersonAbsoluatePath(it.Sex, it.ThumbPath, true),
                    it.NameZh,
                    it.StudentNum,
                    it.VisitTime,
                    MarjorName = GetMarjorName(it.MarjorCode)
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
    }
}