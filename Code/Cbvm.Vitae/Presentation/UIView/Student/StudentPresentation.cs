using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Student
{
    [Serializable]
    public class StudentPresentation : BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string StudentNum
        {
            get;
            set;
        }

        public string NameZh
        {
            get;
            set;
        }

        public string NameEn
        {
            get;
            set;
        }

        public string IDentityNum
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Class
        {
            get;
            set;
        }

        public DateTime? Birthday
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Email
        {
            get; set;
        }

        public bool IsMarried
        {
            get; set;
        }

        public bool IsOnline
        {
            get; set;
        }

        public string MarjorCode
        {
            get; set;
        }

        public string NativePlace
        {
            get; set;
        }

        public string Period
        {
            get; set;
        }

        public SexType Sex
        {
            get; set;
        }

        public string Photo
        {
            get; set;
        }

        public string Telephone
        {
            get; set;
        }

        public string WebSite
        {
            get; set;
        }

        public PoliticsType Politics
        {
            get; set;
        }

        public string Tall
        {
            get; set;
        }

        public string ThumbPath
        {
            get; set;
        }

        public decimal? Weight
        {
            get; set;
        }

        public StudentJobExpectPresentation JobExpect
        {
            get; set;
        }

        public string Name
        {
            get
            {
                return !String.IsNullOrEmpty(this.NameEn)
                    ? String.Format("{0} - {1}", this.NameZh, this.NameEn)
                    : this.NameZh;
            }
        }

        public string CollegeName
        {
            get;
            set;
        }
    }
}
