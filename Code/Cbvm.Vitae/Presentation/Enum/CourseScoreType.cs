using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum CourseScoreType
    {
        [Description(Description = "正常考试")]
        Normal = 0,
        [Description(Description = "补考")]
        Supplementary = 1
    }
}
