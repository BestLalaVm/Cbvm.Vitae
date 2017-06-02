<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.UserControl.ChangePassword" %>
<div class="password-block">
    <div class="caption">
        <h3>密码修改</h3>
    </div>
    <div class="container">
        <div>
            <div>
                <asp:TextBox ID="txtOrginalPassword" runat="server" TextMode="Password" EmptyMessage="原始密码" CssClass="edit-text"
                    Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqvOriginalPassword" runat="server" ControlToValidate="txtOrginalPassword"
                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>原始密码不能为空!</label></span>"></asp:RequiredFieldValidator>
                <br class="clear" />
            </div>
            <div>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" EmptyMessage="新密码" CssClass="edit-text"
                    Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>原始密码不能为空!</label></span>"></asp:RequiredFieldValidator>
                <br class="clear" />
            </div>
            <div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" EmptyMessage="确认密码" CssClass="edit-text"
                    Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqvPassword" runat="server" ControlToValidate="txtPassword"
                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>确认密码不能为空!</label></span>"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="rqcComparePassword" runat="server" ControlToValidate="txtPassword"
                    ControlToCompare="txtNewPassword" Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>新密码与确认密码不相等!</label></span>"></asp:CompareValidator>
                <br class="clear" />
            </div>
            <div>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="确认修改"></asp:Button>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function getInputBoxHeight() {
        return 32;
    }
</script>
