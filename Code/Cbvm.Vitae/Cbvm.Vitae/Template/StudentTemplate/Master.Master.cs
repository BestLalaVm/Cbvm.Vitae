using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service.Student;

namespace Cbvm.Vitae.Template.StudentTemplate
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var service = new StudentService();
                this.vstStudentList.DataSource =
                    service.GetVisitStudentList((this.Page as BaseFrontStudentPage).StudentNum, 10);
                vstStudentList.DataBind();
            }

            Page.Header.DataBind();
        }
    }
}