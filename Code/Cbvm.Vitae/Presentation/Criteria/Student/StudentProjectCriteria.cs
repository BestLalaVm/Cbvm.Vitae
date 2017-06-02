using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Student
{
    public class StudentProjectCriteria:BaseStudentCriteria
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTime? DateFrom
        {
            get; set;
        }

        public DateTime? DateTo
        {
            get; set;
        }

        public string VerfyStatus
        {
            get; set;
        }

        public bool IncludeRelativeData
        {
            get; set;
        }
    }
}
