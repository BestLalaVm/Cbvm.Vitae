using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView
{
    [Serializable]
    public class CommentPresentation:BasePresentation
    {
        public string UserName
        {
            get;
            set;
        }

        public UserType UserType
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public int ReferenceID
        {
            get;
            set;
        }

        public DateTime CommentTime
        {
            get;
            set;
        }
        public int? ParentID
        {
            get;
            set;
        }
    }
}
