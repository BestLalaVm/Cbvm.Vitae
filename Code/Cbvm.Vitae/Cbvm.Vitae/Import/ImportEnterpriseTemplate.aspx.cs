using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service.Enterprise;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Import
{
    public partial class ImportEnterpriseTemplate : System.Web.UI.Page
    {
        protected void btnImport_Click(object sender, EventArgs e)
        {
            EnterpriseService.ReflashEnterpriseTemplate();
        }
    }
}