using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum UserType
    {
        [Description(Description = "学生")] Student = 0,
        [Description(Description = "教师")] Teacher = 1,
        [Description(Description = "企业")] Enterprise = 2,
        [Description(Description = "系管理员")] DepartAdmin = 3,
        [Description(Description = "管理员")] Admin = 4,
        [Description(Description = "家人")] Family = 5,
        [Description(Description = "游客")] Guest = 6,
        [Description(Description = "维护管理员")]
        Supper = 7,
        [Description(Description = "学院管理员")]
        College = 8,
        [Description(Description = "学校管理员")]
        University = 9,
    }
}
