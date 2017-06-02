using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Student
{
    public class StudentDailyEssayCriteria : BaseStudentCriteria
    {
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public DateTime? DateFrom
        {
            get; set;
        }

        public DateTime? DateTo
        {
            get;
            set;
        }

        public bool IncludeRelativeData
        {
            get;
            set;
        }
    }
}
