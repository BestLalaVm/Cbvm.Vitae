using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum RequestQueueType
    {
        [Description(Description = "邀请推荐")] AskForReferral = 0,
        [Description(Description = "自推荐")] Referraled = 1
    }
}
