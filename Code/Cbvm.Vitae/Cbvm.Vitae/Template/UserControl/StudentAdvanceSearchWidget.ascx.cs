using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.Cache;
using Presentation.UIView.Base;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class StudentAdvanceSearchWidget : BaseUserControl
    {
        protected List<MarjorPresentation> Marjors
        {
            get { return GlobalBaseDataCache.MarjorList; }
        }

        protected string GotoUrl
        {
            get { return HttpContext.Current.Request.Url.AbsoluteUri; }
        }

        protected string MarjorCode
        {
            get { return Request.QueryString["marjorCode"]; }
        }

        protected string Sex
        {
            get { return Request.QueryString["sex"]; }
        }

        protected string Keyword
        {
            get { return Request.QueryString["keyword"]; }
        }
    }
}