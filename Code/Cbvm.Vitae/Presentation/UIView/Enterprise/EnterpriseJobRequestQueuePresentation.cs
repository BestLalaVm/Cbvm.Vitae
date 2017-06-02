using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterpriseJobRequestQueuePresentation : BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string EnterpriseName
        {
            get; set;
        }

        public string JobName
        {
            get; set;
        }

        public SexType Sex
        {
            get; set;
        }

        public string Education
        {
            get; set;
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

        public string JobCode
        {
            get;
            set;
        }

        public ReferralState ReferralState
        {
            get; set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public string Note
        {
            get; set;
        }

        public string Class
        {
            get; set;
        }

        public string StudentNum
        {
            get;
            set;
        }

        public string SudentNameZh
        {
            get;
            set;
        }

        public string StudentNameEn
        {
            get;
            set;
        }

        public string MarjorCode
        {
            get; set;
        }

        public IList<EnterpriseJobReferralerPresentation> JobReferralers
        {
            get; set;
        }
    }
}
