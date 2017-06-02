using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.UIView;
using Presentation.Criteria.Base;
using Presentation.UIView.Base;

namespace Business.Interface.College
{
    public interface ICollegeService : IBaseService<string,CollegePresentation, CollegeCriteria>
    {

    }
}
