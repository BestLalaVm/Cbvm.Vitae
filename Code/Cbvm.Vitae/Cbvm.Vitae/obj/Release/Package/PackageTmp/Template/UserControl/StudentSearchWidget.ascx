<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentSearchWidget.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.StudentSearchWidget" %>
<script type="text/javascript">
    $(document).ready(function () {
        $(document).keypress(function (e) {
            var keyCode = e.keyCode;
            if (keyCode == 13) {
                __doPostBack('ctl00$StudentSearchWidget$link', '')
            }
        })
    })
</script>
<div class="search-widget">
    <div class="content">
<%--        <telerik:RadTextBox ID="txtName" EmptyMessage="学号/姓名" runat="server" Width="160px">
        </telerik:RadTextBox>--%>
        <asp:TextBox ID="txtName" runat="server" EmptyMessage="学号/姓名" runat="server" Width="140px" Height="22px"></asp:TextBox>
        <asp:LinkButton ID="link" runat="server" Text="搜索" OnClick="btnSearch_Click" CssClass="search"></asp:LinkButton>
    </div>
</div>
