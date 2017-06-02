using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Criteria.Student
{
    public class StudentDictoryCriteria:BaseStudentCriteria
    {
        public int Id
        {
            get; set;
        }

        public bool IncludePhotoCount
        {
            get; set;
        }
    }
}
