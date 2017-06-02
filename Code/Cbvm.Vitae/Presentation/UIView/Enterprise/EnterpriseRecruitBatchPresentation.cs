using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView.Enterprise
{
    [Serializable]
    public class EnterpriseRecruitBatchPresentation:BasePresentation
    {
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string EnterpriseCode
        {
            get; set;
        }

        private IList<EnterpriseJobRequestPresentation> _JobRequestPresentations;
        public IList<EnterpriseJobRequestPresentation> JobRequestPresentations
        {
            get
            {
                if (_JobRequestPresentations == null)
                {
                    _JobRequestPresentations=new List<EnterpriseJobRequestPresentation>();
                }
                return _JobRequestPresentations;
            }
            set
            {
                _JobRequestPresentations = value;
            }
        }
    }
}
