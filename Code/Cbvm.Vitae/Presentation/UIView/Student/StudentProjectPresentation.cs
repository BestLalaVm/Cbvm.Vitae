using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentProjectPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Position
        {
            get; set;
        }

        public string PositionDescrition
        {
            get; set;
        }

        public string Evaluate
        {
            get; set;
        }

        public string TeamDescription
        {
            get; set;
        }

        public VerifyStatus VerfyStatus
        {
            get; set;
        }

        public DateTime BeginTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public bool IsOnline
        {
            get; set;
        }

        public string StudentNum
        {
            get; set;
        }

        public string TeacherNum
        {
            get; set;
        }

        public string TeacherName
        {
            get;
            set;
        }

        public string EvaluateFromTeacher
        {
            get; set;
        }

        public string VerifyStatusReason
        {
            get; set;
        }

        public int UsableLevel
        {
            get; set;
        }

        public int SkillLevel
        {
            get; set;
        }

        public int DreativeLevel
        {
            get; set;
        }

        public string StudentNameZh
        {
            get; set;
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
                    _AttachmentPresentations=new List<AttachmentPresentation>();
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
