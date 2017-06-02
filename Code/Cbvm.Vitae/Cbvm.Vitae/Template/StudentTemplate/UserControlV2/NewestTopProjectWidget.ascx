<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewestTopProjectWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControlV2.NewestTopProjectWidget" %>
<div class="qylbListTit">
    <asp:HyperLink ID="linkmore" CssClass="more" runat="server" Text="更多"></asp:HyperLink>
    <div class="qylbTitName"><a style="float: left;" href="#">个人项目列表</a></div>
</div>
<asp:Repeater ID="rptProject" runat="server">
    <ItemTemplate>
        <%# (Container.ItemIndex%3==0)?"<div class='qylbList'>":"" %>
        <div class="qylbListDiv"><a href='<%#Eval("Url") %>'><%#Eval("Name")%></a></div>
        <%# (Container.ItemIndex%3==2)?"</div>":"" %>
    </ItemTemplate>
    <FooterTemplate>
<%--        <%# (Container.ItemIndex%3!=2)?"</div>":"" %>--%>
    </FooterTemplate>
</asp:Repeater>
<%= (IsNeedDivClosed)?"</div>":"" %>
<div class="empty-container qylbList">
    <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
</div>
