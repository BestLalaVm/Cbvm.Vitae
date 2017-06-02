using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria;

namespace Business.Interface
{
    public interface IBaseServiceFactory<TCriteria> where TCriteria : BaseCriteria, new()
    {

    }
}
