using System;
using System.Collections.Generic;
using System.Linq;
using Presentation.UIView;
using Telerik.Web.UI;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.UserControl
{
    public abstract partial class StudentAttachmentControl : BaseUserControl
    {

        public bool Enable
        {
            get
            {
                if (this.ViewState["IsReadOnly"] == null) return true;
                return (bool)this.ViewState["IsReadOnly"];
            }
            set
            {
                this.ViewState["IsReadOnly"] = value;
                phAction.Visible = value;
            }
        }

        //public delegate IList<AttachmentPresentation> AttachmentNeedDataSourceEvent(QueryInfo info);
        //public event AttachmentNeedDataSourceEvent AttachmentNeedDataSourceEventHandler;

        public delegate void AttachmentDeleteCommmandEvent(int attachID);
        public event AttachmentDeleteCommmandEvent AttachmentDeleteCommmandEventHandler;

        public delegate void AttachmentInsertCommmandEvent(AttachmentPresentation viewItem);
        public event AttachmentInsertCommmandEvent AttachmentInsertCommmandEventHandler;

        protected int CurrentID
        {
            get
            {
                return (this.Page as BaseStudentDetailPage).CurrentID;
            }
        }

        private int MaxDisplayOrder
        {
            get
            {
                if (this.ViewState["MaxDisplayOrder"] == null) return 0;
                return (int)this.ViewState["MaxDisplayOrder"];
            }
            set
            {
                this.ViewState["MaxDisplayOrder"] = value;
            }
        }

        protected void radGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            var radGrid = source as RadGrid;
            radGrid.CurrentPageIndex = e.NewPageIndex;
        }

        protected void UploadLabelControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {            
            if (AttachmentInsertCommmandEventHandler != null)
            {
                MaxDisplayOrder++;
                AttachmentInsertCommmandEventHandler(new AttachmentPresentation
                {
                    Path = fileItem.FilePath,
                    FileLabel = UploadLabelControl.FileLabel,
                    IsMain = false,
                    DisplayOrder = MaxDisplayOrder,
                    DocumentType = fileItem.DocumentType,
                    ID = (int)DateTime.Now.Ticks,
                    FileContent = fileItem.FileContent,
                    FileName = fileItem.FileName
                });
            }
            radAttachment.Rebind();
        }

        protected void radGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                var attachID = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
                if (AttachmentDeleteCommmandEventHandler != null)
                {
                    AttachmentDeleteCommmandEventHandler(attachID);
                }
            }
        }

        protected override void InitLoadedData()
        {
            base.InitLoadedData();
            this.UploadLabelControl.FinishUploadingImageEventHandler += new BaseUploadControl.FinishUploadingImageEvent(UploadLabelControl_FinishUploadingImageEventHandler);
        }

        public void LoadData(IList<AttachmentPresentation> list)
        {
            var displayIndex = 1;
            this.radAttachment.DataSource = list.Select(it => new
                {
                    it.DisplayOrder,
                    it.DocumentType,
                    it.FileContent,
                    it.FileLabel,
                    it.FileName,
                    it.ID,
                    it.IsMain,
                    it.Path,
                    Index = (displayIndex++),
                    ImagePath = FileHelper.GetImagePathByDocumentType(it.DocumentType, it.ThumbPath)
                }).ToList();
            this.radAttachment.MasterTableView.AllowCustomPaging = false;
            this.radAttachment.MasterTableView.AllowPaging = false;
            this.radAttachment.DataBind();


            if (list.Any())
            {
                MaxDisplayOrder = list.Max(it => it.DisplayOrder);
            }
        }

        protected void radAttachment_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                e.Item.FindControl("btnDelete").Visible = Enable;
            }
        }
    }
}