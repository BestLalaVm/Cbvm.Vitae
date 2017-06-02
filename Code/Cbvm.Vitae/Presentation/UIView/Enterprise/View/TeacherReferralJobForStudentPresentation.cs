using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Enterprise.View
{
    public class TeacherReferralJobForStudentPresentation:BasePresentation
    {
        public string Code
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int Num
        {
            get; set;
        }

        public string SexRequired
        {
            get; set;
        }

        public string Education
        {
            get; set;
        }

        public string WorkPlace
        {
            get; set;
        }

        public string SalaryScope
        {
            get; set;
        }

        public DateTime? StartTime
        {
            get; set;
        }

        public DateTime? EndTime
        {
            get; set;
        }

        public DateTime ReferralTime
        {
            get; set;
        }

        public string EnterpriseName
        {
            get; set;
        }

        public string Referralers
        {
            get; set;
        }

        public string ActualReferralers
        {
            get; set;
        }

        public string RecruitTypeStatus
        {
            get; set;
        }
    }
}
