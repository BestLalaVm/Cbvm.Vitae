using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.MsgBox
{
    [Serializable]
    public class MessageBoxViewPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Subject { get; set; }

        public DateTime Time { get; set; }

        public string SenderKey { get; set; }

        public UserType SenderType { get; set; }

        public string ReceiverKey { get; set; }

        public UserType ReceiverType { get; set; }

        public int NewReplyCount { get; set; }

        public bool IsReaded { get; set; }

        public string SenderName
        {
            get;
            set;
        }

        public string SenderTypeLabel
        {
            get
            {
                return EnumHelper.GetEnumDescription(SenderType);
            }
        }
    }
}
