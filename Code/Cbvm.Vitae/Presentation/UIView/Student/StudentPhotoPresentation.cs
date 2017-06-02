using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentPhotoPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string SmallPath
        {
            get; set;
        }

        public string ThumbPath
        {
            get; set;
        }

        public string PhotoPath
        {
            get; set;
        }

        public int DictoryId
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public bool IsDictoryPhoto
        {
            get; set;
        }

        public IList<CommentPresentation> CommentPresentations
        {
            get;
            set;
        }
    }
}
