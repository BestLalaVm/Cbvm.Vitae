using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presentation.Enum;
using System.Web.UI.WebControls;

using System.IO;
using WebLibrary.Helper;

public class BaseUploadControl : System.Web.UI.UserControl
{
    public class UploadFileDataItem
    {
        public string FilePath { get; set; }

        public Stream FileContent { get; set; }

        public string FileName { get; set; }

        public DocumentType DocumentType { get; set; }
    }

    #region properties

    public BasePage BasePage
    {
        get { return (this.Page as BasePage); }
    }

    public IBasePageControl BasePageControl
    {
        get { return this.Page as IBasePageControl; }
    }

    protected UserType UserType
    {
        get { return BasePageControl.UserType; }
    }

    protected AttachmentType AttachmentType
    {
        get { return BasePageControl.AttachmentType; }
    }

    public string MemberID
    {
        get { return BasePageControl.MemberID.ToString(); }
    }

    protected string UploadPathRelativeDirectory
    {
        get
        {
            var directory = (string) this.ViewState["UploadPathRelativeDirectory"];
            if (directory == null)
            {
                directory = FileHelper.GenerateRelativeDirectory(MemberID, UserType, AttachmentType);
                this.ViewState["UploadPathRelativeDirectory"] = directory;
            }
            return directory;
        }
    }

    protected virtual string FilePath { get; set; }

    protected virtual FileUpload UploadFile
    {
        get { return null; }
    }

    public int? MaxWidth { get; set; }

    public int? MaxHeight { get; set; }

    #endregion

    public delegate void FinishUploadingImageEvent(UploadFileDataItem fileItem);

    public event FinishUploadingImageEvent FinishUploadingImageEventHandler;

    protected void FinishUploadFile(FileUpload fileUpload)
    {
        if (this.FinishUploadingImageEventHandler != null)
        {
            this.FinishUploadingImageEventHandler(new UploadFileDataItem
                {
                    FilePath = this.FilePath,
                    FileContent = fileUpload.FileContent,
                    FileName = fileUpload.FileName,
                    DocumentType = FileHelper.GetDocumentType(FilePath)
                });
        }
    }

    protected bool GenerateFilePath(FileUpload fileUpload)
    {
        if (!string.IsNullOrEmpty(UploadFile.FileName))
        {
            this.FilePath = FileHelper.GenerateRelativeFilePath(MemberID, UserType, AttachmentType, UploadFile.FileName);
            return FileHelper.DrawingUploadFile(fileUpload.FileContent, FileHelper.GeneratePhysicalPath(this.FilePath),
                                                MaxWidth, MaxHeight);
        }
        return false;
    }
}