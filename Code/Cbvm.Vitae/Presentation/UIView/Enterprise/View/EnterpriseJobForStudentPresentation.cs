using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise.View
{
    public class EnterpriseJobForStudentPresentation : BasePresentation
    {
        public string Code
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string EnterpriseName
        {
            get; set;
        }

        public int Num
        {
            get; set;
        }

        public string Education
        {
            get;
            set;
        }

        public string WorkPlace
        {
            get;
            set;
        }

        public string SalaryScope
        {
            get;
            set;
        }

        public DateTime? StartTime
        {
            get;
            set;
        }

        public DateTime? EndTime
        {
            get;
            set;
        }

        public string SexRequired
        {
            get; set;
        }

        public string JobRequestDescription
        {
            get; set;
        }
      }
}
