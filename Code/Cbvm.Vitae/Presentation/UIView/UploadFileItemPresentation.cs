using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView
{
    [Serializable]
    public class UploadFileItemPresentation
    {
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

        public string SmallPath
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
