using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterprisePresentation : BaseUsedAddressPresentation
    {
        public int Id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        //public string Address
        //{
        //    get;
        //    set;
        //}

        public string LicenseNoImage
        {
            get;
            set;
        }

        public string OrganizationCodeImage
        {
            get;
            set;
        }

        public string ContactName
        {
            get;
            set;
        }

        public string ContactPhone
        {
            get;
            set;
        }

        public bool IsOnline
        {
            get;
            set;
        }

        public string ScopeCode
        {
            get;
            set;
        }

        public string EnterpriseTypeCode
        {
            get;
            set;
        }

        public string RegionCode
        {
            get;
            set;
        }

        public string IndustryCode
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string Corporation
        {
            get;
            set;
        }

        public string WebSite
        {
            get;
            set;
        }

        public VerifyStatus VerifyStatus
        {
            get;
            set;
        }

        public string Photo { get; set; }

        public string ThumbPath { get; set; }

        public bool IsFamous { get; set; }

        public IList<EnterpriseJobPresentation> JobPresentations
        {
            get; set;
        }
    }

    public class EnterpriseTopPresentation : BasePresentation
    {
        public string Code
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string WebSite
        {
            get; set;
        }
    }
}
