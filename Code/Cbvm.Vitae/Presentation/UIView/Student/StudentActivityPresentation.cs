using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria.Student;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentActivityPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string ActivityType
        {
            get; set;
        }

        public string Address
        {
            get; set;
        }
        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime BeginTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public bool IsOnline
        {
            get;
            set;
        }

        public string TeacherNum
        {
            get;
            set;
        }

        public string TeacherName
        {
            get;
            set;
        }

        public string EvaluateFromTeacher
        {
            get;
            set;
        }

        public string VerifyStatusReason
        {
            get; set;
        }

        public VerifyStatus VerfyStatus
        {
            get; set;
        }

        public int? ReferenceID
        {
            get; set;
        }

        public string StudentNum
        {
            get; set;
        }

        public string StudentNameZh
        {
            get;
            set;
        }

        public string StudentNameEn
        {
            get;
            set;
        }

        private IList<AttachmentPresentation> _AttachmentPresentations;
        public IList<AttachmentPresentation> AttachmentPresentations
        {
            get
            {
                if (_AttachmentPresentations == null)
                {
                    _AttachmentPresentations = new List<AttachmentPresentation>();
                }
                return _AttachmentPresentations;
            }
            set
            {
                _AttachmentPresentations = value;
            }
        }

        private IList<CommentPresentation> _CommentPresentations;
        public IList<CommentPresentation> CommentPresentations
        {
            get
            {
                if (_CommentPresentations == null)
                {
                    _CommentPresentations=new List<CommentPresentation>();
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
