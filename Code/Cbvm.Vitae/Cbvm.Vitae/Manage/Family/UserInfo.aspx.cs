using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.Enum;

namespace Cbvm.Vitae.Manage.Family
{
    public partial class UserInfo : BaseFamilyPage
    {
        public override AttachmentType InitAttachmentType()
        {
            return AttachmentType.BaseInfo;
        }
    }
}