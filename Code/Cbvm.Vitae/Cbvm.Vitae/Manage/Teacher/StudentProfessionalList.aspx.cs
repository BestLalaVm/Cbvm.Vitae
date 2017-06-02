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


namespace Cbvm.Vitae.Manage.Teacher
{
    public partial class StudentProfessionalList : BaseTeacherListPage<StudentProfessionalPresentation, StudentProfessionalCriteria>
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
                    return "奖励";
                }
            }
        }

        protected override void InitBindData()
        {
            prm_VerfyStatus_.BindSource(BindingSourceType.VerifyStatusInfo, true);
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

        protected override RadGrid RadGridControl
        {
            get { return grdProfessional; }
        }

        protected override EntityCollection<StudentProfessionalPresentation> GetSearchResultList(StudentProfessionalCriteria criteria)
        {
            criteria.Type = SourceType;
            criteria.TeacherNum = CurrentUser.UserName;

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