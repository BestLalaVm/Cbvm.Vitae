using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Attribute;

namespace Presentation.Enum
{
    public enum SystemEmailType
    {
        InviteAudition = 0,
        [Description(Description = "回复")] ReplyEmail = 1,

        ResetPassword = 2
    }
}
