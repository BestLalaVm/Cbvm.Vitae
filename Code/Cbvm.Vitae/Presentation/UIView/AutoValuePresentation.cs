using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView
{
    public class AutoValuePresentation
    {
        public int Index { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UserType UserType { get; set; }

        public string UserTypeLabel
        {
            get { return EnumHelper.GetEnumDescription(UserType); }
        }

        public string ThumbPath { get; set; }
    }
}
