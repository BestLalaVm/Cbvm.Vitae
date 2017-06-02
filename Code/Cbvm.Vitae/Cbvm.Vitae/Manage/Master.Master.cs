using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage
{
    public partial class Master : System.Web.UI.MasterPage
    {
        public string DefaultPage
        {
            get
            {
                if (this.ViewState["DefaultPage"] == null) return "UserInfo.aspx";
                return this.ViewState["DefaultPage"].ToString();
            }
            set
            {
                this.ViewState["DefaultPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }
    }
}