using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Interface.Family;
using Business.Service.Family;
using Presentation.Criteria.Family;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView.Family;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student.UserControl
{
    public partial class UserFamilySetted : BaseUserControl
    {
        private IFamilyService Service
        {
            get
            {
                return new FamilyService();
            }
        }

        private FamilyPresentation StudentFamilyAccount
        {
            get
            {
                var studentFamilyAccount = this.ViewState["StudentFamilyAccount"] as FamilyPresentation;
                if (studentFamilyAccount == null)
                {
                    //var info = new QueryInfo();
                    //info.Parameters.Add("StudentNum", CurrentAccount.UserName);
                    studentFamilyAccount = Service.GetByStudentNum(CurrentUser.UserName);
                    this.ViewState["StudentFamilyAccount"] = studentFamilyAccount;
                }
                return studentFamilyAccount;
            }
        }

        protected override void BindData()
        {
            base.BindData();
            rdoSexType.BindSource(BindingSourceType.SexInfo, false);
        }

        protected override void InitData()
        {
            base.InitData();

            txtNameZh.Text = StudentFamilyAccount.NameZh;
            txtUserName.Text = StudentFamilyAccount.UserName;
            chkEnabled.Checked = StudentFamilyAccount.IsOnline;
            rdoSexType.SelectedValue = ((int) StudentFamilyAccount.Sex).ToString();
            txtRelationship.Text = StudentFamilyAccount.Relationship;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StudentFamilyAccount.NameZh = txtNameZh.Text;
            StudentFamilyAccount.Sex = (SexType) int.Parse(rdoSexType.SelectedValue);
            if (!String.IsNullOrEmpty(txtPassword.Text))
            {
                StudentFamilyAccount.Password = AccountSecurityManage.MD5Password(txtPassword.Text);
            }
            StudentFamilyAccount.IsOnline = chkEnabled.Checked;
            StudentFamilyAccount.Relationship = txtRelationship.Text;

            var actionResult = Service.Save(StudentFamilyAccount);
            ShowMsg(actionResult.IsSucess, actionResult.Message);
        }
    }
}