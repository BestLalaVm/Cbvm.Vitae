<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewestTopActivityWidget.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.NewestTopActivityWidget" %>
<div class="widget widget-list activity-list top-widget">
    <div class="bar expand">
        <div class="caption">
            <span class="img"></span>
                        参加活动列表 <asp:Literal runat="server" ID="ltlRecordCount"></asp:Literal>
        </div>
        <div class="more">
            <asp:HyperLink ID="linkmore" runat="server" Text="更多"></asp:HyperLink>
        </div>
        <br class="clear" />
    </div>
    <div class="container">
        <asp:Repeater ID="rptActivity" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li class="list-item">
                    <div>
                        <a href='<%#Eval("Url") %>'>
                            <%#Eval("Title")%></a>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <br class="clear" />
                </ul>
            </FooterTemplate>
        </asp:Repeater>
        <div class="empty-container">
            <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
        </div>
    </div>
</div>
