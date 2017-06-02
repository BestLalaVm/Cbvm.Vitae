using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cbvm.Vitae.Front
{
    public interface IShortWidget
    {
        int MaxContentLength { get; set; }

        bool EnableShortContent { get; set; }
    }
}
