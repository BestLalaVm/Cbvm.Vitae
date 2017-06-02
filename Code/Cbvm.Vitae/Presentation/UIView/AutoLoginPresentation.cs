using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.UIView
{
    public class AutoLoginPresentation
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public UserType UserType { get; set; }

        public LoginUserPresentation Detail { get; set; }
    }
}
