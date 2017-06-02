using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attribute
{
    public class DescriptionAttribute : System.Attribute
    {
        public string Description
        {
            get;
            set;
        }

        public bool Exclude { get; set; }
    }
}
