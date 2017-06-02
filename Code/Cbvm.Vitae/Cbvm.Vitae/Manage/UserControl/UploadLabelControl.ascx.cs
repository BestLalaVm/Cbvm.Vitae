using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class UploadLabelControl : BaseUploadControl
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
            if (string.IsNullOrEmpty(uploadFile.FileName))
            {
                return;
            }
            if (GenerateFilePath(uploadFile))
            {
                this.FinishUploadFile(uploadFile);
            }
            this.txtFileLabel.Text = "";
        }

        public string FileLabel
        {
            get
            {
                return txtFileLabel.Text;
            }
        }
    }
}