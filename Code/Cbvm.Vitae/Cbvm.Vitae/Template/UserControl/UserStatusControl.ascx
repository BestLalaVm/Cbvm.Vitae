<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserStatusControl.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.UserStatusControl" %>
<div class="xmca-topbar">
    <div class="xmca-container">
        <asp:PlaceHolder ID="phUserContainer" runat="server">
            <div class="xmca-user">
                <div class="user-block">
                    <span>当前用户:</span> <span>
                        <asp:Literal ID="ltlUserName" runat="server"></asp:Literal></span> <span>角色:</span>
                    <span>
                        <asp:Literal ID="ltlRoleName" runat="server"></asp:Literal></span> <span>登入时间:</span>
                    <span>
                        <asp:Literal ID="ltlLoginTime" runat="server"></asp:Literal></span>
                </div>
            </div>
            <div class="xmca-menu">
                <asp:PlaceHolder ID="phHomePage" runat="server" Visible="false">
                    <div class="user-block">
                        <asp:HyperLink ID="link2Home" runat="server"><span>进入个人主页</span></asp:HyperLink>
                    </div>
                </asp:PlaceHolder>
                <div class="user-block">
                    <asp:HyperLink ID="link2Backend" runat="server"><span>进入后台</span></asp:HyperLink>
                </div>
                <div class="user-block">
                    <asp:LinkButton ID="btnLogOut" runat="server" OnClick="btnLogout_Click">
               <span>注销</span>
                    </asp:LinkButton>
                </div>
            </div>
            <br class="clear" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRegisterContainer" runat="server" Visible="false">
            <div class="xmca-user">
                <div class="user-block">
                    <span>欢迎来履历系统</span>
                </div>
                <div class="user-block">
                    <asp:HyperLink ID="link2Login" runat="server"><span>请登入</span></asp:HyperLink>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
