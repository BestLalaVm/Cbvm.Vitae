using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum StudentSearchType
    {
        [Description(Description = "所有学生")]
        All = 0,
        [Description(Description = "点击率最高")]
        TopClick = 1,
        [Description(Description = "评价最好")]
        TopEvaluate = 2
    }
}
