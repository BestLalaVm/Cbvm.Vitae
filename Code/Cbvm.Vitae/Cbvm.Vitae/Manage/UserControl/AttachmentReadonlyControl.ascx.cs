using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.UIView;
using Telerik.Web.UI;

using Presentation.Enum;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class AttachmentReadonlyControl : BaseUserControl
    {
        //public delegate IList<AttachmentPresentation> AttachmentNeedDataSourceEvent(QueryInfo info);
        //public event AttachmentNeedDataSourceEvent AttachmentNeedDataSourceEventHandler;

        public delegate void AttachmentDeleteCommmandEvent(int attachID);
        public event AttachmentDeleteCommmandEvent AttachmentDeleteCommmandEventHandler;

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

        protected void radGrid_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            var radGrid = source as RadGrid;
            radGrid.CurrentPageIndex = e.NewPageIndex;
        }

        protected void radGrid_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
            radAttachment.MasterTableView.VirtualItemCount = list.Count();
            this.radAttachment.DataBind();


        }
    }
}