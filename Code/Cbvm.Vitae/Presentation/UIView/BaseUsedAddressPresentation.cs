using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView
{
    [Serializable]
    public class BaseUsedAddressPresentation:BasePresentation
    {
        private string _ProvinceCode;
        public string ProvinceCode
        {
            get
            {
                return String.IsNullOrEmpty(_ProvinceCode) ? null : _ProvinceCode;
            }
            set {
                _ProvinceCode = value;
            }
        }

        public string ProvinceName { get; set; }

        private string _CityCode;
        public string CityCode
        {
            get
            {
                return String.IsNullOrEmpty(_CityCode) ? null : _CityCode;
            }
            set
            {
                _CityCode = value;
            }
        }

        public string CityName { get; set; }

        private string _DistrictCode;
        public string DistrictCode
        {
            get
            {
                return String.IsNullOrEmpty(_DistrictCode) ? null : _DistrictCode;
            }
            set
            {
                _DistrictCode = value;
            }
        }

        public string DistrictName { get; set; }

        private string _StreetCode;
        public string StreetCode
        {
            get
            {
                return String.IsNullOrEmpty(_StreetCode) ? null : _StreetCode;
            }
            set
            {
                _StreetCode = value;
            }
        }

        public string StreetName { get; set; }

        //public string Address
        //{
        //    get
        //    {
        //        return String.Format("{0}{1}{2}{3}", ProvinceName, CityName, DistrictName, StreetName);
        //    }
        //}

        public string Address { get; set; }
    }
}
