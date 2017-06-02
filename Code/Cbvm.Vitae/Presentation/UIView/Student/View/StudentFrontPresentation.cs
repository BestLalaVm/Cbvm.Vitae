using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Student.View
{
    [Serializable]
    public class StudentFrontPresentation:StudentPresentation
    {
        public string AboutMe
        {
            get;
            set;
        }

        public string Activity
        {
            get;
            set;
        }

        public string Book
        {
            get;
            set;
        }

        public string Interested
        {
            get;
            set;
        }

        public string Movie
        {
            get;
            set;
        }

        public string Music
        {
            get;
            set;
        }

        public string Program
        {
            get;
            set;
        }

        public int VisitedCount
        {
            get;
            set;
        }

        public string MarjorName
        {
            get;
            set;
        }
    }
}
