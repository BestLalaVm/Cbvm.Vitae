using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentJobExpectPresentation : BaseUsedAddressPresentation
    {
        public int OpenType
        {
            get;
            set;
        }

        public string JobAddress
        {
            get;
            set;
        }

        public string JobContent
        {
            get;
            set;
        }

        public string JobRequired
        {
            get;
            set;
        }

        public string JobSalary
        {
            get;
            set;
        }



    }
}
