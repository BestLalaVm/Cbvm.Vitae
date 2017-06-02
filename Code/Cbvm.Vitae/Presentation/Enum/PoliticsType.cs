using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum PoliticsType
    {
        /// <summary>
        /// 团员
        /// </summary>
        [Description(Description = "团员")]
        League = 0,

        /// <summary>
        /// 党员
        /// </summary>
        [Description(Description = "党员")]
        Communist = 1,

        /// <summary>
        /// 群众
        /// </summary>
        [Description(Description = "群众")]
        Mass = 2,

        /// <summary>
        /// 其他
        /// </summary>
        [Description(Description = "其他")]
        Other = 3
    }
}
