using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView
{
    [Serializable]
    public class PublishActivityPresentation:BasePresentation
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

        public string Address
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

        public string Publisher
        {
            get; set;
        }

        public UserType PublisherType
        {
            get; set;
        }

        public string ActivityType
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public PublishAppliedStatus Status
        {
            get; set;
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

        public bool IsReferenced
        {
            get; set;
        }
    }
}
