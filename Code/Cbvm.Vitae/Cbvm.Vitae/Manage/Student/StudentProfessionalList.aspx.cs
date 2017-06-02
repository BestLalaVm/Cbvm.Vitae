using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Cache;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;
using Telerik.Web.UI;
using WebLibrary.Helper;
using Presentation.Enum;
using LkHelper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentProfessionalList : BaseStudentListPage<StudentProfessionalPresentation,StudentProfessionalCriteria >
    {
        private IStudentProfessionalService Service
        {
            get
            {
                return new StudentProfessionalService();
            }
        }

        public ProfessionalType SourceType
        {
            get
            {
                ProfessionalType type;

                Enum.TryParse(Request.QueryString["SourceType"], out type);

                return type;
            }
        }

        public string NameTitle
        {
            get
            {
                if (SourceType == ProfessionalType.Skill)
                {
                    return "证书";
                }
                else
                {
                    return "获奖";
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentProfessionalDetail.aspx?SourceType=" + SourceType.ToString());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridControl.CurrentPageIndex = 0;
            RadGridControl.Rebind();
        }

        protected override Panel PnlConditionControl
        {
            get { return pnlCondition; }
        }

        protected void radGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"];
                var result = Service.Delete(CurrentUser.UserName, id);
                ShowMsg(result.IsSucess, result.Message);
            }
        }

        protected virtual void chkIsOnline_CheckedChanged(object sender, EventArgs e)
        {
            var chkIsOnline = sender as CheckBox;
            var dataItem = chkIsOnline.NamingContainer as GridItem;
            var id = (int)dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"];
            Service.SetStatus(StudentNum, id, chkIsOnline.Checked);
            RadGridControl.Rebind();
        }

        protected override RadGrid RadGridControl
        {
            get { return grdProfessional; }
        }

        protected override EntityCollection<StudentProfessionalPresentation> GetSearchResultList(StudentProfessionalCriteria criteria)
        {
            criteria.Type = SourceType;

            return Service.GetAll(criteria);
        }

        protected override void BindSearchResultList(RadGrid radGrid, IList<StudentProfessionalPresentation> list)
        {
            radGrid.DataSource = list.Select(ix => new
            {
                ix.Id,
                VerfyStatus = GlobalBaseDataCache.GetVerifityStatusLabel(ix.VerfyStatus),
                ix.Index,
                ObtainTime = ix.ObtainTime.ToCustomerShortDateString(),
                ix.EvaluateFromTeacher,
                ix.IsOnline,
                ix.StudentNum,
                ix.TeacherNum,
                ix.LastUpdateTime,
                ix.VerifyStatusReason,
                ix.Type,
                ix.Name
            }).ToList();
        }
    }
}