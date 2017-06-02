using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Presentation.Criteria;
using Presentation.UIView;
using LkDataContext;


namespace Business.Interface.Student
{
    public interface IStudentTruthAuthenticatedService:IOrigianlBaseService
    {
        ActionResult SendAuthenticated(string studentNum, string enterpriseCode);
    }
}
