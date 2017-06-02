using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Web;
using Business.Interface.Enterprise;
using Business.Service.Enterprise;
using Presentation.Criteria.Enterprise;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using WebLibrary;

public class BaseEnterprisePage : BasePage
{
    public override UserType InitUserType()
    {
        return UserType.Enterprise;
    }

    public override AttachmentType InitAttachmentType()
    {
        return AttachmentType.BaseInfo;
    }

    private IEnterpriseService Service
    {
        get { return new EnterpriseService(); }
    }

    public string EnterpriseCode
    {
        get
        {
            return HaddockContext.Current.CurrentUser.Identity;
        }
    }

    public bool IsPermission
    {
        get
        {
            var isPermissionValue = "";
            var isPermission = false;
            if (CurrentUser.AddtionalUser.ContainsKey("isPermission"))
            {
                isPermissionValue = CurrentUser.AddtionalUser["isPermission"];
            }
            bool.TryParse(isPermissionValue, out isPermission);
            return isPermission;
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        Theme = "EnterpriseManage";
        base.OnPreInit(e);
    }
}