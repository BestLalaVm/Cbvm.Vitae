using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterpriseJobRequestPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Company { get; set; }

        public string JobCode { get; set; }

        public string JobName { get; set; }

        public string JobDescription { get; set; }

        public string JobStage { get; set; }

        public DateTime RequestTime { get; set; }

        public string ContactName { get; set; }

        public string StageDescription { get; set; }

        public string StudentNum { get; set; }

        public string StudentName
        {
            get; set;
        }

        public string Telephone
        {
            get; set;
        }

        public SexType Sex
        {
            get; set;
        }

        public string ThumbPath
        {
            get; set;
        }

        public RecruitType RecruitType
        {
            get; set;
        }

        public int JobNum
        {
            get; set;
        }

        public string RequestStatus
        {
            get; set;
        }

        public string ReferralerNames
        {
            get
            {
                return String.Join(",", JobReferralers.Select(ix => ix.NameZh));
            }
        }

        public string SexLabel
        {
            get
            {
                return EnumHelper.GetEnumDescription(Sex);
            }
        }

        public JobRequestRecruitStagePresentation JobRequestRecruitStagePresentation
        {
            get; set;
        }

        public IList<EnterpriseJobReferralerPresentation> JobReferralers
        {
            get; set;
        }
    }
}
