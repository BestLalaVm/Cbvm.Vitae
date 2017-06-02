using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Cache;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterpriseJobPresentation : BaseUsedAddressPresentation
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

        public string AgeScope
        {
            get;
            set;
        }

        //public string Address
        //{
        //    get;
        //    set;
        //}

        public string ContactName
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Telephone
        {
            get;
            set;
        }

        public string RecruitmentTargets
        {
            get;
            set;
        }

        public string Nature
        {
            get;
            set;
        }

        public bool IsOnline
        {
            get;
            set;
        }

        public int Sex
        {
            get;
            set;
        }

        public string EnterpriseCode
        {
            get; set;
        }

        public string DepartName
        {
            get; set;
        }

        public int DisplayOrder
        {
            get; set;
        }

        public string Professional
        {
            get; set;
        }

        public string Tax
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public EnterprisePresentation Enterprise
        {
            get;
            set;
        }

        private List<EnterpriseJobRequestPresentation> _JobRequests;
        public List<EnterpriseJobRequestPresentation> JobRequests
        {
            get
            {
                if (_JobRequests == null)
                {
                    _JobRequests = new List<EnterpriseJobRequestPresentation>();
                }
                return _JobRequests;
            }
            set
            {
                _JobRequests = value;
            }
        }
    }
}
