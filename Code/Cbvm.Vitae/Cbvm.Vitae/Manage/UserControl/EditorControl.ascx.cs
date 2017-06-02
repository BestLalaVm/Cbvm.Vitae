using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class EditorControl : BaseUserControl
    {
        private bool HasInialize
        {
            get
            {
                if (this.ViewState["HasInialize"] == null) return false;

                return (bool)this.ViewState["HasInialize"];
            }
            set
            {
                this.ViewState["HasInialize"] = value;
            }
        }

        protected override void InitData()
        {
            string path = string.Format("~/Upload/{0}/{1}/Document/Image/", UserType.ToString(), MemberID);
            EnsureDirectory(path);
            this.radEditor1.ImageManager.ViewPaths =
                this.radEditor1.ImageManager.DeletePaths =
                this.radEditor1.ImageManager.UploadPaths = new string[] { path };

            string documentPath = string.Format("~/Upload/{0}/{1}/Document/", UserType.ToString(), MemberID);
            EnsureDirectory(documentPath);
            this.radEditor1.DocumentManager.ViewPaths =
                this.radEditor1.DocumentManager.DeletePaths =
                this.radEditor1.DocumentManager.UploadPaths = new string[] { documentPath };

            string mediaPath = string.Format("~/Upload/{0}/{1}/Document/Media/", UserType.ToString(), MemberID);
            EnsureDirectory(mediaPath);
            this.radEditor1.MediaManager.ViewPaths =
            this.radEditor1.MediaManager.DeletePaths =
            this.radEditor1.MediaManager.UploadPaths = new string[] { mediaPath };

            string flashPath = string.Format("~/Upload/{0}/{1}/Document/Flash/", UserType.ToString(), MemberID);
            EnsureDirectory(flashPath);
            this.radEditor1.FlashManager.ViewPaths =
              this.radEditor1.FlashManager.DeletePaths =
              this.radEditor1.FlashManager.UploadPaths = new string[] { flashPath };
            

            base.InitData();

            HasInialize = true;
        }

        public int EditorWidth
        {
            get
            {
                return (int)this.radEditor1.Width.Value;
            }
            set
            {
                this.radEditor1.Width = value;
            }
        }

        public int EditorHeight
        {
            get
            {
                return (int)this.radEditor1.Height.Value;
            }
            set
            {
                this.radEditor1.Height = value;
            }
        }

        public bool EditorEnabled
        {
            set
            {
                this.radEditor1.Enabled = value;
            }
        }

        public void LoadData(string content)
        {
            this.radEditor1.Content = content;
        }

        public string SaveData()
        {
            return this.radEditor1.Content;
        }

        protected override void InitLoadedData()
        {
            if (!HasInialize)
            {
                InitData();
            }

            base.InitLoadedData();
        }

        private void EnsureDirectory(string path)
        {
            var dictionary = new System.IO.DirectoryInfo(Server.MapPath(path));
            if (!dictionary.Exists)
            {
                dictionary.Create();
            }
        }
    }
}