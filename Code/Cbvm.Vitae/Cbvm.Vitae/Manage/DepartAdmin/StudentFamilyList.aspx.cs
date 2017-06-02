using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Business.Interface.Family;
using Business.Service.Family;
using Presentation.Criteria.Family;
using Presentation.UIView;
using Presentation.UIView.Family;
using Presentation.UIView.Family.View;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.DepartAdmin
{
    public partial class StudentFamilyList : BaseDepartAdminListPage<FamilyForAdminPresentation, FamilyCriteria>
    {
        private IFamilyService Service
        {
            get
            {
                return new FamilyService();
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

        protected override RadGrid RadGridControl
        {
            get { return grdStudentFamily; }
        }

        protected override EntityCollection<FamilyForAdminPresentation> GetSearchResultList(FamilyCriteria criteria)
        {
            return Service.GetForAdminAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<FamilyForAdminPresentation> list)
        {
            radGrid.DataSource = list.Select(it => new
            {
                it.Index,
                it.StudentIDentityNum,
                it.StudentNum,
                it.StudentTelephone,
                it.StudentName,
                it.StudentSex,
                StudentThumbPath = FileHelper.GetPersonAbsoluatePath(it.StudentSex, it.StudentThumbPath, true),
                it.UserName,
                it.Name
            }).ToList();
        }
    }
}