using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.UIView.College
{
    public class AssessmentPresentation:BasePresentation
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LastUpdator { get; set; }

        public AssessType AssessType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string StudentNum { get; set; }

        public string StudentName { get; set; }

        public string CollegeCode
        {
            get;
            set;
        }
    }
}
