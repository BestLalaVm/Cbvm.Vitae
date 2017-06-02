using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.UIView.Base
{
     [Serializable]
    public class CollegeAdminPresentation : BasePresentation
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string CollegeCode { get; set; }

        public string CollegeName { get; set; }
    }
}
