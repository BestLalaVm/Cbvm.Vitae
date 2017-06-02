using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum SexType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description(Description = "男")]
        male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description(Description="女")]
        female = 0
    }
}
