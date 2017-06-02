using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.UIView;

namespace Presentation.Criteria
{
    public class BaseCriteria
    {
        public int PageSize
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public bool NeedPaging
        {
            get;
            set;
        }

        public LoginUserPresentation CurrentUser
        {
            get;
            set;
        }
    }
}
