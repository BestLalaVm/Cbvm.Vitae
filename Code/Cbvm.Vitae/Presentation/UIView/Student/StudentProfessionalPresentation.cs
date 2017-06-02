using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentProfessionalPresentation:BasePresentation
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public DateTime ObtainTime
        {
            get; set;
        }

        public DateTime LastUpdateTime
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
            get;
            set;
        }
        public VerifyStatus VerfyStatus
        {
            get;
            set;
        }

        public ProfessionalType Type{ get;set;}

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
    }
}
