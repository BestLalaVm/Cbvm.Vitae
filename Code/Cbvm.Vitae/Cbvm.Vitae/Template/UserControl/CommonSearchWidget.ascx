<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonSearchWidget.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.CommonSearchWidget" %>
<div class="search-block">
    <div class="search">
        <telerik:RadTextBox ID="txtkeyword" runat="server" EmptyMessage="关键字" Width="100px">
        </telerik:RadTextBox>
        <asp:LinkButton ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click"></asp:LinkButton>
    </div>
    <br class="clear" />
</div>
