using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentCareerDzWorkPlacePresentation
    {
        public int ID { get; set; }

        private string _ProvinceCode;
        public string ProvinceCode
        {
            get
            {
                return String.IsNullOrEmpty(_ProvinceCode) ? null : _ProvinceCode;
            }
            set
            {
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
    }
}
