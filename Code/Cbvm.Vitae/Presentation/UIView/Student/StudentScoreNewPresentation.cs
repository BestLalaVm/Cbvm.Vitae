using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentScoreNewPresentation : BasePresentation
    {
        public int Id
        {
            get;
            set;
        }

        public string XN
        {
            get;
            set;
        }

        public string XQ
        {
            get;
            set;
        }

        public string CJ
        {
            get;
            set;
        }

        public string XF
        {
            get;
            set;
        }

        public string KCMC
        {
            get;
            set;
        }
    }
}
