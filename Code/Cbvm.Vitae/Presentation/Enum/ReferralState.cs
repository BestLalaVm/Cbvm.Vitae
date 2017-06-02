using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum ReferralState
    {
        [Description(Description = "等待推荐")] Pending = 0,
        [Description(Description = "已推荐")] Passed = 1,
        [Description(Description = "已拒绝")] Deny = 2,
        [Description(Description = "已转递企业")] TimeOut = 3
    }
}
