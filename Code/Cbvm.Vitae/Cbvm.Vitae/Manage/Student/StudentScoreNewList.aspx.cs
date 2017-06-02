using System;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView.Student;
using Presentation.Criteria;
using WebLibrary.Helper;
using LkDataContext;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentScoreNewList : BaseStudentListPage<StudentScoreNewPresentation, StudentScoreNewCriteria>
    {
        private IStudentService Service
        {
            get
            {
                return new StudentService();
            }
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override Telerik.Web.UI.RadGrid RadGridControl
        {
            get { return grdSource; }
        }

        protected override Presentation.UIView.EntityCollection<StudentScoreNewPresentation> GetSearchResultList(
            StudentScoreNewCriteria criteria)
        {
            var query = Service.GetOrigianlQuery<StudentScoreNew>().Where(it => it.StudentNum == this.StudentNum);

            var pageIndex = criteria.PageIndex;
            pageIndex--;
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            if (!String.IsNullOrEmpty(criteria.CourseName))
            {
                query = query.Where(it => it.KCMC.Contains(criteria.CourseName));
            }

            var list = query.Select(it => new StudentScoreNewPresentation
            {
                CJ = it.CJ,
                Id = it.ID,
                KCMC = it.KCMC,
                XF = it.XF,
                XN = it.XN,
                XQ = it.XQ
            }).Skip(pageIndex * criteria.PageSize).Take(criteria.PageSize).ToList();

            return Service.Translate2Presentations<StudentScoreNewPresentation>(list);
        }
    }
}