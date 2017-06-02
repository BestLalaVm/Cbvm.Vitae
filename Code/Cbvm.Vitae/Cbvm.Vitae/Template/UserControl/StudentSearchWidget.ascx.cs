using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Template.UserControl
{
    public partial class StudentSearchWidget : BaseFrontUserControl
    {
        protected override void InitData()
        {
            txtName.Text = Request.QueryString["keyword"];
            base.InitData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Template/StudentList.aspx?keyword=" + txtName.Text);
        }
    }
}