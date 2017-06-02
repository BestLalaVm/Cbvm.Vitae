using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Presentation.Enum;
using Presentation.UIView;

public interface IBasePageControl
{
    void ShowMsg(bool isSucess, string msg);

    void ShowSelfMsg(bool isSucess, string msg);

    void WriteLog(string message);

    void WriteLog(Exception ex);

    UserType UserType { get; }

    AttachmentType AttachmentType { get; }

    LoginUserPresentation CurrentUser { get; }

    int MemberID { get; }
}
