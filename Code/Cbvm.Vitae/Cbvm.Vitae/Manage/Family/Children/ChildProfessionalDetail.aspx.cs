using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView.Student;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildProfessionalDetail : BaseFamilyDetailPage
    {
        private IStudentProfessionalService Service
        {
            get { return new StudentProfessionalService(); }
        }

        #region property
        private StudentProfessionalPresentation CurrentProfessional
        {
            get
            {
                var _CurrentProfessional = this.ViewState["CurrentProfessional"] as StudentProfessionalPresentation;
                if (_CurrentProfessional == null)
                {
                    _CurrentProfessional = Service.Get(new StudentProfessionalCriteria()
                    {
                        Id = CurrentID
                    });

                    this.ViewState["CurrentProfessional"] = _CurrentProfessional;
                }
                return _CurrentProfessional;
            }
        }
        #endregion

        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.Professional;
        }

        protected override void InitData()
        {
            txt_Description_.Text = (CurrentProfessional.Description);
            txt_Name_.Text = CurrentProfessional.Name;
            txt_ObtainTime_.Text = CurrentProfessional.ObtainTime.ToShortDateString();
            chk_IsOnline_.Checked = CurrentProfessional.IsOnline;

            BindAttachmentList();
        }

        private void BindAttachmentList()
        {
            this.grdAttList.LoadData(CurrentProfessional.AttachmentPresentations);
        }
    }
}