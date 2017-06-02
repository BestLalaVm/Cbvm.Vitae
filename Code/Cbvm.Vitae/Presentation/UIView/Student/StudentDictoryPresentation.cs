using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentDictoryPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string StudentNum
        {
            get; set;
        }

        public StudentOpenType OpenType
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public string ThumbPath
        {
            get; set;
        }

        public int PhotoCount
        {
            get; set;
        }
    }
}
