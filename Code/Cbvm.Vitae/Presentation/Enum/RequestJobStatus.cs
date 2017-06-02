using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum RequestJobStatus
    {
        [Description(Description = "请求")]
        Initial = 0,
        [Description(Description = "查阅")]
        View = 1,
        [Description(Description = "邀请")]
        Invite = 2,
        [Description(Description = "请求")]
        Pass = 3,
        [Description(Description = "拒绝")]
        Deny = 4
    }
}
