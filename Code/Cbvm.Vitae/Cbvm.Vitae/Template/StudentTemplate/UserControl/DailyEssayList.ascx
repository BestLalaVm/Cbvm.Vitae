<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyEssayList.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.DailyEssayList" %>
<%@ Register Src="~/Template/UserControl/CommonSearchWidget.ascx" TagName="CommonSearchWidget"
    TagPrefix="uc" %>


<div class="qylbListTit" style='float:none;display:block;width:auto;height:auto;'>
    <div class="qylbTitName" style="float:none;display:inline-block;"><a style="float: left;" href="#">博文列表</a></div>
</div>
<asp:Repeater ID="rptDailyEssay" runat="server">
    <ItemTemplate>
        <%# (Container.ItemIndex%3==0)?"<div class='qylbList' style='float:none;display:block;width:auto;height:auto;'>":"" %>
        <div class="qylbListDiv" style="float:none;display:block;width:auto;height:auto;"><a href='<%#Eval("Url") %>'><%#Eval("Title")%></a></div>
        <%# (Container.ItemIndex%3==2)?"</div>":"" %>
    </ItemTemplate>
    <FooterTemplate>
        <%# (Container.ItemIndex%3!=2)?"</div>":"" %>
    </FooterTemplate>
</asp:Repeater>
<div class="empty-container qylbList">
    <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
</div>
