using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.Criteria.Student
{
    public class StudentFontAdvanceCriteria:BaseCriteria
    {
        public string KeyWord { get; set; }

        public string MarjorCode { get; set; }

        public SexType? SexType { get; set; }

        public string Marjor { get; set; }
    }
}
