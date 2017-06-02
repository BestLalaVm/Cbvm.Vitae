using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Cache;
using WebLibrary.Helper;
using Cbvm.Vitae.App_Code;
using Presentation.UIView.Front;

namespace Cbvm.Vitae.Template.UserControlV2
{
    public partial class EnterpriseDetailWidget : System.Web.UI.UserControl
    {
        private IEnterpriseService Service
        {
            get
            {
                return new EnterpriseService();
            }
        }

        protected EnterpriseViewPresentation _Presentation;
        protected EnterpriseViewPresentation Presentation
        {
            get
            {
                if (_Presentation == null)
                {
                    _Presentation = Service.GetFrontDetail(Request.QueryString["KeyCode"]);
                }

                return _Presentation;
            }
        }
    }
}