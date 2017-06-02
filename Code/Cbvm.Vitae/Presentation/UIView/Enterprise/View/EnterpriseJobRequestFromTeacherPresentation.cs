using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;
using LkHelper;

namespace Presentation.UIView.Enterprise.View
{
    public class EnterpriseJobRequestFromTeacherPresentation:BasePresentation
    {
        public int Id
        {
            get;
            set;
        }

        public string JobCode { get; set; }

        public string JobName { get; set; }

        public string JobDescription { get; set; }

        public string JobStage { get; set; }

        public int JobNum
        {
            get;
            set;
        }

        public DateTime RequestTime { get; set; }

        public string StudentNum { get; set; }

        public string StudentName
        {
            get;
            set;
        }

        public string StudentTelephone
        {
            get;
            set;
        }

        public SexType StudentSex
        {
            get;
            set;
        }

        public string RequestStatus
        {
            get; set;
        }

        public List<string> Referralers
        {
            get; set;
        }

        private DateTime? _AuthenticatedRequestTime;
        public DateTime? AuthenticatedRequestTime
        {
            get
            {
                if (_AuthenticatedRequestTime == DateTime.MinValue) return null;

                return _AuthenticatedRequestTime;
            }
            set
            {
                _AuthenticatedRequestTime = value;
            }
        }

        public bool IsAuthenticated { get; set; }

        public bool IsInviated { get; set; }

        public DateTime? InviatedDate { get; set; }

        public string InviatedDateValue
        {
            get
            {
                if (!InviatedDate.HasValue) return "";

                return InviatedDate.Value.ToCustomerDateString();
            }
        }
    }
}
