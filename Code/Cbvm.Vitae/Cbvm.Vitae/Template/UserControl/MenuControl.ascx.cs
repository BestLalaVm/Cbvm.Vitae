using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class MenuControl : BaseFrontUserControl
    {
        public string Prefix
        {
            get
            {
                if (this.ViewState["Prefix"] == null) return "/Template";
                return this.ViewState["Prefix"].ToString();
            }
        }
    }
}