using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria
{
    public class CommentCriteria:BaseCriteria
    {
        public int ReferenceId
        {
            get; set;
        }

        public bool IsFrontRequest
        {
            get; set;
        }
    }
}
