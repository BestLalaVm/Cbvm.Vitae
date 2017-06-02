using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum EnterpriseEmailType
    {
        [Description(Description = "请求职位")] Request = 0,
        [Description(Description = "简历查阅")] View = 1,
        [Description(Description = "邀请面试")] Invited = 2,
        [Description(Description = "面试通过")] Passed = 3,
        [Description(Description = "面试失败")] NoPassed = 4,
        [Description(Description = "同意推荐学生")] AgreeToReferral = 5,
        [Description(Description = "推荐学生")] ReferraledToEnterprise = 6
    }
}
