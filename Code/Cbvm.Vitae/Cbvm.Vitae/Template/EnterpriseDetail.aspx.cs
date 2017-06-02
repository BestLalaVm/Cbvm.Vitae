using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service.Enterprise;

namespace Cbvm.Vitae.Template
{
    public partial class EnterpriseDetail : BaseFrontPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Request.UrlReferrer != null)
            {
                if (!IsPostBack)
                {
                    EnterpriseService.AddVisitedRecord(Request.QueryString["KeyCode"],
                                                      HttpContext.Current.Request.UserHostAddress,
                                                      CurrentUser.UserName,
                                                      CurrentUser.UserType);
                }
            }
        }
    }
}