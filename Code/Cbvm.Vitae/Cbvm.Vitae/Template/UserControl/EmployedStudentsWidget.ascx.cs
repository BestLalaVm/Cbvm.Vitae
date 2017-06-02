using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LkHelper;
using Presentation.Cache;
using Presentation.Enum;
using System.Text;
using Presentation.UIView.Student;
using Presentation.UIView.Student.View;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class EmployedStudentsWidget : BaseFrontUserControl
    {
        #region DataSource
        /// <summary>
        /// DataSource
        /// </summary>
        public IList<StudentEmployedFrontPresentation> DataSource
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
            var studentList = DataSource.Select(it => new
                {
                    Url = UrlRuleHelper.GenerateUrl(it.StudentNum.ToString(), it.NameZh, RulePathType.StudentInfo),
                    Photo = FileHelper.GetPersonAbsoluatePath(it.Sex, it.Photo, true),
                    NameZh = AuthorizeHelper.AuthorizateStudentInfo(it, this.GetCultureInfo()),
                    it.StudentNum,
                    MarjorName = GetMarjorName(it.MarjorCode),
                    JobName = it.JobName.Cut(16, ".."),
                    EnterpriseUrl =
                                                          UrlRuleHelper.GenerateUrl(it.EnterpriseCode,
                                                                                    it.EnterpriseName,
                                                                                    RulePathType.Enterprise),
                    ReferralName =
                                                          (String.IsNullOrEmpty(it.ReferralerNames)
                                                               ? ""
                                                               : String.Format("推荐人:{0}", it.ReferralerNames)),
                    EnterpriseName = it.EnterpriseName.Cut(8, ".."),
                    JobNameUrl = String.Format("{0}#{1}", UrlRuleHelper.GenerateUrl(it.EnterpriseCode,
                                                                                    it.EnterpriseName,
                                                                                    RulePathType.Enterprise), it.JobName),
                    EmployedTime = it.EmployedTime.ToCustomerShortDateString()
                }).ToList();
            this.rptStudent.DataSource = studentList;
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