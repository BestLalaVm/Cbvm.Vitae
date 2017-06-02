using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.Criteria.Student
{
    public class StudentProfessionalCriteria:BaseStudentCriteria
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
            get;
            set;
        }

        public bool IncludeRelativeData
        {
            get;
            set;
        }

        public string VerfyStatus
        {
            get;
            set;
        }

        public ProfessionalType Type { get; set; }

        public string TeacherNum
        {
            get;
            set;
        }
    }
}
