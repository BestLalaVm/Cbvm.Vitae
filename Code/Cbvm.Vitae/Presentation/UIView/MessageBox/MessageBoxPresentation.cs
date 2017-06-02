using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.MessageBox
{
    [Serializable]
    public class MessageBoxPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Subject
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public string ReceiverKey
        {
            get; set;
        }

        public UserType ReceiverType
        {
            get; set;
        }

        public string SenderKey
        {
            get; set;
        }

        public UserType SenderType
        {
            get; set;
        }

        public bool IsReaded
        {
            get; set;
        }
    }
}
