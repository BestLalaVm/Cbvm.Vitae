using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.College
{
    public partial class ImportStudent : BaseCollegeDetailPage
    {
        protected override void InitLoadedData()
        {
            btnImport.TableName = "STUDENT";
            base.InitLoadedData();
        }
    }
}