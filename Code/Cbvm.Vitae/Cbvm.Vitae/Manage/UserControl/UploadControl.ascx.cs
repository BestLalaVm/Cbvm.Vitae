using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class UploadControl : BaseUploadControl
    {
        protected override FileUpload UploadFile
        {
            get
            {
                return this.uploadFile;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (GenerateFilePath(uploadFile))
            {
                this.FinishUploadFile(uploadFile);
            }
        }
    }
}