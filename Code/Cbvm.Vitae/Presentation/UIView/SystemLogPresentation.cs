using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView
{
    [Serializable]
    public class SystemLogPresentation:BasePresentation
    {
        public string Name
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public string IPAddress
        {
            get; set;
        }

        public DateTime LogTime
        {
            get; set;
        }

        public LogType LogType
        {
            get; set;
        }
    }
}
