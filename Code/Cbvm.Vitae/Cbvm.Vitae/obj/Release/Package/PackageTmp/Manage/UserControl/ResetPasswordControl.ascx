<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResetPasswordControl.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.UserControl.ResetPasswordControl" %>
<div class="login-block">
    <div class="caption">
        <h3>
            <asp:Literal ID="ltlCaption" runat="server" Text="忘记密码"></asp:Literal></h3>
        <span><a href="/Template/Default.aspx" target="_self">返回首页</a> </span>
    </div>
    <div class="data-detail reset-password">
        <div>
            <asp:TextBox ID="txtUserName" runat="server" Width="270px" EmptyMessage="用户名" CssClass="edit-text"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="txtCheckCode" runat="server" Width="270px" EmptyMessage="验证码" CssClass="edit-text"></asp:TextBox>
            <a onclick="ReflashCheckCode();" href="#">
                <img id="imgCheckCode" class="checkCode" src='../../CheckCode.aspx?UserType=<%=UserType.ToString() %>'
                    class="checkCode" alt="错误" />
            </a>
        </div>
        <div class="action">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="重置密码" Height="28px" Width="90px" />
        </div>
    </div>
</div>
<script type="text/javascript">
    function ReflashCheckCode() {
        var imgCheckCode = document.getElementById("imgCheckCode");
        imgCheckCode.src = "../../CheckCode.aspx?UserType=" + '<%=UserType.ToString() %>&t=' + (new Date()).getMilliseconds().toString();
    }
</script>
