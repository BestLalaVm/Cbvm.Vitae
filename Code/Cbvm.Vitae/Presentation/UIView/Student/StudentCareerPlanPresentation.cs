using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    public class StudentCareerPlanPresentation : BasePresentation
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string StudentNum { get; set; }

        public bool IsOnline { get; set; }

        public bool IsImplemented { get; set; }
    }
}
