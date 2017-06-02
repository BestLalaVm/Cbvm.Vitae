using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Family
{
    [Serializable]
    public class FamilyPresentation : BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string NameZh
        {
            get; set;
        }

        public SexType Sex
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public bool IsOnline
        {
            get; set;
        }

        public string Relationship
        {
            get; set;
        }

        public string StudentNum
        {
            get;
            set;
        }

        public string UserName
        {
            get; set;
        }
        public string AboutMe
        {
            get;
            set;
        }
        public string Interested
        {
            get;
            set;
        }
        public string NameEn
        {
            get;
            set;
        }

        public string Telephone
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }

        public string Photo
        {
            get;
            set;
        }

        public string ThumbPath
        {
            get; set;
        }

        public string StudentIDentityNum
        {
            get; set;
        }

        public SexType StudentSex
        {
            get; set;
        }

        public string StudentSexLabel
        {
            get
            {
                return EnumHelper.GetEnumDescription(StudentSex);
            }
        }

        public string StudentTelephone
        {
            get; set;
        }

        public string StudentThumbPath
        {
            get; set;
        }

        public string StudentNameZh
        {
            get;
            set;
        }

        public string StudentNameEn
        {
            get;
            set;
        }
    }
}
