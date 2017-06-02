using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    public class StudentVoluntaryServicePresentation : BasePresentation
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsOnline { get; set; }

        public string StudentNum { get; set; }

        public VerifyStatus Status { get; set; }

        public string VerifyComment { get; set; }

        public string MarjorName { get; set; }

        public string StudentName { get; set; }
    }
}
