using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.University
{
    [Serializable]
    public class UniversityMessagePresentation : BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public bool IsOnline
        {
            get; set;
        }

        public bool IsImportant
        {
            get;
            set;
        }

        public string UniversityCode
        {
            get; set;
        }

        public DateTime LastUpdateTime
        {
            get; set;
        }

        public DateTime? IsTopTime
        {
            get;
            set;
        }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public bool IsTop
        {
            get;
            set;
        }

        public string Link
        {
            get
            {
                return String.Format(WebLibrary.WebUiConfig.UniversityMessageLink, Id, CategoryCode).Replace("|", "&");
            }
        }
    }

    public class MessageUiPresentation : BasePresentation
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public DateTime LastUpdateTime
        {
            get;
            set;
        }
    }
}
