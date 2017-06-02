using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebLibrary;

namespace Presentation.UIView.University
{
    [Serializable]
    public class JobFareViewPresentation:BasePresentation
    {
        public int ID { get; set; }

        public string UniversityCode { get; set; }

        public string Name { get; set; }

        public string NameLink
        {
            get
            {
                return String.Format(WebUiConfig.FareManageLink, ID);
            }
        }

        public string Address { get; set; }

        public DateTime BeginTime { get; set; }

        public string BeginTimeValue
        {
            get
            {
                return BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public DateTime EndTime { get; set; }

        public bool IsOnline { get; set; }

        public IList<JobFareEnterprisePresentation> Enterprises { get; set; }

        public IList<JobFareCollegePresentation> Colleges { get; set; }
    }

    [Serializable]
    public class JobFareEnterprisePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }

    [Serializable]
    public class JobFareCollegePresentation
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }

    [Serializable]
    public class JobFarePresentation : JobFareViewPresentation
    {
        public string Description { get; set; }
    }
}
