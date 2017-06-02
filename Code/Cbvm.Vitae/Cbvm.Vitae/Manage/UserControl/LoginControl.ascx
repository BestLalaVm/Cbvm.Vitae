<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="Cbvm.Vitae.Manage.UserControl.LoginControl" %>
<div class="login-block">
    <div class="caption">
        <h3>
            登入</h3>
        <span><a href="/Template/Default.aspx" target="_self">返回首页</a> </span>
    </div>
    <div class="data-detail">
        <div>
            <asp:TextBox ID="txt_UserName_" runat="server" Width="270px" EmptyMessage="用户名" CssClass="edit-text"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="txt_Password_" runat="server" TextMode="Password" Width="270px" CssClass="edit-text"
                EmptyMessage="密码"></asp:TextBox>
        </div>
        <div class="action">
            <asp:Button ID="btn_Login" runat="server" OnClick="btn_Login_Click" Text="进入系统" Width="80px" />
            <asp:HyperLink ID="linkResetPassword" runat="server" NavigateUrl="~/Manage/Student/ResetPassword.aspx"
                Text="忘记密码" Target="_self"></asp:HyperLink>
        </div>
    </div>
</div>
