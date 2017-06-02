using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;
using Presentation.UIView;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentPhotoCommentPresentation : BasePresentation
    {
        public string Comment
        {
            get; set;
        }

        public DateTime CommentTime
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public UserType UserType
        {
            get; set;
        }

        public int PhotoId
        {
            get; set;
        }

        public int DictoryId
        {
            get; set;
        }
    }
}
