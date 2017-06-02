<%@ control language="C#" autoeventwireup="true" codebehind="NewestTopProfessionalWidget.ascx.cs"
    inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.NewestTopProfessionalWidget" %>
<div class="widget widget-list top-widget">
    <div class="bar expand">
        <div class="caption">
            <span class="img"></span>
            技能证书列表
            <asp:Literal runat="server" ID="ltlRecordCount"></asp:Literal>
        </div>
        <div class="more">
            <asp:HyperLink ID="linkmore" runat="server" Text="更多"></asp:HyperLink>
        </div>
        <br class="clear" />
    </div>
    <div class="container">
        <asp:Repeater ID="rptProfessional" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="list-item">
                        <a href='<%#Eval("Url") %>'>
                            <%#Eval("Name")%></a>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
        <div class="empty-container">
            <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
        </div>
    </div>
</div>
