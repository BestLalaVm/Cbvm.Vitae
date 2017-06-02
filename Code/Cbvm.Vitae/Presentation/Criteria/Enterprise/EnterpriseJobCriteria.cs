using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.Criteria.Enterprise
{
    public class EnterpriseJobCriteria:BaseCriteria
    {
        public string EnterpriseName
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string WorkPlace
        {
            get; set;
        }

        /// <summary>
        /// Requester
        /// </summary>
        public string StudentNum
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string EnterpriseCode
        {
            get; set;
        }

        public bool IsForStudentPresentation
        {
            get; set;
        }
    }
}
