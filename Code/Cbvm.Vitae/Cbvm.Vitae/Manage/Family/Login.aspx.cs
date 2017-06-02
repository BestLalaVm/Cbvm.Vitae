using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.UIView;


namespace Cbvm.Vitae.Manage.Family
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected LoginUserPresentation LoginControl_GetExtandAccountInfoHandler(LoginUserPresentation account)
        {
            //var baseServer = new Server.Service.BaseService<int, StudentFamilyAccount, Presentation.Criteria.BaseCriteria>();
            //var info = new QueryInfo();
            //info.Parameters.Add("UserName", account.UserName);
            //var familyAccount = baseServer.Load<StudentFamilyAccount>(info);
            //account.AdditionInfo.Add("StudentNum", familyAccount.StudentNum);
            return account;
        }
    }
}