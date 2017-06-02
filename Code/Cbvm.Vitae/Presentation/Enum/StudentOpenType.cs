using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum StudentOpenType:int
    {
        [Description(Description = "学生")] Student = 0,
        [Description(Description = "教师")] Teacher = 1,
        [Description(Description = "企业")] Enterprise = 2,
        [Description(Description = "自己")] Owner = 4,
        [Description(Description = "匿名用户")] Guest = 8,
        [Description(Description = "无", Exclude = true)] None = 16
    }
}
