using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView
{
    [Serializable]
    public class AttachmentPresentation
    {
        public int ID
        {
            get;
            set;
        }

        public DocumentType DocumentType
        {
            get;
            set;
        }

        public string FileLabel
        {
            get;
            set;
        }

        public int DisplayOrder
        {
            get;
            set;
        }

        public bool IsMain
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public Stream FileContent
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string ThumbPath
        {
            get;
            set;
        }

        public bool IsNew
        {
            get;
            set;
        }
    }
}
