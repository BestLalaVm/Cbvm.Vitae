using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView
{
    [Serializable]
    public class EntityCollection<TPresentation>:List<TPresentation> where TPresentation:BasePresentation,new()
    {
        public int TotalCount
        {
            get; set;
        }
    }
}
