using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView
{
    public class ContentPresentation:BasePresentation
    {
        public string Identity
        {
            get; set;
        }

        public string ReferenceCode
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTime Time
        {
            get; set;
        }
    }
}
