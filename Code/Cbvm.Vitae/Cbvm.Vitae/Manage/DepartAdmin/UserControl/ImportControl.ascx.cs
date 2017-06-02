using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Business.Interface;
using Business.Service;
using Presentation;
using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.DepartAdmin.UserControl
{
    public partial class ImportControl : BaseUserControl
    {
        public string TableName
        {
            get;
            set;
        }

        public IImportService ImportServerControl
        {
            get
            {
                return new ImportService(CurrentUser.Identity);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            (this.Page.Master as MasterDetail).EnableAJAX = false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uploadFile.FileName)) return;
            if (!uploadFile.FileName.EndsWith(".xls")) return;
            var filePath = FileHelper.GenerateRelativeFilePath(MemberID.ToString(), UserType, AttachmentType.Post, uploadFile.FileName);
            try
            {
                this.uploadFile.SaveAs(filePath);
                ImportService.ImportDataFromExcel(new ExeclDataSource
                {
                    FileName = filePath,
                    SelectSQL = "SELECT * FROM " + "[" + TableName + "$]"
                }, ImportServerControl);
                ShowMsg(true, "导入成功!");
            }
            catch (Exception ex)
            {
                ShowMsg(true, "导入失败!请联系管理员");
                WriteLog(ex);
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}