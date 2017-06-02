using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Enterprise.View
{
    public class EnterpriseRecruitBatchStatisticsPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public int RequestNum
        {
            get; set;
        }

        public int ProcessLessNum
        {
            get; set;
        }

        public int InterviewNum
        {
            get; set;
        }

        public int PassedNum
        {
            get; set;
        }

        public int DenyNum
        {
            get; set;
        }
    }
}
