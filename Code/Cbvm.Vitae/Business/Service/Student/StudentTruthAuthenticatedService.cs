using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Interface.Student;
using LkDataContext;
using Presentation.UIView;

namespace Business.Service.Student
{
    public class StudentTruthAuthenticatedService : BaseService, IStudentTruthAuthenticatedService
    {
        public ActionResult SendAuthenticated(string studentNum, string enterpriseCode)
        {
            dataContext.StudentTruthAuthenticated.InsertOnSubmit(new StudentTruthAuthenticated()
            {
                RequestDate=DateTime.Now,
                RequestEnterpriseCode=enterpriseCode,
                StudentNum=studentNum,
                IsAuthenticated=false,
            });

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
