using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.UIView;

namespace Presentation.UIView
{
    [Serializable]
    public class MailQueuePresentation:BasePresentation
    {
        public string Name
        {
            get; set;
        }

        public string Message
        {
            get;
            set;
        }

        public string Sender
        {
            get;
            set;
        }

        public string Receiver
        {
            get;
            set;
        }

        public string ReceiverName
        {
            get;
            set;
        }

        public string Cc
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public bool IsSended
        {
            get; set;
        }
    }
}
