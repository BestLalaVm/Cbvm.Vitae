using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentDailyEssayPresentation:BasePresentation
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

        public string StudentNum
        {
            get; set;
        }

        public bool IsOnline
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }


        private IList<CommentPresentation> _CommentPresentations;

        public IList<CommentPresentation> CommentPresentations
        {
            get
            {
                if (_CommentPresentations == null)
                {
                    _CommentPresentations = new List<CommentPresentation>();
                }
                return _CommentPresentations;
            }
            set
            {
                _CommentPresentations = value;
            }
        }
    }
}
