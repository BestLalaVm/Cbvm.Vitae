using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum RecruitType
    {
        [Description(Description = "请求中")] Request = 0,
        [Description(Description = "查看")] View = 1,
        [Description(Description = "邀请")] Invited = 2,
        [Description(Description = "面试中")] Interviewing = 3,
        [Description(Description = "通过")] Passed = 4,
        [Description(Description = "没通过")] NoPassed = 5
    }
}
