<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExercitationList.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.ExercitationList" %>
<%@ Register Src="~/Template/UserControl/CommonSearchWidget.ascx" TagName="CommonSearchWidget"
    TagPrefix="uc" %>

<div class="qylbListTit">
    <div class="qylbTitName"><a style="float: left;" href="#">实习列表</a></div>
</div>
<asp:Repeater ID="rptActivity" runat="server">
    <ItemTemplate>
        <%# (Container.ItemIndex%3==0)?"<div class='qylbList'>":"" %>
        <div class="qylbListDiv"><a href='<%#Eval("Url") %>'><%#Eval("Name")%></a></div>
        <%# (Container.ItemIndex%3==2)?"</div>":"" %>
    </ItemTemplate>
    <FooterTemplate>
       <%# (Container.ItemIndex%3!=2)?"</div>":"" %>
    </FooterTemplate>
</asp:Repeater>
<div class="empty-container qylbList">
    <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
</div>
