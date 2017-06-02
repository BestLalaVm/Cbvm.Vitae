using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Student
{
    public class StudentPhotoCriteria:BaseStudentCriteria
    {
        public int? Id
        {
            get; set;
        }

        public int? DictoryId
        {
            get; set;
        }
    }
}
