<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopHotEnterpriseList.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.TopHotEnterpriseList" %>
<div class="show-widget widget-list widget hot-enterprise">
    <div class="bar">
        <div class="caption">
            <%=Caption %>
        </div>
        <div class="more">
            <a href="<%=Link2MoreEnterprise %>" title="更多企业"><span></span></a>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="container">
        <asp:Repeater ID="rptEnterprise" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="list-item">
                        <a href='<%#Eval("Url") %>' title='<%#Eval("Tooltip") %>'><span>
                            <%#Eval("Name")%></span> </a>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <br class="clear" />
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
