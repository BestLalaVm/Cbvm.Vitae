<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewestTopDailyEssayWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControlV2.NewestTopDailyEssayWidget" %>
<div class="grxmListContent">
    <div class="qylbListTit">
        <asp:HyperLink ID="linkmore" CssClass="more" runat="server" Text="更多"></asp:HyperLink>
        <div style="border-bottom: 2px solid rgb(233,109,131);" class="qylbTitName"><a style="float: left;" href="#">个人博文列表</a></div>
    </div>
    <div style="float: left; width: 92%; margin-left: 4%; _margin-left: 2%;">
        <asp:Repeater ID="rptDailyEssay" runat="server">
            <HeaderTemplate>
     
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="list-item">
                        <a href='<%#Eval("Url") %>'>
                            <%#Eval("Title")%></a>
                    </div>
            </ItemTemplate>
            <FooterTemplate>

            </FooterTemplate>
        </asp:Repeater>
        <div class="empty-container">
            <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
        </div>
    </div>
</div>
