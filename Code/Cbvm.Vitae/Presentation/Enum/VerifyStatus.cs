using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    [Description]
    public enum VerifyStatus
    {
        [Description(Description = "等待审核")]
        WaitAudited = 0,
        [Description(Description = "审核通过")]
        Passed = 2,
        [Description(Description = "审核未通过")]
        UnPassed = 1,
    }
}
